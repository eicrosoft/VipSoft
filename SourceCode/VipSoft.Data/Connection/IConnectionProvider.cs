// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICache.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:6-Nov-2012
// </copyright>
// <summary>
// 生成ADO.NET连接以及Command对象的工厂。
// </summary>
// <remarks>     
//它通过抽象将应用从底层的IDbConnection或IDbCommand隔离开。 
//仅供开发者扩展/实现用，并不暴露给应用程序使用。   
// </remarks>
// ---

using System;
using System.Data;
using VipSoft.Core.Config;
using VipSoft.Core.Driver;

namespace VipSoft.Data.Connection
{
    public interface IConnectionProvider : IDisposable
    {
        // <summary>
        // Initialize the connection provider from the given properties.
        // </summary>
        // <param name="settings">The connection provider settings</param>
        void Configure(SessionElement settings);

        // <summary>
        // Dispose of a used <see cref="IDbConnection"/>
        // </summary>
        // <param name="conn">The <see cref="IDbConnection"/> to clean up.</param>
        void CloseConnection(IDbConnection conn);

        // <summary>
        // Gets the <see cref="IDriver"/> this ConnectionProvider should use to 
        // communicate with the .NET Data Provider
        // </summary>
        // <value>
        // The <see cref="IDriver"/> to communicate with the .NET Data Provider.
        // </value>
        IDriver Driver { get; }

        // <summary>
        // Get an open <see cref="IDbConnection"/>.
        // </summary>
        // <returns>An open <see cref="IDbConnection"/>.</returns>
        IDbConnection GetConnection();
    }
}
