using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IMilkCollectionLogic
    {


        IEnumerable<MilkCollectionModel> GetAllRecordsByDate(DateTime date, string criteria);

       MilkCollectionSummaryModel GetMilkProductSummaryDate(DateTime date);


       void Add(AddMilkCollectionModel model);

       void Edit(int id, EditMilkCollectionModel model);

       MilkCollectionModel Get(int id);

       void Delete(int id);

       IEnumerable<MilkCollectionModel> GetAllRecordsByMonth(DateTime date, string criteria);

       MilkCollectionSummaryModel GetMilkProductSummaryMonth(DateTime date);

       double GetSubTotalVolume(string milkDescription, DateTime date);
    }
}
