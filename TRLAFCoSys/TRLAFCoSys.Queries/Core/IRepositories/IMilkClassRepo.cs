using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IMilkClassRepo : IRepository<MilkClass>
    {

        IEnumerable<MilkClass> GetRecords(string criteria);
    }
}
