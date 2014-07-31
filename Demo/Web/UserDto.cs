using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VipSoft.Core.Entity;

namespace Demo
{
     //DTO             
    public class UserDto : IEntity
    {
        [Column(Name = "UserName")]
        public string UserName { get; set; }
        [Column]
        public string Password { get; set; }
        [Column(Name = "orderno")]
        public string order { get; set; }
        [Column]
        public string customno { get; set; }
    }
}