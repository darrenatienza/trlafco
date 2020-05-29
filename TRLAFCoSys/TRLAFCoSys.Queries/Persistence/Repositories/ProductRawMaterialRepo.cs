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
    public class ProductRawMaterialRepo : Repository<ProductRawMaterial>, IProductRawMaterialRepo
    {
        public ProductRawMaterialRepo(DataContext context)
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





        public IEnumerable<ProductRawMaterial> GetAll(int productID)
        {
            return DataContext.ProductRawMaterials.Include(x => x.Product).Include(x => x.SupplyType).Where(x => x.ProductID == productID).ToList();
        }


        public ProductRawMaterial GetV2(int productRawMaterialID)
        {
            return DataContext.ProductRawMaterials.Include(x => x.SupplyType.SupplyClass).FirstOrDefault(x => x.ProductRawMaterialID == productRawMaterialID);
        }


        public IEnumerable<ProductRawMaterial> GetBySupplyTypeID(int supplyTypeID)
        {
            return DataContext.ProductRawMaterials.Where(x => x.SupplyTypeID == supplyTypeID);
        }
    }
}
