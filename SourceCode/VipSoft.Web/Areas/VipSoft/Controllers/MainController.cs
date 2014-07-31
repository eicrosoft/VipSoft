using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            if(Session["UserName"]==null)
            {
                //return RedirectToAction("Login", "User");   
            }            
            return View();
        }

        public ActionResult Loginout()
        {
        
            Session["UserName"]=null;
            Response.Write("<script>alert('退出成功')</script>");

            return RedirectToAction("Login", "User"); 
        }

    }
}
