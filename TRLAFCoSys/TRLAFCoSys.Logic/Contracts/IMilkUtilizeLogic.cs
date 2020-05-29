using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IMilkUtilizeLogic
    {
        IEnumerable<MilkUtilizeListModel> GetRecords(DateTime date);

        void Add(MilkUtilizeAddModel model);

        void Delete(int id);

        void Edit(int id, MilkUtilizeEditModel model);

        MilkUtilizeModel GetRecord(int id);

        MilkUtilizeResultModel Compute(MilkUtilizeComputeModel model);




        MilkForUtilizationtModel GetComputedRecordForAdd(DateTime date);

        MilkForUtilizationtModel GetMilkForUtilization(DateTime dateTime);

        MilkUtilizeSummaryModel GetSummary(DateTime date);
        IEnumerable<MilkUtilizeProductSummaryModel> GetProductSummary(DateTime date);

        MilkUtilizeModel GetRecord(DateTime date);

        bool CheckRecordIfExists(DateTime date);
    }
}
