using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class MilkClass
    {
        public MilkClass() { }
        public int MilkClassID { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
    }
}
