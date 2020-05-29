using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class MilkUtilizeListModel
    {
        public MilkUtilizeListModel() { }
        public int ID { get; set; }
        public DateTime ActualDate { get; set; }
        /// <summary>
        /// Volume of the previous date
        /// </summary>
        public double BeginningVolume { get; set; }
        public double MilkDeliveredFromFarmers { get; set; }
        public double TotalMilkForUtilization { get; set; }
        public string Customer { get; set; }
        public string WithdrawnByProcessor { get; set; }
        public double RawMilkProcess { get; set; }
        public double TotalRawMilkWithdrawn { get; set; }
        public double Spillage { get; set; }
        public double Analysis { get; set; }
        public double EndingVolumeBalance { get; set; }
        public int Demo { get; set; }
        public double SpoilageQty { get; set; }
        public double SpoilageValue { get; set; }
        public string Remarks { get; set; }

    }
    public class MilkUtilizeModel
    {
        public MilkUtilizeModel() {
           
        }
        public DateTime ActualDate { get; set; }
        /// <summary>
        /// Volume of the previous date
        /// </summary>
        public double BeginningVolumeBalance { get; set; }
        public double TotalMilkDeliveredFromFarmers { get; set; }
        public double TotalMilkForUtilization { get; set; }
        public string WithdrawnByProcessor { get; set; }
        public double RawMilkProcess { get; set; }
        public double TotalRawMilkWithdrawn { get; set; }
        public double Spillage { get; set; }
        public double Analysis { get; set; }
        public double EndingVolumeBalance { get; set; }
        public int Demo { get; set; }
        public double SpoilageQty { get; set; }
        public double SpoilageValue { get; set; }
        public string Remarks { get; set; }
        public double RawMilkSold { get; set; }

        public int ID { get; set; }
    }
    public class MilkUtilizeAddModel
    {

        public MilkUtilizeAddModel() {
           
        }
        public DateTime ActualDate { get; set; }
        public string WithdrawnByProcessor { get; set; }
        public double RawMilkProcess { get; set; }
        public double Spillage { get; set; }
        public double Analysis { get; set; }
        public int Demo { get; set; }
        public double SpoilageQty { get; set; }
        public double SpoilageValue { get; set; }
        public string Remarks { get; set; }
        public int ID { get; internal set; }
    }

    public class MilkUtilizeEditModel
    {
        public MilkUtilizeEditModel()
        {
           
        }
        public string WithdrawnByProcessor { get; set; }
        public double RawMilkProcess { get; set; }
        public double Spillage { get; set; }
        public double Analysis { get; set; }
        public int Demo { get; set; }
        public double SpoilageQty { get; set; }
        public double SpoilageValue { get; set; }
        public string Remarks { get; set; }


       
    }
    public class MilkUtilizeComputeModel
    {
        public MilkUtilizeComputeModel() { }

        public double Analysis { get; set; }

        public double Spillage { get; set; }

        public double RawMilkProcess { get; set; }

        public double TotalMilkForUtilization { get; set; }

        public double TotalMilkSold { get; set; }

        public double MilkDeliveredByFarmers { get; set; }

        public double EndingVolumeBalance { get; set; }

        public double BeginningVolumeBalance { get; set; }

        public DateTime ActualDate { get; set; }

        public double TotalMilkWithdrawn { get; set; }

        public int ID { get; set; }

        public int Demo { get; set; }

        public string Remarks { get; set; }

        public double SpoilageQty { get; set; }

        public double SpoilageValue { get; set; }

        public string WithdrawnByProcessor { get; set; }
    }
    public class MilkForUtilizationtModel
    {
        public double BeginningBalance { get; set; }
        public double TotalMilkDeliveredByFarmers { get; set; }
        public double TotalMilkForUtilization { get; set; }
    }
    public class MilkUtilizeResultModel
    {
        public double TotalRawMilkWithdrawn { get; set; }
        public double EndingBalance { get; set; }
    }
    public class ResultMilkDelivUtilizModelV2
    {
        public ResultMilkDelivUtilizModelV2() { }
        public DateTime ActualDate { get; set; }
        public double BeginningBalance { get; set; }
        public double EndingBalance { get; set; }
    }
    public class MilkDelivUtilizProductRecordListModel
    {
        public MilkDelivUtilizProductRecordListModel()
        {

        }
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }

    public class AddEditMilkDelivUtilizProductListModel
    {
        public AddEditMilkDelivUtilizProductListModel()
        {

        }
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
    public class MilkUtilizeSummaryModel
    {
        public double MilkCollectionPerMonth { get; set; }
        public double MilkSoldPerMonth { get; set; }
        public double MilkProcessPerMonth { get; set; }
        public double MilkWithdrawnPerMonth { get; set; }
        public double MilkAnalysisPerMonth { get; set; }
    }
    public class MilkUtilizeProductSummaryModel
    {
        public string ProductName { get; internal set; }
        public int Total { get; internal set; }
    }
}
