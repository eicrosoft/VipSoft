using System.Data;
using VipSoft.Data.Connection;
using VipSoft.Data.Persister;
using VipSoft.Data.Transaction;

namespace VipSoft.Data.Config
{
    /// <summary>
    /// Settings that affect the behavior of ORM at runtime.
    /// </summary>
    public sealed class Settings
    {
        public IConnectionProvider ConnectionProvider { get; internal set; }

        public ITransactionFactory TransactionFactory { get; internal set; }

        public IBatcherFactory BatcherFactory { get; internal set; }

        public IsolationLevel IsolationLevel { get; internal set; }

        public int Id { get; internal set; }

        public string SessionFactoryName { get; internal set; }

        public string ConnectionString { get; internal set; }

        public string Driver { get; internal set; }

        public int CommandTimeOut { get; internal set; }

        public bool PrepareSqlEnabled { get; internal set; }

        public ConnectionReleaseMode ConnectionReleaseMode { get; internal set; }

        // public int DefaultBatchFetchSize { get; internal set; }

        public int BatchSize { get; internal set; }

        public bool IsAutoCloseSessionEnabled { get; internal set; }

        // public ICacheProvider CacheProvider { get; internal set; }

        public bool IsStatisticsEnabled { get; internal set; }

    }

    public enum ConnectionReleaseMode
    {
        AfterStatement,
        AfterTransaction,
        OnClose
    }
}
