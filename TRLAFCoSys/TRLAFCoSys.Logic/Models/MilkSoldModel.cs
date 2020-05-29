using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class AddRawMilkSoldModel
    {
        public AddRawMilkSoldModel() { }
        public string CustomerName { get; set; }
        public double Volume { get; set; }
    }
    public class RawMilkSoldListModel
    {
        public RawMilkSoldListModel() { }
        public string CustomerName { get; set; }
        public double Volume { get; set; }
    }
}
