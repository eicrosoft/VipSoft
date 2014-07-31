using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Demo.Entity;
using VipSoft.Core.Cache;
using VipSoft.Data.Config;
using VipSoft.Data.Persister;
using VipSoft.FastReflection;

namespace Demo
{
    public partial class WebToModel : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<UserModel> list = new List<UserModel>();
            NameValueCollection nameValueCollection = Request.Form;

            //UserModel model = new UserModel(); 
            //model.UName = txtUName.Text;
            //model.Password = Password.Text;

            list.Add(SetEntity<UserModel>(nameValueCollection));
            GridView1.DataSource = list;
            GridView1.DataBind();      
            
        }
                                                 
        private T SetEntity<T>(NameValueCollection nameValueCollection)
        {
            T result = Activator.CreateInstance<T>();
            try
            {      
                Type type = typeof(T);
                for (var i = 0; i < nameValueCollection.Keys.Count; i++)
                {
                    string value = nameValueCollection[i];
                    foreach (var propertyInfo in type.GetProperties())
                    {
                        if (propertyInfo.Name.ToUpper() == nameValueCollection.Keys[i].ToUpper())
                        {
                            propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                        }
                    }
                }
            }
            catch
            {
                result = default(T);
            }
            return result;
        }
    }
}