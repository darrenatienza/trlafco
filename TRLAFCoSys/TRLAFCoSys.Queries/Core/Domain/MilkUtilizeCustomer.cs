using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    /// <summary>
    /// Milk Delivered and Utilization Product
    /// </summary>
    public class MilkUtilizeCustomer
    {
        public MilkUtilizeCustomer() { }
        public int MilkUtilizeCustomerID { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string FullName { get; set; }
        public double RawMilkSold { get; set; }
        public virtual MilkUtilizeRecord MilkUtilizeRecord { get; set; }
        public int MilkUtilizeRecordID { get; set; }
    }
}
