// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategoryService.cs" company=""VipSoft.com.cn"">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:18-Nov-2012
// </copyright>    

using System.Collections.Generic;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Service;
using VipSoft.Core.Utility;

namespace VipSoft.CMS.Core.Service
{
    /// <summary> 
    ///  类别Service接口
    /// </summary>
    public interface ICategoryService:IService<Category>
    {
        int Delete(int categoryId);
        /// <summary> 
        /// 设置类别排序
        /// </summary>
        int SetCategorySequence(int categoryId, int sequence);

        /// <summary> 
        /// 批量设置类别状态
        /// </summary>
        int BatchSetCategory(int[] categoryId, int values);

        /// <summary> 
        /// 根据id 得到他的兄弟节点
        /// </summary>   
        List<Category> GetBrotherNode(int id);

        /// <summary>
        ///  得到子节点。
        /// </summary>             
        List<Category> GetChildNode(int id);

        /// <summary>
        ///  得到子节点ID（包括当前的ID）。
        /// </summary>             
        int[] GetChildIds(int id);
                          
        /// <summary> 
        /// 根据当前节点，得到祖先节点
        /// </summary>   
        List<Category> GetAncestorNote(Category category);
    }
}
