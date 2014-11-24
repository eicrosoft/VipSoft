// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Article.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:19-Nov-2012
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using VipSoft.Core.Entity;

namespace VipSoft.CMS.Core.Entity
{
    /// <summary> 
    ///  文章分类
    /// </summary>   
    [Table("vipsoft_category")]    
    public class Category : IEntity
    {
        /// <summary> 
        /// ID
        /// </summary>
        [Column(ColumnType.IncrementPrimary, Name = "id")]
        public int ID { get; set; }


        /// <summary> 
        /// 父类ID
        /// </summary>
        [Display(Name = "选择父类")]
        [Column(Name = "parent_id")]
        public int ParentId { get; set; }

        /// <summary> 
        /// 深度
        /// </summary>
        [Column(Name = "depth")]
        public int Depth { get; set; }

        /// <summary> 
        /// 分类名称
        /// </summary>    
        [Display(Name = "分类名称")]
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary> 
        /// 分类名称
        /// </summary>    
        [Display(Name = "分类类型")]
        [Column(Name = "category_type")]
        public string CategoryType { get; set; }

        /// <summary> 
        /// 缩 略 图
        /// </summary>    
        [Display(Name = "缩 略 图")]
        [Column(Name = "thumbnail")]
        public string Thumbnail { get; set; }
                                                     
        /// <summary> 
        /// SEO描述
        /// </summary>
        [Column(Name = "seo_title")]
        public string Seo { get; set; }


        /// <summary> 
        /// META关键字
        /// </summary>
        [Column(Name = "seo_keywords")]
        public string SeoKeywords { get; set; }


        /// <summary> 
        /// META描述
        /// </summary>
        [Column(Name = "seo_description")]
        public string SeoDescription { get; set; }


        /// <summary> 
        /// 外连接
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }


        /// <summary> 
        /// 序号
        /// </summary>
        [Column(Name = "sequence")]
        public int Sequence { get; set; }


        /// <summary> 
        /// 状态
        /// </summary>
        [Column(Name = "status")]
        public int Status { get; set; }

        /// <summary> 
        /// 可用来放ClassName够成 TreeGrid
        /// </summary> 
        public string DepthDescription { get; set; }
    }
}