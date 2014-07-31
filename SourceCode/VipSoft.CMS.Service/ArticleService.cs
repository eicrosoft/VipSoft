// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleService.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:22-Dec-2012
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
    public class ArticleService : AbstractService, IArticleService
    {
        //spring ioc
        public IArticleDao ArticleDao { get; set; }//= Wac.GetObject("ArticleDao") as IArticleDao;

        public int AddArticle(Article article)
        {
            article.CreateDate = DateTime.Now;
            return ArticleDao.Add(article);
        }

        public int DeleteArticle(int articleId)
        {
            return ArticleDao.Delete(articleId);
        }

        public int UpdateArticle(Article article)
        {
            return ArticleDao.Update(article);
        }

        public int UpdateVisitationCount(int articleId)
        {
            throw new NotImplementedException();
        }

        public int UpdateAgreeCount(int articleId)
        {
            throw new NotImplementedException();
        }

        public int UpdateArgueCount(int articleId)
        {
            throw new NotImplementedException();
        }

        public int BatchUpdateArticle(int[] articleId)
        {
            throw new NotImplementedException();
        }

        public Article GetArticle(int articleId)
        {
            return ArticleDao.Get(articleId);
        }

        public Article GetArticle(Article article)
        {
            return ArticleDao.Get(article);
        }

        public Article GetArticle(Criteria criteria)
        {
            return ArticleDao.Get(criteria);
        }

        public IList<Article> GetArticleList(Article article)
        {
            //var obj = CacheHelper<IList<Article>>.GetCache("GetArticleList");
            //if (obj == null)
            //{
            //    obj = ArticleDao.GetList(article);

            //    CacheHelper<IList<Article>>.SetCache("GetArticleList", obj);
            //}
            //return (IList<Article>)obj;

            article.OrderBy = "ORDER BY ID DESC";
            return ArticleDao.GetList(article);

        }

        public IList<Article> GetArticleList(Criteria criteria)
        {
            //var obj = CacheHelper<IList<Article>>.GetCache("GetArticleList");
            //if (obj == null)
            //{
            //    obj = ArticleDao.GetList(article);

            //    CacheHelper<IList<Article>>.SetCache("GetArticleList", obj);
            //}
            //return (IList<Article>)obj;

           //article.OrderBy = "ORDER BY ID DESC";
            return ArticleDao.GetList(criteria);

        }


        public IList<Article> GetArticleList(int[] categoryIds)
        {    
            return ArticleDao.GetList(categoryIds);    
        }   
    }
}