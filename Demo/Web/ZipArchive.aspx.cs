using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;

namespace Demo
{
    public partial class ZipArchive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnZip_Click(object sender, EventArgs e)
        {
            using (ZipFile zip = new ZipFile())
            {
                // add this map file into the "images" directory in the zip archive
                zip.AddFile(@"E:\own\VS2012 Project\VipSoft\trunk\Demo\Web\ConfigHelper.cs","");
                // add the report into a different directory in the archive
                zip.AddFile(@"E:\own\VS2012 Project\VipSoft\trunk\Demo\Web\CriteriaTest.aspx", "");
                zip.Save(@"E:\own\VS2012 Project\VipSoft\trunk\Demo\Web\Jimmy.zip");
            }
        }
    }
}