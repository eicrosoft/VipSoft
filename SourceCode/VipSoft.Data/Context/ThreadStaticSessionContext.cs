using System;
using VipSoft.Data.Engine;

namespace VipSoft.Data.Context
{
    /// <summary>
    /// Provides a <see cref="ISessionFactory.GetCurrentSession()">current session</see>
    /// for each thread using the [<see cref="ThreadStaticAttribute"/>].
    /// To avoid if there are two session factories in the same thread.
    /// </summary>
    [Serializable]
    public class ThreadStaticSessionContext : CurrentSessionContext
    {
        [ThreadStatic]
        private static ISession _session;

        public ThreadStaticSessionContext(ISessionFactoryImplementor factory)
        {
        }

        /// <summary> Gets or sets the currently bound session. </summary>
        protected override ISession Session
        {
            get { return _session; }
            set { _session = value; }
        }
    }
}