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
    public class MilkUtilizeRecordRepo : Repository<MilkUtilizeRecord>, IMilkUtilizeRecordRepo
    {
        public MilkUtilizeRecordRepo(DataContext context)
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




        public IEnumerable<MilkUtilizeRecord> GetAllByMonth(DateTime date)
        {
            return DataContext.MilkUtilizeRecords
                
                .Where(r => DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(date).Value.Month);
        }


        public IEnumerable<MilkUtilizeRecord> GetRecords(DateTime date, string criteria)
        {
            return DataContext.MilkUtilizeRecords
                .Where(r => DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(date).Value.Month);
        }


        public IEnumerable<MilkUtilizeRecord> GetAllRecords()
        {
            return DataContext
                .MilkUtilizeRecords
                .Include(r => r.MilkUtilizeCustomers)
                .OrderBy(r => r.ActualDate).ToList();
        }


        public MilkUtilizeRecord GetRecords(int id)
        {
            return DataContext
                .MilkUtilizeRecords
                .Include(r => r.MilkUtilizeCustomers)
                .Include(r => r.MilkUtilizeProductRecords.Select(y => y.MilkUtilizeProduct))
                .FirstOrDefault(r => r.MilkUtilizeRecordID == id);
        }


        public MilkUtilizeRecord GetBy(DateTime dateTime)
        {
            return DataContext
                .MilkUtilizeRecords
             
                .FirstOrDefault(r => DbFunctions.TruncateTime(r.ActualDate) == DbFunctions.TruncateTime(dateTime));
        }


        public IEnumerable<MilkUtilizeRecord> GetAllBy(DateTime dateTime)
        {
            return DataContext
               .MilkUtilizeRecords
               .Where(r => DbFunctions.TruncateTime(r.ActualDate) == DbFunctions.TruncateTime(dateTime)).ToList();
        }


        
    }
}
