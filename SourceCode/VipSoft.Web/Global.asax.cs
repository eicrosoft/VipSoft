﻿using System.Web.Mvc;
using System.Web.Routing;
using log4net.Config;

namespace VipSoft.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcVipSoft : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "MapCategory", // Route name
                "{controller}/{action}/{cid}", // URL with parameters
                //new { controller = "Home", action = "Index", cid = UrlParameter.Optional }, // Parameter defaults
                new string[] { "VipSoft.CMS.Controllers" }
            );

            routes.MapRoute(
               "Article", // Route name
               "{controller}/{action}/{cid}/{id}", // URL with parameters
               new { controller = "Home", action = "Index", cid = UrlParameter.Optional, id = UrlParameter.Optional }, // Parameter defaults
               new string[] { "VipSoft.CMS.Controllers" }
           );



            routes.MapRoute(
                "Menu", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index" }, // Parameter defaults
                new string[] { "VipSoft.CMS.Controllers" }
            );

        }

        protected void Application_Start()
        {
            // Initialize log4net
            XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}