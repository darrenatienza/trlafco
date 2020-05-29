using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IMilkCollectionRepo : IRepository<MilkCollection>
    {

        IEnumerable<MilkCollection> GetAllRecords(DateTime date, string criteria);

        IEnumerable<MilkCollection> GetAllBy(DateTime date);

        IEnumerable<MilkCollection> GetAllBy(DateTime date, int milkClassID);

        MilkCollection GetMilkCollection(int id);

        IEnumerable<MilkCollection> GetAllRecordsByMonth(DateTime date, string criteria);

        IEnumerable<MilkCollection> GetAllByMonth(DateTime date, int p);

        IEnumerable<MilkCollection> GetMonthlyRecords(DateTime date, int farmerID);

        IEnumerable<MilkCollection> GetAllByMonth(DateTime date);

        IEnumerable<MilkCollection> GetAllByMonth(DateTime dateTime, string criteria);

        IEnumerable<MilkCollection> GetAllRecords();

        IEnumerable<MilkCollection> GetAllBy(DateTime date, string milkDescription);

        IEnumerable<MilkCollection> GetAllByMonthV2(DateTime dateTime, int supplyTypeID);
    }
}
