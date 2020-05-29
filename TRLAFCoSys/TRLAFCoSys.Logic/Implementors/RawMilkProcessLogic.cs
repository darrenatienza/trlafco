using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Persistence;

namespace TRLAFCoSys.Logic.Implementors
{
    public class RawMilkProcessLogic : IRawMilkProcessLogic
    {

        
        public List<RawMilkProcessListModel> GetRecords(DateTime dateTime)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var models = new List<RawMilkProcessListModel>();
                //Get Total Raw Milk Process from Production per day
                var productProductions = uow.Productions.GetProductsByMonth(month: dateTime, isProduce: true)
                    .Select(x => new
                    {
                        rawMilkProcess = x.Product.ProductRawMaterials.Where(n => n.SupplyType.SupplyClass.Description == "Raw Milk")
                            .Sum(y => y.Quantity * x.Quantity),
                            productName = x.Product.Name,
                            date = x.Date
                    });
                foreach (var item in productProductions)
                {
                    var model = new RawMilkProcessListModel();
                    model.Date = item.date;
                    model.ProductName = item.productName;
                    model.Quantity = item.rawMilkProcess;
                    models.Add(model);
                }
                return models;
            }
        }


        public List<RawMilkProcessSummaryListModel> GetSummary(DateTime dateTime)
        {
            var summaries = this.GetRecords(dateTime).GroupBy(x => x.Date).Select(y => new { date = y.FirstOrDefault().Date, totalQuantity = y.Sum(z => z.Quantity) } );
            var models = new List<RawMilkProcessSummaryListModel>();
            foreach (var item in summaries)
            {
                var model = new RawMilkProcessSummaryListModel();
                model.Date = item.date;
                model.TotalQuantityPerDay = item.totalQuantity;
                models.Add(model);
            }
            return models;
            
            
        }


        public double GetMonthlyTotal(DateTime dateTime)
        {
            return this.GetSummary(dateTime).Sum(x => x.TotalQuantityPerDay);

        }
    }
}
