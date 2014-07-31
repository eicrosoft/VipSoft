using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Demo.Entity;
using VipSoft.Service;

namespace Demo
{
    public partial class SiteSettings : System.Web.UI.Page
    {
        SettingsService s = new SettingsService();  
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
                divCell.InnerHtml = s.SystemTitle; 
            } 
        }


        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            s.SystemTitle = "VipSoft CMS";
        }

        protected void btnGetValue_Click(object sender,EventArgs e)
        {
            divCell.InnerHtml = s.SystemTitle; 
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            s.SystemURL = txtSiteName.Text.Trim();
            divCell.InnerHtml = "<br/>" + s.SystemName;
            s.Update();
        }
    }
}