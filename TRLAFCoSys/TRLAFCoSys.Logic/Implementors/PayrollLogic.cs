using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Persistence;

namespace TRLAFCoSys.Logic.Implementors
{
    public class PayrollLogic : IPayrollLogic
    {
        public PayrollLogic() { }


        public IEnumerable<PayrollListModel> GetMonthlyRecords(DateTime date, string criteria)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var models = new List<PayrollListModel>();
                    var farmers = uow.Farmers.GetAllRecords(criteria).ToList();
                    // For every farmers get all details
                    foreach (var farmer in farmers)
                    {
                        //Get Total Volume for the current month
                        double monthlyTotalVolume = 0.0;
                        double monthlyTotalAmount = 0.0;
                        var model = new PayrollListModel();
                        model.FarmerFullName = farmer.FullName;

                        var monthlyCollectionsByFarmer = uow.MilkCollections.GetMonthlyRecords(date, farmer.FarmerID);
                        monthlyTotalVolume = monthlyCollectionsByFarmer.Sum(x => x.Volume);
                        monthlyTotalAmount = monthlyCollectionsByFarmer.Sum(x => x.Volume * x.SupplyType.UnitPrice);
                        
                        //Get First Quarter Amount (1-15)
                        double firstQuarterAmount = 0.0;
                        for (int i = 0; i <= 15; i++)
                        {
                            firstQuarterAmount = firstQuarterAmount + monthlyCollectionsByFarmer.Where(r => r.ActualDate.Day == i).Sum(x => x.SupplyType.UnitPrice * x.Volume);
                            
                        }
                        model.TotalAmount = monthlyTotalAmount;
                        model.FirstQuarterAmount = firstQuarterAmount;
                        model.TotalVolume = monthlyTotalVolume;
                        model.Savings = Math.Round(monthlyTotalVolume);
                        model.SecondQuarterAmount = monthlyTotalAmount - (firstQuarterAmount + model.Savings);
                        models.Add(model);
                    }
                    return models;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
