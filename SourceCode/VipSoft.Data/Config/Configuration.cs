using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using VipSoft.Core.Cache;
using VipSoft.Core.Config;
using VipSoft.Core.Entity;
using VipSoft.Data.Connection;
using VipSoft.Data.Impl;
using VipSoft.Data.Persister;
using VipSoft.Data.Transaction;

namespace VipSoft.Data.Config
{
    /// <summary>
    /// Allows the application to specify properties and mapping documents to be used when creating
    /// a <see cref="ISessionFactory" />.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Usually an application will create a single <see cref="Configuration" />, build a single instance
    /// of <see cref="ISessionFactory" />, and then instantiate <see cref="ISession"/> objects in threads
    /// servicing client requests.
    /// </para>
    /// <para>
    /// The <see cref="Configuration" /> is meant only as an initialization-time object. <see cref="ISessionFactory" />
    /// is immutable and does not retain any association back to the <see cref="Configuration" />
    /// </para>
    /// </remarks>

    public class Configuration
    {
        private const string SessionFactoriesCachkey = "SESSION_FACTORIES_CACH_KEY";
        private const string PersistentClassCachkey = "PERSISTENT_CLASS_CACH_KEY";
        private const string DirverSettingsCachkey = "DRIVERS_SETTINGS_CACH_KEY";

        private readonly IDictionary<string, Settings> setttings;

        private readonly IDictionary<string, ISessionFactory> sessionFactories;

        private readonly IDictionary<string, ClassMetadata> classes; // entityName, PersistentClass

        private static readonly SessionSection DbSessionSection = ConfigurationManager.GetSection("DbSessionSettings") as SessionSection;

        private static readonly string ModelAssembly = ConfigurationManager.AppSettings["ModelAssembly"];

        private Configuration()
        {
            setttings = CacheHelper<IDictionary<string, Settings>>.GetCache(DirverSettingsCachkey);
            sessionFactories = CacheHelper<IDictionary<string, ISessionFactory>>.GetCache(SessionFactoriesCachkey);
            classes = CacheHelper<IDictionary<string, ClassMetadata>>.GetCache(PersistentClassCachkey);
            if (setttings != null && sessionFactories != null && classes != null) return;
            classes = new Dictionary<string, ClassMetadata>();
            foreach(var model in ModelAssembly.Split(','))
            {
                Assembly assembly = Assembly.Load(model);
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                   if (type.Namespace.Contains("Entity"))
                   {
                       classes[type.Name] = CreatePersistentClass(type);   
                   }                    
                }   
            }

            var typeofsettings = typeof(SystemSettings);
            classes[typeofsettings.Name] = CreatePersistentClass(typeofsettings);   
            
            CacheHelper<IDictionary<string, ClassMetadata>>.SetCache(SessionFactoriesCachkey, classes);

            setttings = new Dictionary<string, Settings>();
            sessionFactories = new Dictionary<string, ISessionFactory>();
            foreach (SessionElement element in DbSessionSection.GetElementCollection)
            {
                var set = BuildSettings(element);
                setttings[element.Name] = set;
                sessionFactories[element.Name] = new SessionFactoryImpl(this, set);

            }
            CacheHelper<List<Settings>>.SetCache(DirverSettingsCachkey, setttings);
            CacheHelper<List<Settings>>.SetCache(SessionFactoriesCachkey, sessionFactories);

        }

        private static ClassMetadata CreatePersistentClass(System.Type type)
        {
            ClassMetadata result = null;

            if (type != null)
            {
                result = new ClassMetadata { EntityType = type, EntityName = type.Name };

                var at = type.GetCustomAttributes(false);
                if (at.Length > 0)
                {
                    var tableAttribute = (TableAttribute)at[0];
                    result.TableName = tableAttribute.Name;
                    result.TableName = tableAttribute.Name;
                    result.AssociateTable = tableAttribute.AssociateTable;
                    result.ForeignKey = tableAttribute.ForeignKey;
                }
                var columnInfos = new List<ColumnInfo>();
                var propertyInfos = type.GetProperties();
                ColumnInfo model;
                foreach (var info in propertyInfos)
                {
                    model = new ColumnInfo { PropertyInfo = info };
                    var attributes = info.GetCustomAttributes(false);
                    foreach (var att in attributes)
                    {
                        if (att is ColumnAttribute)
                        {
                            var ptt = att as ColumnAttribute;
                            model.PropertyName = info.Name;
                            model.PropertyType = info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? info.PropertyType.GetGenericArguments()[0] : info.PropertyType;
                            if (ptt.IsPrimaryKey)
                            {
                                result.PrimaryKey = info.Name;
                                result.PKColumnName = ptt.Name;
                            }
                            model.IsIncrement = ptt.IsIncrement;
                            model.IsNullable = ptt.IsNullable;
                            model.IsUnique = ptt.IsUnique;
                            model.ParameterDirection = ptt.ParameterDirection;
                            model.SameParameters = ptt.SameParameters;
                            model.IsColumn = true;
                            model.Name = ptt.Name;
                            model.IsPrimaryKey = ptt.IsPrimaryKey;

                            model.IsInsertable = ptt.IsInsertable;

                            model.IsUpdatable = ptt.IsUnique;

                            model.ColumnDefinition = ptt.ColumnDefinition;

                            model.Table = ptt.Table;

                            model.Length = ptt.Length;

                            model.Precision = ptt.Precision;

                            model.Scale = ptt.Scale;

                        }
                        else
                        {
                            //break;
                        }
                    }
                    if (info.DeclaringType != typeof(IEntity))
                        columnInfos.Add(model);
                }
                result.ColumnInfos = columnInfos;
            }
            return result;
        }


        /// <summary>
        /// The class mappings 
        /// </summary>
        public ICollection<ClassMetadata> ClassMappings
        {
            get { return classes.Values; }
        }

        /// <summary>
        /// Get the mapping for a particular class
        /// </summary>
        public ClassMetadata GetClassMapping(Type persistentClass)
        {
            return GetClassMapping(persistentClass.Name);
        }

        public ClassMetadata GetClassMapping(string entityName)
        {
            ClassMetadata result;
            classes.TryGetValue(entityName, out result);
            return result;
        }

        /// <summary>
        /// Configure using the <c>&lt;DbSessionSettings&gt;</c> section
        /// from the application config file
        /// </summary>
        /// <returns>A configuration object initialized with the file.</returns>
        public static readonly Configuration Configure = new Configuration();

        /// <summary>
        /// Instantiate a new <see cref="ISessionFactory" />, using the properties and mappings in this
        /// configuration. The <see cref="ISessionFactory" /> will be immutable, so changes made to the
        /// configuration after building the <see cref="ISessionFactory" /> will not affect it.
        /// </summary>
        /// <returns>An <see cref="ISessionFactory" /> instance.</returns> 
        public ISessionFactory BuildSessionFactory()
        {
            return BuildSessionFactory(0);
        }

        public ISessionFactory BuildSessionFactory(int id)
        {
            var t = DbSessionSection.GetElementCollection[id].Name;
            return BuildSessionFactory(t);
        }

        public ISessionFactory BuildSessionFactory(string dbName)
        {
            ISessionFactory result;
            sessionFactories.TryGetValue(dbName, out result);
            return result;
        }

        private Settings BuildSettings(SessionElement sessionElement)
        {
            Settings settings = new Settings();
            IConnectionProvider connectionProvider = ConnectionProviderFactory.NewConnectionProvider(sessionElement);
            ITransactionFactory transactionFactory = CreateTransactionFactory(sessionElement);
            string releaseModeName = sessionElement.ConnectionReleaseMode;
            ConnectionReleaseMode releaseMode = "auto".Equals(releaseModeName) ? ConnectionReleaseMode.AfterTransaction : ParseConnectionReleaseMode(releaseModeName);
            settings.SessionFactoryName = sessionElement.Name;
            settings.ConnectionReleaseMode = releaseMode;
            // settings.DefaultBatchFetchSize = sessionElement.DefaultBatchFetchSize;
            settings.BatchSize = sessionElement.BatchSize;
            settings.BatcherFactory = new BatcherFactory();// CreateBatcherFactory(sessionElement, settings.BatchSize, connectionProvider);
            string isolationString = sessionElement.IsolationString;
            IsolationLevel isolation = IsolationLevel.Unspecified;

            if (isolationString.Length > 0)
            {
                try
                {
                    isolation = (IsolationLevel)Enum.Parse(typeof(IsolationLevel), isolationString);
                }
                catch
                {
                    throw new Exception("The isolation level of " + isolationString + " is not a valid IsolationLevel.  Please use one of the Member Names from the IsolationLevel.");
                }
            }
            settings.IsolationLevel = isolation;
            settings.ConnectionProvider = connectionProvider;
            settings.TransactionFactory = transactionFactory;
            return settings;
        }

        //private static IBatcherFactory CreateBatcherFactory(SessionElement properties, int batchSize, IConnectionProvider connectionProvider)
        //{
        //    return new NonBatchingBatcherFactory();
        //}

        private static ConnectionReleaseMode ParseConnectionReleaseMode(string name)
        {
            switch (name)
            {
                case "after_statement":
                    throw new Exception("aggressive connection release (after_statement) not supported by FSORM");
                case "after_transaction":
                    return ConnectionReleaseMode.AfterTransaction;
                case "on_close":
                    return ConnectionReleaseMode.OnClose;
                default:
                    throw new Exception("could not determine appropriate connection release mode [" + name + "]");
            }
        }

        // TransactionManagerLookup transactionManagerLookup = TransactionManagerLookupFactory.GetTransactionManagerLookup( properties );

        private static ITransactionFactory CreateTransactionFactory(SessionElement sessionElement)
        {
            return (ITransactionFactory)Activator.CreateInstance(Type.GetType(sessionElement.TransactionFactory));
        }

    }
}

