using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class ProductSaleModel
    {
        public int ID { get; internal set; }
        public DateTime Date { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double AdditionalQty { get; internal set; }
        public bool IsBuyOneTakeOne { get; set; }


        public string OutletSaleName { get; set; }
    }
    public class ProductSaleListModel
    {
        public string OutletSaleName;
        public int ID { get; internal set; }
        public string CustomerName { get; internal set; }
        public string ProductName { get; internal set; }
        public double Quantity { get; set; }
        public double Total { get; set; }

        public DateTime Date { get; set; }

        public string OutletSale { get; set; }
    }
    
}
