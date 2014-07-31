// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Menu.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:21-Feb-2013
// </copyright>

using System;
using VipSoft.Core.Entity;

namespace VipSoft.Membership.Core.Entity
{
    /// <summary> 
    ///  后台菜单
    /// </summary>
    [Table("vipsoft_menu")]
    public class Menu : IEntity
    {
        /// <summary> 
        /// ID
        /// </summary>
        [Column(Name = "id")]
        public int ID { get; set; }

        /// <summary> 
        /// 父级ID
        /// </summary>
        [Column(Name = "parent_id")]
        public int ParentId { get; set; }

        /// <summary> 
        /// 级数
        /// </summary>
        [Column(Name = "depth")]
        public int Depth { get; set; }


        /// <summary> 
        /// 菜单名
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary> 
        /// 分页数
        /// </summary>
        [Column(Name = "page_size")]
        public int PageSize { get; set; }

        /// <summary> 
        /// 菜单ID
        /// </summary>
        [Column(Name = "category_id")]
        public int CategoryId { get; set; }

        /// <summary> 
        /// 菜单类型，对应 CategoryElement
        /// </summary>
        [Column(Name = "category_type")]
        public int CategoryType { get; set; }

        /// <summary> 
        /// HTML类型，用于控制哪些属性是否隐藏
        /// </summary>
        [Column(Name = "html_type")]
        public string HtmlType { get; set; }

        /// <summary> 
        /// URL
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }

        /// <summary> 
        /// 状态
        /// </summary>
        [Column(Name = "status")]
        public int Status { get; set; }



        /// <summary> 
        /// 状态
        /// </summary>
        [Column(Name = "sequence")]
        public int Sequence { get; set; }

        /// <summary> 
        /// 创建时间
        /// </summary>
        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        /// <summary> 
        /// 更新时间
        /// </summary>
        [Column(Name = "update_date")]
        public DateTime UpdateDate { get; set; }


        /// <summary> 
        /// 可用来放ClassName够成 TreeGrid
        /// </summary> 
        public string DepthDescription { get; set; }
    }
}