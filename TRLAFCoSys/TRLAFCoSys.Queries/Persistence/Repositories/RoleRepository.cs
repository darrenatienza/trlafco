using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext context)
            :base(context)
        {

        }
        public IEnumerable<Role> GetPermissions()
        {
            return DataContext.Roles.ToList();
        }
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }


        public IEnumerable<Role> GetPermissionsByUserID(int userID)
        {
            return DataContext.Roles
                .Where(u => u.Users.Any(p => p.UserID == userID));
        }


        public IEnumerable<Role> GetAvailablePermission(IEnumerable<Role> excludePermissions)
        {
            //StringBuilder excludedIDs = new StringBuilder();
            //foreach (Permission item in excludePermissions)
            //{
            //    excludedIDs.AppendFormat("{0},", item.PermissionID);
            //}
            //string[] excludeIDArray = excludedIDs.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var exPermissions = excludePermissions.Select(ex => ex.RoleID).ToList();
            return DataContext.Roles
               .Where(p => !exPermissions.Any(ex => ex == p.RoleID));
        }


        public IEnumerable<Role> GetAvailablePermission(string[] permissionIDs)
        {
            return DataContext.Roles
               .Where(p => !permissionIDs.Any(p1 => p1 == p.RoleID.ToString()));
        }


        public IEnumerable<Role> GetValidPermissionsList(IEnumerable<Role> permissionList)
        {
            List<Role> newPermissions = new List<Role>();
            permissionList.ToList().ForEach(p => newPermissions.Add(DataContext.Roles.Find(p.RoleID)));
            return newPermissions;
        }
    }
}
