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
    public interface IMenuService
    {      
        /// <summary> 
        /// 显示菜单
        /// </summary>
        Menu GetMenu(Menu menu);

        /// <summary> 
        /// 显示菜单列表
        /// </summary>
        IList<Menu> GetMenuList(Menu menu);
    }
}
