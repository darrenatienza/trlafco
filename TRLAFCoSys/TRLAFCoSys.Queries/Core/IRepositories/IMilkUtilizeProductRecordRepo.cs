using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IMilkUtilizeProductRecordRepo : IRepository<MilkUtilizeProductRecord>
    {

        IEnumerable<MilkUtilizeProductRecord> GetAllBy(DateTime date);

        IEnumerable<MilkUtilizeProductRecord> GetAllBy(int milkUtilizeRecordID, string criteria);

        MilkUtilizeProductRecord GetBy(int milkUtilizeProductID);

        IEnumerable<MilkUtilizeProductRecord> GetAllBy(DateTime date, int productID);
    }
}
