// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryController.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:15-Dec-2012
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using VipSoft.CMS.Core.Entity;

namespace VipSoft.Web.Areas.VipSoft.Controllers
{
    /// <summary>
    /// mid=MenuID,cid=ParentID,id=CatetoryId
    /// </summary>
    public class CategoryController : BaseController
    {
        private bool IsModify { get { return Request["act"] == "2"; } }
        //
        // GET: /Category/

        public ActionResult List(int cid=0)
        {
            IList<Category> list = new List<Category>();
            CategoryTreeTable(list, cid, 0);
            ViewData["Category"] = new SelectList(list, "ID", "Name");
            ViewBag.SubTitle = GetCurrentMenu.Name;
            //var list = CategoryService.GetCategoryList(new Category { ParentId = cid, Conditaion = "parent_id=[ParentId];" });
            
            return View(list);
        }


        //方法待改造和迁移
        public void CategoryTreeTable(IList<Category> result, int parentId, int depth)
        {
            var list = CategoryList.FindAll(p => p.ParentId == parentId);
            foreach (var item in list)
            {
                var parentDsc = "treegrid-" + item.ID;
                item.DepthDescription = depth == 0 ? parentDsc : parentDsc + " treegrid-parent-" + item.ParentId;
                result.Add(item);
                CategoryTreeTable(result, item.ID, item.Depth);
            }
        }


        public JsonResult CategoryListJson()
        {
            var sbCategory = new StringBuilder("{Rows:[");
            CategoryTreeJson(sbCategory, CIdToInt, false);
            sbCategory.Append("]}");
            Object obj = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(sbCategory.ToString());
            return Json(obj);
        }

        /// <summary>
        ///  得到分类列表的JSON数据格式
        /// </summary>
        /// <param name="sbCategory">要返回的结果，格式为：{"Rows":[{"ID":"9","Name":"公司新闻","ParentId":"3","children":[{"ID":"13","Name":"AAA","ParentId":"9"}]},{"ID":"10","Name":"行业新闻","ParentId":"3"}]}</param>
        /// <param name="parentId">父级的ID</param>
        /// <param name="haveChild">是否有子节点</param>
        public void CategoryTreeJson(StringBuilder sbCategory, int parentId, bool haveChild)
        {
            var list = CategoryService.GetCategoryList().FindAll(p => p.ParentId == parentId);
            if (haveChild && list.Count > 0)
            {
                sbCategory.Append("children: [");
            }
            foreach (var item in list)
            {
                sbCategory.AppendFormat("{{ ID: \"{0}\", Name: \"{1}\", Status: \"{2}\",", item.ID, item.Name, item.Status);
                CategoryTreeJson(sbCategory, item.ID, true);
                sbCategory.AppendFormat("}},");
            }
            if (haveChild && list.Count > 0)
            {
                sbCategory.Append("]");
            }
        }


        /// <summary>
        /// 进入添加页面
        /// </summary>                        
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(int id = 0)
        {
            var model = new Category();
            CategoryBuild();
            if (id > 0 && IsModify)
            {
                model = CategoryService.GetCategory(id);
            }
            else
            {
                model.ParentId = id;             //添加子分类时，当前的ID作为 ParentId 绑定 DropDownlist 用 ,修改的时候还是用的ParentID
            }
            ViewBag.SubTitle = GetCurrentMenu.Name;
            return View(model);
        }

        private void CategoryBuild()
        {
            IList<Category> list = new List<Category>();
            CategoryTreeBuild(list, CIdToInt, 0);
            ViewData["Category"] = new SelectList(list, "ID", "Name");
        }

        //Save data
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(FormCollection formCollection,Category category)
        {
            int result;             
            if(category.ParentId == 0)
            {
                category.ParentId = Convert.ToInt32(formCollection.Get("ParentId").Trim(','));
            }
            var parentCategory = CategoryService.GetCategory(category.ParentId);
            category.Depth = parentCategory.Depth + 1;
            if (category.ID > 0 && IsModify)
            {

                result = CategoryService.UpdateCategory(category);
            }
            else
            {
                category.ID = 0;
                result = CategoryService.AddCategory(category);
            }
            // RedirectToAction("List");   
            return Json(result);
        }

        //Delete data  View里面如何实现
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = CategoryService.DeleteCategory(id);
            return Json(result);
        }

    }
}
