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
using VipSoft.Core.Utility;


namespace VipSoft.CMS.Core.Service
{
    /// <summary> 
    ///  文章接口
    /// </summary>
    public interface IArticleService
    {      
        /// <summary> 
        /// 新增文章
        /// </summary>
        int AddArticle(Article article);

        /// <summary> 
        /// 删除文章
        /// </summary>
        int DeleteArticle(int articleId);

        /// <summary> 
        /// 更新文章
        /// </summary>
        int UpdateArticle(Article article);

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
        /// 显示文章详细
        /// </summary>
        Article GetArticle(int articleId);

        Article GetArticle(Criteria criteria);


        /// <summary> 
        /// 显示文章详细
        /// </summary>
        Article GetArticle(Article article);

        /// <summary> 
        /// 显示文章列表
        /// </summary>
        IList<Article> GetArticleList(Article article);

        IList<Article> GetArticleList(Criteria criteria);
                                                                            

        /// <summary> 
        /// 显示文章列表
        /// </summary>
        IList<Article> GetArticleList(int[] categoryIds);
    }
}
