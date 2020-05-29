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
    public class ProductLogic : IBaseLogic<ProductModel>, IProductLogic
    {
        public void Add(ProductModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = new Product();
                obj.Name = model.Name;
                obj.UnitPrice = model.UnitPrice;
                obj.IsProduce = model.IsProduce;
                uow.Products.Add(obj);
                uow.Complete();
                model.ID = obj.ProductID;
            }
        }

        public void Edit(int id, ProductModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Products.Get(id);
                obj.Name = model.Name;
                obj.UnitPrice = model.UnitPrice;
                obj.IsProduce = model.IsProduce;
                uow.Products.Edit(obj);
                uow.Complete();
                
            }
        }

        public void Delete(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Products.Get(id);
                uow.Products.Remove(obj);
                uow.Complete();

            }
        }

        public List<ProductListModel> GetAll(string criteria)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.Products.GetAll(criteria);
                var models = new List<ProductListModel>();
                foreach (var item in objs)
                {
                    var model = new ProductListModel();
                    model.ID = item.ProductID;
                    model.Name = item.Name;
                    model.RawMaterialsCount = item.ProductRawMaterials.Count;
                    models.Add(model);
                }
                return models;
            }
        }


        public List<ProductRawMaterialListModel> GetAllProductRawMaterials(int productID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.ProductRawMaterials.GetAll(productID);
                var models = new List<ProductRawMaterialListModel>();
                foreach (var item in objs)
                {
                    var model = new ProductRawMaterialListModel();
                    model.ID = item.ProductRawMaterialID;
                    model.Name = item.SupplyType.Description;
                    model.Quantity = item.Quantity;
                    models.Add(model);
                }
                return models;
            }
        }


        public ProductModel Get(int productID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Products.GetBy(productID);
                var model = new ProductModel();
                model.ID = obj.ProductID;
                model.Name = obj.Name;
                model.UnitPrice = obj.UnitPrice;
                model.IsProduce = obj.IsProduce;
                model.ProductRawMaterialCount = obj.ProductRawMaterials.Count;
                return model;
            }
        }


        public void EditProductRawMaterial(int productRawMaterialID, AddEditProductRawMaterialModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.ProductRawMaterials.Get(productRawMaterialID);
                obj.ProductID = model.ProductID;
                obj.SupplyTypeID = model.SupplyTypeID;
                obj.Quantity = model.Quantity;
                uow.ProductRawMaterials.Edit(obj);
                uow.Complete();
            }
        }

        public void AddProductRawMaterial(AddEditProductRawMaterialModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = new ProductRawMaterial();
                obj.ProductID = model.ProductID;
                obj.SupplyTypeID = model.SupplyTypeID;
                obj.Quantity = model.Quantity;
                uow.ProductRawMaterials.Add(obj);
                uow.Complete();
                model.ID = obj.ProductRawMaterialID;
            }
        }


        public ProductRawMaterialModel GetProductRawMaterial(int productRawMaterialID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.ProductRawMaterials.GetV2(productRawMaterialID);
                var model = new ProductRawMaterialModel();
                model.ProductID = obj.ProductID;
                model.Quantity = obj.Quantity;
                model.SupplyTypeID = obj.SupplyTypeID;
                model.SupplyClassID = obj.SupplyType.SupplyClassID;
                return model;
            }
        }


        public void DeleteProductRawMaterial(int productRawMaterialID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.ProductRawMaterials.Get(productRawMaterialID);
                uow.ProductRawMaterials.Remove(obj);
                uow.Complete();
            }
        }
    }
}
