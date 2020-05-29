using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IPayrollLogic
    {
        IEnumerable<PayrollListModel> GetMonthlyRecords(DateTime date, string criteria);
    }
}
