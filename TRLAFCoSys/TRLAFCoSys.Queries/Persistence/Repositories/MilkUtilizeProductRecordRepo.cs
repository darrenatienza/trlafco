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
    public class MilkUtilizeProductRecordRepo : Repository<MilkUtilizeProductRecord>, IMilkUtilizeProductRecordRepo
    {
        public MilkUtilizeProductRecordRepo(DataContext context)
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




        public IEnumerable<MilkUtilizeProductRecord> GetAllBy(DateTime date)
        {
            return DataContext.MilkUtilizeProductRecords
                .Include(r => r.MilkUtilizeProduct)
                .Where(r => DbFunctions.TruncateTime(r.MilkUtilizeRecord.ActualDate) == DbFunctions.TruncateTime(date));
        }


        public IEnumerable<MilkUtilizeProductRecord> GetAllBy(int milkUtilizeRecordID, string criteria)
        {
            return DataContext.MilkUtilizeProductRecords
                .Include(r => r.MilkUtilizeProduct)
                .Where(r => r.MilkUtilizeRecordID == milkUtilizeRecordID && r.MilkUtilizeProduct.Description.Contains(criteria));
        }


        public MilkUtilizeProductRecord GetBy(int milkUtilizeProductID)
        {
            return DataContext.MilkUtilizeProductRecords
                 .Include(r => r.MilkUtilizeProduct)
                 .FirstOrDefault(r => r.MilkUtilizeProductRecordID == milkUtilizeProductID);
        }





        public IEnumerable<MilkUtilizeProductRecord> GetAllBy(DateTime date, int productID)
        {
            return DataContext.MilkUtilizeProductRecords
                 .Where(r => DbFunctions.TruncateTime(r.MilkUtilizeRecord.ActualDate).Value.Month 
                     == DbFunctions.TruncateTime(date).Value.Month
                     && r.MilkUtilizeProductID == productID
                 );
        }
    }
}
