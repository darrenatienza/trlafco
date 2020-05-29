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
    public class MilkUtilizeProduct
    {
        public MilkUtilizeProduct() { }
        public int MilkUtilizeProductID { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Description { get; set; }
    }
}
