using System.Collections.Generic;
using System.Web.Mvc;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;
using VipSoft.Core.Utility;
using Webdiyer.WebControls.Mvc;

namespace VipSoft.CMS.Controllers
{
    public class HomeController : BaseController
    {

        public IArticleService ArticleService = Wac.GetObject("ArticleService") as IArticleService;
        //
        // GET: /Home/

        public ActionResult Index()
        {
            // var articleList = ArticleService.GetArticleList(new Article { CategoryId = 2, Conditaion = "category_id=[CategoryId];" });
            var newsList = GetList(5, 1, 9);
            var productList = GetList(4, 1, 4);
            //var about = ArticleService.GetArticle(new Article() { CategoryId = 11, Conditaion = "category_id=[CategoryId]" });
            var about = ArticleService.GetArticle(new Criteria("CategoryId", 11));
            var msg = ArticleService.GetArticle(new Criteria("CategoryId", 11));
            ViewBag.Msg = msg != null ? msg.Title : "";
            var model = new CmsIndex {Article = about, NewsList = newsList, ProductList = productList};
            return View(model);
        }
        [NonAction]
        private PagedList<Article> GetList(int cid, int page, int pageSize)
        {
            var articleList = ArticleService.GetArticleList(new Criteria("CategoryId", cid));//new Article { CategoryId = cid, Conditaion = "category_id=[CategoryId];" }));
            return articleList.ToPagedList(page, pageSize);
        }
    }

    public class CmsIndex
    {
        public Article Article { get; set; }
        public List<Article> NewsList { get; set; }
        public List<Article> ProductList { get; set; }
        // public List<Article> users { get; set; }
    }

}
