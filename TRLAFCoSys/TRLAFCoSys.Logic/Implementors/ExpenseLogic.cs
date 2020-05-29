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
    public class ExpenseLogic : IExpenseLogic
    {
        public ExpenseLogic() { }




        public IEnumerable<ExpenseListModel> GetAllBy(string searchType, DateTime dateTime, string criteria)
        {
            try
            {
                var models = new List<ExpenseListModel>();
                IEnumerable<MilkCollection> objsMilk = new List<MilkCollection>();
                IEnumerable<Expense> objsExpense = new List<Expense>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    switch (searchType)
                    {
                        case "date":
                            //objsMilk = uow.MilkCollections.GetAllRecords(dateTime, criteria);
                            objsExpense = uow.Expenses.GetAllBy(dateTime, criteria);
                            break;
                        case "month":
                            //objsMilk = uow.MilkCollections.GetAllByMonth(dateTime, criteria);
                            objsExpense = uow.Expenses.GetAllByMonth(dateTime, criteria);
                            break;
                    }
                    //foreach (var item in objsMilk)
                    //{
                    //    var model = new ExpenseListModel();
                    //    model.Date = item.ActualDate;
                    //    model.Particular = item.SupplyType.Description;
                    //    model.ExpenseTypeDescription = "Raw Milk";
                    //    model.UnitPrice = item.MilkClass.Cost;
                    //    model.Quantity = item.Volume;
                    //    model.Total = model.Quantity * model.UnitPrice;
                    //    models.Add(model);
                    //}
                    foreach (var item in objsExpense)
                    {
                        var model = new ExpenseListModel();
                        model.Date = item.ActualDate;
                        model.ID = item.ExpenseID;
                        model.Particular = item.Particulars;
                        model.ExpenseTypeDescription = item.ExpenseType.Description;
                        model.UnitPrice = item.UnitPrice;
                        model.Quantity = item.Quantity;
                        model.Total = model.Quantity * model.UnitPrice;
                        models.Add(model);
                    }
                }
                return models.OrderBy(r => r.Date);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ExpenseModel GetBy(int expenseID)
        {
            try
            {

                using (var uow = new UnitOfWork(new DataContext()))
                {

                    var obj = uow.Expenses.Get(expenseID);
                 
                        var model = new ExpenseModel();
                        model.Date = obj.ActualDate;
                        model.ID = obj.ExpenseID;
                        model.Particular = obj.Particulars;
                        model.ExpenseTypeID = obj.ExpenseTypeID;
                        model.UnitPrice = obj.UnitPrice;
                        model.Quantity = obj.Quantity;
                        model.Total = obj.Quantity * obj.UnitPrice;
                        return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Add(ExpenseModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = new Expense();
                obj.CreateDateTime = DateTime.Now;
                obj.ActualDate = model.Date;
                obj.Particulars = model.Particular;
                obj.ExpenseTypeID = model.ExpenseTypeID;
                obj.UnitPrice = model.UnitPrice;
                obj.Quantity = model.Quantity;
                uow.Expenses.Add(obj);
                uow.Complete();
                model.ID = obj.ExpenseID;
            }
        }

        public void Edit(int id, ExpenseModel model)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {

                var obj = uow.Expenses.Get(id);
                obj.ActualDate = model.Date;
                obj.Particulars = model.Particular;
                obj.ExpenseTypeID = model.ExpenseTypeID;
                obj.UnitPrice = model.UnitPrice;
                obj.Quantity = model.Quantity;
                uow.Expenses.Edit(obj);
                uow.Complete();

            }
        }


        public double ComputeTotal(int quantity, double unitprice)
        {
            return quantity * unitprice;
        }


        public void Delete(int expensesID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Expenses.Get(expensesID);
                uow.Expenses.Remove(obj);
                uow.Complete();

            }
        }


        public IEnumerable<ExpenseSummaryListModel> GetAllSummary(DateTime date)
        {

            // generate summary for records
            var summaries = new List<ExpenseSummaryListModel>();
            var objs = this.GetAllBy("month", date, "")
                .GroupBy(r => r.ExpenseTypeDescription)
                .Select(r => new { Total = r.Sum(x => x.Total), Description = r.FirstOrDefault().ExpenseTypeDescription });
            foreach (var item in objs)
            {
                var summary = new ExpenseSummaryListModel();
                summary.ExpenseTypeDescription = item.Description;
                summary.Total = item.Total;
                summaries.Add(summary);
            }
            return summaries;
        }
    }
}
