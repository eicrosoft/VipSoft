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


namespace VipSoft.CMS.Service
{
    public class FeedBackService : AbstractService, IFeedBackService
    {
        //public IArticleDao ArticleDao = Wac.GetObject("ArticleDao") as IArticleDao;
        public IFeedBackDao FeedBackDao { get; set; }//= Wac.GetObject("FeedBackDao") as IFeedBackDao;
        public int AddFeedback(Feedback feedback)
        {
           return  FeedBackDao.Add(feedback);
        }

        public int DeleteFeedback(int fid)
        {
            return FeedBackDao.Delete(fid);
        }

        public int UpdateFeedback(Feedback feedback)
        {
            return FeedBackDao.Update(feedback);
        }

        public IList<Feedback> GetFeedbackList(Feedback feedback)
        {
            return FeedBackDao.GetList(feedback);
        }
    }
}
