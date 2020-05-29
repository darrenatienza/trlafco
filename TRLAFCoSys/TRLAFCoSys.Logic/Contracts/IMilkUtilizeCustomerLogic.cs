using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IMilkUtilizeCustomerLogic
    {

        IEnumerable<MilkUtilizeCustomerListModel> GetAllBy(int recordId, string criteria);
        /// <summary>
        /// Add new record
        /// </summary>
        /// <param name="recordID">MilkUtilizeRecord ID</param>
        /// <param name="model"></param>
        void Add(MilkUtilizeCustomerAddModel model);
        void Edit(int id, MilkUtilizeCustomerEditModel model);
        void Delete(int id);



        MilkUtilizeCustomerModel GetBy(int milkUtilizeCustomerID);

        IEnumerable<MilkUtilizeCustomerComboModel> GetAllBy(DateTime selectedDate);
    }
}
