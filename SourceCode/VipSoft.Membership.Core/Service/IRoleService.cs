// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoleervice.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:28-April-2013
// </copyright>

using System.Collections.Generic;
using VipSoft.Membership.Core.Entity;

namespace VipSoft.Membership.Core.Service
{
    public interface IRoleService
    {

        /// <summary> 
        /// 新增角色
        /// </summary>
        int AddRole(Role role);

        /// <summary> 
        /// 删除角色
        /// </summary>
        int DeleteRole(int roleId);

        /// <summary> 
        /// 更新角色
        /// </summary>
        int UpdateRole(Role role);
                                      
        /// <summary> 
        /// 更新角色状态
        /// </summary>
        int UpdateRole(int roleId);

        /// <summary> 
        /// 显示角色
        /// </summary>
        Role GetRole(int roleId);

        /// <summary> 
        /// 显示角色
        /// </summary>
        Role GetRole(Role role);

        /// <summary> 
        /// 显示角色列表
        /// </summary>
        IList<Role> GetRoleList(Role role);
    }
}
