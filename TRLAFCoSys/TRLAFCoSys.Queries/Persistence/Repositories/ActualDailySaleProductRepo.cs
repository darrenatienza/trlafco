using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class ActualDailySaleProductRepo : Repository<ActualDailySaleProduct>, IActualDailySaleProductRepo
    {
        public ActualDailySaleProductRepo(DataContext context)
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


       
    }
}
