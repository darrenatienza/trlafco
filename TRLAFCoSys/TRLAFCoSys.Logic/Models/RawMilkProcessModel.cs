using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class RawMilkProcessListModel
    {
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
    }
    public class RawMilkProcessSummaryListModel
    {
        public DateTime Date { get; set; }
        public double TotalQuantityPerDay { get; set; }
    }
}
