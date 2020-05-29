using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class MilkUtilizeProductListModel
    {
        public MilkUtilizeProductListModel() { }
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
       
    }
    public class MilkUtilizeProductModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public int ProductID { get; set; }
    }
    public class MilkUtilizeProductAddModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public int MilkUtilizeRecordID { get; set; }
    }
    public class MilkUtilizeProductEditModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
   
}
