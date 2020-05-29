using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IMilkUtilizeRecordRepo : IRepository<MilkUtilizeRecord>
    {

        IEnumerable<MilkUtilizeRecord> GetAllByMonth(DateTime monthDate);

        IEnumerable<MilkUtilizeRecord> GetRecords(DateTime date, string criteria);



        IEnumerable<MilkUtilizeRecord> GetAllRecords();

        MilkUtilizeRecord GetRecords(int id);

        MilkUtilizeRecord GetBy(DateTime dateTime);

        IEnumerable<MilkUtilizeRecord> GetAllBy(DateTime dateTime);

    }
}
