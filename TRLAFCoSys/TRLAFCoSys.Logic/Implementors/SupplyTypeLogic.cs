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
    public class SupplyTypeLogic : ISupplyTypeLogic
    {
        public SupplyTypeLogic() { }






        public void Add(SupplyTypeModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = new SupplyType();
                obj.Description = model.Description;
                obj.SupplyClassID = model.SupplyClassID;
                obj.UnitPrice = model.UnitPrice;
                uow.SupplyTypes.Add(obj);
                uow.Complete();
            }
        }

        public void Edit(int id, SupplyTypeModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.SupplyTypes.Get(id);
                obj.Description = model.Description;
                obj.SupplyClassID = model.SupplyClassID;
                obj.UnitPrice = model.UnitPrice;
                uow.SupplyTypes.Edit(obj);
                uow.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.SupplyTypes.Get(id);
                uow.SupplyTypes.Remove(obj);
                uow.Complete();
            }
        }


        public IEnumerable<SupplyTypeListModel> GetAll()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var models = new List<SupplyTypeListModel>();
                var objs = uow.SupplyTypes.GetAll();
                foreach (var item in objs)
                {
                    var model = new SupplyTypeListModel();
                    model.ID = item.SupplyTypeID;
                    model.Description = item.Description;
                    models.Add(model);
                }
                return models;
            }
        }


        public IEnumerable<SupplyTypeListModel> GetAllBy(int supplyClassID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var models = new List<SupplyTypeListModel>();
                var objs = uow.SupplyTypes.GetAll(supplyClassID);
                foreach (var item in objs)
                {
                    var model = new SupplyTypeListModel();
                    model.ID = item.SupplyTypeID;
                    model.Description = item.Description;
                    models.Add(model);
                }
                return models;
            }
        }


        public SupplyTypeModel GetBy(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
               
                var obj = uow.SupplyTypes.Get(id);
                var model = new SupplyTypeModel();
                if (obj != null)
                {
                    model.Description = obj.Description;
                    model.UnitPrice = obj.UnitPrice;
                    model.ID = obj.SupplyTypeID;
                    model.SupplyClassID = obj.SupplyClassID;
                }
                return model;
                
            }
        }


        public IEnumerable<SupplyTypeGridListModel> GetAllBy(string criteria)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var models = new List<SupplyTypeGridListModel>();
                var objs = uow.SupplyTypes.GetAll(criteria);
                foreach (var item in objs)
                {
                    var model = new SupplyTypeGridListModel();
                    model.ID = item.SupplyTypeID;
                    model.Description = item.Description;
                    model.UnitPrice = item.UnitPrice;
                    model.SupplyClassDescription = item.SupplyClass.Description;
                    models.Add(model);
                }
                return models;
            }
        }
    }
}
