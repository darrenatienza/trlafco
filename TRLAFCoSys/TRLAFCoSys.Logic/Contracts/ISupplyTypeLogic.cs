using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface ISupplyTypeLogic
    {


        

        void Add(SupplyTypeModel model);

        void Edit(int id, SupplyTypeModel model);

        void Delete(int id);



        IEnumerable<SupplyTypeListModel> GetAll();

        IEnumerable<SupplyTypeListModel> GetAllBy(int supplyClassID);

        SupplyTypeModel GetBy(int id);

        IEnumerable<SupplyTypeGridListModel> GetAllBy(string criteria);
    }
}
