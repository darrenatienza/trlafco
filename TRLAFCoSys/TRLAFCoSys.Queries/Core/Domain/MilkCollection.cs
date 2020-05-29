using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class MilkCollection
    {
        public MilkCollection() { }
        public int MilkCollectionID { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ActualDate { get; set; }
        public virtual MilkClass MilkClass { get; set; }
        public int? MilkClassID { get; set; }
        public virtual Farmer Farmer { get; set; }
        public int FarmerID { get; set; }
        public double Volume { get; set; }
        public virtual SupplyType SupplyType { get; set; }
        public int? SupplyTypeID { get; set; }

    }
}
