using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class SupplyTypeModel
    {
        public int SupplyTypeID;
        public int ID { get; set; }
        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public int SupplyClassID { get; set; }

    }
    public class SupplyTypeListModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
    }
    public class SupplyTypeGridListModel
    {
        public int ID { get; set; }
        public string SupplyClassDescription { get; set; }
        public string Description { get; set; }

        public double UnitPrice { get; set; }
    }
}
