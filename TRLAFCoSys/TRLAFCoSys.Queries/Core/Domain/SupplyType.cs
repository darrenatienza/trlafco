﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class SupplyType
    {
        public SupplyType() { }
        public int SupplyTypeID { get; set; }
        public string Description { get; set; }

        public double UnitPrice { get; set; }
        public virtual SupplyClass SupplyClass { get; set; }
        public int SupplyClassID { get; set; }
    }
}
