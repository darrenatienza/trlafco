using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IProductionRepo : IRepository<Production>
    {


        IEnumerable<Production> GetAll(int month, int year, string criteria);

        IEnumerable<Production> GetProducts(DateTime dateTime, int supplyTypeID);

        IEnumerable<Production> GetProducts(int supplyTypeID, int month, int year);

        IEnumerable<Production> GetProducts(DateTime dateTime, bool isProduce);

       

        IEnumerable<Production> GetProductsByMonth(DateTime month, bool isProduce);

        IEnumerable<Production> GetProductsV2(int p, int month, int year);
    }
}
