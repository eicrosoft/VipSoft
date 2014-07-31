using System;
using System.Data;
using VipSoft.Data.Connection;
using VipSoft.Data.Persister;
using VipSoft.Data.Transaction;

namespace VipSoft.Data.Engine
{
    /// <summary>
    /// Defines the internal contract between the <c>Session</c> and other parts of ORM
    /// such as implementors of <c>Type</c> or <c>ClassPersister</c>
    /// </summary>
    public interface ISessionImplementor
    {
        /// <summary>
        /// Initialize the session after its construction was complete
        /// </summary>
        void Initialize();


        /// <summary>
        /// Get the creating SessionFactoryImplementor
        /// </summary>
        /// <returns></returns>
        ISessionFactoryImplementor Factory { get; }

        /// <summary>
        /// Get the prepared statement <c>Batcher</c> for this session
        /// </summary>
        IBatcher Batcher { get; }
        
        /// <summary>
        /// System time before the start of the transaction
        /// </summary>
        /// <returns></returns>
        long Timestamp { get; }
        
      //  IInterceptor Interceptor { get; }


        /// <summary>
        /// Notify the session that an ORM transaction has begun.
        /// </summary>
        void AfterTransactionBegin(ITransaction tx);

        /// <summary>
        /// Notify the session that the transaction is about to complete
        /// </summary>
        void BeforeTransactionCompletion(ITransaction tx);

        /// <summary>
        /// Notify the session that the transaction completed, so we no longer own the old locks.
        /// (Also we should release cache softlocks). May be called multiple times during the transaction
        /// completion process.
        /// </summary>
        void AfterTransactionCompletion(bool successful, ITransaction tx);

        /// <summary>
        /// Return the identifier of the persistent object, or null if transient
        /// </summary>
        object GetContextEntityIdentifier(object obj);
    

        ConnectionManager ConnectionManager { get; }

        ///// <summary> Get the persistence context for this session</summary>
        //IPersistenceContext PersistenceContext { get; }

        /// <summary>
        /// Is the <c>ISession</c> still open?
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Is the <c>ISession</c> currently connected?
        /// </summary>
        bool IsConnected { get; }

        IDbConnection Connection { get; }

        /// <summary> Determine whether the session is closed.  Provided separately from
        /// {@link #isOpen()} as this method does not attempt any JTA synch
        /// registration, where as {@link #isOpen()} does; which makes this one
        /// nicer to use for most internal purposes. 
        /// </summary>
        /// <returns> True if the session is closed; false otherwise.
        /// </returns>
        bool IsClosed { get; }


        /// <summary> 
        /// Does this <tt>Session</tt> have an active FSORM transaction
        /// or is there a JTA transaction in progress?
        /// </summary>
        bool TransactionInProgress { get; }


        Guid SessionId { get; }

        ITransactionContext TransactionContext { get; set; }

        void CloseSessionFromDistributedTransaction();
    }
}
