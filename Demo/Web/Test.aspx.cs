using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Demo
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptPaymentTerm.DataSource = GetPaymentList();
                rptPaymentTerm.DataBind();
                GetName();
            }
        }

        private void GetName()
        {
            var strxml = @"<root><property name='PackageFolder' value='G:\AutoBuild\打包记录' /></root>";  
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"<root><property name='PackageFolder' value='G:\AutoBuild\打包记录' /></root>");
            //XmlNodeList nodes = doc.SelectNodes("/a/property");
            //string temp = "";
            //Regex regex = new Regex(@"{.*}");
            //foreach (XmlNode i in nodes)
            //{
            //    if ("dest_folder_src_plugin" == i.Attributes["name"].Value)
            //    {
            //        temp = regex.Match(i.Attributes["location"].Value).Value;
            //        temp = temp.Substring(1, temp.Length - 2);
            //    }
            //}
            //foreach (XmlNode i in nodes)
            //{
            //    if (temp == i.Attributes["name"].Value)
            //    {
            //        temp = regex.Match(i.Attributes["location"].Value).Value;
            //        divCellter.InnerHtml += temp.Substring(1, temp.Length - 2);
                    
            //    }
            //}    

            //XElement root = XElement.Load(strxml);
            //var rootChild = from x in root.Elements() select x; //获取根节点下的子节点集合也就是head
            string s = " <a href=\"www.csdn.net\" class=\"main\">CSDN</a><property name=\"PackageFolder\" value=\"G:\\AutoBuild\\打包记录\" />";
            divCellter.InnerHtml = GetTitleContent(s, "property", "value");
        }

        public static string GetTitleContent(string str, string title, string attrib)
        {

            string tmpStr = string.Format("<{0}[^>]*?{1}=(['\"\"]?)(?<url>[^'\"\"\\s>]+)\\1[^>]*>", title, attrib); //获取<title>之间内容  

            Match TitleMatch = Regex.Match(str, tmpStr, RegexOptions.IgnoreCase);

            string result = TitleMatch.Groups["url"].Value;
            return result;
        }  


        private DataTable GetPaymentList()
        {
            var result = new DataTable();
            result.Columns.Add("Term");
            DataRow newRow = result.NewRow();
            newRow["Term"] = "a";
            result.Rows.Add(newRow);
            newRow = result.NewRow();
            newRow["Term"] = "b";
            result.Rows.Add(newRow);
            newRow = result.NewRow();
            newRow["Term"] = "c";
            result.Rows.Add(newRow);
            return result;
        }

        protected void Save(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            foreach (RepeaterItem item in rptPaymentTerm.Items)
            {
                HtmlInputCheckBox chkcredit = item.FindControl("chkcredit") as HtmlInputCheckBox;
                HtmlInputCheckBox chkecheck = item.FindControl("chkecheck") as HtmlInputCheckBox;
                if (chkcredit != null && chkcredit.Checked)
                {
                    builder.Append(chkcredit.Value);
                    builder.Append("<br/>");
                }
                if (chkecheck != null && chkecheck.Checked)
                {
                    builder.Append(chkecheck.Value);
                    builder.Append("<br/>");
                }
            }


            divCellter.InnerHtml = builder.ToString();
        }

        //private List<string> GetResult()
        //{
        //    var list = new List<string>();
        //    list.Add("b_2");
        //    list.Add("c_1");
        //    return list;
        //}

        protected void rptPaymentTerm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string paymentTerm = "b_2,c_2,";
            HtmlInputCheckBox chkcredit = e.Item.FindControl("chkcredit") as HtmlInputCheckBox;
            HtmlInputCheckBox chkecheck = e.Item.FindControl("chkecheck") as HtmlInputCheckBox;
            if (chkcredit != null)
            {
                chkcredit.Checked = paymentTerm.Contains(chkcredit.Value);
            }
            if (chkecheck != null)
            {
                chkecheck.Checked = paymentTerm.Contains(chkecheck.Value);
            }
        }



        //public static DataTable ExecuteDataTable(string sql, CommandType cmdType, DbParameter[] parameters)
        //{
        //    DbConnection conn = null;
        //    DbCommand cmd = null;
        //    DbDataAdapter dataAdapter = null;
        //    var result = new DataTable();
        //    try
        //    {
        //        conn = GetConnection();
        //        cmd = CreateCommand(conn, sql, cmdType, parameters);
        //        dataAdapter = DbFactory.CreateDataAdapter();
        //        dataAdapter.SelectCommand = cmd;
        //        dataAdapter.Fill(result);
        //        cmd.Parameters.Clear();
        //    }
        //    catch (DbException e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    finally
        //    {
        //        if (cmd != null) cmd.Dispose();
        //        if (dataAdapter != null) dataAdapter.Dispose();
        //        Close(conn);
        //    }
        //    return result;

        //}


        //public static DbDataReader ExecuteReader(string sql, CommandType cmdType, DbParameter[] parameters)
        //{
        //    DbDataReader result;
        //    DbConnection connection = null;
        //    try
        //    {
        //        connection = GetConnection();
        //        var command = CreateCommand(connection, sql, cmdType, parameters);
        //        result = command.ExecuteReader(CommandBehavior.CloseConnection);
        //    }
        //    catch (DbException e)
        //    {
        //        Close(connection);
        //        throw new Exception(e.Message);
        //    }
        //    return result;
        //}



        //using (DbDataReader dr = DBHelper.ExecuteReader(sql))
        //    {
        //        while (dr.Read())
        //        {
        //            ArtifactInfo ArtifactInfo 
        //        }
        //        dr.Close();
        //    }
    }
}