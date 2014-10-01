using System.Text;
using System.Web.Mvc;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;
using VipSoft.Core.Utility;

namespace VipSoft.CMS.Controllers
{
    public class CategoryController : BaseController
    {
        public IArticleService ArticleService = Wac.GetObject("ArticleService") as IArticleService;


        /// <summary>
        /// 不知道为什么不能自动注入了,待解决。
        /// </summary>                                                           
        //public ICategoryService CategoryService = Wac.GetObject("CategoryService") as ICategoryService;


        //public ICategoryService CategoryService { get; set; }

        //
        // GET: /Category/

        public ActionResult List()
        {
            //var list = CategoryService.GetCategoryList(new Category { ParentID = 0, Conditaion = "parent_id=[ParentID];" });
            var list = CategoryService.GetCategoryList();
            return View(list);
        }

        public ActionResult MenuList()
        {                                                                                                                      
           // var list = CategoryService.GetCategoryList(new Category { Status = 1, Conditaion = "status=[Status];" });
            var list = CategoryService.GetCategoryList(new Criteria("Status", 1), new Order("Sequence"));
            return View(list);
        }

        /// <summary>
        /// 左侧导航
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeMenu()
        {
            var sbCategory = new StringBuilder();
            CategoryTreeJson(sbCategory, 4, false);
            ViewBag.Category = sbCategory.ToString();
            ViewBag.Contact = ArticleService.GetArticle(new Criteria("CategoryId", 15)).Content;
            return View();   
        }

        /// <summary>
        /// 如果有子分类就显示子分类，如果没有就显示同级分类
        /// </summary>
        /// <returns></returns>
        public ActionResult NavigationMenu()
        {            
            ViewBag.CurrentCategory = CurrentCategory;
            // var model = new Category { ID = CId, Conditaion = "parent_id=[ID]" };   
            var criteria = new Criteria("ParentID", CId);
            criteria.Add(LOP.AND, "Status", 1);
            var list = CategoryService.GetCategoryList(criteria);
            if (list.Count == 0)  //有子类就显示子类，没有子类就显示同级节点。
            {
                list = CategoryService.GetBrotherNode(CId);
            }
            return View(list);
        }

        /// <summary>
        ///  得到分类
        /// </summary>
        /// <param name="sbCategory">要返回的结果，格式为<li><a href="#">新书教育书架</a>  <ul><li><a href="#">分类一</a></li><li><a href="#">分类二</a></li></ul></li></param>
        ///  <param name="parentId">父级的ID</param>
        /// <param name="haveChild">是否有子节点</param>
        public void CategoryTreeJson(StringBuilder sbCategory, int parentId, bool haveChild)
        {
            var list = CategoryService.GetCategoryList().FindAll(p => p.ParentId == parentId);
            if (haveChild && list.Count > 0)
            {
                sbCategory.Append("<ul>");
            }
            foreach (var item in list)
            {
                sbCategory.AppendFormat("<li><a href='/Article/Pic/{1}'>{0}</a>", item.Name,item.ID);
                CategoryTreeJson(sbCategory, item.ID, true);
                sbCategory.Append("</li>");
            }
            if (haveChild && list.Count > 0)
            {
                sbCategory.Append("</ul>");
            }
        }



        public ActionResult SiteMapPath()
        {
            ViewBag.CurrentCategory = CurrentCategory;
            var list = CategoryService.GetAncestorNote(CurrentCategory);
            return View(list);
        }

        //
        // GET: /Add/ 

        public ActionResult Add(int id = 0)
        {
            var model = new Category();
            var list = CategoryService.GetCategoryList();
            ViewData["Parent"] = new SelectList(list, "ID", "Name");
            if (id > 0)
            {
                model = CategoryService.GetCategory(id);
            }
            return View(model);
        }

        //Save data
        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.ID != 0)
                {
                    CategoryService.UpdateCategory(category);
                }
                else
                {
                    CategoryService.AddCategory(category);
                }
                return RedirectToAction("List");
            }
            return View(category);
        }

        //Delete data  View里面如何实现
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CategoryService.DeleteCategory(id);
            return RedirectToAction("List");
        }

    }
}
