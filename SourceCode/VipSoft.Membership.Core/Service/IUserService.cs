// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:28-Jan-2013
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using VipSoft.Membership.Core.Entity;

namespace VipSoft.Membership.Core.Service
{
    public interface IUserService
    {

        /// <summary> 
        /// 新增用户
        /// </summary>
        int AddUser(Users user);

        /// <summary> 
        /// 删除用户
        /// </summary>
        int DeleteUser(int userId);

        /// <summary> 
        /// 更新用户
        /// </summary>
        int UpdateUser(Users user);

        /// <summary> 
        /// 更新密码
        /// </summary>
        int UpdatePwd(int userId);

        /// <summary> 
        /// 更新用户状态
        /// </summary>
        int UpdateUser(int userId);

        /// <summary> 
        /// 显示用户
        /// </summary>
        Users GetUser(int userId);

        /// <summary> 
        /// 显示用户
        /// </summary>
        Users GetUser(Users users);

        /// <summary> 
        /// 显示用户列表
        /// </summary>
        IList<Users> GetUserList(Users users);
    }
}
