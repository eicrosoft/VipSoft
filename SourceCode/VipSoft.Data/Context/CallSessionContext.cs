using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using VipSoft.Data.Engine;

namespace VipSoft.Data.Context
{
    /// <summary>
    /// Provides a <see cref="ISessionFactory.GetCurrentSession()">current session</see>
    /// for each <see cref="System.Runtime.Remoting.Messaging.CallContext"/>.
    /// Not recommended for .NET 2.0 web applications.
    /// </summary>
    [Serializable]
    public class CallSessionContext : MapBasedSessionContext
    {
        private const string SessionFactoryMapKey = "FSORM.Context.CallSessionContext.SessionFactoryMapKey";

        public CallSessionContext(ISessionFactoryImplementor factory) : base(factory)
        {
        }

        /// <summary>
        /// The key is the session factory and the value is the bound session.
        /// </summary>
        protected override void SetMap(IDictionary value)
        {
            CallContext.SetData(SessionFactoryMapKey, value);
        }

        /// <summary>
        /// The key is the session factory and the value is the bound session.
        /// </summary>
        protected override IDictionary GetMap()
        {
            return CallContext.GetData(SessionFactoryMapKey) as IDictionary;
        }
    }
}