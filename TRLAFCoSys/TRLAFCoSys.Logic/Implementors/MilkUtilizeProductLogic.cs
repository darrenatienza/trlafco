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
    public class MilkUtilizeProductLogic : IMilkUtilizeProductLogic
    {
        public MilkUtilizeProductLogic() { }



        public IEnumerable<MilkUtilizeProductListModel> GetAll()
        {
            try
            {
                var models = new List<MilkUtilizeProductListModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                   
                    var objs = uow.MilkUtilizeProductRecords.GetAll();
                    foreach (var item in objs)
                    {
                        var model = new MilkUtilizeProductListModel();
                         model.ID = item.MilkUtilizeProductID;
                         model.ProductName = item.MilkUtilizeProduct.Description;
                         model.Quantity = item.Quantity;
                         models.Add(model);
                    }
                    return models;
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<MilkUtilizeProductListModel> GetAllBy(int milkUtilizeRecordID, string criteria)
        {
            try
            {
                var models = new List<MilkUtilizeProductListModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {

                    var objs = uow.MilkUtilizeProductRecords.GetAllBy(milkUtilizeRecordID, criteria);
                    foreach (var item in objs)
                    {
                        var model = new MilkUtilizeProductListModel();
                        model.ID = item.MilkUtilizeProductRecordID;
                        model.ProductName = item.MilkUtilizeProduct.Description;
                        model.Quantity = item.Quantity;
                        models.Add(model);
                    }
                    return models;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public MilkUtilizeProductModel GetBy(int milkUtilizeProductID)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkUtilizeProductRecords.GetBy(milkUtilizeProductID);
                    var model = new MilkUtilizeProductModel();
                    if (obj != null)
                    {
                        model.ProductName = obj.MilkUtilizeProduct.Description;
                        model.ProductID = obj.MilkUtilizeProductID;
                        model.Quantity = obj.Quantity;
                    }

                    return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Add(MilkUtilizeProductAddModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new MilkUtilizeProductRecord();

                    obj.MilkUtilizeProductID = model.ProductID;
                    obj.MilkUtilizeRecordID = model.MilkUtilizeRecordID;
                    obj.Quantity = model.Quantity;
                    uow.MilkUtilizeProductRecords.Add(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Edit(int milkUtilizeProductID, MilkUtilizeProductEditModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkUtilizeProductRecords.Get(milkUtilizeProductID);

                    obj.MilkUtilizeProductID = model.ProductID;
                    obj.Quantity = model.Quantity;
                    uow.MilkUtilizeProductRecords.Edit(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Delete(int milkUtilizeProductID)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkUtilizeProductRecords.GetBy(milkUtilizeProductID);

                    if (obj != null)
                    {
                        uow.MilkUtilizeProductRecords.Remove(obj);
                        uow.Complete();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
