using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IRawMilkProcessLogic
    {
        List<RawMilkProcessListModel> GetRecords(DateTime dateTime);

        List<RawMilkProcessSummaryListModel> GetSummary(DateTime dateTime);

        double GetMonthlyTotal(DateTime dateTime);
    }
}
