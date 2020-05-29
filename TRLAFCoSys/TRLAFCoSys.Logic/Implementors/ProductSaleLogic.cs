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
    public class ProductSaleLogic : IProductSaleLogic
    {
        public void Add(ProductSaleModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = new ProductSale();
                obj.CreateTimeStamp = model.Date;
                obj.CustomerName = model.CustomerName;
                obj.Discount = model.Discount;
                obj.ProductID = model.ProductID;
                obj.Quantity = model.Quantity;
                obj.isBuyOneTakeOne = model.IsBuyOneTakeOne;
                obj.UnitPrice = model.UnitPrice;
                obj.OutletSaleName = model.OutletSaleName;
                uow.ProductSales.Add(obj);
                uow.Complete();
                model.ID = obj.ProductSaleID;
            }
        }

        public void Edit(int id, ProductSaleModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.ProductSales.Get(id);
                obj.CustomerName = model.CustomerName;
                obj.Discount = model.Discount;
                obj.ProductID = model.ProductID;
                obj.Quantity = model.Quantity;
                obj.UnitPrice = model.UnitPrice;
                obj.OutletSaleName = model.OutletSaleName;
                obj.isBuyOneTakeOne = model.IsBuyOneTakeOne;
                uow.ProductSales.Edit(obj);
                uow.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.ProductSales.Get(id);
                uow.ProductSales.Remove(obj);
                uow.Complete();

            }
        }

        public List<ProductSaleListModel> GetAll(string criteria)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.ProductSales.GetAll(criteria);
                var models = new List<ProductSaleListModel>();
                foreach (var item in objs)
                {
                    var model = new ProductSaleListModel();
                    model.ID = item.ProductID;
                    model.CustomerName = item.CustomerName;
                    model.ProductName = item.Product.Name;
                    model.Quantity = item.Quantity;
                    model.Total = ((item.Quantity * item.UnitPrice) - item.Discount);
                    model.OutletSaleName = item.OutletSaleName;
                    models.Add(model);
                   
                }
                return models;
            }
        }


        public ProductSaleModel Get(int productSaleID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.ProductSales.Get(productSaleID);
                var model = new ProductSaleModel();
                model.CustomerName = obj.CustomerName;
                model.Date = obj.CreateTimeStamp;
                model.Discount = obj.Discount;
                model.ProductID = obj.ProductID;
                model.Quantity = obj.Quantity;
                model.UnitPrice = obj.UnitPrice;
                model.IsBuyOneTakeOne = obj.isBuyOneTakeOne;
                model.AdditionalQty = obj.Quantity * 2;
                model.OutletSaleName = obj.OutletSaleName;
                return model;
            }
        }


        public List<ProductSaleListModel> GetAllByMonth(DateTime date, string criteria)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.ProductSales.GetAllByMonth(date,criteria);
                var models = new List<ProductSaleListModel>();
                foreach (var item in objs)
                {
                    var model = new ProductSaleListModel();
                    model.ID = item.ProductSaleID;
                    model.Date = item.CreateTimeStamp;
                    model.CustomerName = item.CustomerName;
                    model.ProductName = item.Product.Name;
                   
                    model.Quantity = item.Quantity;
                    model.Total = ((item.Quantity * item.UnitPrice) - item.Discount);
                    models.Add(model);
                }
                return models;
            }
        }


        public double ComputeTotal(double quantity, double unitPrice, double discount)
        {
            var subTotalPrice = quantity * unitPrice;
            var subDiscountPrice = quantity * discount;
            return subTotalPrice - subDiscountPrice;
        }
    }
}
