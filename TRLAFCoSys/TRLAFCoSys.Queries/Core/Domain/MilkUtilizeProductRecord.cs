using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class MilkUtilizeProductRecord
    {
        public MilkUtilizeProductRecord() { }
        public int MilkUtilizeProductRecordID { get; set; }
        public virtual MilkUtilizeProduct MilkUtilizeProduct { get; set; }
        public int MilkUtilizeProductID { get; set; }
        public virtual MilkUtilizeRecord MilkUtilizeRecord { get; set; }
        public int MilkUtilizeRecordID { get; set; }
        public int Quantity { get; set; }
    }
}
