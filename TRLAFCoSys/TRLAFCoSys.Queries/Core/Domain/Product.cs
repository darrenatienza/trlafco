using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class Product
    {
        public Product() {
            ProductRawMaterials = new HashSet<ProductRawMaterial>();
        }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public bool IsProduce { get; set; }
        public double UnitPrice { get; set; }
        /// <summary>
        /// Raw Materials
        /// </summary>
        public ICollection<ProductRawMaterial> ProductRawMaterials { get; set; }
    }
}
