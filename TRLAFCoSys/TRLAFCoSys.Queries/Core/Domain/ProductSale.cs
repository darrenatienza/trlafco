using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class ProductSale
    {
        public ProductSale()
        {
        }
        public int ProductSaleID { get; set; }
        public DateTime CreateTimeStamp { get; set; }
        public virtual Product Product { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public bool isBuyOneTakeOne { get; set; }
        public string OutletSaleName { get; set; }
    }
}
