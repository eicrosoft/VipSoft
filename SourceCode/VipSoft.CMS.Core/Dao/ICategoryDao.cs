// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategoryDao.cs" company=""VipSoft.com.cn"">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:18-Nov-2012
// </copyright>    

using System.Collections.Generic;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Dao;

namespace VipSoft.CMS.Core.Dao
{
    /// <summary> 
    ///  类别Dao接口
    /// </summary>
    public interface ICategoryDao:IDao<Category>
    {
        /// <summary> 
        /// 设置类别排序
        /// </summary>
        int SetCategorySequence(int categoryId, int sequence);

        /// <summary> 
        /// 批量设置类别状态
        /// </summary>
        int BatchSetCategory(int[] categoryId, int values);

        /// <summary> 
        /// 根据id 得到他的兄弟节点对象列表
        /// </summary>   
        List<Category> GetBrotherNode(int id);      

    }
}
