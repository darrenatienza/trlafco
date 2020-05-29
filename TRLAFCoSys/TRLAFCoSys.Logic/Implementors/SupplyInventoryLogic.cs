﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Persistence;

namespace TRLAFCoSys.Logic.Implementors
{
    public class SupplyInventoryLogic : ISupplyInventoryLogic
    {
        public SupplyInventoryLogic() { }

        public void Add(SupplyInventoryModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {

                var supplyType = uow.SupplyTypes.GetIncludeSupplyClass(model.SupplyTypeID);
                if (supplyType.SupplyClass.Description == "Raw Milk")
                {
                    throw new ApplicationException("Values for this group (Raw Milk) will automatically generated by milk collection form and production form!");
                }
                // use to avoid multiple record of supply type per month
                var supplyInv = uow.SupplyInventories.GetByMonth(model.Date,supplyTypeID: model.SupplyTypeID);
                if (supplyInv != null)
                {
                    throw new ApplicationException("Record for selected Supply was already created!");
                }
                var obj = new SupplyInventory();
                obj.CreateDateTime = DateTime.Now;
                obj.ActualDate = model.Date;
                obj.PurchaseQuantity = model.PurchaseQuantity;
                obj.SupplyTypeID = model.SupplyTypeID;
                obj.WithdrawQuantity = 0;
                uow.SupplyInventories.Add(obj);
                uow.Complete();
                model.ID = obj.SupplyInventoryID;
            }
        }

        public void Edit(int id, SupplyInventoryModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.SupplyInventories.Get(id);
                obj.ActualDate = model.Date;
                obj.PurchaseQuantity = model.PurchaseQuantity;
                obj.SupplyTypeID = model.SupplyTypeID;
                obj.WithdrawQuantity = 0;
                uow.SupplyInventories.Edit(obj);
                uow.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.SupplyInventories.Get(id);
                uow.SupplyInventories.Remove(obj);
                uow.Complete();
            }
        }

        private IEnumerable<SupplyInventoryListModel> GetComputedList()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {


                    var models = new List<SupplyInventoryListModel>();
                    var objsMilk = new List<MilkCollection>();
                    var objsInventory = uow.SupplyInventories.GetAllRecords().ToList();

                    //Get recorded months from inventory records to identify where the computation start
                    var months = objsInventory.GroupBy(r => r.ActualDate.Month).Select(r => new { Date = r.FirstOrDefault().ActualDate }).OrderBy(r => r.Date);
                    var supplyTypes = uow.SupplyTypes.GetAllRecords().ToList();

                    
                    foreach (var type in supplyTypes)
                    {
                        // store quantity total after loop
                        var beginningQuantity = 0.0;
                        var endingQuantity = 0.0;

                        foreach (var month in months)
                        {
                            
                            // beginning will same as ending;
                            beginningQuantity = endingQuantity;

                            var model = new SupplyInventoryListModel();
                            var supplyInventory = objsInventory.FirstOrDefault(r => r.ActualDate.Month == month.Date.Month
                                && r.ActualDate.Year == month.Date.Year
                                && r.SupplyTypeID == type.SupplyTypeID);
                            if (supplyInventory != null)
                            {
                                // goes here if found previous inventory
                                model.ID = supplyInventory.SupplyInventoryID;
                                model.Description = type.Description;
                                model.Date = supplyInventory.ActualDate;
                                model.UnitPrice = supplyInventory.SupplyType.UnitPrice;

                                model.BeginningQuantity = beginningQuantity;
                                model.BeginningTotal = beginningQuantity * model.UnitPrice;

                                model.PurchaseQuantity = supplyInventory.PurchaseQuantity;
                                model.PurchaseTotal = supplyInventory.PurchaseQuantity * model.UnitPrice;
                                
                                
                                model.WithdrawQuantity = supplyInventory.WithdrawQuantity;
                                model.WithdrawTotal = supplyInventory.WithdrawQuantity * model.UnitPrice;

                                model.EndingQuantity = model.BeginningQuantity + model.PurchaseQuantity - model.WithdrawQuantity;
                                model.EndingTotal = model.EndingQuantity * model.UnitPrice;

                                endingQuantity = model.EndingQuantity;

                                models.Add(model);
                            }
                            else
                            {
                                // goes here if previous inventory not found, starting of record loop
                                model.Description = type.Description;
                                model.Date = month.Date;
                                model.UnitPrice = type.UnitPrice;
                                // just compute for beginning and ending quantity to avoid confusion even record cant found.
                                model.BeginningQuantity = beginningQuantity;
                                model.BeginningTotal = beginningQuantity * model.UnitPrice;

                                model.EndingQuantity = model.BeginningQuantity + model.PurchaseQuantity - model.WithdrawQuantity;
                                model.EndingTotal = model.EndingQuantity * model.UnitPrice;

                                models.Add(model);
                            }
                            
                           
                        }
                    }

                    return models;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Total Withdraw of supply type on particular month and year
        /// </summary>
        /// <param name="supplyTypeID">ID of Supply Type</param>
        /// <param name="month">Current Month</param>
        /// <param name="year">Current Year</param>
        /// <returns></returns>
        private double GetWithdraw(int supplyTypeID, int month, int year)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                double totalWithdraw = 0.0;
                // get products with selected supply type in raw materials
                var products = uow.Products.GetAllBy(supplyTypeID);

                foreach (var p in products)
                {
                    var partialWithdraw = 0.0;
                    // if is produce, get quantity on production to get withdrawn values
                    if (p.IsProduce)
                    {
                        // get all production with particular supply type , month and year
                        var productions = uow.Productions.GetProducts(supplyTypeID, month, year);
                        foreach (var production in productions)
                        {
                            // get quantity of selected product raw material and multiply to the production quantity
                            // to get total withdrawn of supply type
                            var productionQuantity =  production.Product.ProductRawMaterials.FirstOrDefault(x => x.SupplyTypeID == supplyTypeID).Quantity;
                            partialWithdraw += production.Quantity * productionQuantity;
                        }
                    }
                    else
                    {
                        // product withdraw in product sales
                        // get all product sales with particular supply type , month and year
                        var productSales = uow.ProductSales.GetAll(supplyTypeID, month, year);
                        foreach (var productSale in productSales)
                        {
                            // get quantity of selected product raw material and multiply to the product sale quantity
                            // to get total withdrawn of supply type
                            partialWithdraw += productSale.Quantity * productSale.Product.ProductRawMaterials.FirstOrDefault(x => x.SupplyTypeID == supplyTypeID).Quantity;
                        }
                    }
                    totalWithdraw += partialWithdraw;
                }
                return totalWithdraw;
                
            }
            
        }

        /// <summary>
        /// Get Total Withdraw of supply type on particular month and year
        /// </summary>
        /// <param name="supplyTypeID">ID of Supply Type</param>
        /// <param name="month">Current Month</param>
        /// <param name="year">Current Year</param>
        /// <returns></returns>
        private double GetWithdrawV2(int supplyTypeID, int month, int year)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                double totalWithdraw = 0.0;
                // get product with selected supply type in raw materials
                //var product = uow.Products.GetBySupplierID(supplyTypeID);
                List<Product> products = uow.Products.GetAllBySupplierID(supplyTypeID).ToList();
                foreach (var product in products)
                {
                    if (product != null)
                    {


                        // if is produce, get quantity on production to get withdrawn values
                        if (product.IsProduce)
                        {
                            // get all production with particular supply type , month and year
                            //var productions = uow.Productions.GetProducts(supplyTypeID, month, year);

                            // get all production with particular supply type , month and year
                            var productions = uow.Productions.GetProductsV2(product.ProductID, month, year);
                            
                            foreach (var production in productions)
                            {
                                // get quantity of selected product raw material and multiply to the production quantity
                                // to get total withdrawn of supply type
                                var productionQuantity = production.Product.ProductRawMaterials.FirstOrDefault(x => x.SupplyTypeID == supplyTypeID).Quantity;
                                totalWithdraw += production.Quantity * productionQuantity;
                            }
                        }
                        else
                        {
                            // product withdraw in product sales
                            // get all product sales with particular supply type , month and year
                            var productSales = uow.ProductSales.GetAll(supplyTypeID, month, year);
                            foreach (var productSale in productSales)
                            {
                                // get quantity of selected product raw material and multiply to the product sale quantity
                                // to get total withdrawn of supply type
                                totalWithdraw += productSale.Quantity * productSale.Product.ProductRawMaterials.FirstOrDefault(x => x.SupplyTypeID == supplyTypeID).Quantity;
                            }
                        }
                    }
                }
               
                return totalWithdraw;

            }

        }

        public IEnumerable<SupplyInventoryListModel> GetComputedByMonth(DateTime dateTime, string criteria)
        {

            /**
             * Loop for every supply types
             * Inside current supply types, loop every months
             * inside active month, get supply inventory by using current supply type and current month
             * if value found, get endingquantity value then set as beginning quantity
             * compute for new ending quantity by beginning quantity + purchase quantity - widthdraw quantity
             * if  supply type for current month not found in inventory, just set previous ending quantiy as beginning quantity
             * no computation for purchase and withdraw
             * 
             */

            using (var uow = new UnitOfWork(new DataContext()))
            {


                var models = new List<SupplyInventoryListModel>();
                var objsMilk = new List<MilkCollection>();
                var objsInventory = uow.SupplyInventories.GetAllRecords().ToList();
                var objMilkCollections = uow.MilkCollections.GetAllRecords().ToList();
                //Get recorded months from inventory records to identify where the computation start
                var supplyMonths = uow.MilkCollections.GetAll().GroupBy(r => r.ActualDate.Month).Select(r => new { Date = r.FirstOrDefault().ActualDate }).OrderBy(r => r.Date).ToList();
                var supplyTypes = uow.SupplyTypes.GetAllRecords().ToList();


             
                foreach (var type in supplyTypes)
                {
                    // store quantity total after loop
                    var beginningQuantity = 0.0;
                    var endingQuantity = 0.0;

                    foreach (var sMonth in supplyMonths)
                    {
                        // beginning will same as ending;
                        beginningQuantity = endingQuantity;

                        var model = new SupplyInventoryListModel();
                        var supplyInventory = objsInventory.FirstOrDefault(r => r.ActualDate.Month == sMonth.Date.Month
                            && r.ActualDate.Year == sMonth.Date.Year
                            && r.SupplyTypeID == type.SupplyTypeID);
                        if (supplyInventory != null)
                        {
                            // goes here if a supplyInventory found on current supplytype and current month
                            model.ID = supplyInventory.SupplyInventoryID;
                            model.Description = type.SupplyClass.Description + " - " + type.Description;
                            model.SupplyClassDescription = type.SupplyClass.Description;
                            model.Date = supplyInventory.ActualDate;
                            model.UnitPrice = supplyInventory.SupplyType.UnitPrice;

                            model.BeginningQuantity = beginningQuantity;
                            model.BeginningTotal = beginningQuantity * model.UnitPrice;

                            // get purchase from milk collections for raw milk
                            if (type.SupplyClass.Description == "Raw Milk")
                            {
                                // get total milk purchase from farmers in milk collections
                                var totalRawMilk = uow.MilkCollections.GetAllByMonthV2(sMonth.Date, supplyTypeID: type.SupplyTypeID).Sum(x => x.Volume);
                                model.PurchaseQuantity = totalRawMilk;
                                model.PurchaseTotal = model.PurchaseQuantity * model.UnitPrice;
                            }
                            else
                            {
                                // get purchase from supply inventory for non milk supplies
                                model.PurchaseQuantity = supplyInventory.PurchaseQuantity;
                                model.PurchaseTotal = model.PurchaseQuantity * model.UnitPrice;
                            }
                           
                            
                            //Get Withdraw from production/sales using current supply type, current month and year
                            model.WithdrawQuantity = GetWithdrawV2(type.SupplyTypeID, sMonth.Date.Month,sMonth.Date.Year);
                            model.WithdrawTotal = model.WithdrawQuantity * model.UnitPrice;

                            model.EndingQuantity = model.BeginningQuantity + model.PurchaseQuantity - model.WithdrawQuantity;
                            model.EndingTotal = model.EndingQuantity * model.UnitPrice;

                            // set ending quantity to get the beginning of quantity for the next month. this is important!
                            endingQuantity = model.EndingQuantity;

                            models.Add(model);
                        }
                        else
                        {
                            //initial values
                            // goes here if a supplyInventory not found on current supply type and current month
                            model.Description = type.SupplyClass.Description + " - " + type.Description;
                            model.SupplyClassDescription = type.SupplyClass.Description;
                            model.SupplyTypeID = type.SupplyTypeID;
                            model.SupplyClassID = type.SupplyClassID;
                            model.Date = sMonth.Date;
                            model.UnitPrice = type.UnitPrice;
                            // just compute for beginning and ending quantity to avoid confusion even record cant found.
                            model.BeginningQuantity = beginningQuantity;
                            model.BeginningTotal = beginningQuantity * model.UnitPrice;

                            // get purchase from milk collections for raw milk
                            if (type.SupplyClass.Description == "Raw Milk")
                            {
                                // get total milk purchase from milk collections
                                var totalRawMilk = uow.MilkCollections.GetAllByMonthV2(sMonth.Date, supplyTypeID: type.SupplyTypeID).Sum(x => x.Volume);
                                model.PurchaseQuantity = totalRawMilk;
                                model.PurchaseTotal = model.PurchaseQuantity * model.UnitPrice;
                            }
                            else
                            {
                                // get purchase from supply inventory for non milk supplies
                                model.PurchaseQuantity = 0;
                                model.PurchaseTotal = model.PurchaseQuantity * model.UnitPrice;
                            }

                            //Get Withdraw from production/sales using current supply type, current month and year
                            model.WithdrawQuantity = GetWithdrawV2(type.SupplyTypeID, sMonth.Date.Month, sMonth.Date.Year);
                            model.WithdrawTotal = model.WithdrawQuantity * model.UnitPrice;

                            model.EndingQuantity = model.BeginningQuantity + model.PurchaseQuantity - model.WithdrawQuantity;
                            model.EndingTotal = model.EndingQuantity * model.UnitPrice;

                            // set ending quantity to get the beginning of quantity for the next month. this is important!
                            endingQuantity = model.EndingQuantity;

                            models.Add(model);
                        }
                    }

                    
                }

                return models.Where(r => r.Date.Month == dateTime.Month && r.Date.Year == dateTime.Year && r.Description.Contains(criteria));
            }
        }


        public IEnumerable<SupplyInventorySummaryModel> GetAllSummary(DateTime date)
        {
            var records = this.GetComputedByMonth(date, "");
            var models = new List<SupplyInventorySummaryModel>();
            

            using (var uow = new UnitOfWork(new DataContext()))
            {
                var supplyClassList = uow.SupplyClasses.GetAll();

                foreach (var item in supplyClassList)
                {

                    var objSubTotal = records.Where(r => r.SupplyClassDescription == item.Description).GroupBy(r => r.SupplyClassDescription)
                        .Select(r => 
                            new { 
                                BeginningSubTotal = r.Sum(x => x.BeginningTotal),
                                PurchaseSubTotal = r.Sum(x => x.PurchaseTotal),
                                WithdrawSubTotal = r.Sum(x => x.WithdrawTotal),
                                EndingSubTotal = r.Sum(x => x.EndingTotal),
                                Date = r.FirstOrDefault().Date
                        }).FirstOrDefault();
                    if (objSubTotal != null)
                    {
                        var model = new SupplyInventorySummaryModel();
                        model.Description = item.Description;
                        model.BeginningSubTotalAmount = objSubTotal.BeginningSubTotal;
                        model.PurchaseSubTotalAmount = objSubTotal.PurchaseSubTotal;
                        model.WithdrawSubTotalAmount = objSubTotal.WithdrawSubTotal;
                        model.EndingSubTotalAmount = objSubTotal.EndingSubTotal;
                        models.Add(model);
                    }
                   
                }
                return models;
            }
        }

        /// <summary>
        /// Get Single record of Supply Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplyInventoryModel GetBy(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var model = new SupplyInventoryModel();
                var obj = uow.SupplyInventories.GetBy(id);

                if (obj.SupplyType.SupplyClass.Description == "Raw Milk")
                {
                    throw new ApplicationException("Values for this group (Raw Milk) will automatically generated by milk collection form and production form!");
                    //var totalRawMilk = uow.MilkCollections.GetAllByMonthV2(obj.ActualDate, supplyTypeID: obj.SupplyTypeID).Sum(x => x.Volume);
                    //model.PurchaseQuantity = totalRawMilk;
                }
                else
                {
                    model.PurchaseQuantity = obj.PurchaseQuantity;
                }


              
                model.Date = obj.ActualDate;
                model.UnitPrice = obj.SupplyType.UnitPrice;
                model.supplyTypeClassName = obj.SupplyType.SupplyClass.Description;
                model.SupplyTypeName = obj.SupplyType.Description;
                model.SupplyClassID = obj.SupplyType.SupplyClassID;
                model.SupplyTypeID = obj.SupplyTypeID;
                model.WithdrawQuantity = GetWithdrawV2(obj.SupplyTypeID,model.Date.Month,model.Date.Year);
                model.WithdrawTotaAmount = model.UnitPrice * model.WithdrawQuantity;
                model.PurchaseTotalAmount = model.UnitPrice * model.PurchaseQuantity;
                return model;
            }
        }





        public double Compute(double quantity, double unitprice)
        {
            return quantity * unitprice;
        }


        public SupplyGrandTotalSummaryModel GetGrandTotalSummary(DateTime date)
        {
            var objs = this.GetAllSummary(date);
            var model = new SupplyGrandTotalSummaryModel();

            var obj = objs.GroupBy(r => r.Date)
                .Select(r => new
                {
                    BeginningGrandTotalAmount = r.Sum(x => x.BeginningSubTotalAmount),
                    PurchaseGrandTotalAmount = r.Sum(x => x.PurchaseSubTotalAmount),
                    WithdrawGrandTotalAmount = r.Sum(x => x.WithdrawSubTotalAmount),
                    EndingGrandTotalAmount = r.Sum(x => x.EndingSubTotalAmount)
                }).FirstOrDefault();
            if (obj != null)
            {
                model.BeginningGrandTotalAmount = obj.BeginningGrandTotalAmount;
                model.EndingGrandTotalAmount = obj.EndingGrandTotalAmount;
                model.PurchaseGrandTotalAmount = obj.PurchaseGrandTotalAmount;
                model.WithdrawGrandTotalAmount = obj.WithdrawGrandTotalAmount;
            }
            return model;
        }
    }
}