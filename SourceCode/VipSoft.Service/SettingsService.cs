using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VipSoft.Core.Entity;
using VipSoft.Data;
using VipSoft.Data.Config;
using VipSoft.Data.Engine;

namespace VipSoft.Service
{
    public class SettingsService
    {
        
        private static Hashtable settingsCache = Hashtable.Synchronized(new Hashtable());

        private static Hashtable needUpdateCache = Hashtable.Synchronized(new Hashtable());

        private static Hashtable needInsertCache = Hashtable.Synchronized(new Hashtable());
         
        protected ISessionFactory SessionFaction;

        private string SettingsTableName;

        public SettingsService()
        { 
            SessionFaction = Configuration.Configure.BuildSessionFactory();
            SettingsTableName = "vipsoft_settings";
            if (settingsCache.Count == 0) LoadData();
        }

        public virtual bool Update()
        {
            var result = false;
            StringBuilder sbSettings=new StringBuilder();
            foreach(var item in needUpdateCache.Keys)
            {
                sbSettings.AppendFormat("UPDATE {0} SET property_value='{1}' WHERE property_name='{2}';", SettingsTableName, needUpdateCache[item],item);
            }
            foreach (var item in needInsertCache.Keys)
            {
                sbSettings.AppendFormat("INSERT INTO {0} (property_value,property_name) VALUES ('{1}','{2}');", SettingsTableName, needInsertCache[item], item);
            }
            var sql = sbSettings.ToString().Trim();
            if(!string.IsNullOrEmpty(sql))
            using (ISession session = SessionFaction.OpenSession())
            {
                if(session.ExecuteNonQuery<SystemSettings>(sql)>0)
                {
                    needUpdateCache.Clear();
                    needInsertCache.Clear();
                    result = true;
                } 
            }
            return result;
        }

        private  void LoadData()
        {
            
            using(ISession session = SessionFaction.OpenSession())
            {
                var sql = "SELECT * FROM " + SettingsTableName;

                var list = session.Load<SystemSettings>(sql);
                foreach (var item in list)
                {
                    var propertyName = item.PropertyName.Trim();
                    if (!settingsCache.Contains(propertyName))
                        settingsCache.Add(propertyName, item.PropertyValue.Trim());
                }
            }
        }

        public virtual string this[string key]
        {
            get
            {
                if (!settingsCache.ContainsKey(key))
                {
                    return string.Empty; // default(string);
                }
                return settingsCache[key].ToString();
            }
        }

        protected virtual string GetSetting(string settingName)
        {
            return GetSetting(settingName, null);
        }

        protected virtual string GetSetting(string settingName, string defaultValue)
        {
            return false == settingsCache.ContainsKey(settingName) ? defaultValue : settingsCache[settingName].ToString();
        }

        protected virtual void UpdateSetting(string settingName, string settingValue)
        {
            UpdateSetting(settingName, settingValue, false);
        }

        private volatile object _lock = new object(); 
        protected virtual void UpdateSetting(string settingName, string settingValue, bool doEncryption)
        {

            lock (_lock)
            {
                if (this[settingName].Trim() == settingValue.Trim()) return;

                if(string.IsNullOrEmpty(this[settingName].Trim()))
                {
                    if(needInsertCache.Contains(settingName))
                    {
                        needInsertCache[settingName] = settingValue.Trim();
                    }
                    else
                    { 
                        needInsertCache.Add(settingName, settingValue.Trim()); 
                    }
                }
                else
                {
                    if (needUpdateCache.Contains(settingName))
                    {
                        needUpdateCache[settingName] = settingValue.Trim();
                    }
                    else
                    { 
                        needUpdateCache.Add(settingName, settingValue.Trim());   
                    }
                }

                settingsCache[settingName] = settingValue.Trim();
            }
        }



        #region :: System Info ::
         
        public string SystemTitle
        {
            get { return GetSetting("SystemTitle", "VipSoft CMS"); }
            set { UpdateSetting("SystemTitle", value); }
        }

        public string SystemName
        {
            get { return GetSetting("SystemName", "VipSoft"); }
            set { UpdateSetting("SystemName", value); }
        }

        public string SystemLogo
        {
            get { return GetSetting("SystemLogo", "VipSoft Logo"); }
            set { UpdateSetting("SystemLogo", value); }
        }

        public string SystemURL
        {
            get { return GetSetting("SystemURL", "http://www.vipsoft.com.cn"); }
            set { UpdateSetting("SystemURL", value); }
        }

        public string Copyright
        {
            get { return GetSetting("Copyright", "Copyright &copy; 2013 - 2016"); }
            set { UpdateSetting("Copyright", value); }
        }

        public string ICP
        {
            get { return GetSetting("ICP", "苏ICP备07508903号"); }
            set { UpdateSetting("ICP", value); }
        }

        public string Tel
        {
            get { return GetSetting("Tel", ""); }
            set { UpdateSetting("Tel", value); }
        }

        public string Email
        {
            get { return GetSetting("Email", ""); }
            set { UpdateSetting("Email", value); }
        }


        #endregion
    }
}
