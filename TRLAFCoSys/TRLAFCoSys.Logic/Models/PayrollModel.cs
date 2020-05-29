using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class PayrollListModel
    {
        public PayrollListModel() { }
        public string FarmerFullName { get; set; }
        public double TotalVolume { get; set; }
        public double TotalAmount { get; set; }
        public double FirstQuarterAmount { get; set; }
        public double Savings { get; set; }
        public double SecondQuarterAmount { get; set; }
    }
}
