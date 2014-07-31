using System.Web.Mvc;

namespace VipSoft.Web.Areas.VipSoft
{
    public class VipSoftAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "VipSoft";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
           
            context.MapRoute(
                "Admin_Menu",
                AreaName + "/{controller}/{action}/{mid}/{cid}/{id}",
                new { controller = "Main", action = "Index", mid = UrlParameter.Optional, cid = UrlParameter.Optional, id = UrlParameter.Optional },
                new string[] { "VipSoft.Web.Areas.VipSoft.Controllers" }
            );

            context.MapRoute(
               "Admin_default",
               AreaName + "/{controller}/{action}/{cid}",
               new { controller = "Main", action = "Index", cid = UrlParameter.Optional },
               new string[] { "VipSoft.Web.Areas.VipSoft.Controllers" }
           );
        }
    }
}
