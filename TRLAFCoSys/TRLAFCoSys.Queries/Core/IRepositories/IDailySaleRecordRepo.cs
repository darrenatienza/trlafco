using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IDailySaleRecordRepo : IRepository<DailySaleRecord>
    {



        DailySaleRecord GetRecordBy(int id);
        /// <summary>
        /// Get List  of Actual Daily Sales on particular date
        /// </summary>
        /// <param name="date">Date of Sale</param>
        /// <returns></returns>
        IEnumerable<DailySaleRecord> GetAllByMonthYear(DateTime date);
    }
}
