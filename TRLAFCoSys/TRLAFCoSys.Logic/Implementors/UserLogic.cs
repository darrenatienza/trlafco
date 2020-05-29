using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Persistence;

namespace TRLAFCoSys.Logic.Implementors
{
    public class UserLogic : IUserLogic
    {
        public UserLogic() { }

        public void Add(UserModel model)
        {
            if (model.Password != model.ReTypePassword)
            {
                throw new ApplicationException("Password does not match!");
            }
            var crypto = new SimpleCrypto.PBKDF2();
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var user = new User();
                var encrypPass = crypto.Compute(model.Password);
                user.FirstName = "";
                user.MiddleName = "";
                user.LastName = "";
                user.UserName = model.UserName;
                user.Password = encrypPass;
                user.PasswordSalt = crypto.Salt;
                uow.Users.Add(user);
                uow.Complete();
            }
        }

        public void Edit(int id, UserModel model)
        {
            if (model.Password != model.ReTypePassword)
            {
                throw new ApplicationException("Password does not match!");
            }
            var crypto = new SimpleCrypto.PBKDF2();
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var user = uow.Users.Get(id);
                var encrypPass = crypto.Compute(model.Password);

                user.Password = encrypPass;
                user.PasswordSalt = crypto.Salt;
                uow.Users.Edit(user);
                uow.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Users.Get(id);
                uow.Users.Remove(obj);
                uow.Complete();
            }
        }
        public void LoginUser(string userName, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            using (var uow = new UnitOfWork(new DataContext()))
            {
                User user = uow.Users.GetUser(userName);
                if (user != null)
                {
                    if (user.Password != crypto.Compute(password, user.PasswordSalt))
                    {
                        throw new ApplicationException("Invalid Password!");
                    }
                    else
                    {
                        CurrentUser.UserID = user.UserID;
                        CurrentUser.UserName = user.UserName;

                    }
                }
                else
                {
                    throw new ApplicationException("Invalid User Name!");
                }
            }
        }


        public UserModel Get(int userID)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Users.Get(userID);
                var model = new UserModel();
                model.ID = obj.UserID;
                model.UserName = obj.UserName;
                return model;
            }
        }


        public List<UserModel> GetAll()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var models = new List<UserModel>();
                var objs = uow.Users.GetAll();
                foreach (var item in objs)
                {
                    var model = new UserModel();
                    model.ID = item.UserID;
                    model.UserName = item.UserName;
                    models.Add(model);
                }
                return models;
            }
        }
    }
}
