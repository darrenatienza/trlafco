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
    public class MilkCollectionRepo : Repository<MilkCollection>, IMilkCollectionRepo
    {
        public MilkCollectionRepo(DataContext context)
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



        public IEnumerable<MilkCollection> GetAllRecords(DateTime date, string criteria)
        {
            return DataContext.MilkCollections
                .Include(r => r.Farmer)
                .Include(r => r.MilkClass)
                .Include(r => r.SupplyType)
                .Where(r => DbFunctions.TruncateTime(r.ActualDate) == DbFunctions.TruncateTime(date) 
                    && (r.SupplyType.Description.Contains(criteria) || r.Farmer.FullName.Contains(criteria)));
        }


        public IEnumerable<MilkCollection> GetAllBy(DateTime date)
        {
            return DataContext.MilkCollections.Where(r => DbFunctions.TruncateTime(r.ActualDate) == DbFunctions.TruncateTime(date));
        }


        public IEnumerable<MilkCollection> GetAllBy(DateTime date, int milkClassID)
        {
            return DataContext.MilkCollections
                .Where(r => r.SupplyTypeID == milkClassID && DbFunctions.TruncateTime(r.ActualDate) == DbFunctions.TruncateTime(date));
        }


        public MilkCollection GetMilkCollection(int id)
        {
            return DataContext.MilkCollections
                 .Include(r => r.Farmer)
                 .Include(r => r.MilkClass)
                 .Include(r => r.SupplyType)
                 .FirstOrDefault(r => r.MilkCollectionID == id);
        }


        public IEnumerable<MilkCollection> GetAllRecordsByMonth(DateTime date, string criteria)
        {
            return DataContext.MilkCollections
                .Include(r => r.Farmer)
                .Include(r => r.MilkClass)
                .Include(r => r.SupplyType)
                .Where(r => DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(date).Value.Month
                    && (r.SupplyType.Description.Contains(criteria) || r.Farmer.FullName.Contains(criteria)));
        }


        public IEnumerable<MilkCollection> GetAllByMonth(DateTime date, int milkClassID)
        {
            return DataContext.MilkCollections
                .Where(r => r.SupplyTypeID == milkClassID && DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(date).Value.Month);
        }


        public IEnumerable<MilkCollection> GetMonthlyRecords(DateTime date, int farmerID)
        {
            return DataContext.MilkCollections
                .Include(r => r.SupplyType)
                  .Where(r => r.FarmerID == farmerID && DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(date).Value.Month).ToList();
        }


        public IEnumerable<MilkCollection> GetAllByMonth(DateTime date)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<MilkCollection> GetAllByMonth(DateTime dateTime, string criteria)
        {
            return DataContext.MilkCollections
                .Include(r => r.MilkClass)
                .Include(r => r.SupplyType)
                  .Where(r => r.SupplyType.Description.Contains(criteria) && DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(dateTime).Value.Month);
        }


        public IEnumerable<MilkCollection> GetAllRecords()
        {
            return DataContext.MilkCollections
                .Include(r => r.MilkClass)
                .Include(r => r.SupplyType)
                .Include(x => x.Farmer)
                ;
                  
        }


        public IEnumerable<MilkCollection> GetAllBy(DateTime date, string milkDescription)
        {
            return DataContext.MilkCollections.Where(r => DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(date).Value.Month
                && DbFunctions.TruncateTime(r.ActualDate).Value.Year == DbFunctions.TruncateTime(date).Value.Year
                && r.SupplyType.Description == milkDescription);
        }


        public IEnumerable<MilkCollection> GetAllByMonthV2(DateTime dateTime, int supplyTypeID)
        {
            return DataContext.MilkCollections
                .Where(r => r.SupplyTypeID == supplyTypeID 
                    && DbFunctions.TruncateTime(r.ActualDate).Value.Month == DbFunctions.TruncateTime(dateTime).Value.Month
                    && DbFunctions.TruncateTime(r.ActualDate).Value.Year == DbFunctions.TruncateTime(dateTime).Value.Year).ToList();
        }
    }
}
