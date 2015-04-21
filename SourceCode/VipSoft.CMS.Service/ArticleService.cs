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
using VipSoft.Core.Dao;
using VipSoft.Service;

namespace VipSoft.CMS.Service
{
    public class ArticleService : VipSoftService<Article>, IArticleService
    {
        //spring ioc
        public IArticleDao ArticleDao { get; set; }//= Wac.GetObject("ArticleDao") as IArticleDao;
         
        public override IDao<Article> Dao { get { return ArticleDao; } }
  

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
          
        public IList<Article> GetArticleList(int[] categoryIds)
        {
            return ArticleDao.GetList(categoryIds);
        } 
    }
}