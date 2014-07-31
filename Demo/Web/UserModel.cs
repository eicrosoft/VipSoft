using VipSoft.Core.Entity;

namespace Demo.Entity
{
    [Table("vipsoft_users")]
    public class UserModel:IEntity
    {
        [Column( ColumnType.IncrementPrimary,Name = "ID")]
        public int? ID { get; set; }

        [Column(ColumnType.Unique,Name = "UserName")]
        public string UName { get; set; }

        [Column(Name="Password")]                      
        public string Password { get; set; }
    
        [Column(Name = "Status")]
        public string Status { get; set; }   

         
    }   
}