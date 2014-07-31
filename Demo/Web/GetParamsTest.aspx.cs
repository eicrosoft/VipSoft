using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Demo.Entity;
using VipSoft.Core.Entity;
using VipSoft.Data;
using VipSoft.Data.Config;

namespace Demo
{
    public partial class GetParamsTest : System.Web.UI.Page
    {
        private readonly ISessionFactory sessionFaction = Configuration.Configure.BuildSessionFactory();
                                           
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ORM
                //var list = GetListORM<UserModel>(new UserModel { UName = "a", Conditaion = "UserName=[UName];" });
                //SQL
                var list = GetListSQL<UserModel>();
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
        }  

        //ORM
        public List<T> GetListORM<T>(IEntity model)
        {
            List<T> result;
            using (ISession session = sessionFaction.OpenSession())
            {               
                result = session.Load<T>(model);
            }
            return result;
        }


        //自定义SQL
        public List<T> GetListSQL<T>()
        {                                                                    
            List<T> result;
            using (ISession session = sessionFaction.OpenSession())
            {
                string sql = "SELECT * FROM users WHERE UserName=?P_UserName";
                DbParameter[] s = session.GetParameters<UserModel>("UserName");
                s[0].Value = "b";            
                result = session.Load<T>(sql, CommandType.Text,s);
            }
            return result;
        }    
    }
}