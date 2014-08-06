// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:28-April-2013
// </copyright>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VipSoft.Membership.Core.Entity;
using VipSoft.Membership.Core.Service;
using Webdiyer.WebControls.Mvc;

namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    public class RoleController :BaseController
    {
        private static IRoleService RoleService = Wac.GetObject("RoleService") as IRoleService;

        private IMenuService MenuService = Wac.GetObject("MenuService") as IMenuService;

        #region View
      
        //
        // GET: /VipSoft/Role/

        public ActionResult Add(int id = 0)
        {

            Role model = null;
            if (id > 0)
            {
                model = RoleService.GetRole(id);
            }
            return View(model);
        }
        
        public ActionResult List(int page=1)
        {
            PagedList<Role> list = RoleService.GetRoleList(new Role()).ToPagedList(page, PageSize);
            return View(list);
        }


        public ActionResult RoleAccess(int page = 1)
        {                                                                                            
            var  roleDto=new RoleDto
                             {
                                 RoleList = RoleService.GetRoleList(new Role()),
                                 MenuList = MenuService.GetMenuList(new Menu())
                             };

            return View(roleDto);
        }

        #endregion


        #region Action

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveRole(Role role)
        {
            var result = 0;                  
            var flag = (role.ID != 0 ? RoleService.UpdateRole(role) : RoleService.AddRole(role)) > 0;
            if (flag)
            {
                result = 1;
                //RedirectToAction("List");
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveRoleAccess(Role role, FormCollection formCollection)
        {
            var result = 0;

            formCollection.Get("chkAccess");
            //var flag = (role.ID != 0 ? RoleService.UpdateRole(role) : RoleService.AddRole(role)) > 0;
            //if (flag)
            //{
            //    result = 1;
            //    RedirectToAction("List");
            //}
            return Json(result);
        }

         
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result=RoleService.DeleteRole(id);
            //return RedirectToAction("List");
            return Json(result);
        }

        #endregion

    }



     public class RoleDto
     {
         public IList<Role> RoleList { get; set; }
         public IList<Menu> MenuList { get; set; }
     }
}
