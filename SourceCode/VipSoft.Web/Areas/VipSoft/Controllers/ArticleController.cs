// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleController.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:25-Dec-2012
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Enum;
using VipSoft.CMS.Core.Service;
using VipSoft.Membership.Core.Entity;
using VipSoft.Web.Ajax;
using Webdiyer.WebControls.Mvc;

namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    public class ArticleController : BaseController
    {

        #region Private
        private IArticleService ArticleService = Wac.GetObject("ArticleService") as IArticleService;

        //public IMenuService MenuService { get; set; }

        private string ContentTemplate = ",1,5,6,7";
        //private string ContentTemplate = "36";

        private string GetHref(string action)
        {
            return string.Format("/{0}/{1}/{2}/{3}/{4}", AreaName, ControllerName, action, MId, CId);
        }

        private void EditValueBind(Article model)
        {
            Session["file_info"] = model.FileName;
            Session["db_file"] = model.FileName;
            ViewBag.Picture = "<img src=\"" + model.FileName + "\" />";
            ViewBag.Count = model.Content;
        }

        private void CategoryBuild()
        {
            IList<Category> list = new List<Category>();
            CategoryTreeBuild(list, CIdToInt, 0);
            ViewData["Category"] = new SelectList(list, "ID", "Name");
        }



        #region SaveFile

        private string filePath = "/Uploads/";
        private bool isSaveFilePath = true;

        private void SavePicture(Article article)
        {
            if (Session["file_info"] != null)
            {
                List<Thumbnail> thumbnails = Session["file_info"] as List<Thumbnail>;

                string UploadPath = Server.MapPath(filePath);

                var fileName = "";
                var orginalImge = Session["OriginalImage"] as HttpPostedFile;
                if (orginalImge != null)
                {
                    var extension = Path.GetExtension(orginalImge.FileName); 
                    foreach (Thumbnail img in thumbnails)
                    {
                        fileName = img.ID + extension;
                        orginalImge.SaveAs(Path.Combine(UploadPath, fileName));
                        FileStream fs = new FileStream(UploadPath + "s_" + fileName, FileMode.Create);
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(img.Data);
                        bw.Close();
                        fs.Close();
                    }
                }
                article.FileName = isSaveFilePath ? (filePath + fileName) : fileName;
                //Session.Remove("file_info");
            }
        }
        #endregion


        #endregion

        #region View

        public ActionResult Add(int mid = 0, int cid = -1, int id = 0)
        {
            var articleDto = new ArticleDto { Menu = GetCurrentMenu };
            ViewData["BackHref"] = GetHref("List");
            ViewBag.IsNews = !ContentTemplate.Contains(cid.ToString());
            ViewBag.IsSchool = false;// cid == 33;
            ViewBag.Teacher = false;//"18,19".Contains(cid.ToString());
            ViewBag.SubTitle = articleDto.Menu.Name;
            Session.Remove("file_info");
            Article model = null;
            CategoryBuild();
            if (id > 0)
            {
                model = ArticleService.Get(id);
                EditValueBind(model);
            }
            else if (articleDto.Menu.CategoryType == Convert.ToInt32(CategoryType.Content))
            {
                model = ArticleService.Get(new Article() { CategoryId = cid, Conditaion = "category_id=[CategoryId]" });
                EditValueBind(model);
            }

            articleDto.Article = model;
            return View(articleDto);
        }

        //
        // GET: /Article/    
        public ActionResult List(FormCollection formCollection, int cid = 0, int page = 1)
        {
            var category = formCollection.Get("Category");
            var keywords = formCollection.Get("txtKeywords");
            var childIds = CategoryService.GetChildIds(cid);
            var articleList = ArticleService.GetArticleList(childIds);
            ViewBag.SubTitle = GetCurrentMenu.Name;
            if (!string.IsNullOrEmpty(category))
            { 
                articleList = articleList.Where(c => c.CategoryId == Convert.ToInt32(category)).ToList();
            }
            if (!string.IsNullOrEmpty(keywords))
            { 
                articleList = articleList.Where(c => c.Title.Contains(keywords)).ToList();
            }

            var pageList = articleList.ToPagedList(page, PageSize);
            CategoryBuild(); 
            return View(articleList);
        }

        #endregion

        #region Action

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(Article article, FormCollection formCollection)
        {
            int result = 0;
            //if (ModelState.IsValid)
            //{
            bool flag;

            article.CategoryId = article.CategoryId == 0 ? Convert.ToInt32(formCollection.Get("CategoryId")) : article.CategoryId;
            article.ID = !string.IsNullOrEmpty(formCollection.Get("id")) ? Convert.ToInt32(formCollection.Get("id")) : article.ID;

            article.UpdateDate = DateTime.Now;
            if (Session["file_info"] != null)
            {
                List<Thumbnail> thumbnails = Session["file_info"] as List<Thumbnail>;
                if (thumbnails != null && thumbnails.Count > 0)
                {
                    article.FileName = thumbnails[0].ID + ".jpg";
                }
            }

            if (Session["db_file"] != Session["file_info"])
            {
                SavePicture(article);
            }
            if (article.ID != 0)
            {
                article.CreateDate = DateTime.Now;
            }
            flag = (article.ID != 0 ? ArticleService.Update(article) : ArticleService.Add(article)) > 0;
            if (flag)
            {
                //if (ContentTemplate.Contains(categoryId.ToString()))
                //{
                TempData["Article"] = article;
                //  return RedirectToAction("Add", new { id = article.ID, cid = categoryId });
                result = 1;

                //}
                // RedirectToAction("List", new { cid = CId });
            }
            // }
            //CategoryBuild();
            //ViewBag.Picture = "<img src=\"" + filePath + Session["file_info"] + "\" />";
            // View(article);
            // return "aa";
            return Json(result);
        } 
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = ArticleService.Delete(id);
            return Json(result);
        }


        #endregion
         
    }

    public class ArticleDto
    {
        public Article Article { get; set; }
        public Menu Menu { get; set; }


        /// <summary>
        /// 是否显示该属性
        /// </summary>
        /// <param name="articleElement">界面元素</param>
        /// <returns>是/否</returns>
        public bool IsShowElement(ArticleElement articleElement)
        {
            var val = string.Format(",{0},", Convert.ToInt32(articleElement));
            return Menu.HtmlType.Contains(val);
        }

    }


}
