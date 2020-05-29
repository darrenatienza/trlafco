using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class DailySaleRecordRepo : Repository<DailySaleRecord>, IDailySaleRecordRepo
    {
        public DailySaleRecordRepo(DataContext context)
            :base(context)
        {

        }
       
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }




        


        public DailySaleRecord GetRecordBy(int id)
        {
            return DataContext.DailySaleRecords.FirstOrDefault(r => r.DailySaleRecordID == id);
        }


        public IEnumerable<DailySaleRecord> GetAllByMonthYear(DateTime date)
        {
            return DataContext.DailySaleRecords.Where(r => DbFunctions.TruncateTime(r.CreateDateTime).Value.Year == DbFunctions.TruncateTime(date).Value.Year && DbFunctions.TruncateTime(r.CreateDateTime).Value.Month == DbFunctions.TruncateTime(date).Value.Month).ToList();
        }
    }
}
