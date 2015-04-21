// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedBackService.cs" company="VipSoft.com.cn">
//    Author:Wang,Haifeng
//        QQ:739292350
//     Email:wanghaifeng@VipSoft.com.cn
//    Create:21-Feb-2013
// </copyright>

using System.Collections.Generic;
using VipSoft.CMS.Core.Dao;
using VipSoft.CMS.Core.Entity;
using VipSoft.CMS.Core.Service;
using VipSoft.Core.Dao;
using VipSoft.Service;


namespace VipSoft.CMS.Service
{
    public class FeedBackService : VipSoftService<Feedback>, IFeedBackService
    {
        //public IArticleDao ArticleDao = Wac.GetObject("ArticleDao") as IArticleDao;
        public IFeedBackDao FeedBackDao { get; set; }//= Wac.GetObject("FeedBackDao") as IFeedBackDao;

        public override IDao<Feedback> Dao { get { return FeedBackDao; } }
         
    }
}
