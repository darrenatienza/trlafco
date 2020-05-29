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
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        public ProductRepo(DataContext context)
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




        public IEnumerable<Product> GetAll(string criteria)
        {
            return DataContext.Products.Include(x => x.ProductRawMaterials).Where(x => x.Name.Contains(criteria));
        }





        public IEnumerable<Product> GetAllBy(int supplyTypeID)
        {
            return DataContext.Products.Include(x => x.ProductRawMaterials).Where(x => x.ProductRawMaterials.Any(y => y.SupplyTypeID == supplyTypeID)).ToList();
        }


        public Product GetBySupplierID(int supplyTypeID)
        {
            return DataContext.Products.FirstOrDefault(x => x.ProductRawMaterials.Any(y => y.SupplyTypeID == supplyTypeID));
        }


        public IEnumerable<Product> GetAllBySupplierID(int supplyTypeID)
        {
            return DataContext.Products.Where(x => x.ProductRawMaterials.Any(y => y.SupplyTypeID == supplyTypeID)).ToList();
        }


        public Product GetBy(int productID)
        {
            return DataContext.Products.Include(x => x.ProductRawMaterials)
                .FirstOrDefault(x => x.ProductID == productID);
        }
    }
}
