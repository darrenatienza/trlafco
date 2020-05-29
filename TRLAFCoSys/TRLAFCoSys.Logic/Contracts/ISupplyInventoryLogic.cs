using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface ISupplyInventoryLogic
    {


        

        void Add(SupplyInventoryModel model);

        void Edit(int id, SupplyInventoryModel model);

        void Delete(int id);



        IEnumerable<SupplyInventoryListModel> GetComputedByMonth(DateTime date, string criteria);

        IEnumerable<SupplyInventorySummaryModel> GetAllSummary(DateTime date);

        SupplyInventoryModel GetBy(int id);

        double Compute(double quantity, double unitprice);

        SupplyGrandTotalSummaryModel GetGrandTotalSummary(DateTime date);
    }
}
