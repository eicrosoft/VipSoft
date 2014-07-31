using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using VipSoft.Core.Driver;
using VipSoft.Data.Connection;
using VipSoft.Data.Driver;
using VipSoft.Data.Engine;
using Iesi.Collections.Generic;
using log4net;                     

namespace VipSoft.Data.Persister
{
    /// <summary>
    /// Manages prepared statements. Class exists to enforce separation of concerns
    /// </summary>
    public class Batcher : IBatcher
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Batcher));

        private static int openCommandCount;
        private static int openReaderCount;

        private readonly ConnectionManager connectionManager;
        private readonly ISessionFactoryImplementor factory;

        private readonly Iesi.Collections.Generic.ISet<IDbCommand> commandsToClose = new HashedSet<IDbCommand>();
        private readonly Iesi.Collections.Generic.ISet<IDataReader> readersToClose = new HashedSet<IDataReader>();
        private readonly IDictionary<IDataReader, Stopwatch> readersDuration = new Dictionary<IDataReader, Stopwatch>();

        private bool releasing;


        /// <summary>
        /// Initializes a new instance of the <see cref="Batcher"/> class.
        /// </summary>
        /// <param name="connectionManager">The <see cref="ConnectionManager"/> owning this batcher.</param>
        public Batcher(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            factory = connectionManager.Factory;
        }

        private IDriver Driver
        {
            get { return factory.ConnectionProvider.Driver; }
        }


        public IDbCommand Generate(string commandText, CommandType type, DbParameter[] parameters)
        {
            IDbCommand cmd = factory.ConnectionProvider.Driver.GenerateCommand(commandText, type, parameters);
            LogOpenPreparedCommand();
            commandsToClose.Add(cmd);
            return cmd;
        }


        public IDbDataAdapter GenerateDataAdapter(string commandText, CommandType type, params DbParameter[] parameters)
        {
            IDbDataAdapter cmd = factory.ConnectionProvider.Driver.GenerateDataAdapter(commandText, type, parameters);
            LogOpenPreparedCommand();
            return cmd;
        }

        /// <summary>
        /// Prepares the <see cref="IDbCommand"/> for execution in the database.
        /// </summary>
        /// <remarks>
        /// This takes care of hooking the <see cref="IDbCommand"/> up to an <see cref="IDbConnection"/>
        /// and <see cref="IDbTransaction"/> if one exists.  It will call <c>Prepare</c> if the Driver
        /// supports preparing commands.
        /// </remarks>
        protected void Prepare(IDbCommand cmd)
        {
            try
            {
                IDbConnection sessionConnection = connectionManager.GetConnection();

                if (cmd.Connection != null)
                {
                    // make sure the commands connection is the same as the Sessions connection
                    // these can be different when the session is disconnected and then reconnected
                    if (cmd.Connection != sessionConnection)
                    {
                        cmd.Connection = sessionConnection;
                    }
                }
                else
                {
                    cmd.Connection = sessionConnection;
                }

                connectionManager.Transaction.Enlist(cmd);
                Driver.PrepareCommand(cmd);
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        /// <summary>
        /// Prepares the <see cref="IDbCommand"/> for execution in the database.
        /// </summary>
        /// <remarks>
        /// This takes care of hooking the <see cref="IDbCommand"/> up to an <see cref="IDbConnection"/>
        /// and <see cref="IDbTransaction"/> if one exists.  It will call <c>Prepare</c> if the Driver
        /// supports preparing commands.
        /// </remarks>
        protected void Prepare(IDbDataAdapter adapter)
        {
            try
            {
                IDbConnection sessionConnection = connectionManager.GetConnection();

                if (adapter.SelectCommand.Connection != null)
                {
                    // make sure the commands connection is the same as the Sessions connection
                    // these can be different when the session is disconnected and then reconnected
                    if (adapter.SelectCommand.Connection != sessionConnection)
                    {
                        adapter.SelectCommand.Connection = sessionConnection;
                    }
                }
                else
                {
                    adapter.SelectCommand.Connection = sessionConnection;
                }

                connectionManager.Transaction.Enlist(adapter.SelectCommand);
                Driver.PrepareCommand(adapter.SelectCommand);
            }
            catch
            {
                throw new Exception("While preparing  an error occurred");
            }
        }


        public int ExecuteNonQuery(IDbCommand cmd)
        {
            CheckReaders();
            Prepare(cmd);
            Stopwatch duration = null;
            if (log.IsDebugEnabled)
                duration = Stopwatch.StartNew();
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.Data["actual-sql-query"] = cmd.CommandText;
                log.Error("Could not execute command: " + cmd.CommandText, e);
                throw;
            }
            finally
            {
                if (log.IsDebugEnabled && duration != null)
                    log.DebugFormat("ExecuteNonQuery took {0} ms", duration.ElapsedMilliseconds);
            }
        }

        public IDataReader ExecuteReader(IDbCommand cmd)
        {
            CheckReaders();
            Prepare(cmd);
            Stopwatch duration = null;
            if (log.IsDebugEnabled) duration = Stopwatch.StartNew();
            IDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                e.Data["actual-sql-query"] = cmd.CommandText;
                log.Error("Could not execute query: " + cmd.CommandText, e);
                throw;
            }
            finally
            {
                if (log.IsDebugEnabled && duration != null && reader != null)
                {
                    log.DebugFormat("ExecuteReader took {0} ms", duration.ElapsedMilliseconds);
                    readersDuration[reader] = duration;
                }
            }

            if (!factory.ConnectionProvider.Driver.SupportsMultipleOpenReaders)
            {
                reader = new NHybridDataReader(reader);
            }

            readersToClose.Add(reader);
            LogOpenReader();
            return reader;
        }

        public DataTable ExecuteSchemaTable(IDbCommand cmd)
        {
            CheckReaders();
            Prepare(cmd);
            DataTable result;
            IDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();// (CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly);
                result = reader.GetSchemaTable();
            }
            finally
            {
                //if (reader != null) reader.Close();
            }
            CloseReader(reader);
            // readersToClose.Add(reader);
            // commandsToClose.Add(cmd);
            return result;
        }

        public object ExecuteScalar(IDbCommand cmd)
        {
            CheckReaders();
            Prepare(cmd);
            Stopwatch duration = null;
            if (log.IsDebugEnabled) duration = Stopwatch.StartNew();
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                e.Data["actual-sql-query"] = cmd.CommandText;
                log.Error("Could not execute command: " + cmd.CommandText, e);
                throw;
            }
            finally
            {
                if (log.IsDebugEnabled && duration != null)
                    log.DebugFormat("ExecuteNonQuery took {0} ms", duration.ElapsedMilliseconds);
            }
        }

        public DataSet ExecuteDataSet(IDbDataAdapter adapter)
        {
            CheckReaders();
            Prepare(adapter);
            DataSet result;
            try
            {
                result = new DataSet();
                adapter.Fill(result);
            }
            catch
            {
                log.Error("Could not execute command: " + adapter.SelectCommand.CommandText);
                throw new Exception("Could not execute command: " + adapter.SelectCommand.CommandText);                
            }
            return result;
        } 

        /// <summary>
        /// Ensures that the Driver's rules for Multiple Open DataReaders are being followed.
        /// </summary>
        protected void CheckReaders()
        {
            // early exit because we don't need to move an open IDataReader into memory
            // since the Driver supports mult open readers.
            if (factory.ConnectionProvider.Driver.SupportsMultipleOpenReaders)
            {
                return;
            }

            foreach (NHybridDataReader reader in readersToClose)
            {
                reader.ReadIntoMemory();
            }
        }

        public void CloseCommands()
        {
            releasing = true;
            try
            {
                foreach (IDataReader reader in new HashedSet<IDataReader>(readersToClose))
                {
                    try
                    {
                        CloseReader(reader);
                    }
                    catch (Exception e)
                    {
                        log.Warn("Could not close IDataReader", e);
                    }
                }

                foreach (IDbCommand cmd in commandsToClose)
                {
                    try
                    {
                        CloseCommand(cmd);
                    }
                    catch (Exception e)
                    {
                        // no big deal
                        log.Warn("Could not close ADO.NET Command", e);
                    }
                }
                commandsToClose.Clear();
            }
            finally
            {
                releasing = false;
            }
        }

        private void CloseCommand(IDbCommand cmd)
        {
            try
            {
                // no equiv to the java code in here
                cmd.Dispose();
                LogClosePreparedCommand();
            }
            catch (Exception e)
            {
                log.Warn("exception clearing maxRows/queryTimeout", e);
                return; // NOTE: early exit!
            }
            finally
            {
                if (!releasing)
                {
                    connectionManager.AfterStatement();
                }
            }

            //if (lastQuery == cmd)
            //{
            //    lastQuery = null;
            //}
        }

        public void CloseCommand(IDbCommand st, IDataReader reader)
        {
            commandsToClose.Remove(st);
            try
            {
                CloseReader(reader);
            }
            finally
            {
                CloseCommand(st);
            }
        }

        public void CloseReader(IDataReader reader)
        {
            /* This method was added because PrepareCommand don't really prepare the command
             * with its connection. 
             * In some case we need to manage a reader outsite the command scope. 
             * To do it we need to use the Batcher.ExecuteReader and then we need something
             * to close the opened reader.
             */
            // TODO NH: Study a way to use directly IDbCommand.ExecuteReader() outsite the batcher
            // An example of it's use is the management of generated ID.
            if (reader == null) return;

            ResultSetWrapper rsw = reader as ResultSetWrapper;
            var actualReader = rsw == null ? reader : rsw.Target;
            readersToClose.Remove(actualReader);
            reader.Dispose();
            LogCloseReader();

            if (!log.IsDebugEnabled) return;

            var nhReader = actualReader as NHybridDataReader;
            actualReader = nhReader == null ? actualReader : nhReader.Target;

            Stopwatch duration;
            if (readersDuration.TryGetValue(actualReader, out duration) == false)
                return;
            readersDuration.Remove(actualReader);
            log.DebugFormat("DataReader was closed after {0} ms", duration.ElapsedMilliseconds);
        }


        ///// <summary>
        ///// Gets or sets the size of the batch, this can change dynamically by
        ///// calling the session's SetBatchSize.
        ///// </summary>
        ///// <value>The size of the batch.</value>
        //public int BatchSize
        //{
        //    get { return 1; }
        //}

        /// <summary>
        /// Gets the <see cref="ISessionFactoryImplementor"/> the Batcher was
        /// created in.
        /// </summary>
        /// <value>
        /// The <see cref="ISessionFactoryImplementor"/> the Batcher was
        /// created in.
        /// </value>
        protected ISessionFactoryImplementor Factory
        {
            get { return factory; }
        }

        /// <summary>
        /// Gets the <see cref="ConnectionManager"/> for this batcher.
        /// </summary>
        protected ConnectionManager ConnectionManager
        {
            get { return connectionManager; }
        }

        protected void LogCommand(IDbCommand command)
        {
            // factory.Settings.SqlStatementLogger.LogCommand(command, FormatStyle.Basic);
        }

        private void LogOpenPreparedCommand()
        {
            if (log.IsDebugEnabled)
            {
                int currentOpenCommandCount = Interlocked.Increment(ref openCommandCount);
                log.Debug("Opened new IDbCommand, open IDbCommands: " + currentOpenCommandCount);
            }

            //if (factory.Statistics.IsStatisticsEnabled)
            //{
            //    factory.StatisticsImplementor.PrepareStatement();
            //}
        }

        private void LogClosePreparedCommand()
        {
            if (log.IsDebugEnabled)
            {
                int currentOpenCommandCount = Interlocked.Decrement(ref openCommandCount);
                log.Debug("Closed IDbCommand, open IDbCommands: " + currentOpenCommandCount);
            }

            //if (factory.Statistics.IsStatisticsEnabled)
            //{
            //    factory.StatisticsImplementor.CloseStatement();
            //}
        }

        private static void LogOpenReader()
        {
            if (log.IsDebugEnabled)
            {
                int currentOpenReaderCount = Interlocked.Increment(ref openReaderCount);
                log.Debug("Opened IDataReader, open IDataReaders: " + currentOpenReaderCount);
            }
        }

        private static void LogCloseReader()
        {
            if (log.IsDebugEnabled)
            {
                int currentOpenReaderCount = Interlocked.Decrement(ref openReaderCount);
                log.Debug("Closed IDataReader, open IDataReaders :" + currentOpenReaderCount);
            }
        }


        public bool HasOpenResources
        {
            get { return commandsToClose.Count > 0 || readersToClose.Count > 0; }
        }


        #region IDisposable Members

        /// <summary>
        /// A flag to indicate if <c>Dispose()</c> has been called.
        /// </summary>
        private bool _isAlreadyDisposed;

        /// <summary>
        /// Finalizer that ensures the object is correctly disposed of.
        /// </summary>
        ~Batcher()
        {
            // Don't log in the finalizer, it causes problems
            // if the output stream is finalized before the batcher.
            //log.Debug( "running BatcherImpl.Dispose(false)" );
            Dispose(false);
        }

        /// <summary>
        /// Takes care of freeing the managed and unmanaged resources that 
        /// this class is responsible for.
        /// </summary>
        public void Dispose()
        {
            log.Debug("running BatcherImpl.Dispose(true)");
            Dispose(true);
        }

        /// <summary>
        /// Takes care of freeing the managed and unmanaged resources that 
        /// this class is responsible for.
        /// </summary>
        /// <param name="isDisposing">Indicates if this BatcherImpl is being Disposed of or Finalized.</param>
        /// <remarks>
        /// If this BatcherImpl is being Finalized (<c>isDisposing==false</c>) then make sure not
        /// to call any methods that could potentially bring this BatcherImpl back to life.
        /// </remarks>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isAlreadyDisposed)
            {
                // don't dispose of multiple times.
                return;
            }

            // free managed resources that are being managed by the AdoTransaction if we
            // know this call came through Dispose()
            if (isDisposing)
            {
                CloseCommands();
            }

            // free unmanaged resources here

            _isAlreadyDisposed = true;
            // nothing for Finalizer to do - so tell the GC to ignore it
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}