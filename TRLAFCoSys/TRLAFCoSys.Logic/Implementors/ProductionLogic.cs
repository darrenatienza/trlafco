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
    public class ProductionLogic : IBaseLogic<AddEditProductionModel>, IProductionLogic
    {
        public void Add(AddEditProductionModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = new Production();
                obj.ProductID = model.ProductID;
                obj.Quantity = model.Quantity;
                obj.Date = model.Date;
                uow.Productions.Add(obj);
                uow.Complete();
                model.ID = obj.ProductionID;
            }
        }

        public void Edit(int id, AddEditProductionModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Productions.Get(id);
                obj.ProductID = model.ProductID;
                obj.Quantity = model.Quantity;
                obj.Date = model.Date;
                uow.Productions.Edit(obj);
                uow.Complete();
                
            }
        }

        public void Delete(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Productions.Get(id);
                uow.Productions.Remove(obj);
                uow.Complete();

            }
        }



        

        public List<ProductionListModel> GetAll(int month, int year, string criteria)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.Productions.GetAll(month,year,criteria);
                var models = new List<ProductionListModel>();
                foreach (var item in objs)
                {
                    var model = new ProductionListModel();
                    model.Date = item.Date;
                    model.ID = item.ProductionID;
                    model.ProductionQuantity = item.Quantity;
                    model.ProductName = item.Product.Name;
                    models.Add(model);
                }
                return models;

            }
        }


        public ProductionModel Get(int productionID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Productions.Get(productionID);
                var model = new ProductionModel();
                model.Date = obj.Date;
                model.ProductID = obj.ProductID;
                model.Quantity = obj.Quantity;
                return model;

            }
        }
    }
}
