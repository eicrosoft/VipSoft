// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:16-April-2013
// </copyright>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VipSoft.Membership.Core.Entity;
using VipSoft.Membership.Core.Service;
using Webdiyer.WebControls.Mvc;

namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    public class UserController : BaseController
    {
        private static IUserService UserService = Wac.GetObject("UserService") as IUserService;

        #region View

        public ActionResult Add(int id = 0)
        {
            Users model = null;
            if (id > 0)
            {
                model = UserService.GetUser(id);
            }
            return View(model);
        }

        public ActionResult List(int page = 1)
        {
            PagedList<Users> list = UserService.GetUserList(new Users()).ToPagedList(page, PageSize);
            return View(list);
        }

        public ActionResult Login(int id = 0, int cid = -1)
        {
            Session["UserName"] = null;
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        #endregion

        #region Action


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(Users user)
        {
            user.Conditaion = "user_name=[UserName];AND Password = [Password];";
            var model = UserService.GetUser(user);
            if (model != null)
            {
                Session["UserName"] = model.UserName;
                return RedirectToAction("Index", "Main");
            }
            ModelState.AddModelError("", "用户名或密码错误！");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveUser(Users user)
        {
            var result = 0;
            user.UpdateDate = DateTime.Now;
            var flag = (user.ID != 0 ? UserService.UpdateUser(user) : UserService.AddUser(user)) > 0;
            if (flag)
            {
                result = 1;
                //RedirectToAction("List");
            }
            return Json(result);
        }
         
        [HttpPost]
        public ActionResult Delete(int id=0)
        {
            var result = UserService.DeleteUser(id);
            return Json(result);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangePassword(FormCollection collection)
        {
            var oldPassword = collection["OldPassword"];
            var password = collection["Password"];
            var password1 = collection["Password1"];
            if (password != password1)
            {
                ModelState.AddModelError("", "两次密码不统一");
                return View();
            }
            var user = new Users
                           {
                               UserName = "admin",
                               Password = oldPassword,
                               Conditaion = "user_name=[UserName];AND Password = [Password];"
                           };
            var userlist = UserService.GetUserList(user);
            if (userlist.Count > 0)
            {
                user.ID = userlist[0].ID;
                user.Conditaion = null;
                user.Password = password;
                UserService.UpdateUser(user);  
                ModelState.AddModelError("", "密码修改成功");
            }
            else
            {
                ModelState.AddModelError("", "旧密码错误");
            }
            return View();
        }

        public ActionResult Profile()
        { 
            return View();
        }
        #endregion
        ////
        //// GET: /Admin/User/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////
        //// GET: /Admin/User/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Admin/User/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        ////
        //// POST: /Admin/User/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Admin/User/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Admin/User/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Admin/User/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Admin/User/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
