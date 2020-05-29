using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class MilkClassRepo : Repository<MilkClass>, IMilkClassRepo
    {
        public MilkClassRepo(DataContext context)
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




        public IEnumerable<MilkClass> GetRecords(string criteria)
        {
            return DataContext.MilkClasses.Where(r => r.Description.Contains(criteria));
        }
    }
}
