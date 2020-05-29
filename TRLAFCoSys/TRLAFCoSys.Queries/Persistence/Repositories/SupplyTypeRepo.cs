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
    public class SupplyTypeRepo : Repository<SupplyType>, ISupplyTypeRepo
    {
        public SupplyTypeRepo(DataContext context)
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




        public IEnumerable<SupplyType> GetAll(int supplyClassID)
        {
            return DataContext.SupplyTypes.Where(r => r.SupplyClassID == supplyClassID);
        }


        public IEnumerable<SupplyType> GetAllRecords()
        {
            return DataContext.SupplyTypes.Include(r => r.SupplyClass);
        }


        public IEnumerable<SupplyType> GetAll(string criteria)
        {
            return DataContext.SupplyTypes.Include(r => r.SupplyClass).Where(x => x.Description.Contains(criteria));
        }


        public SupplyType GetIncludeSupplyClass(int supplyTypeID)
        {
            return DataContext.SupplyTypes.Include(r => r.SupplyClass).FirstOrDefault(x => x.SupplyTypeID == supplyTypeID);
        }
    }
}
