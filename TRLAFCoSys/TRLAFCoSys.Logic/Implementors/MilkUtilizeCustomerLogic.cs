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
    public class MilkUtilizeCustomerLogic : IMilkUtilizeCustomerLogic
    {
        public MilkUtilizeCustomerLogic() { }



        public IEnumerable<MilkUtilizeCustomerListModel> GetAllBy(int recordID, string criteria)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.MilkUtilizeCustomers.GetAllBy(recordID, criteria);
                    var models = new List<MilkUtilizeCustomerListModel>();
                    foreach (var item in objs)
                    {
                        var model = new MilkUtilizeCustomerListModel();
                        model.ID = item.MilkUtilizeCustomerID;
                        model.FullName = item.FullName;
                        model.Volume = item.RawMilkSold.ToString();
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

        public void Add(MilkUtilizeCustomerAddModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new MilkUtilizeCustomer();
                    obj.CreateDateTime = DateTime.Now;
                    obj.FullName = model.FullName;
                    obj.MilkUtilizeRecordID = model.MilkUtilizeRecordID;
                    obj.RawMilkSold = model.Volume;
                    uow.MilkUtilizeCustomers.Add(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Edit(int id, MilkUtilizeCustomerEditModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {

                    var obj = uow.MilkUtilizeCustomers.Get(id);
                    obj.FullName = model.FullName;
                    obj.RawMilkSold = model.Volume;
                    uow.MilkUtilizeCustomers.Edit(obj);
                    uow.Complete();
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
                    var obj = uow.MilkUtilizeCustomers.Get(id);
                    uow.MilkUtilizeCustomers.Remove(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public MilkUtilizeCustomerModel GetBy(int milkUtilizeCustomerID)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkUtilizeCustomers.Get(milkUtilizeCustomerID);


                    var model = new MilkUtilizeCustomerModel();
                        
                        model.FullName = obj.FullName;
                        model.Volume = obj.RawMilkSold;
        
                        return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<MilkUtilizeCustomerComboModel> GetAllBy(DateTime selectedDate)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.MilkUtilizeCustomers.GetAllBy(selectedDate);
                var models = new List<MilkUtilizeCustomerComboModel>();
                foreach (var item in objs)
                {
                    var model = new MilkUtilizeCustomerComboModel();
                    model.ID = item.MilkUtilizeCustomerID;
                    model.FullName = item.FullName;
                    model.Volume = item.RawMilkSold;
                    models.Add(model);
                }
                return models;
            }
        }
    }
}
