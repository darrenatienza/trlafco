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
    public class MilkClassLogic : IMilkClassLogic
    {
        public MilkClassLogic() { }


        public IEnumerable<MilkClassModel> GetAllRecord()
        {
            try
            {
                var models = new List<MilkClassModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.MilkClasses.GetAll();
                   
                    foreach (var item in objs)
                    {
                         var model = new MilkClassModel();
                         model.ID = item.MilkClassID;
                         model.Description = item.Description;
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


        public IEnumerable<MilkClassListModel> GetRecords(string criteria)
        {
            try
            {
                var models = new List<MilkClassListModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.MilkClasses.GetRecords(criteria);

                    foreach (var item in objs)
                    {
                        var model = new MilkClassListModel();
                        model.ID = item.MilkClassID;
                        model.Description = item.Description;
                        model.Cost = item.Cost;
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


        public void Add(AddMilkClassModel model)
        {
            try
            {
 
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new MilkClass();
                    obj.Cost = model.Cost;
                    obj.Description = model.Description;
                    uow.MilkClasses.Add(obj);
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
                    var obj = uow.MilkClasses.Get(id);
                    uow.MilkClasses.Remove(obj);
                    uow.Complete();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public MilkClassModel GetRecord(int id)
        {
            try
            {

                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkClasses.Get(id);

                    
                        var model = new MilkClassModel();
                        model.ID = obj.MilkClassID;
                        model.Description = obj.Description;
                        model.Cost = obj.Cost;
                    
                    return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Edit(int id, EditMilkClassModel model)
        {
            try
            {

                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkClasses.Get(id);
                    obj.Cost = model.Cost;
                    obj.Description = model.Description;
                    uow.MilkClasses.Edit(obj);
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
