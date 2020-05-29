using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IUserLogic : IBaseLogic<UserModel>
    {
        void LoginUser(string userName, string password);

        UserModel Get(int userID);

        List<UserModel> GetAll();
    }
}
