using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers();

        User GetUser(string userName);

        User GetUser(int userID);
        void RemoveUsersPermissions(int userID);
    }
}
