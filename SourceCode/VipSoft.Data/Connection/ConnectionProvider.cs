// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionProvider.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:6-Nov-2012
// </copyright>
// <summary>
// The base class for the ConnectionProvider.
// </summary>   
// ---


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VipSoft.Core.Config;
using VipSoft.Core.Driver;

namespace VipSoft.Data.Connection
{
    public abstract class ConnectionProvider : IConnectionProvider
    {
        private string connString;
        private IDriver driver;

        public virtual void Configure(SessionElement settings)
        {
            connString = GetNamedConnectionString(settings);
            ConfigureDriver(settings);
        }

        protected virtual string GetNamedConnectionString(SessionElement settings)
        {
            return settings.ConnectionString;
        }

        protected virtual void ConfigureDriver(SessionElement settings)
        {
            try
            {
                driver = (IDriver)Activator.CreateInstance(Type.GetType(settings.Driver));
                driver.Configure(settings);
            }
            catch (Exception e)
            {
                throw new Exception("Could not create the driver from " + settings.Driver + ".", e);
            }
        }

        protected virtual string ConnectionString
        {
            get { return connString; }
        }

        public virtual void CloseConnection(IDbConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch
            {
                throw new Exception("Could not close " + conn.GetType() + " connection");
            }
        }

        public IDriver Driver
        {
            get { return driver; }
        }

        /// <summary>
        /// Get an open <see cref="IDbConnection"/>.
        /// </summary>
        /// <returns>An open <see cref="IDbConnection"/>.</returns>
        public abstract IDbConnection GetConnection();

        #region IDisposable Members

        /// <summary>
        /// A flag to indicate if <c>Disose()</c> has been called.
        /// </summary>
        private bool isAlreadyDisposed;

        /// <summary>
        /// Finalizer that ensures the object is correctly disposed of.
        /// </summary>
        ~ConnectionProvider()
        {
            Dispose(false);
        }

        /// <summary>
        /// Takes care of freeing the managed and unmanaged resources that 
        /// this class is responsible for.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isAlreadyDisposed)
            {
                // don't dispose of multiple times.
                return;
            }
            // free unmanaged resources here

            isAlreadyDisposed = true;
            // nothing for Finalizer to do - so tell the GC to ignore it
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
