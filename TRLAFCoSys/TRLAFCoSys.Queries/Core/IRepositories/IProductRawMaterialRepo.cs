using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    /// <summary>
    /// Product Raw Materials
    /// </summary>
    public interface IProductRawMaterialRepo : IRepository<ProductRawMaterial>
    {

        IEnumerable<ProductRawMaterial> GetAll(int productID);

        ProductRawMaterial GetV2(int productRawMaterialID);

        IEnumerable<ProductRawMaterial> GetBySupplyTypeID(int supplyTypeID);
    }
}
