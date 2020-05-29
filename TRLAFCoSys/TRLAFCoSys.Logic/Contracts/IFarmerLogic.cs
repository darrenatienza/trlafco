using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IFarmerLogic
    {
        IEnumerable<FarmerListModel> GetAllRecord();

        IEnumerable<FarmerListModel> GetRecords(string criteria);

        void Add(AddFarmerModel model);


        void Edit(int id, EditFarmerModel model);

        FarmerModel GetRecord(int id);

        void Delete(int id);
    }
}
