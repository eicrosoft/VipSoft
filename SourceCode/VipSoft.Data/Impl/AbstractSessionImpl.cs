using System;
using System.Data;
using VipSoft.Data.Connection;
using VipSoft.Data.Engine;
using VipSoft.Data.Persister;
using VipSoft.Data.Transaction;

namespace VipSoft.Data.Impl
{
    /// <summary> Functionality common to stateless and stateful sessions </summary>
    [Serializable]
    public abstract class AbstractSessionImpl : ISessionImplementor
    {
        [NonSerialized]
        private ISessionFactoryImplementor factory;

        private readonly Guid sessionId = Guid.NewGuid();
        private bool closed;

        private bool isAlreadyDisposed;

        internal AbstractSessionImpl() { }

        protected internal AbstractSessionImpl(ISessionFactoryImplementor factory)
        {
            this.factory = factory;
        }

        protected internal virtual void CheckAndUpdateSessionStatus()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                ErrorIfClosed();
                EnlistInAmbientTransactionIfNeeded();
            }
        }

        protected internal virtual void ErrorIfClosed()
        {
            if (IsClosed || IsAlreadyDisposed)
            {
                throw new ObjectDisposedException("ISession", "Session is closed!");
            }
        }

        protected bool IsAlreadyDisposed
        {
            get { return isAlreadyDisposed; }
            set { isAlreadyDisposed = value; }
        }

        protected internal void SetClosed()
        {
            try
            {
                if (TransactionContext != null) TransactionContext.Dispose();
            }
            catch
            {
               
            }
            closed = true;
        }

        protected void AfterOperation(bool success)
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                if (!ConnectionManager.IsInActiveTransaction)
                {
                    ConnectionManager.AfterNonTransactionalQuery(success);
                }
            }
        }

        protected void EnlistInAmbientTransactionIfNeeded()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                factory.TransactionFactory.EnlistInDistributedTransactionIfNeeded(this);
            }
        }

        #region ISessionImplementor Members

        public ITransactionContext TransactionContext { get; set; }

        public Guid SessionId
        {
            get { return sessionId; }
        }
        public void Initialize()
        {
            using (new SessionIdLoggingContext(SessionId))
            {
                CheckAndUpdateSessionStatus();
            }
        }
        public ISessionFactoryImplementor Factory
        {
            get { return factory; }
            protected set { factory = value; }
        }

        public abstract IBatcher Batcher { get; }
       // public abstract IInterceptor Interceptor { get; }
        public abstract void CloseSessionFromDistributedTransaction();
        public abstract void AfterTransactionBegin(ITransaction tx);
        public abstract void BeforeTransactionCompletion(ITransaction tx);
        public abstract void AfterTransactionCompletion(bool successful, ITransaction tx);
       // public abstract IEntityPersister GetEntityPersister(string entityName, object obj);
        public abstract object GetContextEntityIdentifier(object obj);
        //public abstract IPersistenceContext PersistenceContext { get; }
        public abstract ConnectionManager ConnectionManager { get; }
        public abstract bool IsOpen { get; }
        public abstract bool IsConnected { get; }
        public abstract IDbConnection Connection { get; }
        public bool IsClosed
        {
            get { return closed; }
        }
        public abstract bool TransactionInProgress { get; }
       
        public abstract long Timestamp { get; }

        #endregion
    }
}
