
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VipSoft.Core.Entity;
using VipSoft.Data.Config;
using VipSoft.Data.Connection;
using VipSoft.Data.Context;
using VipSoft.Data.Engine;
using VipSoft.Data.Persister;  
using VipSoft.Data.Transaction;

namespace VipSoft.Data.Impl
{
    /// <summary>
    ///  Concrete implementation of a SessionFactory.
    /// </summary>
    /// <remarks>
    /// Has the following responsibilities:
    /// <list type="">
    /// <item>
    /// Caches configuration settings (immutably)</item>
    /// <item>
    /// Delegates <c>IDbConnection</c> management to the <see cref="IConnectionProvider"/>
    /// </item>
    /// <item>
    /// Factory for instances of <see cref="ISession"/>
    /// </item>
    /// </list>
    /// <para>
    /// This class must appear immutable to clients, even if it does all kinds of caching
    /// and pooling under the covers.  It is crucial that the class is not only thread safe
    /// , but also highly concurrent.  Synchronization must be used extremely sparingly.
    /// </para>
    /// </remarks>
    /// <seealso cref="ISession"/>
    /// <seealso cref="IEntityPersister"/>
    /// <seealso cref="IEntityPersister"/>
    [Serializable]
    public sealed class SessionFactoryImpl : ISessionFactoryImplementor, IObjectReference
    {
        private bool disposed;

        [NonSerialized]
        private bool isClosed = false;

        [NonSerialized]
        private readonly ICurrentSessionContext currentSessionContext;

        [NonSerialized]
        private readonly IDictionary<string, IEntityPersister> entityPersisters;
      
        private readonly string uuid;

        private readonly string name;

        public SessionFactoryImpl(Configuration cfg, Settings settings)
        {
            this.settings = settings;

            //settings.CacheProvider.Start(null);

            #region Persisters

            entityPersisters = new Dictionary<string, IEntityPersister>();
            foreach (ClassMetadata model in cfg.ClassMappings)
            {
                //string cacheRegion = model.EntityName;
                //ICacheConcurrencyStrategy cache;
                //if (!caches.TryGetValue(cacheRegion, out cache))
                //{
                //    cache =CacheFactory.CreateCache(model.CacheConcurrencyStrategy, cacheRegion, model.IsMutable, settings, properties);
                //    if (cache != null)
                //    {
                //        caches.Add(cacheRegion, cache);
                //    }
                //}

                IEntityPersister cp = new EntityPersister(model, this);  // PersisterFactory.CreateClassPersister(model, cache, this, mapping);
                entityPersisters[model.EntityName] = cp;
            }

            #endregion

            #region Serialization info

            name = settings.SessionFactoryName;

            uuid = Guid.NewGuid().ToString("N");

            SessionFactoryObjectFactory.AddInstance(uuid, name, this);

            #endregion

            currentSessionContext = BuildCurrentSessionContext();
        }

        public IConnectionProvider ConnectionProvider
        {
            get { return Settings.ConnectionProvider; }
        }

        /// <summary></summary>
        public ITransactionFactory TransactionFactory
        {
            get { return Settings.TransactionFactory; }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            Close();
        }

        /// <summary>
        /// Closes the session factory, releasing all held resources.
        /// <list>
        /// <item>cleans up used cache regions and "stops" the cache provider.</item>
        /// <item>close the ADO.NET connection</item>
        /// </list>
        /// </summary>
        public void Close()
        {
            isClosed = true;

           // settings.CacheProvider.Stop();

            try
            {
                settings.ConnectionProvider.Dispose();
            }
            finally
            {
                SessionFactoryObjectFactory.RemoveInstance(uuid, name);
            }
        }

        public bool IsClosed
        {
            get { return isClosed; }
        }
        
        /// <summary>
        /// Gets the ICurrentSessionContext instance attached to this session factory.
        /// </summary>
        public ICurrentSessionContext CurrentSessionContext
        {
            get { return currentSessionContext; }
        }

        public Settings Settings { get { return settings; } }

        [NonSerialized]
        private readonly Settings settings;

        public ISession OpenSession()
        {
            return new SessionImpl(this);
        }

        public ISession OpenSession(IDbConnection connection)
        {
            return new SessionImpl(connection, this, long.MinValue, settings.IsAutoCloseSessionEnabled, settings.ConnectionReleaseMode);
        }

        public ISession OpenSession(IDbConnection connection, bool autoCloseSessionEnabled, ConnectionReleaseMode connectionReleaseMode)
        {
            return new SessionImpl(connection, this, 0, autoCloseSessionEnabled, connectionReleaseMode);
        }

        public ISession GetCurrentSession()
        {
            return currentSessionContext != null ? currentSessionContext.CurrentSession() : null;
        }

        private ICurrentSessionContext BuildCurrentSessionContext()
        {
            var impl = System.Configuration.ConfigurationManager.AppSettings["sessionContextType"];

            ICurrentSessionContext result = null;
            switch (impl)
            {
                case "CallSessionContext":
                    result = new CallSessionContext(this);
                    break;
                case "ThreadStaticSessionContext":
                    result = new ThreadStaticSessionContext(this);
                    break;
                case "WebSessionContext":
                    result = new WebSessionContext(this);
                    break;
                case "ManagedWebSessionContext":
                    result = new ManagedWebSessionContext(this);
                    break;
            }
            return result;
        }
        
        public IEntityPersister GetEntityPersister(string entityName)
        {
            return TryGetEntityPersister(entityName);
        }

        public IEntityPersister TryGetEntityPersister(string entityName)
        {
            IEntityPersister result;
            entityPersisters.TryGetValue(entityName, out result);
            return result;
        }

        public object GetRealObject(StreamingContext context)
        {
            // the SessionFactory that was serialized only has values in the properties
            // "name" and "uuid".  In here convert the serialized SessionFactory into
            // an instance of the SessionFactory in the current AppDomain.
            //log.Debug("Resolving serialized SessionFactory");

            // look for the instance by uuid - this will work when a SessionFactory
            // is serialized and deserialized in the same AppDomain.
            ISessionFactory result = SessionFactoryObjectFactory.GetInstance(uuid);
            if (result == null)
            {
                // if we were deserialized into a different AppDomain, look for an instance with the
                // same name.
                result = SessionFactoryObjectFactory.GetNamedInstance(name);
                if (result == null)
                {
                    throw new NullReferenceException("Could not find a SessionFactory named " + name + " or identified by uuid " + uuid);
                }
            }


            return result;
        }
    }
}
