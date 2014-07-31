using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VipSoft.Core.Entity
{
    [Table("vipsoft_settings")]
    public class SystemSettings:IEntity
    {

        /// <summary> 
        /// 类别ID
        /// </summary> 
        [Column(Name = "property_name")]
        public string PropertyName { get; set; }

        /// <summary> 
        /// 标题
        /// </summary> 
        [Column(Name = "property_value")]
        public string PropertyValue { get; set; } 
    }
}
