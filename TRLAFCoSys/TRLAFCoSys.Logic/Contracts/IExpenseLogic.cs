using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IExpenseLogic
    {


        IEnumerable<ExpenseListModel> GetAllBy(string searchType ,DateTime dateTime, string criteria);

        ExpenseModel GetBy(int expenseID);

        void Add(ExpenseModel model);

        void Edit(int id, ExpenseModel model);

        double ComputeTotal(int quantity, double unitprice);

        void Delete(int expensesID);

        IEnumerable<ExpenseSummaryListModel> GetAllSummary(DateTime date);
    }
}
