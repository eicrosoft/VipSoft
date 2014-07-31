// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionProviderFactory.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:6-Nov-2012
// </copyright>
// <summary>
// A strategy for obtaining ADO.NET <see cref="IDbConnection"/>.
// </summary>
// <remarks>
// The base class for the ConnectionProvider.
// </remarks>
// ---

using System;
using VipSoft.Core.Config;    

namespace VipSoft.Data.Connection
{
    /// <summary>
    /// Instanciates a connection provider given configuration properties.
    /// </summary>
    public class ConnectionProviderFactory
    {

        // cannot be instantiated
        private ConnectionProviderFactory()
        {
            throw new InvalidOperationException("ConnectionProviderFactory can not be instantiated.");
        }

        public static IConnectionProvider NewConnectionProvider(SessionElement settings)
        {
            IConnectionProvider connectionProvider;
            string providerClass = settings.ConnectionProvider;
            if (!string.IsNullOrEmpty(providerClass))
            {
                try
                {
                    connectionProvider = (IConnectionProvider)Activator.CreateInstance(Type.GetType(providerClass));
                }
                catch (Exception e)
                {

                    throw new Exception("Could not instantiate connection provider: " + providerClass, e);
                }
            }
            else if (!string.IsNullOrEmpty(settings.ConnectionString))
            {
                connectionProvider = new DriverConnectionProvider();
            }
            else
            {
                connectionProvider = new UserSuppliedConnectionProvider();
            }
            connectionProvider.Configure(settings);
            return connectionProvider;
        }
    }
}