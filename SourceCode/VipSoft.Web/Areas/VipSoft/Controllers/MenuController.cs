using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VipSoft.Membership.Core.Entity;
using VipSoft.Membership.Core.Service;

namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    public class MenuController : BaseController
    {
        //
        // GET: /Admin/Menu/                                                                                  
        public IMenuService MenuService = Wac.GetObject("MenuService") as IMenuService;

        public ActionResult AdminMenu()
        {

            var list = MenuService.GetMenuList(new Menu() { Status = 1, Conditaion = "Status=[Status]", OrderBy = "ORDER BY sequence,id" });
            ViewData["AdminMenu"] = list;
            ViewBag.AreaName = AreaName;
            return View(list.ToList());
        }

        public ActionResult List(int cid = 0)
        {
            IList<Menu> list = new List<Menu>();
            MenuTreeTable(list, cid, 0); 

            //var list = CategoryService.GetCategoryList(new Category { ParentId = cid, Conditaion = "parent_id=[ParentId];" });

            return View(list); 
        }


        //方法待改造和迁移
        public void MenuTreeTable(IList<Menu> result, int parentId, int depth)
        {
            List<Menu> menuList = MenuService.GetMenuList(new Menu()).ToList();
            var list = menuList.FindAll(p => p.ParentId == parentId);
            foreach (var item in list)
            {
                var parentDsc = "treegrid-" + item.ID;
                item.DepthDescription = depth == 0 ? parentDsc : parentDsc + " treegrid-parent-" + item.ParentId;
                result.Add(item);
                MenuTreeTable(result, item.ID, item.Depth);
            }
        }
    }
}
