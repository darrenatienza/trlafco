using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class PayrollRepo : Repository<Payroll>, IPayrollRepo
    {
        public PayrollRepo(DataContext context)
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
