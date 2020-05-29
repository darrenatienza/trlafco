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
    public class MilkProductLogic : IMilkProductLogic
    {
        public MilkProductLogic() { }



        public IEnumerable<MilkProductListModel> GetAll()
        {
            try
            {
                var models = new List<MilkProductListModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                   
                    var objs = uow.MilkUtilizeProducts.GetAll();
                    foreach (var item in objs)
                    {
                         var model = new MilkProductListModel();
                         model.ID = item.MilkUtilizeProductID;
                         model.Description = item.Description;
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


        public IEnumerable<MilkProductListModel> GetAllBy(int milkUtilizeRecordID, string p)
        {
            throw new NotImplementedException();
        }
    }
}
