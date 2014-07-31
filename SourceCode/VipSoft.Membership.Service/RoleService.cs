using System;
using System.Collections.Generic;
using VipSoft.Membership.Core.Dao;
using VipSoft.Membership.Core.Entity;
using VipSoft.Membership.Core.Service;

namespace VipSoft.Membership.Service
{
    public class RoleService : AbstractService, IRoleService
    {
        public IRoleDao RoleDao { get; set; } //= Wac.GetObject("UserDao") as IUserDao;

        public int AddRole(Role role)
        {
            return RoleDao.Add(role);
        }

        public int DeleteRole(int roleId)
        {
            return RoleDao.Delete(roleId);
        }

        public int UpdateRole(Role role)
        {
            return RoleDao.Update(role);
        }
                    

        public int UpdateRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(int roleId)
        {
            return RoleDao.Get(roleId);
        }

        public Role GetRole(Role role)
        {
            throw new NotImplementedException();
        }

        public IList<Role> GetRoleList(Role role)
        {
            return RoleDao.GetList(role);
        }
    }
}
