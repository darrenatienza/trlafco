using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IExpenseRepo : IRepository<Expense>
    {

        IEnumerable<Expense> GetAllBy(DateTime dateTime, string criteria);

        IEnumerable<Expense> GetAllByMonth(DateTime dateTime, string criteria);
    }
}
