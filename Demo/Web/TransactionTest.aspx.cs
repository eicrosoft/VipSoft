using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data;
using VipSoft.Data.Config;

namespace Demo
{
    public partial class TransactionTest : System.Web.UI.Page
    {
        private readonly ISessionFactory sessionFaction = Configuration.Configure.BuildSessionFactory();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }


        //事务测试
        public void TransactionMethod()
        { 
            using (ISession session = sessionFaction.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    { 
                        string sql = "UPDATE vipsoft_category SET seo_title='A15' WHERE ID=15";
                        session.ExecuteNonQuery<Category>(sql);

                        string sql2 = "UPDATE vipsoft_category SET seo_keywords='A16' WHERE ID=15";
                        session.ExecuteNonQuery<Category>(sql2);


                        string sql3 = "UPDATE vipsoft_category SET seo_description='A17' WHERE NAME=A12";
                        session.ExecuteNonQuery<Category>(sql3);

                        tran.Commit(); 
                    }
                    catch (Exception)
                    {
                        tran.Rollback(); 
                    } 
                } 
            } 
        }

        public DataTable GetTable()
        {
            DataTable result;
            using (ISession session = sessionFaction.OpenSession())
            {
                string sql = "SELECT * FROM vipsoft_category";
                result = session.ExecuteDataTable(sql, CommandType.Text);
            }
            return result;
        }

        //
        public DataTable GetListSQL(Criteria criteria, Order order)
        {
            DataTable result;
            using (ISession session = sessionFaction.OpenSession())
            { 
                string sql = "SELECT * FROM vipsoft_category";
                result = session.ExecuteDataTable(sql, CommandType.Text); 
            }
            return result;
        }

        public void BindList()
        {
            //TransactionMethod();
            var list = GetListSQL(new Criteria("Depth", OP.GE, 0), new Order("Parent_Id"));
            GridView1.DataSource = GetTable();
            GridView1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BindList();
        }
    }
}