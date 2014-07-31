using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VipSoft.Data.Driver;

namespace VipSoft.Data.Connection
{
    /// <summary>
    /// A ConnectionProvider that uses an IDriver to create connections.
    /// </summary>
    public class DriverConnectionProvider : ConnectionProvider
    {  
        /// <summary>
        /// Closes and Disposes of the <see cref="IDbConnection"/>.
        /// </summary>
        /// <param name="conn">The <see cref="IDbConnection"/> to clean up.</param>
        public override void CloseConnection(IDbConnection conn)
        {
            base.CloseConnection(conn);
            conn.Dispose();
        }

        /// <summary>
        /// Gets a new open <see cref="IDbConnection"/> through 
        /// the <see cref="IDriver"/>.
        /// </summary>
        /// <returns>
        /// An Open <see cref="IDbConnection"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// If there is any problem creating or opening the <see cref="IDbConnection"/>.
        /// </exception>
        public override IDbConnection GetConnection()
        {                                                            
            IDbConnection conn = Driver.CreateConnection();
            try
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
            }
            catch (Exception)
            {
                conn.Dispose();
                throw;
            }

            return conn;
        }
    }
}