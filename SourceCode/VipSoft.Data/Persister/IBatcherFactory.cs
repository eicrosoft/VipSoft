 
using VipSoft.Data.Connection;

namespace VipSoft.Data.Persister
{
    /// <summary> Factory for <see cref="IBatcher"/> instances.</summary>
    public interface IBatcherFactory
    {
        IBatcher CreateBatcher(ConnectionManager connectionManager);
    }
}