using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class SupplyInventory
    {
        public SupplyInventory() { }
        public DateTime CreateDateTime { get; set; }
        public DateTime ActualDate { get; set; }
        public int SupplyInventoryID { get; set; }
        public double PurchaseQuantity { get; set; }
        public double WithdrawQuantity { get; set; }
        public virtual SupplyType SupplyType { get; set; }
        public int SupplyTypeID { get; set; }

    }
}
