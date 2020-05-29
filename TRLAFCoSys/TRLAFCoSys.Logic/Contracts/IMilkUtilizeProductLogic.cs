using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IMilkUtilizeProductLogic
    {

        IEnumerable<MilkUtilizeProductListModel> GetAll();

        IEnumerable<MilkUtilizeProductListModel> GetAllBy(int milkUtilizeRecordID, string p);

        MilkUtilizeProductModel GetBy(int milkUtilizeProductID);

        void Add(MilkUtilizeProductAddModel model);

        void Edit(int milkUtilizeProductID, MilkUtilizeProductEditModel model);

        void Delete(int milkUtilizeProductID);
    }
}
