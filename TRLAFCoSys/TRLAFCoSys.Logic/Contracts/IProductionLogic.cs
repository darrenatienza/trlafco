using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IProductionLogic : IBaseLogic<AddEditProductionModel>
    {

       

        List<ProductionListModel> GetAll(int month, int year, string criteria);

        ProductionModel Get(int productionID);
    }
}
