using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Demo
{
    public partial class XMLDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divCell.InnerHtml = GetXML + "<BR/>" + xmlToSql(GetXML);

            divCell.InnerHtml += "<BR/>" + ListToXml(GetList);
        }



        public  string xmlToSql(string xml)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder fields = new StringBuilder();
            StringBuilder values = new StringBuilder();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            System.Xml.XmlNode root = doc.SelectSingleNode("//CustomerInfo"); 
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    fields.Append(node.Name).Append(",");
                    values.Append("'").Append(node.InnerText).Append("',");
                }
                sql.Append("INSERT INTO TableName(").Append(fields.ToString().TrimEnd(',')).Append(")");
                sql.Append(" VALUES (").Append(values.ToString().TrimEnd(',')).Append(")");
            }
            return sql.ToString();
        }

        public  string ListToXml(List<string> list)
        {  
            XmlDocument doc = new XmlDocument(); 
            XmlElement root = doc.CreateElement("Test");//主内容
            doc.AppendChild(root);  
            foreach(string item in GetList)
            {
                XmlElement element = doc.CreateElement(item);
                element.InnerText = item;
                root.AppendChild(element);
            }
            return doc.InnerXml;
        }

        private List<string> GetList
        {
            get
            {
                var list = new List<string>();
                list.Add("A");
                list.Add("B");
                list.Add("C");
                list.Add("D");
                return list;
            }
        }

        private string GetXML
        {
            get
            {
                string xml = @"<CustomerInfo>
                    <CUSTNMBR>EPAYWEB002</CUSTNMBR>
                    <CUSTNAME>TEST COMPANY</CUSTNAME>
                    <CUSTCLAS>Nodus</CUSTCLAS>
                    <UseCustomerClass>1</UseCustomerClass>
                    <TAXSCHID>2</TAXSCHID>
                    <SHIPMTHD>3</SHIPMTHD>
                    <SALSTERR>4</SALSTERR>
                    <SLPRSNID>6</SLPRSNID>
                    <CHEKBKID>7</CHEKBKID>
                    <PYMTRMID>8</PYMTRMID>
                    <STMTNAME>9</STMTNAME>
                    <SHRTNAME>1</SHRTNAME>
                    <COMMENT1>2</COMMENT1>
                    <COMMENT2>3</COMMENT2>
                    <USERDEF1>4</USERDEF1>
                    <USERDEF2>5</USERDEF2>
                    <CRLMTTYP>6</CRLMTTYP>
                    <CRLMTAMT>7</CRLMTAMT>
                    <CURNCYID>8</CURNCYID>
                    <ADRSCODE>PRIMARY</ADRSCODE>
                    <CNTCPRSN>Test Customer</CNTCPRSN>
                    <ADDRESS1>124 TEST ST</ADDRESS1>
                    <ADDRESS2></ADDRESS2>
                    <ADDRESS3></ADDRESS3>
                    <CITY>TUSTIN</CITY>
                    <STATE>CA</STATE>
                    <ZIPCODE>12345</ZIPCODE>
                    <COUNTRY>USA</COUNTRY>
                    <PHNUMBR1>909-482-4701</PHNUMBR1>
                    <PHNUMBR2>1</PHNUMBR2>
                    <PHNUMBR3>2</PHNUMBR3>
                         <FAX>3</FAX>
                    <PRBTADCD>4</PRBTADCD>
                    <PRSTADCD>5</PRSTADCD>
                    <STADDRCD>6</STADDRCD>
                    </CustomerInfo>";
                return xml;
            }
        }
    }
}