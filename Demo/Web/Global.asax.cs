using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using log4net.Config;

namespace Demo
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Initialize log4net
            XmlConfigurator.Configure();
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            /*routes.MapRoute(
               "Default",
               "{controller}/{action}/{id}",
               new { action = "Index", id = "0" },
               new { controller = @"^\w+", action = @"^\w+", id = @"^\d+" });

            // Archive/2008-05-07
            routes.MapRoute(
                "BlogArchive",
                "Archive/{date}",
                new { controller = "Blog", action = "Archive" },
                new { date = @"^\d{4}-\d{2}-\d{2}" });

            // Car/bmw.abc
            routes.MapRoute(
                "Car",
                "Car/{make}.{model}",
                new { controller = "Car", action = "Index" },
                new { make = @"(acural|bmw)" });

            //必须是提交post url直接回车是get
            routes.MapRoute(
                "Book",
                "Book/Add/{name}",
                new { controller = "Book", action = "Add" },
                new { httpMethod = "POST" });

            //后面所有东西都捕获
            routes.MapRoute(
                "CatchIt",
                "Product/{*values}",
                new { controller = "Product", action = "Index" });*/
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}