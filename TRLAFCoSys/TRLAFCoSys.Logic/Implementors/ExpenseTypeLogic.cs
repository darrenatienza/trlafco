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
    public class ExpenseTypeLogic : IExpenseTypeLogic
    {
        public ExpenseTypeLogic() { }




        public IEnumerable<ExpenseTypeListModel> GetAll()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var models = new List<ExpenseTypeListModel>();
                var objs = uow.ExpenseTypes.GetAll();
                foreach (var item in objs)
                {
                    var model = new ExpenseTypeListModel();
                    model.ID = item.ExpenseTypeID;
                    model.Description = item.Description;
                    models.Add(model);
                }
                return models;
            }
        }
    }
}
