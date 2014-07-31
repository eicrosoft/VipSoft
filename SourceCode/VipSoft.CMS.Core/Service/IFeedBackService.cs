// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFeedBackService.cs" company="VipSoft.com.cn">
//    Author:Wang,Haifeng
//        QQ:739292350
//     Email:wanghaifeng@VipSoft.com.cn
//    Create:21-Feb-2013
// </copyright>


using System;
using System.Collections.Generic;
using VipSoft.CMS.Core.Entity;

namespace VipSoft.CMS.Core.Service
{

    /// <summary>
    /// 留言接口
    /// </summary>
   public interface IFeedBackService
    {
        /// <summary> 
        /// 新增留言
        /// </summary>
        int AddFeedback(Feedback feedback);

        /// <summary> 
        /// 删除留言
        /// </summary>
        int DeleteFeedback(int fid);

        /// <summary> 
        /// 更新留言
        /// </summary>
        int UpdateFeedback(Feedback feedback);

        /// <summary> 
        /// 显示留言列表
        /// </summary>
        IList<Feedback> GetFeedbackList(Feedback feedback);
    }
}
