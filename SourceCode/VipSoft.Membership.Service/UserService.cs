using System;
using System.Collections.Generic;
using VipSoft.Membership.Core.Dao;
using VipSoft.Membership.Core.Entity;
using VipSoft.Membership.Core.Service;

namespace VipSoft.Membership.Service
{
    public class UserService : AbstractService, IUserService
    {
        public IUserDao UserDao { get; set; } //= Wac.GetObject("UserDao") as IUserDao;

        public int AddUser(Users user)
        {
            user.CreateDate = DateTime.Now;
            return UserDao.Add(user);
        }

        public int DeleteUser(int userId)
        {
            return UserDao.Delete(userId);
        }

        public int UpdateUser(Users user)
        {
            return UserDao.Update(user);
        }

        public int UpdatePwd(int userId)
        {
            return 1;
        }

        public int UpdateUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Users GetUser(int userId)
        {
            return UserDao.Get(userId);
        }

        public Users GetUser(Users users)
        {
            return UserDao.Get(users);
        }

        public IList<Users> GetUserList(Users users)
        {
            return UserDao.GetList(users);
        }
    }
}
