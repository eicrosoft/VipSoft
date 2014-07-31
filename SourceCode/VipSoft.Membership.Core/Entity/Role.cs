// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:28-April-2013
// </copyright>

using System.ComponentModel.DataAnnotations;
using VipSoft.Core.Entity;

namespace VipSoft.Membership.Core.Entity
{
    /// <summary> 
    ///  文章分类
    /// </summary>
    [Table("vipsoft_role")]
    public class Role : IEntity
    {
        /// <summary> 
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        [Column(ColumnType.IncrementPrimary, Name = "id")]
        public int ID { get; set; }

        /// <summary> 
        /// 编码
        /// </summary>
        [Display(Name = "编码")]
        [Column(Name = "code")]
        public string Code { get; set; }

        /// <summary> 
        /// 角色名
        /// </summary>
        [Display(Name = "角色名")]
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary> 
        /// 角色描述
        /// </summary>
        [Display(Name = "角色描述")]
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary> 
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        [Column(Name = "status")]
        public string Status { get; set; }
    }
}