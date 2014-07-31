using System;
using System.Collections;
using System.Data;
using VipSoft.Data.Engine;
using log4net;

namespace VipSoft.Data.Transaction
{
	public class AdoNetTransactionFactory : ITransactionFactory
	{
		private readonly ILog isolaterLog = LogManager.GetLogger(typeof(Isolater));

		public ITransaction CreateTransaction(ISessionImplementor session)
		{
			return new AdoTransaction(session);
		}

		public void EnlistInDistributedTransactionIfNeeded(ISessionImplementor session)
		{
			// nothing need to do here, we only support local transactions with this factory
		}

		public bool IsInDistributedActiveTransaction(ISessionImplementor session)
		{
			return false;
		}

		public void ExecuteWorkInIsolation(ISessionImplementor session, IIsolatedWork work, bool transacted)
		{
			IDbConnection connection = null;
			IDbTransaction trans = null;
			// bool wasAutoCommit = false;
			try
			{
				// We make an exception for SQLite and use the session's connection,
				// since SQLite only allows one connection to the database.
                //if (session.Factory.Dialect is SQLiteDialect)
                //    connection = session.Connection;
                //else
					connection = session.Factory.ConnectionProvider.GetConnection();

				if (transacted)
				{
					trans = connection.BeginTransaction();
					// TODO NH: a way to read the autocommit state is needed
					//if (TransactionManager.GetAutoCommit(connection))
					//{
					//  wasAutoCommit = true;
					//  TransactionManager.SetAutoCommit(connection, false);
					//}
				}

				work.DoWork(connection, trans);

				if (transacted)
				{
					trans.Commit();
					//TransactionManager.Commit(connection);
				}
			}
			catch 
			{
				try
				{
					if (trans != null && connection.State != ConnectionState.Closed)
					{
						trans.Rollback();
					}
				}
				catch (Exception ignore)
				{
					isolaterLog.Debug("unable to release connection on exception [" + ignore + "]");
				}
			}
			finally
			{
				//if (transacted && wasAutoCommit)
				//{
				//  try
				//  {
				//    // TODO NH: reset autocommit
				//    // TransactionManager.SetAutoCommit(connection, true);
				//  }
				//  catch (Exception)
				//  {
				//    log.Debug("was unable to reset connection back to auto-commit");
				//  }
				//}
				//if (session.Factory.Dialect is SQLiteDialect == false) 
                    session.Factory.ConnectionProvider.CloseConnection(connection);
			}
		}

		public void Configure(IDictionary props)
		{
		}
	}
}