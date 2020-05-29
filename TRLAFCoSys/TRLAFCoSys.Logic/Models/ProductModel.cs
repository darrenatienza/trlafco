using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class ProductModel
    {
        public int ID { get; internal set; }
        public string Name { get; set; }
        public  double UnitPrice { get; set; }


        public bool IsProduce { get; set; }

        public int ProductRawMaterialCount { get; set; }
    }
    public class ProductListModel
    {
        public int ID { get; internal set; }
        public string Name { get; set; }
        /// <summary>
        /// Raw Materials Count
        /// </summary>
        public int RawMaterialsCount { get; set; }
    }
    public class ProductRawMaterialModel
    {
        public int SupplyTypeID { get; set; }
        public int ProductID { get; set; }
        public double Quantity { get; set; }

        public int SupplyClassID { get; set; }
    }
    public class ProductRawMaterialListModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
    }

    public class AddEditProductRawMaterialModel
    {
        public int ID { get; internal set; }
        public int SupplyTypeID  { get; set; }
        public int ProductID { get; set; }
        public double Quantity { get; set; }
    }
}
