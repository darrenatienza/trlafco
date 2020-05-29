using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class MilkCollectionModel
    {
        public DateTime ActualDate { get; set; }

        public string MilkClass { get; set; }

        public string FullName { get; set; }

        public double Amount { get; set; }

        public double MilkCost { get; set; }

        public int ID { get; set; }

        public double Volume { get; set; }
    }
    
    public class MilkCollectionSummaryModel
    {

        public IEnumerable<MilkCollectionSubTotalModel> MilkProductSubTotalModels { get; set; }
        public double MilkVolumeTotal { get; set; }
        public double MilkAmountTotal { get; set; }


    }
    public class MilkCollectionSubTotalModel
    {
        public string MilkClass { get; set; }
        public double MilkVolumeSubTotal { get; set; }
        public double MilkAmountSubTotal { get; set; }
    }
    public class AddMilkCollectionModel
    {
        public DateTime ActualDate { get; set; }

        public int MilkClassID { get; set; }

        public int FarmerID { get; set; }

        public double Volume { get; set; }
    }
    public class EditMilkCollectionModel
    {
        public DateTime ActualDate { get; set; }

        public int MilkClassID { get; set; }

        public int FarmerID { get; set; }

        public double Volume { get; set; }
    }
}
