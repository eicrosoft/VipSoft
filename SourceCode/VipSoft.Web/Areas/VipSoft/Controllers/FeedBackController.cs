// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedBackController.cs" company="VipSoft.com.cn">
//    Author:Wang,Haifeng
//        QQ:739292350
//     Email:wanghaifeng@VipSoft.com.cn
//    Create:21-Feb-2013
// </copyright>

using System.Web.Mvc;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;
using Webdiyer.WebControls.Mvc;



namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    public class FeedBackController : BaseController
    {
        public static IFeedBackService FeedBackService = Wac.GetObject("FeedBackService") as IFeedBackService ;
        //
        // GET: /Admin/FeedBack/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List( int id=0,int? page=1)
        {
            //int ids = 2;
           int   pageindex=1;
            var feedbackList = FeedBackService.GetList();
            if (page.HasValue==true)
            {

                pageindex =(int)page; 
            }
            ViewBag.SubTitle = GetCurrentMenu.Name;
            PagedList<Feedback> pageList = feedbackList.ToPagedList(pageindex, 8);
            return View(pageList);
        }
        //
        // GET: /Admin/FeedBack/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/FeedBack/Create

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(int id = 0)
        {
            ViewBag.SubTitle = GetCurrentMenu.Name;
            return View("add");
        }
        //Save data
        [HttpPost]
        public ActionResult Add(Feedback feedback)
        {

            if (ModelState.IsValid)
            {
                var flag = FeedBackService.Add(feedback);

                if (flag != 0)
                {
                   // Response.Redirect("<script>alert('增加成功')</script>");
                }
                return RedirectToAction("List");
            }

            return View();
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
