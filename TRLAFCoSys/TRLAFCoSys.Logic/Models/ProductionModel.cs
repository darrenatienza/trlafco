using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class AddEditProductionModel
    {
        public int ID { get; internal set; }
        public DateTime Date { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

    }
    public class ProductionListModel
    {
        public int ID { get; internal set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public int ProductionQuantity { get; set; }

    }
    public class ProductionModel
    {
        public int ID { get; internal set; }
        public DateTime Date { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

    }
}
