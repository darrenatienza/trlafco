using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface ISupplyClassLogic
    {


        

        void Add(SupplyClassModel model);

        void Edit(int id, SupplyClassModel model);

        void Delete(int id);



        IEnumerable<SupplyClassListModel> GetAll();
    }
}
