using System;
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
    public class DailySaleRecordLogic : IDailySaleRecordLogic
    {
        public DailySaleRecordLogic() { }








        public void Add(DailySaleRecordModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                
               

                var obj = new DailySaleRecord();

                obj.CreateDateTime = model.Date;
                obj.Debtor = model.Debtor;
                
                obj.ProcessingSale = model.ProcessingSale;
               
                obj.SaleOnAccount = model.SaleOnAccount;
                uow.DailySaleRecords.Add(obj);
                uow.Complete();
                model.ID = obj.DailySaleRecordID;
            }
        }

        public void Edit(int id, DailySaleRecordModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.DailySaleRecords.Get(id);

                obj.Debtor = model.Debtor;
                
                obj.ProcessingSale = model.ProcessingSale;
                obj.SaleOnAccount = model.SaleOnAccount;
                uow.DailySaleRecords.Edit(obj);
                uow.Complete();

            }
        }

        

       

        public void Delete(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.DailySaleRecords.Get(id);
                uow.DailySaleRecords.Remove(obj);
                uow.Complete();

            }
        }





        public double ComputeTotalSale(double outletSale1,double outletSale2, double processingSale, double saleOnAccount, double rawMilkSale)
        {
            // deduct processing Sale because it is already added in totalCashSale
            return outletSale1  + outletSale2 + processingSale + saleOnAccount + rawMilkSale;
        }


        public List<DailySaleRecordListModel> GetAllByMonth(DateTime date)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var dailySales = uow.DailySaleRecords.GetAllByMonthYear(date);
                var models = new List<DailySaleRecordListModel>();
                foreach (var item in dailySales)
                {
                    // get total sales of outlets
                    var outletTotalSale1 = uow.ProductSales.GetAllBy(item.CreateDateTime, "Outlet Sale 1", excludeProduct:"Raw Milk")
                        .Select(x => new { Quantity = x.Quantity, UnitPrice = x.UnitPrice, Discount = x.Discount })
                        .Sum(x => (x.Quantity * x.UnitPrice) - (x.Discount * x.Quantity));
                    // get total sales of outlets
                    var outletTotalSale2 = uow.ProductSales.GetAllBy(item.CreateDateTime, "Outlet Sale 2", excludeProduct: "Raw Milk")
                    .Select(x => new { Quantity = x.Quantity, UnitPrice = x.UnitPrice, Discount = x.Discount })
                     .Sum(x => (x.Quantity * x.UnitPrice) - (x.Discount * x.Quantity));
                    
                    var rawMilkSales = uow.ProductSales.GetAllBy(item.CreateDateTime,"Raw Milk").Sum(x => x.Quantity * x.UnitPrice);

                    var totalCashSale = outletTotalSale1 + outletTotalSale2 + item.ProcessingSale;

                    var totalSaleDairyProduct = totalCashSale + item.SaleOnAccount;

                    var totalSale = totalSaleDairyProduct + rawMilkSales;
                    var model = new DailySaleRecordListModel();
                    model.ID = item.DailySaleRecordID;
                    model.Date = item.CreateDateTime;
                    model.OutletSale1 = outletTotalSale1;
                    model.OutletSale2 = outletTotalSale2;
                    model.ProcessingSale = item.ProcessingSale;
                    model.SaleOnAccount = item.SaleOnAccount;
                    model.TotalCashSales = totalCashSale;
                    model.TotalSaleForDairyProduct = totalSaleDairyProduct;
                    model.TotalSales = totalSale;
                    model.Debtor = item.Debtor;
                    models.Add(model);
                   

                }
                return models;
                

            }
            
        }


        public DailySaleRecordModel Get(int dailySaleID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objDailySaleRecord = uow.DailySaleRecords.Get(dailySaleID);
                // get total sales of outlets
                var outletTotalSale1 = uow.ProductSales.GetAllBy(objDailySaleRecord.CreateDateTime, "Outlet Sale 1", excludeProduct: "Raw Milk")
                    .Select(x => new { Quantity = x.Quantity, UnitPrice = x.UnitPrice, Discount = x.Discount })
                    .Sum(x => (x.Quantity * x.UnitPrice) - (x.Discount * x.Quantity));
                // get total sales of outlets
                var outletTotalSale2 = uow.ProductSales.GetAllBy(objDailySaleRecord.CreateDateTime, "Outlet Sale 2", excludeProduct: "Raw Milk")
                .Select(x => new { Quantity = x.Quantity, UnitPrice = x.UnitPrice, Discount = x.Discount })
                 .Sum(x => (x.Quantity * x.UnitPrice) - (x.Discount * x.Quantity));

                var rawMilkSales = uow.ProductSales.GetAllBy(objDailySaleRecord.CreateDateTime, "Raw Milk").Sum(x => x.Quantity * x.UnitPrice);

                var totalCashSale = outletTotalSale1 + outletTotalSale2 + objDailySaleRecord.ProcessingSale;

                var totalSaleDairyProduct = totalCashSale + objDailySaleRecord.SaleOnAccount;

                var totalSale = totalSaleDairyProduct + rawMilkSales;
                var model = new DailySaleRecordModel();
                model.Date = objDailySaleRecord.CreateDateTime;
                model.OutletSale1 = outletTotalSale1;
                model.OutletSale2 = outletTotalSale2;
                model.ProcessingSale = objDailySaleRecord.ProcessingSale;
                model.RawMilkSales = rawMilkSales;
                model.SaleOnAccount = objDailySaleRecord.SaleOnAccount;
                model.TotalCashSales = totalCashSale;
                model.TotalSaleForDairyProduct = totalSaleDairyProduct;
                model.TotalSales = totalSale;
                model.Debtor = objDailySaleRecord.Debtor;
                return model;
            }
        }


        public DailySaleRecordModelV2 GetInitial(DateTime date)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                // get total sales of outlets
                var outletTotalSale1 = uow.ProductSales.GetAllBy(date, "Outlet Sale 1", excludeProduct: "Raw Milk")
                    .Select(x => new { Quantity = x.Quantity, UnitPrice = x.UnitPrice, Discount = x.Discount })
                    .Sum(x => (x.Quantity * x.UnitPrice) - (x.Discount * x.Quantity));
                // get total sales of outlets
                var outletTotalSale2 = uow.ProductSales.GetAllBy(date, "Outlet Sale 2", excludeProduct: "Raw Milk")
                .Select(x => new { Quantity = x.Quantity, UnitPrice = x.UnitPrice, Discount = x.Discount })
                 .Sum(x => (x.Quantity * x.UnitPrice) - (x.Discount * x.Quantity));

                var rawMilkSales = uow.ProductSales.GetAllBy(date, "Raw Milk").Sum(x => x.Quantity * x.UnitPrice);

               
                var model = new DailySaleRecordModelV2();
                model.Date = date;
                model.OutletSale1 = outletTotalSale1;
                model.OutletSale2 = outletTotalSale2;
                
                model.RawMilkSales = rawMilkSales;
                return model;
            }
        }


        public double ComputeTotalCashSale(double outletSale1, double outletSale2, double processingSale)
        {
            return outletSale2 + outletSale1 + processingSale;
        }


        public double ComputeTotalSaleDairyProduct(double outletSale1, double outletSale2, double processingSale, double saleOnAccount)
        {
            return outletSale2 + outletSale1 + processingSale + saleOnAccount;
        }
    }
}
