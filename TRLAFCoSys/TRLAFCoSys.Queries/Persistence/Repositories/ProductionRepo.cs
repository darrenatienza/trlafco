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
    public class ProductionRepo : Repository<Production>, IProductionRepo
    {
        public ProductionRepo(DataContext context)
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





        public IEnumerable<Production> GetAll(int month, int year, string criteria)
        {
            return DataContext.Productions.Include(x => x.Product)
                .Where(x => DbFunctions.TruncateTime(x.Date).Value.Month == month
                && DbFunctions.TruncateTime(x.Date).Value.Year == year
                && x.Product.Name.Contains(criteria));
        }
        public IEnumerable<Production> GetAll2(int month, int year, string criteria)
        {
            return DataContext.Productions.Include(x => x.Product)
                .Where(x => DbFunctions.TruncateTime(x.Date).Value.Month == month
                && DbFunctions.TruncateTime(x.Date).Value.Year == year
                && x.Product.ProductRawMaterials.Any(y => y.SupplyTypeID == 1));
        }


        public IEnumerable<Production> GetProducts(DateTime dateTime, int supplyTypeID)
        {
            return DataContext.Productions.Include(x => x.Product.ProductRawMaterials)
                .Where(x => DbFunctions.TruncateTime(x.Date).Value.Month == dateTime.Month
                && DbFunctions.TruncateTime(x.Date).Value.Year == dateTime.Year
                && x.Product.ProductRawMaterials.Any(y => y.SupplyTypeID == supplyTypeID)).ToList();
        }


        public IEnumerable<Production> GetProducts(int supplyTypeID, int month, int year)
        {
            return DataContext.Productions.Include(x => x.Product.ProductRawMaterials)
                .Where(x => DbFunctions.TruncateTime(x.Date).Value.Month == month
                && DbFunctions.TruncateTime(x.Date).Value.Year == year
                && x.Product.ProductRawMaterials.Any(y => y.SupplyTypeID == supplyTypeID)).ToList();
        }


        public IEnumerable<Production> GetProducts(DateTime dateTime, bool isProduce)
        {
            return DataContext.Productions
                .Include(x => x.Product.ProductRawMaterials)
                .Include(y => y.Product.ProductRawMaterials.Select(z => z.SupplyType.SupplyClass))
                 .Where(a => DbFunctions.TruncateTime(a.Date) == DbFunctions.TruncateTime(dateTime)
                 && a.Product.IsProduce == isProduce).ToList();
        }


        public IEnumerable<Production> GetProductsByMonth(DateTime month, bool isProduce)
        {
            return DataContext.Productions
                 .Include(x => x.Product.ProductRawMaterials)
                 .Include(y => y.Product.ProductRawMaterials.Select(z => z.SupplyType.SupplyClass))
                  .Where(a => DbFunctions.TruncateTime(a.Date).Value.Month == DbFunctions.TruncateTime(month).Value.Month
                      && DbFunctions.TruncateTime(a.Date).Value.Year == DbFunctions.TruncateTime(month).Value.Year
                  && a.Product.IsProduce == isProduce).OrderBy(x => x.Date).ToList();
        }


        public IEnumerable<Production> GetProductsV2(int productID, int month, int year)
        {
            return DataContext.Productions.Include(x => x.Product.ProductRawMaterials)
               .Where(x => DbFunctions.TruncateTime(x.Date).Value.Month == month
               && DbFunctions.TruncateTime(x.Date).Value.Year == year
               && x.ProductID == productID).ToList();
        }
    }
}
