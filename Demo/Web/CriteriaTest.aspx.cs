using System;
using System.Collections.Generic;
using System.Data.Common;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data;
using VipSoft.Data.Config;

namespace Demo
{
    public partial class CriteriaTest : System.Web.UI.Page
    {
        private readonly ISessionFactory sessionFaction = Configuration.Configure.BuildSessionFactory();
                                           
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {      
                //var list = GetListSQL(new Criteria("Name",OP.LIKE,"我们"),new Order("Name"));
                var list = GetListSQL(new Criteria("Depth", OP.GE, 0), new Order("Parent_Id"));
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
        }  
          

        //自定义SQL
        public List<Category> GetListSQL()
        {
            List<Category> result;
            using (ISession session = sessionFaction.OpenSession())
            {
                string sql = "SELECT * FROM vipsoft_category WHERE depth=?P_depth";
                DbParameter[] s = session.GetParameters<Category>("depth");
                s[0].Value = 1;
                result = session.Load<Category>(sql, s);
            }
            return result;
        }

        //自定义SQL
        public List<Category> GetListSQL(Criteria criteria,Order order)
        {
            List<Category> result;
            using (ISession session = sessionFaction.OpenSession())
            {
//                string sql = "SELECT * FROM vipsoft_category WHERE depth=?P_depth";
//                DbParameter[] s = session.GetParameters<Category>("depth");
//                s[0].Value = 1;
                result = session.Load<Category>(criteria,order);
            }
            return result;
        }    
    }
}