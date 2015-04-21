using System;
using System.Web.Mvc;
using Spring.Context;
using Spring.Context.Support;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;

namespace VipSoft.CMS.Controllers
{
    public class BaseController : Controller
    {

        protected static readonly IApplicationContext Wac = ContextRegistry.GetContext();

        protected ICategoryService CategoryService = Wac.GetObject("CategoryService") as ICategoryService;


        //类别ID
        public string CIdStr
        {
            get { return Request.RequestContext.RouteData.Values["cid"].ToString(); }
        }

        //类别ID
        public int CId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(CIdStr);
                }
                catch
                {
                    return -1;
                }
            }
        }

        //得到当前的类别对象      
        //需要缓存。
        protected Category CurrentCategory
        {

            get
            {
                try
                {
                    return CategoryService.Get(Convert.ToInt32(CIdStr));
                }
                catch
                {
                    return new Category();
                }
            }
        }
    }
}