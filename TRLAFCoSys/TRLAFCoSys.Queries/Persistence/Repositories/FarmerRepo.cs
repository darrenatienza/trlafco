using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class FarmerRepo : Repository<Farmer>, IFarmerRepo
    {
        public FarmerRepo(DataContext context)
            :base(context)
        {

        }
       
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }




        public IEnumerable<Farmer> GetAllRecords(string criteria)
        {
            return DataContext.Farmers.Where(r => r.FullName.Contains(criteria) || r.Address.Contains(criteria) || r.PhoneNumber.Contains(criteria));
        }
    }
}
