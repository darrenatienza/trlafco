using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> GetPermissions();
        /// <summary>
        /// Get Users Permission as Many to Many relationship
        /// </summary>
        /// <param name="userID">ID of the User</param>
        /// <returns></returns>
        IEnumerable<Role> GetPermissionsByUserID(int userID);
        IEnumerable<Role> GetAvailablePermission(IEnumerable<Role> permissions);

        IEnumerable<Role> GetAvailablePermission(string[] permissionIDs);

        IEnumerable<Role> GetValidPermissionsList(IEnumerable<Role> permissionList);
    }
}
