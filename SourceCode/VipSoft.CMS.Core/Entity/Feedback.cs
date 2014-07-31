// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="VipSoft.com.cn">
//    Author:Wang,Haifeng
//        QQ:739292350
//     Email:wanghaifeng@VipSoft.com.cn
//    Create:21-Feb-2013
// </copyright>


using System;
using System.ComponentModel.DataAnnotations;
using VipSoft.Core.Entity;
namespace VipSoft.CMS.Core.Entity
{

    /// <summary> 
    ///  留言
    /// </summary>
    [Table("vipsoft_feedback")]
    public  class Feedback : IEntity
    {

        /// <summary> 
        /// 留言ID
        /// </summary>
        [Column(ColumnType.IncrementPrimary, Name = "id")]
        public int ID { get; set; }

        /// <summary> 
        /// 标题
        /// </summary>
        [Required]
        [Display(Name = "留言标题")]
        [Column(Name = "title")]
        public string Title { get; set; }

     

        /// <summary> 
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary> 
        /// 电话
        /// </summary>
        [Display(Name = "电话")]
        [Column(Name = "tel")]
        public string Tel { get; set; }

        /// <summary> 
        /// Email
        /// </summary>
        [Display(Name = "电子邮箱")]
        [Column(Name = "email")]
        public string Email { get; set; }

        /// <summary> 
        /// QQ
        /// </summary>
        [Display(Name = "QQ")]
        [Column(Name = "qq")]
        public string QQ { get; set; }

        /// <summary> 
        /// 内容
        /// </summary>
        [Display(Name = "留言内容")]
        [Column(Name = "content")]
        public string Content { get; set; }

        /// <summary> 
        /// 日期
        /// </summary>

        [Display(Name = "留言日期")]
        [Column(Name = "create_date")]
        public DateTime? CreateDate { get; set; }

    }
}
