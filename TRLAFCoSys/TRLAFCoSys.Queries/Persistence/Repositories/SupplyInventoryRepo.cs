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
    public class SupplyInventoryRepo : Repository<SupplyInventory>, ISupplyInventoryRepo
    {
        public SupplyInventoryRepo(DataContext context)
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



        public SupplyInventory GetBy(int id)
        {
            return DataContext.SupplyInventories.Include(r => r.SupplyType.SupplyClass).FirstOrDefault(r => r.SupplyInventoryID == id);
        }


        public IEnumerable<SupplyInventory> GetAllBy(DateTime dateTime, string criteria)
        {
            return DataContext.SupplyInventories.Include(r => r.SupplyType)
                .Where(r => DbFunctions.TruncateTime(r.ActualDate) == DbFunctions.TruncateTime(dateTime) && r.SupplyType.Description.Contains(criteria));
        }


        public IEnumerable<SupplyInventory> GetAllRecords()
        {
            return DataContext.SupplyInventories.Include(r => r.SupplyType);
        }


        public SupplyInventory GetByMonth(DateTime dateTime)
        {
           return DataContext.SupplyInventories.Include(r => r.SupplyType)
                .FirstOrDefault(r => DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(dateTime).Value.Month
                    && DbFunctions.TruncateTime(r.ActualDate).Value.Year == DbFunctions.TruncateTime(dateTime).Value.Year);
        }


        public SupplyInventory GetByMonth(DateTime dateTime, int supplyTypeID)
        {
            return DataContext.SupplyInventories.Include(r => r.SupplyType)
                .FirstOrDefault(r => DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(dateTime).Value.Month
                    && DbFunctions.TruncateTime(r.ActualDate).Value.Year == DbFunctions.TruncateTime(dateTime).Value.Year && r.SupplyTypeID == supplyTypeID);
        }
    }
}
