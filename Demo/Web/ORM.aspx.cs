using System;
using Demo.Entity;
using VipSoft.Core.Entity;

namespace Demo
{
    public partial class Default : System.Web.UI.Page
    {
        private UserService UserService = UserService.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var list = UserService.GetList<UserModel>(new UserModel { ID = 1,Conditaion = "ID>[ID];" });
               //  var list = UserService.GetList<UserDto>(new UserDto());
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
        }
    }

     
}