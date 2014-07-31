// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Article.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:25-Dec-2012
// </copyright>


using System;
using System.ComponentModel.DataAnnotations;
using VipSoft.Core.Entity;

namespace VipSoft.CMS.Core.Entity
{
    /// <summary> 
    ///  文章
    /// </summary>
    [Table("vipsoft_article")]
    public class Article : IEntity
    {
        /// <summary> 
        /// 文章ID
        /// </summary>
        [Column(ColumnType.IncrementPrimary, Name = "id")]
        public int ID { get; set; }

        /// <summary> 
        /// 类别ID
        /// </summary>
        [Display(Name = "所属类别")]
        [Column(Name = "category_id")]
        public int CategoryId { get; set; }

        /// <summary> 
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [Column(Name = "title")]
        public string Title { get; set; }

        /// <summary> 
        /// 作者
        /// </summary>
        [Display(Name = "作者")]
        [Column(Name = "author")]
        public string Author { get; set; }

        /// <summary> 
        /// 来源
        /// </summary> 
        [Display(Name = "来源")]
        [Column(Name = "source")]
        public string Source { get; set; }

        /// <summary> 
        /// 摘要
        /// </summary>  
        [Display(Name = "摘要")]
        [Column(Name = "summary")]
        public string Summary { get; set; }

        /// <summary> 
        /// 小图
        /// </summary> 
        [Display(Name = "图片")]
        [Column(Name = "file_name")]
        public string FileName { get; set; }

        /// <summary> 
        /// SEO标题
        /// </summary> 
        [Display(Name = "SEO标题")]
        [Column(Name = "seo_title")]
        public string SeoTitle { get; set; }

        /// <summary> 
        /// SEO关健字
        /// </summary>   
        [Display(Name = "SEO关健字")]
        [Column(Name = "seo_keywords")]
        public string SeoKeywords { get; set; }

        /// <summary> 
        /// SEO描述
        /// </summary> 
        [Display(Name = "SEO描述")]
        [Column(Name = "seo_description")]
        public string SeoDescription { get; set; }

        /// <summary> 
        /// URL连接
        /// </summary>   
        [Display(Name = "连接地址")]
        [Column(Name = "link_url")]
        public string LinkUrl { get; set; }

        /// <summary> 
        /// 内容
        /// </summary>  
        [Display(Name = "详细内容")]
        [Column(Name = "content")]
        public string Content { get; set; }

        /// <summary> 
        /// 是否使用外部连接
        /// </summary>     
        [Display(Name = "使用外部连接")]
        [Column(Name = "enable_link_url")]
        public string EnableLinkUrl { get; set; }

        /// <summary> 
        /// 是否保存远程图片
        /// </summary>  
        [Display(Name = "保存远程图片")]
        [Column(Name = "is_save_remote_pic")]
        public string IsSaveRemotePic { get; set; }

        /// <summary> 
        /// 赞成人数
        /// </summary> 
        [Display(Name = "赞成人数")]
        [Column(Name = "agree_count")]
        public int AgreeCount { get; set; }

        /// <summary> 
        /// 反对人数
        /// </summary>   
        [Display(Name = "反对人数")]
        [Column(Name = "argue_count")]
        public int ArgueCount { get; set; }

        /// <summary> 
        /// 浏览次数
        /// </summary> 
        [Display(Name = "浏览次数")]
        [Column(Name = "visitation_count")]
        public int VisitationCount { get; set; }

        /// <summary> 
        /// 推荐类型
        /// </summary> 
        [Display(Name = "推荐类型")]
        [Column(Name = "attribute")]
        public string Attribute { get; set; }

        /// <summary> 
        /// 排序规则 0：创建时间
        /// </summary> 
        [Display(Name = "排序规则")]
        [Column(Name = "sequence_rule")]
        public string SequenceRule { get; set; }

        /// <summary> 
        /// 状态  0：禁用，1：启用
        /// </summary>    
        [Display(Name = "状态")]
        [Column(Name = "status")]
        public int Status { get; set; }

        /// <summary> 
        /// 创建时间
        /// </summary>  
        [Display(Name = "创建时间")]
        [Column(Name = "create_date")]
        public DateTime? CreateDate { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>   
        [Display(Name = "修改时间")]
        [Column(Name = "update_date")]
        public DateTime UpdateDate { get; set; }
        
    }
}