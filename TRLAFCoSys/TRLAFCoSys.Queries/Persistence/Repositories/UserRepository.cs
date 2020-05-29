using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context)
            : base(context)
        {
        }
        public IEnumerable<User> GetUsers()
        {
            return DataContext.Users.ToList();
        }
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }


        public User GetUser(string userName)
        {

            return DataContext.Users.Include(u => u.Roles).FirstOrDefault(user => user.UserName == userName);
        }


        public void RemoveUsersPermissions(int userID)
        {

            var a = DataContext.Users.Include(i => i.Roles).Where(u => u.UserID == userID).FirstOrDefault<User>();
            var p = a.Roles.ToList();
            p.ForEach(pe => a.Roles.Remove(pe));
        }


        public User GetUser(int userID)
        {
            return DataContext.Users.Include(i => i.Roles).Where(u => u.UserID == userID).FirstOrDefault<User>();
        }
    }

   
}
