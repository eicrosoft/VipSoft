using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Demo
{
    public partial class DemoConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //read();
            divDemoConfig.InnerHtml = ReadValueByKey("ModelAssembly");
        }

        public  string ReadValueByKey( string key)
        {
            string value = string.Empty;
            string filename = Server.MapPath("") + @"\Demo.config";
             

            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件
            XmlNode node = doc.SelectSingleNode("//appSettings"); //得到[appSettings]节点
            ////得到[appSettings]节点中关于Key的子节点
            XmlElement element = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
            if (element != null)
            {
                value = element.GetAttribute("value");
            }
            return value;
        }

        public void read()
        {
            string filename = Server.MapPath("") + @"\Demo.config";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);

            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            foreach (XmlElement element in topM)
            {
                //if (element.Name == "appSettings")
                //{
                    XmlNodeList node = element.ChildNodes;
                    if (node.Count > 0)
                    {
                         
                        foreach (XmlElement el in node)
                        {
                            //divDemoConfig.InnerHtml=el.Attributes["key"].InnerXml+"<br/>";
                            divDemoConfig.InnerHtml = el.Value+ "<br/>";
                        }
                    }
                //}
            }
        }

    }
}