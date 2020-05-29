using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class SupplyInventoryModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int SupplyClassID { get; set; }
        public int SupplyTypeID { get; set; }
        public double UnitPrice { get; internal set; }
        public double PurchaseQuantity { get; set; }
        public double PurchaseTotalAmount { get; set; }
        public double WithdrawQuantity { get; internal set; }
        public double WithdrawTotaAmount { get; set; }


        public string SupplyTypeName { get; set; }

        public string supplyTypeClassName { get; set; }
    }
    public class SupplyInventoryComputeModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string SupplyTypeDescription { get; set; }
        public int SupplyClassID { get; set; }
        public int SupplyTypeID { get; set; }
        public double UnitPrice { get; internal set; }
        public double PurchaseQuantity { get; set; }
        public double PurchaseTotal { get; set; }
        public double WithdrawQuantity { get; set; }
        public double WithdrawTotal { get; set; }

    }
    public class SupplyInventoryListModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }


        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public double BeginningQuantity { get; set; }

        public double BeginningTotal { get; set; }
        public double PurchaseQuantity { get; set; }
        public double PurchaseTotal { get; set; }
        public double WithdrawQuantity { get; set; }
        public double WithdrawTotal { get; set; }
        public double EndingQuantity { get; set; }
        public double EndingTotal { get; set; }

        public string SupplyClassDescription { get; set; }

        public int SupplyTypeID { get; set; }

        public int SupplyClassID { get; set; }
    }
    public class SupplyInventorySummaryModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }


        public string Description { get; set; }

        public double BeginningSubTotalAmount { get; set; }

        public double PurchaseSubTotalAmount { get; set; }

        public double WithdrawSubTotalAmount { get; set; }

        public double EndingSubTotalAmount { get; set; }
    }
    public class SupplyGrandTotalSummaryModel
    {
        public double BeginningGrandTotalAmount { get; set; }
        public double PurchaseGrandTotalAmount { get; set; }
        public double WithdrawGrandTotalAmount { get; set; }
        public double EndingGrandTotalAmount { get; set; }
    }
}
