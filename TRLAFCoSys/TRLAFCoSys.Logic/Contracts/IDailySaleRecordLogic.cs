using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IDailySaleRecordLogic
    {



        void Add(DailySaleRecordModel model);

        void Edit(int id, DailySaleRecordModel model);

        void Delete(int id);



        double ComputeTotalSale(double outletSale1, double outletSale2, double processingSale, double saleOnAccount, double rawMilkSales);
        /// <summary>
        /// Get Records by month and year of selected date and criteria
        /// </summary>
        /// <param name="date">Get only year and month</param>
        /// <returns></returns>
        List<DailySaleRecordListModel> GetAllByMonth(DateTime date);

        DailySaleRecordModel Get(int dailySaleID);
        DailySaleRecordModelV2 GetInitial(DateTime date);

        double ComputeTotalCashSale(double outletSale1, double outletSale2, double processingSale);

        double ComputeTotalSaleDairyProduct(double outletSale1, double outletSale2, double processingSale, double saleOnAccount);
    }
}
