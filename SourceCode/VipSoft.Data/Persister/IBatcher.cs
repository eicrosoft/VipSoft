using System;
using System.Data;
using System.Data.Common;

namespace VipSoft.Data.Persister
{
    /// <summary>
    /// Manages <see cref="IDbCommand"/>s and <see cref="IDataReader"/>s 
    /// for an <see cref="ISession"/>. 
    /// </summary>
    /// <remarks>
    /// <p>
    /// Abstracts ADO.NET batching to maintain the illusion that a single logical batch 
    /// exists for the whole session, even when batching is disabled.
    /// Provides transparent <c>IDbCommand</c> caching.
    /// </p>
    /// <p>
    /// This will be useful once ADO.NET gets support for batching.  Until that point
    /// no code exists that will do batching, but this will provide a good point to do
    /// error checking and making sure the correct number of rows were affected.
    /// </p>
    /// </remarks>
    public interface IBatcher : IDisposable
    {
        /// <summary>
        /// Close a <see cref="IDbCommand"/> opened using <c>PrepareCommand()</c>
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to ensure is closed.</param>
        /// <param name="reader">The <see cref="IDataReader"/> to ensure is closed.</param>
        void CloseCommand(IDbCommand cmd, IDataReader reader);

        /// <summary>
        /// Close a <see cref="IDataReader"/> opened using <see cref="ExecuteReader"/>
        /// </summary>
        /// <param name="reader">The <see cref="IDataReader"/> to ensure is closed.</param>
        void CloseReader(IDataReader reader);

        /// <summary>
        /// Close any query statements that were left lying around
        /// </summary>
        /// <remarks>
        /// Use this method instead of <c>Dispose</c> if the <see cref="IBatcher"/>
        /// can be used again.
        /// </remarks>
        void CloseCommands();

        /// <summary>
        /// Gets an <see cref="IDataReader"/> by calling ExecuteReader on the <see cref="IDbCommand"/>.
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to execute to get the <see cref="IDataReader"/>.</param>
        /// <returns>The <see cref="IDataReader"/> from the <see cref="IDbCommand"/>.</returns>
        /// <remarks>
        /// The Batcher is responsible for ensuring that all of the Drivers rules for how many open
        /// <see cref="IDataReader"/>s it can have are followed.
        /// </remarks>
        IDataReader ExecuteReader(IDbCommand cmd);

        /// <summary>
        /// Gets an <see cref="IDataReader"/> by calling ExecuteReader on the <see cref="IDbCommand"/>.
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to execute to get the <see cref="IDataReader"/>.</param>
        /// <returns>The <see cref="IDataReader"/> from the <see cref="IDbCommand"/>.</returns>
        /// <remarks>
        /// The Batcher is responsible for ensuring that all of the Drivers rules for how many open
        /// <see cref="IDataReader"/>s it can have are followed.
        /// </remarks>
        DataTable ExecuteSchemaTable(IDbCommand cmd);

        /// <summary>
        /// Executes the <see cref="IDbCommand"/>. 
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to execute.</param>
        /// <returns>The number of rows affected.</returns>
        /// <remarks>
        /// The Batcher is responsible for ensuring that all of the Drivers rules for how many open
        /// <see cref="IDataReader"/>s it can have are followed.
        /// </remarks>
        int ExecuteNonQuery(IDbCommand cmd);

        /// <summary>
        /// Executes the <see cref="IDbCommand"/>. 
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to execute.</param>
        /// <returns>The number of rows affected.</returns>
        /// <remarks>
        /// The Batcher is responsible for ensuring that all of the Drivers rules for how many open
        /// <see cref="IDataReader"/>s it can have are followed.
        /// </remarks>
        object ExecuteScalar(IDbCommand cmd);

        /// <summary>
        /// Executes the <see cref="IDbCommand"/>. 
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to execute.</param>
        /// <returns>The number of rows affected.</returns>
        /// <remarks>
        /// The Batcher is responsible for ensuring that all of the Drivers rules for how many open<see cref="IDataReader"/>s it can have are followed.
        /// </remarks>
        DataSet ExecuteDataSet(IDbDataAdapter adapter);

        /// <summary>
        /// Gets the value indicating whether there are any open resources
        /// managed by this batcher (IDbCommands or IDataReaders).
        /// </summary>
        bool HasOpenResources { get; }

        ///// <summary>
        ///// Gets or sets the size of the batch, this can change dynamically by
        ///// calling the session's SetBatchSize.
        ///// </summary>
        ///// <value>The size of the batch.</value>
        //int BatchSize { get; }
         
        IDbCommand Generate(string commandText, CommandType type, DbParameter[] parameters);
        IDbDataAdapter GenerateDataAdapter(string commandText, CommandType type, params DbParameter[] parameters);

    }
}