using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    
    public class DailySaleRecordModel
    {

        public int ID { get; internal set; }
        public DateTime Date { get; set; }
        public double ProcessingSale { get; set; }
        public double SaleOnAccount { get; set; }
        public double TotalSales { get; internal set; }
        public string Debtor { get; set; }


        public double OutletSale1 { get; internal set; }

        public double OutletSale2 { get; internal set; }
        public double RawMilkSales { get; internal set; }

        public double TotalCashSales { get; internal set; }

        public double TotalSaleForDairyProduct { get; internal set; }
    }
    public class DailySaleRecordModelV2
    {


        public DateTime Date { get; set; }
        public double OutletSale1 { get; internal set; }

        public double OutletSale2 { get; internal set; }
        public double RawMilkSales { get; internal set; }

    }
    public class DailySaleRecordListModel
    {
        
        public int ID { get; internal set; }
        public DateTime Date { get; set; }
        public double OutletSale1 { get; set; }

        public double OutletSale2 { get; set; }
        public double ProcessingSale { get; set; }

        public double TotalCashSales { get; set; }

        public double SaleOnAccount { get; set; }

        public double TotalSaleForDairyProduct { get; set; }

        public double TotalSales { get; set; }



        public string Debtor { get; set; }
    }
    public class DailySaleRecordListModelV2
    {

        public int ID { get; internal set; }
        public double OutletSale1 { get; set; }

        public double OutletSale2 { get; set; }

        public double TotalCashSales { get; set; }

        public double SaleOnAccount { get; set; }
        public double ProcessingSale { get; set; }
        /// <summary>
        /// Sum of Dairy Outlet Sale 1 and Outlet Sale 2
        /// </summary>
        public double TotalSaleForDairyProduct { get; set; }
        /// <summary>
        /// Sum of Raw Milk, Total Sale for Dairy Product
        /// </summary>
        public double TotalSales { get; set; }

        public DateTime Date { get; set; }
    }
    public class DailySaleRecordSummaryModel
    {
        public double TotalB1plus1;

        public double TotalMilkSold { get; set; }

        public double TotalMilkSoldAmount { get; set; }

        public double TotalDiscountSale { get; set; }

        public double TotalEcoBag { get; set; }

        public double TotalOutletSale1 { get; set; }

        public double TotalOutletSale2 { get; set; }

        public double TotalProcessingSale { get; set; }

        public double GrandTotalCashSale { get; set; }

        public double TotalSaleOnAccount { get; set; }

        public double GrandTotalSaleDairyProduct { get; set; }

        public double GrandTotalSales { get; set; }
    }
}
