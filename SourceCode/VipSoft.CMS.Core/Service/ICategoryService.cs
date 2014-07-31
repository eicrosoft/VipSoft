// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategoryService.cs" company=""VipSoft.com.cn"">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:18-Nov-2012
// </copyright>    

using System.Collections.Generic;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Utility;

namespace VipSoft.CMS.Core.Service
{
    /// <summary> 
    ///  类别Service接口
    /// </summary>
    public interface ICategoryService
    {      
        /// <summary> 
        /// 新增类别
        /// </summary>
        int AddCategory(Category category);

        /// <summary> 
        /// 修改类别名称
        /// </summary>
        int DeleteCategory(int categoryId);

        /// <summary> 
        /// 修改类别名称
        /// </summary>
        int UpdateCategory(Category category);

        /// <summary> 
        /// 设置类别排序
        /// </summary>
        int SetCategorySequence(int categoryId, int sequence);

        /// <summary> 
        /// 批量设置类别状态
        /// </summary>
        int BatchSetCategory(int[] categoryId, int values);

        /// <summary> 
        /// 显示类别详细
        /// </summary>
        Category GetCategory(int categoryId);

        /// <summary> 
        /// 显示所有类别列表
        /// </summary>
        List<Category> GetCategoryList();

        /// <summary> 
        /// 显示类别列表                    
        /// </summary>
        List<Category> GetCategoryList(Category category);

        List<Category> GetCategoryList(Criteria criteria,params Order[] order);                  

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
