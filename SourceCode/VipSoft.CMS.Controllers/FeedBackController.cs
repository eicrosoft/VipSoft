// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedBackController.cs" company="VipSoft.com.cn">
//    Author:Wang,Haifeng
//        QQ:739292350
//     Email:wanghaifeng@VipSoft.com.cn
//    Create:21-Feb-2013
// </copyright>

using System;
using System.Web.Mvc;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;

namespace VipSoft.CMS.Controllers
{
    public class FeedBackController : BaseController
    {
        public static IFeedBackService FeedBackService = Wac.GetObject("FeedBackService") as IFeedBackService;

        public ActionResult Add(int id = 0)
        {
            ViewBag.CategoryName = CurrentCategory.Name;
            return View("add");
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(Feedback feedback, FormCollection formCollection)
        {   
            var result = 0;
            feedback.CreateDate = DateTime.Now;
            var flag = FeedBackService.Add(feedback) > 0;          
            if (flag)
            {
                result = 1;
            }         
            // return "aa";
            return Json(result);
        }

        //
        // POST: /Admin/FeedBack/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/FeedBack/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/FeedBack/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: /Admin/FeedBack/Delete/5


        [HttpPost]
        public ActionResult Delete(int id)
        {
            FeedBackService.Delete(id);
            return RedirectToAction("List");
        }

        //
        // POST: /Admin/FeedBack/Delete/5


    }
}
