using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    /// <summary>
    /// Raw Materials
    /// </summary>
    public class ProductSupplyTypeListModel
    {
        public int ID { get; set; }
        public string SupplyTypeDescription { get; set; }
        public int Quantity { get; set; }
    }
}
