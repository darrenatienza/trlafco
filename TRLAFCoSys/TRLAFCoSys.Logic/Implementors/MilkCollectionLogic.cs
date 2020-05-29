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
    public class MilkCollectionLogic : IMilkCollectionLogic
    {


        public MilkCollectionLogic()
        {
            
        }
       


        public IEnumerable<MilkCollectionModel> GetAllRecordsByDate(DateTime date, string criteria)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var milkProducts = new List<MilkCollectionModel>();
                    var objs = uow.MilkCollections.GetAllRecords(date, criteria);
                    foreach (var item in objs)
                    {
                        var model = new MilkCollectionModel();
                        model.ID = item.MilkCollectionID;
                        model.ActualDate = item.ActualDate;
                        model.MilkClass = item.SupplyType.Description;
                        model.MilkCost = item.SupplyType.UnitPrice;
                        model.FullName = item.Farmer.FullName;
                        model.Volume = item.Volume;
                        model.Amount = item.Volume * model.MilkCost;
                        milkProducts.Add(model);
                    }
                    return milkProducts;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public MilkCollectionSummaryModel GetMilkProductSummaryDate(DateTime date)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var subTotalList = new List<MilkCollectionSubTotalModel>();
                    var milkProductSummary = new MilkCollectionSummaryModel();
                    var milkClasses = uow.MilkClasses.GetAll();
                    foreach (var item in milkClasses)
                    {
                        var subTotal = new MilkCollectionSubTotalModel();
                        double subTotalVol = 0.0;
                        var milkProductList = uow.MilkCollections.GetAllBy(date, item.MilkClassID);
                        foreach (var milkProduct in milkProductList)
                        {
                            subTotalVol = subTotalVol + milkProduct.Volume;
                        }

                        subTotal.MilkClass = item.Description;
                        subTotal.MilkVolumeSubTotal = subTotalVol;
                        subTotal.MilkAmountSubTotal = subTotalVol * item.Cost;
                        subTotalList.Add(subTotal);
                    }
                    var totalVolume = 0.0;
                    var totalAmount = 0.0;
                    foreach (var item in subTotalList)
                    {
                        totalVolume = totalVolume + item.MilkVolumeSubTotal;
                        totalAmount = totalAmount + item.MilkAmountSubTotal;
                    }
                    milkProductSummary.MilkProductSubTotalModels = subTotalList;
                    milkProductSummary.MilkAmountTotal = totalAmount;
                    milkProductSummary.MilkVolumeTotal = totalVolume;
                    return milkProductSummary;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void Add(AddMilkCollectionModel model)
        {
            try
            {
                using (var uow  = new UnitOfWork(new DataContext()))
                {
                    var obj = new MilkCollection();
                    obj.ActualDate = model.ActualDate;
                    obj.CreateDateTime = DateTime.Now;
                    obj.FarmerID = model.FarmerID;
                    obj.SupplyTypeID = model.MilkClassID;
                    obj.Volume = model.Volume;
                    uow.MilkCollections.Add(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public void Edit(int id, EditMilkCollectionModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkCollections.Get(id);
                    obj.ActualDate = model.ActualDate;
                    obj.FarmerID = model.FarmerID;
                    obj.SupplyTypeID = model.MilkClassID;
                    obj.Volume = model.Volume;
                    uow.MilkCollections.Edit(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public MilkCollectionModel Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var model = new MilkCollectionModel();
                    var obj = uow.MilkCollections.GetMilkCollection(id);
                    model.ActualDate = obj.ActualDate;
                    model.FullName = obj.Farmer.FullName;
                    model.MilkClass = obj.SupplyType.Description;
                    model.Volume = obj.Volume;
                    return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Delete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    
                    var obj = uow.MilkCollections.Get(id);
                    uow.MilkCollections.Remove(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<MilkCollectionModel> GetAllRecordsByMonth(DateTime date, string criteria)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var milkProducts = new List<MilkCollectionModel>();
                    var objs = uow.MilkCollections.GetAllRecordsByMonth(date, criteria);
                    foreach (var item in objs)
                    {
                        var model = new MilkCollectionModel();
                        model.ID = item.MilkCollectionID;
                        model.ActualDate = item.ActualDate;
                        model.MilkClass = item.SupplyType.Description;
                        model.MilkCost = item.SupplyType.UnitPrice;
                        model.FullName = item.Farmer.FullName;
                        model.Volume = item.Volume;
                        model.Amount = item.Volume * model.MilkCost;
                        milkProducts.Add(model);
                    }
                    return milkProducts;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public MilkCollectionSummaryModel GetMilkProductSummaryMonth(DateTime date)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var subTotalList = new List<MilkCollectionSubTotalModel>();
                    var milkProductSummary = new MilkCollectionSummaryModel();
                    var milkClasses = uow.MilkClasses.GetAll();
                    foreach (var item in milkClasses)
                    {
                        var subTotal = new MilkCollectionSubTotalModel();
                        double subTotalVol = 0.0;
                        var milkProductList = uow.MilkCollections.GetAllByMonth(date, item.MilkClassID);
                        foreach (var milkProduct in milkProductList)
                        {
                            subTotalVol = subTotalVol + milkProduct.Volume;
                        }

                        subTotal.MilkClass = item.Description;
                        subTotal.MilkVolumeSubTotal = subTotalVol;
                        subTotal.MilkAmountSubTotal = subTotalVol * item.Cost;
                        subTotalList.Add(subTotal);
                    }
                    var totalVolume = 0.0;
                    var totalAmount = 0.0;
                    foreach (var item in subTotalList)
                    {
                        totalVolume = totalVolume + item.MilkVolumeSubTotal;
                        totalAmount = totalAmount + item.MilkAmountSubTotal;
                    }
                    milkProductSummary.MilkProductSubTotalModels = subTotalList;
                    milkProductSummary.MilkAmountTotal = totalAmount;
                    milkProductSummary.MilkVolumeTotal = totalVolume;
                    return milkProductSummary;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public double GetSubTotalVolume(string milkDescription, DateTime date)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.MilkCollections.GetAllBy(date, milkDescription);
                var obj = objs.GroupBy(r => r.MilkClassID).Select(r => new { Subtotal = r.Sum(x => x.Volume) }).FirstOrDefault();
                var subTotal = 0.0;
                if (obj != null)
                {
                    subTotal = obj.Subtotal;
                }
                return subTotal;
               
            }
        }
    }
}
