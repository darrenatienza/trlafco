using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class MilkUtilizeCustomerListModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Volume { get; set; }
    }
    public class MilkUtilizeCustomerComboModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public double Volume { get; set; }
    }
    public class MilkUtilizeCustomerModel
    {
        public string FullName { get; set; }
        public double Volume { get; set; }
    }
    public class MilkUtilizeCustomerAddModel
    {
        public int MilkUtilizeRecordID { get; set; }
        public string FullName { get; set; }
        public double Volume { get; set; }
    }
    public class MilkUtilizeCustomerEditModel
    {
        public string FullName { get; set; }
        public double Volume { get; set; }
    }
}
