// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:25-Nov-2012
// </copyright>

using System;
using System.Collections.Generic;
using VipSoft.CMS.Core.Dao;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;
using VipSoft.Core.Cache;
using VipSoft.Core.Utility;

namespace VipSoft.CMS.Service
{
    public class CategoryService : AbstractService, ICategoryService
    {
        /// <summary>
        /// //spring ioc
        /// </summary>                                                          
        //public ICategoryDao CategoryDao = Wac.GetObject("CategoryDao") as ICategoryDao;

        /// <summary>
        /// spring ioc
        /// </summary>
        public ICategoryDao CategoryDao { get; set; }

        public int AddCategory(Category category)
        {
            return CategoryDao.Add(category);
        }

        public int DeleteCategory(int categoryId)
        {                                   
            CacheHelper<List<Category>>.CleanAll();
            var ids = GetChildIds(categoryId);
            return CategoryDao.Delete(ids);
        }

        public int[] GetChildIds(int categoryId)
        {   
            var list = GetChildNode(categoryId);
            var result = new int[list.Count + 1];
            for (var i = 0; i < list.Count; i++)
            {
                result[i] = list[i].ID;
            }
            result[list.Count] = categoryId;
            return result;
        }


        public int UpdateCategory(Category category)
        {
            return CategoryDao.Update(category);
        }

        public int SetCategorySequence(int categoryId, int sequence)
        {
            return 0;
        }

        public int BatchSetCategory(int[] categoryId, int values)
        {
            return 0;
        }

        public Category GetCategory(int categoryId)
        {
            return CategoryDao.Get(categoryId);
        }

        public List<Category> GetCategoryList()
        {
            return CategoryDao.GetList();
        }

        public List<Category> GetCategoryList(Category category)
        {
            return CategoryDao.GetList(category);
        }

        public List<Category> GetCategoryList(Criteria criteria,params  Order[] order)
        {
            return CategoryDao.GetList(criteria,order);
        }               
                    
        public List<Category> GetBrotherNode(int id)
        {
            return CategoryDao.GetBrotherNode(id);
        }


        public List<Category> GetChildNode(int id)
        {
            var result = new List<Category>();
            var list = CategoryDao.GetCacheList();
            GetChildNode(result, list, id);
            return result;
        }

       

        //待改造
        private void GetChildNode(ICollection<Category> result, List<Category> list, int id)
        {
            var cList = list.FindAll(p => p.ParentId == id);
            foreach (var cModel in cList)
            {
                GetChildNode(result, list, cModel.ID);
                result.Add(cModel);
            }
        }

        public List<Category> GetAncestorNote(Category category)
        {
            var result = new List<Category>();
            var list = CategoryDao.GetCacheList();
            GetParentNote(result, list, category.ParentId);
            result.Add(category);
            return result;
        }

        private void GetParentNote(ICollection<Category> result, List<Category> list, int parentId)
        {
            if (parentId != 0)
            {
                var pModle = list.Find(p => p.ID == parentId);
                GetParentNote(result, list, pModle.ParentId);
                result.Add(pModle);
            }
        }






    }
}
