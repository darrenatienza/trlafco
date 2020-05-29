using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    /// <summary>
    /// Product Raw Materials
    /// </summary>
    public class ProductRawMaterial
    {
        public ProductRawMaterial() { }
        public int ProductRawMaterialID { get; set; }
        public virtual SupplyType SupplyType { get; set; }
        public int SupplyTypeID { get; set; }
        public virtual Product Product { get; set; }
        public int ProductID { get; set; }
        public double Quantity { get; set; }
    }
}
