using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class ProductSaleRepo : Repository<ProductSale>, IProductSaleRepo
    {
        public ProductSaleRepo(DataContext context)
            :base(context)
        {

        }
      
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }

        public IEnumerable<ProductSale> GetAll(string criteria)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ProductSale> GetAllByMonth(DateTime date, string criteria)
        {
            return DataContext.ProductSales
                .Include(x => x.Product).
                Where(x => 
                    DbFunctions.TruncateTime(x.CreateTimeStamp).Value.Year == DbFunctions.TruncateTime(date).Value.Year
                    && DbFunctions.TruncateTime(x.CreateTimeStamp).Value.Month == DbFunctions.TruncateTime(date).Value.Month 
                    && x.CustomerName.Contains(criteria));
        }


        public IEnumerable<ProductSale> GetAllBy(DateTime date, string outlet, string excludeProduct)
        {
            return DataContext.ProductSales
                .Include(x => x.Product)
                .Where(x =>DbFunctions.TruncateTime(x.CreateTimeStamp) == DbFunctions.TruncateTime(date)
                && x.OutletSaleName == outlet && !x.Product.Name.Contains(excludeProduct)).ToList();
        }


        public IEnumerable<ProductSale> GetAllBy(DateTime date, string productName)
        {
            return DataContext.ProductSales
                 .Include(x => x.Product)
                 .Where(x => DbFunctions.TruncateTime(x.CreateTimeStamp) == DbFunctions.TruncateTime(date)
                 && x.Product.Name.Contains(productName)).ToList();
        }


        public IEnumerable<ProductSale> GetAll(int supplyTypeID, int month, int year)
        {
            return DataContext.ProductSales.Include(x => x.Product.ProductRawMaterials)
               .Where(x => DbFunctions.TruncateTime(x.CreateTimeStamp).Value.Month == month
               && DbFunctions.TruncateTime(x.CreateTimeStamp).Value.Year == year
               && x.Product.ProductRawMaterials.Any(y => y.SupplyTypeID == supplyTypeID)).ToList();
        }
    }
}
