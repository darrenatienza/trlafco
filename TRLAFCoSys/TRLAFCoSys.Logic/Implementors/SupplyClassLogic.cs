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
    public class SupplyClassLogic : ISupplyClassLogic
    {
        public SupplyClassLogic() { }






        public void Add(SupplyClassModel model)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, SupplyClassModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<SupplyClassListModel> GetAll()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var models = new List<SupplyClassListModel>();
                var objs = uow.SupplyClasses.GetAll();
                foreach (var item in objs)
                {
                    var model = new SupplyClassListModel();
                    model.ID = item.SupplyClassID;
                    model.Description = item.Description;
                    models.Add(model);
                }
                return models;
            }
        }
    }
}
