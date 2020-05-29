using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    /// <summary>
    /// Milk Delivery and Utilization Record
    /// </summary>
    public class MilkUtilizeRecord
    {
        public MilkUtilizeRecord() {
            MilkUtilizeCustomers = new HashSet<MilkUtilizeCustomer>();
            MilkUtilizeProductRecords = new HashSet<MilkUtilizeProductRecord>();
        }
        public int MilkUtilizeRecordID { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ActualDate { get; set; }

        //Milk Delivered from Farmers get from milk product with same date

        //Total Milk for Utilization will compute as Beginning Balance + Milk Delivered
        public ICollection<MilkUtilizeCustomer> MilkUtilizeCustomers { get; set; }
        public ICollection<MilkUtilizeProductRecord> MilkUtilizeProductRecords { get; set; }
        public string WithdrawnByProcessor { get; set; }
        public double RawMilkProcess { get; set; }

        // Total Raw Milk withdraw will compute as Raw Milk sold +  Raw Milk process
        public double Spillage { get; set; }
        public double Analysis { get; set; }

        //Ending Balance will compute as Total Milk for Utilization - Total Raw Milk Withdrawn - Spillage - Analysis
        public int Demo { get; set; }
        public double SpoilageQty { get; set; }
        public double SpoilageValue { get; set; }
        public string Remarks { get; set; }

    }
}
