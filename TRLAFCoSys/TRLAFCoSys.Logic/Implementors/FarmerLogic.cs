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
    public class FarmerLogic : IFarmerLogic
    {
        public FarmerLogic() { }


        public IEnumerable<FarmerListModel> GetAllRecord()
        {
            try
            {
                var models = new List<FarmerListModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.Farmers.GetAll();
                   
                    foreach (var item in objs)
                    {
                         var model = new FarmerListModel();
                         model.ID = item.FarmerID;
                         model.FullName = item.FullName;
                         model.Address = item.Address;
                         model.PhoneNumber = item.PhoneNumber;
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


        public IEnumerable<FarmerListModel> GetRecords(string criteria)
        {
            try
            {
                var models = new List<FarmerListModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.Farmers.GetAllRecords(criteria);
                   
                    foreach (var item in objs)
                    {
                         var model = new FarmerListModel();
                         model.ID = item.FarmerID;
                         model.FullName = item.FullName;
                         model.Address = item.Address;
                         model.PhoneNumber = item.PhoneNumber;
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


        public void Add(AddFarmerModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new Farmer();
                    obj.Address = model.Address;
                    obj.FullName = model.FullName;
                    obj.PhoneNumber = model.PhoneNumber;
                    uow.Farmers.Add(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Edit(int id, EditFarmerModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.Farmers.Get(id);
                    obj.Address = model.Address;
                    obj.FullName = model.FullName;
                    obj.PhoneNumber = model.PhoneNumber;
                    uow.Farmers.Edit(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public FarmerModel GetRecord(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var model = new FarmerModel();
                    var obj = uow.Farmers.Get(id);
                    model.Address = obj.Address;
                    model.FullName = obj.FullName;
                    model.PhoneNumber = obj.PhoneNumber;
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
                   
                    var obj = uow.Farmers.Get(id);
                    uow.Farmers.Remove(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
