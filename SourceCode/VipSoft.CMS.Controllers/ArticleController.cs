using System.Collections.Generic;
using System.Web.Mvc;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;
using VipSoft.Core.Utility;
using Webdiyer.WebControls.Mvc;

namespace VipSoft.CMS.Controllers
{
    public class ArticleController : BaseController
    {
        public IArticleService ArticleService = Wac.GetObject("ArticleService") as IArticleService;



        //   
        public ActionResult News(int cid = 0, int page = 1)
        {
            var pageList = GetList(cid, page, 12);
            return View(pageList);
        }

        public ActionResult TopList(int cid = 0, int page = 1, int pagesize = 5)
        {
            ViewBag.CId = cid;
            PagedList<Article> pageList = GetList(cid, page, pagesize);     
            return View(pageList);
        }

        public ActionResult Blogroll(int cid = 0)
        {
           //var list = ArticleService.GetArticleList(new Article { CategoryId = cid, Conditaion = "category_id=[CategoryId];" });
            var list = ArticleService.GetArticleList(new Criteria("CategoryId", cid));
           return View(list);
        }


        public ActionResult List(int cid = 0, int page = 1)
        {                  
            var pageList = GetList(cid, page, 12);
            return View(pageList);
        }


        public virtual ActionResult Content(int id = 0, int cid = 0)
        {
            //var article = id == 0 ? new Article { CategoryId = cid, Conditaion = "category_id=[CategoryId]" } : new Article { ID = id, Conditaion = "id=[ID]" };
            var criteria = id == 0 ? new Criteria("CategoryId", cid) : new Criteria("ID", id);
            var model = ArticleService.GetArticle(criteria);
            // ViewBag.Content = model.Content;
           // ViewBag.IsContent = ",1,7".IndexOf(CIdStr) > 0;
            //ViewData.Add("model",model);
            ViewBag.CategoryName = CurrentCategory.Name;
            return View(model??new Article());
        }
                 
        public ActionResult Pic(int cid = 0, int page = 1)
        {
            var pageList = GetList(cid, page, 9);
            return View(pageList);
        }

        public ActionResult Products(int cid = 0, int page = 1)
        {
           
            return View();
        }

        [NonAction]
        private PagedList<Article> GetList(int cid, int page, int pageSize)
        {   
            ViewBag.CategoryName = CurrentCategory.Name;

            var childIds = CategoryService.GetChildIds(cid);
            var articleList = ArticleService.GetArticleList(childIds);
           // var articleList = ArticleService.GetArticleList(new Article { CategoryId = cid, Conditaion = "category_id=[CategoryId];" });
            //var articleList = ArticleService.GetArticleList(new Criteria("category_id", cid));
            return articleList.ToPagedList(page, pageSize);
        }
    }
}
