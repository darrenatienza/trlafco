using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface ISupplyInventoryRepo : IRepository<SupplyInventory>
    {

        SupplyInventory GetBy(int id);

        IEnumerable<SupplyInventory> GetAllBy(DateTime dateTime, string criteria);

        IEnumerable<SupplyInventory> GetAllRecords();

        SupplyInventory GetByMonth(DateTime dateTime);

        SupplyInventory GetByMonth(DateTime dateTime, int supplyTypeID);
    }
}
