using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface ISupplyTypeRepo : IRepository<SupplyType>
    {

        IEnumerable<SupplyType> GetAll(int supplyClassID);

        IEnumerable<SupplyType> GetAllRecords();

        IEnumerable<SupplyType> GetAll(string criteria);

        SupplyType GetIncludeSupplyClass(int supplyTypeID);
    }
}
