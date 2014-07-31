// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Users.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:28-Jan-2013
// </copyright>


using System;
using System.ComponentModel.DataAnnotations;
using VipSoft.Core.Entity;

namespace VipSoft.Membership.Core.Entity
{

    /// <summary> 
    ///  用户信息
    /// </summary>
    [Table("vipsoft_users")]
    public class Users : IEntity
    {
        /// <summary> 
        /// ID
        /// </summary>
        [Column(ColumnType.IncrementPrimary, Name = "id")]
        public int ID { get; set; }
        /// <summary> 
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Column(Name = "user_name")]
        public string UserName { get; set; }
        /// <summary> 
        /// 密码
        /// </summary>    
        [Display(Name = "密码")]
        [Column(Name = "password")]
        public string Password { get; set; }
        /// <summary> 
        /// 真实姓名
        /// </summary>    
        [Display(Name = "真实姓名")]
        [Column(Name = "real_name")]
        public string RealName { get; set; }
        /// <summary> 
        /// 昵称
        /// </summary>   
        [Display(Name = "昵称")]
        [Column(Name = "nick_name")]
        public string NickName { get; set; }
        /// <summary> 
        /// 证件类型
        /// </summary>  
        [Display(Name = "证件类型")]
        [Column(Name = "card_type")]
        public string CardType { get; set; }
        /// <summary> 
        /// 证件号码
        /// </summary>  
        [Display(Name = "证件号码")]
        [Column(Name = "card_no")]
        public string CardNo { get; set; }
        /// <summary> 
        /// 省
        /// </summary>  
        [Display(Name = "省")]
        [Column(Name = "province")]
        public string Province { get; set; }
        /// <summary> 
        /// 市
        /// </summary>  
        [Display(Name = "市")]
        [Column(Name = "city")]
        public string City { get; set; }
        /// <summary> 
        /// 地址
        /// </summary> 
        [Display(Name = "地址")]
        [Column(Name = "address")]
        public string Address { get; set; }
        /// <summary> 
        /// 邮编
        /// </summary>  
        [Display(Name = "邮编")]
        [Column(Name = "zip_code")]
        public string ZipCode { get; set; }
        /// <summary> 
        /// 电话
        /// </summary> 
        [Display(Name = "电话")]
        [Column(Name = "tel")]
        public string Tel { get; set; }
        /// <summary> 
        /// 手机
        /// </summary>  
        [Display(Name = "手机")]
        [Column(Name = "mobile")]
        public string Mobile { get; set; }
        /// <summary> 
        /// 传真
        /// </summary>   
        [Display(Name = "传真")]
        [Column(Name = "fax")]
        public string Fax { get; set; }
        /// <summary> 
        /// 邮箱
        /// </summary> 
        [Display(Name = "邮箱")]
        [Column(Name = "email")]
        public string Email { get; set; }
        /// <summary> 
        /// QQ
        /// </summary>  
        [Display(Name = "QQ")]
        [Column(Name = "qq")]
        public string QQ { get; set; }
        /// <summary> 
        /// MSN
        /// </summary>   
        [Display(Name = "MSN")]
        [Column(Name = "msn")]
        public string MSN { get; set; }
        /// <summary> 
        /// 密保问题
        /// </summary>  
        [Display(Name = "密保问题")]
        [Column(Name = "question")]
        public string Question { get; set; }
        /// <summary> 
        /// 密保答案
        /// </summary>   
        [Display(Name = "密保答案")]
        [Column(Name = "answer")]
        public string Answer { get; set; }
        /// <summary> 
        /// 状态
        /// </summary>     
        [Display(Name = "状态")]
        [Column(Name = "status")]
        public string Status { get; set; }
        /// <summary> 
        /// 创建时间
        /// </summary>   
        [Display(Name = "创建时间")]
        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary> 
        /// 更新时间
        /// </summary> 
        [Display(Name = "更新时间")]
        [Column(Name = "update_date")]
        public DateTime UpdateDate { get; set; }    
    }
}
