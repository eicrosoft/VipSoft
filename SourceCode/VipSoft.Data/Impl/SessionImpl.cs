using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using VipSoft.Core.Driver;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data.Config;
using VipSoft.Data.Connection;
using VipSoft.Data.Engine;
using VipSoft.Data.Persister;
using VipSoft.Data.Transaction;

namespace VipSoft.Data.Impl
{
    [Serializable]
    public sealed class SessionImpl : AbstractSessionImpl, ISession
    {

        [NonSerialized]
        private readonly bool autoCloseSessionEnabled;

        private readonly long timestamp;

        private readonly ConnectionManager connectionManager;

        [NonSerialized]
        private readonly ISession rootSession;

        [NonSerialized]
        private readonly ConnectionReleaseMode connectionReleaseMode;

        public ConnectionReleaseMode ConnectionReleaseMode
        {
            get { return connectionReleaseMode; }
        }

        internal SessionImpl(ISessionFactoryImplementor factory)
            : base(factory)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                // temporaryPersistenceContext = new StatefulPersistenceContext(this);
                connectionManager = new ConnectionManager(this, null, ConnectionReleaseMode.AfterTransaction);
                CheckAndUpdateSessionStatus();
            }
        }
        /// <summary>
        /// Constructor used for OpenSession(...) processing, as well as construction
        /// of sessions for GetCurrentSession().
        /// </summary>
        /// <param name="connection">The user-supplied connection to use for this session.</param>
        /// <param name="factory">The factory from which this session was obtained</param>
        /// <param name="timestamp">The timestamp for this session</param>
        /// <param name="autoCloseSessionEnabled">Should we auto close after completion of transaction</param>
        /// <param name="connectionReleaseMode">The mode by which we should release JDBC connections.</param>
        internal SessionImpl(IDbConnection connection, ISessionFactoryImplementor factory, long timestamp, bool autoCloseSessionEnabled, ConnectionReleaseMode connectionReleaseMode)
            : base(factory)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                //if (interceptor == null) throw new AssertionFailure("The interceptor can not be null.");

                rootSession = null;
                this.timestamp = timestamp;
                this.autoCloseSessionEnabled = autoCloseSessionEnabled;
                this.connectionReleaseMode = connectionReleaseMode;
                connectionManager = new ConnectionManager(this, connection, connectionReleaseMode);// new ConnectionManager(this, connection, connectionReleaseMode, interceptor);
                CheckAndUpdateSessionStatus();
            }
        }



        /// <summary>
        /// Constructor used to recreate the Session during the deserialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <remarks>
        /// This is needed because we have to do some checking before the serialization process
        /// begins.  I don't know how to add logic in ISerializable.GetObjectData and have .net
        /// write all of the serializable fields out.
        /// </remarks>
        private SessionImpl(SerializationInfo info, StreamingContext context)
        {
            timestamp = info.GetInt64("timestamp");

            SessionFactoryImpl fact = (SessionFactoryImpl)info.GetValue("factory", typeof(SessionFactoryImpl));
            Factory = fact;
            connectionManager = (ConnectionManager)info.GetValue("connectionManager", typeof(ConnectionManager));
        }

        #region Implements method from Abstract class AbstractSessionImpl

        public override IBatcher Batcher
        {
            get
            {
                CheckAndUpdateSessionStatus();
                return connectionManager.Batcher;
            }
        }

        /// <summary></summary>
        public override long Timestamp
        {
            get { return timestamp; }
        }

        //public override IEntityPersister GetEntityPersister(string entityName, object obj)
        //{
        //    using (new SessionIdLoggingContext(SessionId))
        //    {
        //        CheckAndUpdateSessionStatus();

        //        if ( entityName == null)
        //        {
        //            return Factory.GetEntityPersister(entityName);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}


        public override void CloseSessionFromDistributedTransaction()
        {
            Dispose(true);
        }


        public override void AfterTransactionBegin(ITransaction tx)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                // interceptor.AfterTransactionBegin(tx);
            }
        }

        public override void BeforeTransactionCompletion(ITransaction tx)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                if (rootSession == null)
                {
                    try
                    {
                        // interceptor.BeforeTransactionCompletion(tx);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public ISessionImplementor GetSessionImplementation()
        {
            return this;
        }

        public override object GetContextEntityIdentifier(object obj)
        {
            CheckAndUpdateSessionStatus();
            return null;
        }

        public override ConnectionManager ConnectionManager
        {
            get { return connectionManager; }
        }

        //public override IPersistenceContext PersistenceContext
        //{
        //    get
        //    {
        //        CheckAndUpdateSessionStatus();
        //        return persistenceContext;
        //    }
        //}

        public override bool IsOpen
        {
            get { return !IsClosed; }
        }

        public override bool IsConnected
        {
            get { return connectionManager.IsConnected; }
        }

        /// <summary></summary>
        public IDbConnection Disconnect()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                // log.Debug("disconnecting session");
                return connectionManager.Disconnect();
            }
        }

        public void Reconnect()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                // log.Debug("reconnecting session");
                connectionManager.Reconnect();
            }
        }

        public void Reconnect(IDbConnection conn)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                // log.Debug("reconnecting session");
                connectionManager.Reconnect(conn);
            }
        }

        public bool IsAutoCloseSessionEnabled
        {
            get { return autoCloseSessionEnabled; }
        }

        public override void AfterTransactionCompletion(bool success, ITransaction tx)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                // log.Debug("transaction completion");
                //if (Factory.Statistics.IsStatisticsEnabled)
                //{
                //    Factory.StatisticsImplementor.EndTransaction(success);
                //}

                connectionManager.AfterTransaction();
                //persistenceContext.AfterTransactionCompletion();
                //actionQueue.AfterTransactionCompletion(success);
                //if (rootSession == null)
                //{
                //    try
                //    {
                //        interceptor.AfterTransactionCompletion(tx);
                //    }
                //    catch (Exception t)
                //    {
                //        log.Error("exception in interceptor afterTransactionCompletion()", t);
                //    }
                //}


                //if (autoClear)
                //	Clear();
            }
        }

        public override IDbConnection Connection
        {
            get { return connectionManager.GetConnection(); }
        }

        public override bool TransactionInProgress
        {
            get
            {
                return !IsClosed && Transaction.IsActive;
            }
        }

        #endregion

        #region IDisposable Members

        //private bool _isAlreadyDisposed;

        /// <summary>
        /// Finalizer that ensures the object is correctly disposed of.
        /// </summary>
        ~SessionImpl()
        {
            Dispose(false);
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                if (TransactionContext != null)
                {
                    TransactionContext.ShouldCloseSessionOnDistributedTransactionCompleted = true;
                    return;
                }
                Dispose(true);
            }
        }

        private void Dispose(bool isDisposing)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                if (IsAlreadyDisposed)
                {
                    // don't dispose of multiple times.
                    return;
                }

                // log.Debug(string.Format("[session-id={0}] executing real Dispose({1})", SessionId, isDisposing));

                // free managed resources that are being managed by the session if we
                // know this call came through Dispose()
                if (isDisposing && !IsClosed)
                {
                    Close();
                }

                // free unmanaged resources here

                IsAlreadyDisposed = true;
                // nothing for Finalizer to do - so tell the GC to ignore it
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region Implements method from interface Isession

        public ISessionFactory SessionFactory
        {
            get { return Factory; }
        }

        /// <summary> Get the current ORM transaction.</summary>
        public ITransaction Transaction
        {
            get { return connectionManager.Transaction; }
        }

        //private void Cleanup()
        //{
        //    using (new SessionIdLoggingContext(SessionId))
        //    {
        //        persistenceContext.Clear();
        //    }
        //}
        /// <summary></summary>
        public void Clear()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                //  actionQueue.Clear();
                // persistenceContext.Clear();
            }
        }

        public bool ShouldAutoClose
        {
            get { return IsAutoCloseSessionEnabled && !IsClosed; }
        }

        /// <summary> Close the stateless session and release the ADO.NET connection.</summary>
        public IDbConnection Close()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                //  log.Debug("closing session");
                if (IsClosed)
                {
                    throw new Exception("Session was already closed");
                }

                //if (Factory.Statistics.IsStatisticsEnabled)
                //{
                //    Factory.StatisticsImplementor.CloseSession();
                //}

                try
                {
                    //try
                    //{
                    //    if (childSessionsByEntityMode != null)
                    //    {
                    //        foreach (KeyValuePair<EntityMode, ISession> pair in childSessionsByEntityMode)
                    //        {
                    //            pair.Value.Close();
                    //        }
                    //    }
                    //}
                    //catch
                    //{
                    //    // just ignore
                    //}

                    if (rootSession == null) return connectionManager.Close();
                    else return null;
                }
                finally
                {
                    SetClosed();
                    //Cleanup();
                }
            }
        }

        public int Insert(params IEntity[] entity)
        {
            var result = 0;
            if (entity.Length > 0)
            {
                using (new SessionIdLoggingContext(SessionId))
                {
                    CheckAndUpdateSessionStatus();
                    var persister = Factory.GetEntityPersister(entity[0].GetType().Name);
                    result = persister.Insert(this, entity);

                }
            }
            return result;
        }

        public int InsertWithReturnIdentity(IEntity entity)
        {
            var result = 0;
            if (entity != null)
            {
                using (new SessionIdLoggingContext(SessionId))
                {
                    CheckAndUpdateSessionStatus();
                    var persister = Factory.GetEntityPersister(entity.GetType().Name);
                    result = persister.InsertWithReturnIdentity(this, entity);

                }
            }
            return result;
        }


        /// <summary>Update a entity.</summary>
        /// <param name="entity">a detached entity instance </param>
        public int Update(IEntity entity)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                return Factory.GetEntityPersister(entity.GetType().Name).Update(this, entity);
            }
        }

        /// <summary> Delete a entity. </summary>
        /// <param name="entity">a detached entity instance </param>
        public int Delete(IEntity entity)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                return Factory.GetEntityPersister(entity.GetType().Name).Delete(this, entity);
            }
        }

        public int Delete<T>(params object[] pKeys)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                return Factory.GetEntityPersister(typeof(T).Name).Delete(this, pKeys);
            }
        }

        public int DeleteWithAssociate<T>(params object[] pKeys)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                return Factory.GetEntityPersister(typeof(T).Name).DeleteWithAssociate(this, pKeys);
            }
        }

        public int DeleteWithValidate<T>(params object[] pKeys)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                return Factory.GetEntityPersister(typeof(T).Name).DeleteWithValidate(this, pKeys);
            }
        }

        public T Get<T>(object pKey)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                var result = Factory.GetEntityPersister(typeof(T).Name).Get<T>(this, pKey);
                //temporaryPersistenceContext.Clear();
                return result;
            }
        }

        public T Get<T>(IEntity obj)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                var result = Factory.GetEntityPersister(typeof(T).Name).Get<T>(this, obj);
                //temporaryPersistenceContext.Clear();
                return result;
            }
        }

        public T Get<T>(Criteria criteria)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                var result = Factory.GetEntityPersister(typeof(T).Name).Get<T>(this, criteria);
                //temporaryPersistenceContext.Clear();
                return result;
            }
        }


        public T Get<T>(string sql, CommandType commandType, params DbParameter[] parameters)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                var result = Factory.GetEntityPersister(typeof(T).Name).Get<T>(this, sql, commandType, parameters);
                //temporaryPersistenceContext.Clear();
                return result;
            }
        }

        #region MyRegion

        public List<T> Load<T>()
        {
            List<T> result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).Load<T>(this);
                //temporaryPersistenceContext.Clear();

            }
            return result;
        }

        public List<T> Load<T>(Criteria criteria)
        {
            List<T> result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).Load<T>(this, criteria);
                //temporaryPersistenceContext.Clear();

            }
            return result;
        }

        public List<T> Load<T>(Criteria criteria, Order order)
        {
            List<T> result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).Load<T>(this, criteria, order);
                //temporaryPersistenceContext.Clear();

            }
            return result;
        }

        public List<T> LoadPageList<T>(int pageSize, int dataStart, out int totalItemCount, Criteria criteria, Order order)
        {
            List<T> result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).LoadPageList<T>(this,pageSize,dataStart,out totalItemCount,criteria, order);
                //temporaryPersistenceContext.Clear();

            }
            return result;
        }

        #endregion

        public List<T> Load<T>(IEntity obj)
        {
            List<T> result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).Load<T>(this, obj);
                //temporaryPersistenceContext.Clear();

            }
            return result;
        }

        public List<T> Load<T>(string sql, params DbParameter[] parameters)
        {
            return Load<T>(sql, CommandType.Text, parameters);
        }

        public List<T> Load<T>(string sql, CommandType commandType, params DbParameter[] parameters)
        {
            List<T> result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).Load<T>(this, sql, commandType, parameters);
                // temporaryPersistenceContext.Clear();

            }
            return result;
        }

        public DbParameter[] GetParameters<T>(params string[] columnName)
        {
            return Factory.GetEntityPersister(typeof(T).Name).GetParameters(this, columnName);
        }

        public int ExecuteNonQuery<T>(string sql, params DbParameter[] parameters)
        {
            return ExecuteNonQuery<T>(sql, CommandType.Text, parameters);
        }

        public int ExecuteNonQuery<T>(string sql, CommandType commandType, params DbParameter[] parameters)
        {
            int result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).ExecuteNonQuery(this, sql, commandType, parameters);
                // temporaryPersistenceContext.Clear();
            }
            return result;
        }

        public object ExecuteScalar<T>(string sql, CommandType commandType, params DbParameter[] parameters)
        {
            object result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).ExecuteScalar(this, sql, commandType, parameters);
                //temporaryPersistenceContext.Clear();
            }
            return result;
        }

        public object ExecuteScalar<T>(string sql, params DbParameter[] parameters)
        {
            return ExecuteScalar<T>(sql, CommandType.Text, parameters);
        }
        public DataSet ExecuteDataSet<T>(string sql, CommandType commandType, params DbParameter[] parameters)
        {
            DataSet result;
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
                result = Factory.GetEntityPersister(typeof(T).Name).ExecuteDataSet(this, sql, commandType, parameters);
                //temporaryPersistenceContext.Clear();
            }
            return result;
        }

        public DataTable ExecuteDataTable(string sql, params DbParameter[] parameters)
        {
            return ExecuteDataTable(sql, CommandType.Text, parameters);
        }

        public DataTable ExecuteDataTable(string sql, CommandType commandType, params DbParameter[] parameters)
        {
            DataTable result;
            using (new SessionIdLoggingContext(SessionId))
            {
                var ds = this.Batcher.ExecuteDataSet(this.Batcher.GenerateDataAdapter(sql, commandType, parameters));
                result = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
            }
            return result;
        }


        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                if (rootSession != null)
                {
                    // Todo : should seriously consider not allowing a txn to begin from a child session
                    //      can always route the request to the root session...
                    //log.Warn("Transaction started on non-root session");
                }

                CheckAndUpdateSessionStatus();
                return connectionManager.BeginTransaction(isolationLevel);
            }
        }

        /// <summary> Begin a ORM transaction.</summary>
        public ITransaction BeginTransaction()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                if (rootSession != null)
                {
                    // Todo : should seriously consider not allowing a txn to begin from a child session
                    //      can always route the request to the root session...
                    //log.Warn("Transaction started on non-root session");
                }

                CheckAndUpdateSessionStatus();
                return connectionManager.BeginTransaction();
            }
        }

        #endregion

        public void ManagedClose()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                if (IsClosed)
                {
                    throw new Exception("Session was already closed!");
                }
                ConnectionManager.Close();
                SetClosed();
            }
        }
    }
}
