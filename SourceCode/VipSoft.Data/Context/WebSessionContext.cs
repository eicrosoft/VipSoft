using System;
using System.Collections;
using System.Web;
using VipSoft.Data.Engine;

namespace VipSoft.Data.Context
{
    /// <summary>
    /// Provides a <see cref="ISessionFactory.GetCurrentSession()">current session</see>
    /// for each <see cref="System.Web.HttpContext"/>. Works only with web applications.
    /// </summary>
    [Serializable]
    public class WebSessionContext : MapBasedSessionContext
    {
        private const string SessionFactoryMapKey = "FSORM.Context.WebSessionContext.SessionFactoryMapKey";

        public WebSessionContext(ISessionFactoryImplementor factory) : base(factory)
        {
        }

        protected override IDictionary GetMap()
        {
            return HttpContext.Current.Items[SessionFactoryMapKey] as IDictionary;
        }

        protected override void SetMap(IDictionary value)
        {
            HttpContext.Current.Items[SessionFactoryMapKey] = value;
        }
    }
}