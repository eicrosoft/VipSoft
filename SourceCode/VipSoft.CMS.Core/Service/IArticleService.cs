// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArticleService.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:22-Dec-2012
// </copyright>

using System;
using System.Collections.Generic;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Service;
using VipSoft.Core.Utility;


namespace VipSoft.CMS.Core.Service
{
    /// <summary> 
    ///  文章接口
    /// </summary>
    public interface IArticleService : IService<Article>
    {  
        /// <summary> 
        /// 更新文章阅读次数
        /// </summary>
        int UpdateVisitationCount(int articleId);     

        /// <summary> 
        /// 更新赞成
        /// </summary>
        int UpdateAgreeCount(int articleId);

        /// <summary> 
        /// 更新反对
        /// </summary>
        int UpdateArgueCount(int articleId);

        /// <summary> 
        /// 批量设置文章特性(如置顶，标题着色)
        /// </summary>
        int BatchUpdateArticle(int[] articleId);
                        
        /// <summary> 
        /// 显示文章列表
        /// </summary>
        IList<Article> GetArticleList(int[] categoryIds);
    }
}
