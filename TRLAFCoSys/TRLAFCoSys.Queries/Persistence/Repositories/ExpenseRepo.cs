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
    public class ExpenseRepo : Repository<Expense>, IExpenseRepo
    {
        public ExpenseRepo(DataContext context)
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




        public IEnumerable<Expense> GetAllBy(DateTime dateTime, string criteria)
        {
            return DataContext.Expenses
                .Include(r => r.ExpenseType)
                .Where(r => DbFunctions.TruncateTime(r.ActualDate) == DbFunctions.TruncateTime(dateTime)
               && r.Particulars.Contains(criteria));
        }


        public IEnumerable<Expense> GetAllByMonth(DateTime dateTime, string criteria)
        {
            return DataContext.Expenses
                .Include(r => r.ExpenseType)
                .Where(r => DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(dateTime).Value.Month
               && r.Particulars.Contains(criteria));
        }
    }
}
