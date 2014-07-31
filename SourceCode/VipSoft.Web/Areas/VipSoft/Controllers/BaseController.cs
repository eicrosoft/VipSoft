using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Spring.Context;
using Spring.Context.Support;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;
using VipSoft.Membership.Core.Entity;
using VipSoft.Membership.Core.Service;
using VipSoft.WebUtility;

namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    public class BaseController : Controller
    {
        public string AreaName
        {
            get
            {
                return "VipSoft";
            }
        }

        public string ControllerName
        {
            get
            {
                return Request.RequestContext.RouteData.Values["controller"].ToString();
            }
        }

        public string ActionName
        {
            get
            {
                return Request.RequestContext.RouteData.Values["action"].ToString();
            }
        }

        //菜单ID
        public string MId
        {
            get
            {
                var cid = Request.RequestContext.RouteData.Values["mid"];
                return (cid ?? "-1").ToString();
            }
        }

        //类别ID
        public string CId
        {
            get
            {
                var cid = Request.RequestContext.RouteData.Values["cid"];
                return (cid ?? "-1").ToString();
            }
        }

        //详细Id
        public string Id
        {
            get
            {
                var cid = Request.RequestContext.RouteData.Values["id"];
                return (cid ?? "-1").ToString();
            }
        }

        public int MIdToInt
        {
            get
            {
                var mid = -1;
                try { mid = Convert.ToInt32(MId); }
                catch
                { }
                return mid;
            }
        }     
        public int CIdToInt
        {
            get
            {
                var cid = -1;
                try { cid = Convert.ToInt32(CId); }
                catch
                { }
                return cid;
            }
        }       
        public int IdToInt
        {
            get
            {
                var id = -1;
                try { id = Convert.ToInt32(Id); }
                catch
                { }
                return id;
            }
        }

        public int PageSize = 15;

        protected static readonly IApplicationContext Wac = ContextRegistry.GetContext();
        
        public ICategoryService CategoryService = Wac.GetObject("CategoryService") as ICategoryService;
        protected List<Category> CategoryList { get { return CategoryService.GetCategoryList(); } }

        //方法待改造和迁移
        public void CategoryTreeBuild(IList<Category> result, int parentId, int spaceCount)
        {
            var str = "";
            if (spaceCount > 0)
            {
                str = "├";
            }
            str = string.Format("{0}{1}", WebHandler.SpaceString(spaceCount), str);
            var list = CategoryList.FindAll(p => p.ParentId == parentId);
            foreach (var item in list)
            {
                item.Name = str + item.Name;
                result.Add(item);
                CategoryTreeBuild(result, item.ID, item.Depth);
            }
        }
        private IMenuService MenuService = Wac.GetObject("MenuService") as IMenuService;
        public Menu GetCurrentMenu
        {
            get { return MenuService.GetMenu(new Menu() { ID = Convert.ToInt32(MId), Conditaion = "id=[ID]" }); }
        }
    }
}