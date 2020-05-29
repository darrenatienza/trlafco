using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IMilkUtilizeCustomerRepo : IRepository<MilkUtilizeCustomer>
    {




        MilkUtilizeCustomer GetRecord(string fullName);

        IEnumerable<MilkUtilizeCustomer> GetAllBy(int recordID,string criteria);

        /// <summary>
        /// Get all records by current month year and fullname criteria of customer
        /// </summary>
        /// <param name="date"></param>
        /// <param name="criteria"></param>
        /// <returns>MilkUtilizeCustomers</returns>
        IEnumerable<MilkUtilizeCustomer> GetAllByMonth(DateTime date, string criteria);

        IEnumerable<MilkUtilizeCustomer> GetAllBy(DateTime selectedDate);
    }
}
