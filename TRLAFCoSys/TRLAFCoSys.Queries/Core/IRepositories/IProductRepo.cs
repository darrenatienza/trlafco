using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IProductRepo : IRepository<Product>
    {

        IEnumerable<Product> GetAll(string criteria);



        IEnumerable<Product> GetAllBy(int supplyTypeID);

        Product GetBySupplierID(int supplyTypeID);

        IEnumerable<Product> GetAllBySupplierID(int supplyTypeID);

        Product GetBy(int productID);
    }
}
