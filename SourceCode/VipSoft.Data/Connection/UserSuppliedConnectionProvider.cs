﻿using System;
using System.Data;
using VipSoft.Core.Config;

namespace VipSoft.Data.Connection
{
    /// <summary>
    /// An implementation of the <c>IConnectionProvider</c> that simply throws an exception when
    /// a connection is requested.
    /// </summary>
    /// <remarks>
    /// This implementation indicates that the user is expected to supply an ADO.NET connection
    /// </remarks>
    public class UserSuppliedConnectionProvider : ConnectionProvider
    {
        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if this method is called
        /// because the user is responsible for closing <see cref="IDbConnection"/>s.
        /// </summary>
        /// <param name="conn">The <see cref="IDbConnection"/> to clean up.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this method is called.  User is responsible for closing
        /// <see cref="IDbConnection"/>s.
        /// </exception>
        public override void CloseConnection(IDbConnection conn)
        {
            throw new InvalidOperationException("The User is responsible for closing ADO.NET connection.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if this method is called
        /// because the user is responsible for creating <see cref="IDbConnection"/>s.
        /// </summary>
        /// <returns>
        /// No value is returned because an <see cref="InvalidOperationException"/> is thrown.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this method is called.  User is responsible for creating
        /// <see cref="IDbConnection"/>s.
        /// </exception>
        public override IDbConnection GetConnection()
        {
            throw new InvalidOperationException("The user must provide an ADO.NET connection - ORM is not creating it.");
        }

        /// <summary>
        /// Configures the ConnectionProvider with only the Driver class.
        /// </summary>
        /// <param name="settings"></param>
        /// <remarks>
        /// All other settings of the Connection are the responsibility of the User since they configured
        /// ORM to use a Connection supplied by the User.
        /// </remarks>
        public override void Configure(SessionElement settings)
        {
            ConfigureDriver(settings);
        }
    }
}