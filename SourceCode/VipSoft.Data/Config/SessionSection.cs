using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace VipSoft.Data.Config
{
    /// <summary>
    /// Encapsulates a section of Web/App.config to declare which session factories are to be created.
    /// </summary>

    public class SessionSection : ConfigurationSection
    {
        [ConfigurationProperty("sessionFactories", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SessionElementCollection), AddItemName = "sessionFactory", ClearItemsName = "clearFactories")]
        public SessionElementCollection GetElementCollection
        {
            get
            {
                SessionElementCollection sessionElementCollection = (SessionElementCollection)base["sessionFactories"];
                return sessionElementCollection;
            }
        }
    }
}