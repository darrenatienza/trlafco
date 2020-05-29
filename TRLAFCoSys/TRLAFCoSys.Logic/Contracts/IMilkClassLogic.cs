using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IMilkClassLogic
    {
        IEnumerable<MilkClassModel> GetAllRecord();

        IEnumerable<MilkClassListModel> GetRecords(string criteria);

        void Add(AddMilkClassModel model);

        void Delete(int id);

        MilkClassModel GetRecord(int id);

        

        void Edit(int id, EditMilkClassModel model);
    }
}
