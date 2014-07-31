using VipSoft.Data.Connection;

namespace VipSoft.Data.Persister
{
    /// <summary> 
    /// A BatcherFactory implementation which constructs Batcher instances
    /// that do not perform batch operations. 
    /// </summary>
    public class BatcherFactory : IBatcherFactory
    {
        public virtual IBatcher CreateBatcher(ConnectionManager connectionManager)
        {
            return new Batcher(connectionManager);
        }
    }
}