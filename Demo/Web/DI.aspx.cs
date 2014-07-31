using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Demo.Entity;
using Spring.Context;
using Spring.Context.Support;

namespace Demo
{
    public partial class DI : System.Web.UI.Page
    {

        public UserService UserService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //IApplicationContext ctx = ContextRegistry.GetContext();
            //UserService = ctx.GetObject("UserService") as UserService;
            var list = UserService.GetList<UserModel>(new UserModel());
            GridView1.DataSource = list;
            GridView1.DataBind();

        }
    }
}