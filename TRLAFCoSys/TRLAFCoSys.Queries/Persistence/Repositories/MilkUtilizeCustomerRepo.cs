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
    public class MilkUtilizeCustomerRepo : Repository<MilkUtilizeCustomer>, IMilkUtilizeCustomerRepo
    {
        public MilkUtilizeCustomerRepo(DataContext context)
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






        public MilkUtilizeCustomer GetRecord(string fullName)
        {
            return DataContext.MilkUtilizeCustomers.FirstOrDefault(r => r.FullName == fullName);
        }


        public IEnumerable<MilkUtilizeCustomer> GetAllBy(int recordID, string criteria)
        {
            return DataContext.MilkUtilizeCustomers.Where(r => r.MilkUtilizeRecordID == recordID && r.FullName.Contains(criteria));
        }

        /// <summary>
        /// Get all records by current month and year
        /// </summary>
        /// <param name="date"></param>
        /// <param name="criteria"></param>
        /// <returns>MilkUtilizeCustomers</returns>
        public IEnumerable<MilkUtilizeCustomer> GetAllByMonth(DateTime date, string criteria)
        {
            return DataContext.MilkUtilizeCustomers.Include(r => r.MilkUtilizeRecord).Where(r => DbFunctions.TruncateTime(r.MilkUtilizeRecord.ActualDate).Value.Month == DbFunctions.TruncateTime(date).Value.Month
                && DbFunctions.TruncateTime(r.MilkUtilizeRecord.ActualDate).Value.Year == DbFunctions.TruncateTime(date).Value.Year
                && r.FullName.Contains(criteria));
        }


        public IEnumerable<MilkUtilizeCustomer> GetAllBy(DateTime selectedDate)
        {
            return DataContext.MilkUtilizeCustomers.Where(r => DbFunctions.TruncateTime(r.MilkUtilizeRecord.ActualDate) == DbFunctions.TruncateTime(selectedDate));
        }
    }
}
