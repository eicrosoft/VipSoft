// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArticleDao.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:22-Dec-2012
// </copyright>

using System.Collections.Generic;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Dao;

namespace VipSoft.CMS.Core.Dao
{  /// <summary> 
    ///  文章接口
    /// </summary>
    public interface IArticleDao : IDao<Article>
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
        /// 得到所有子类的文章
        /// </summary>                    
        IList<Article> GetList(int[] categoryIds);

    }
}
