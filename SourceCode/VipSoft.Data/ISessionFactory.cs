// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISessionFactory.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:5-Nov-2012
// </copyright>
// <summary> 
// A command-oriented API for performing bulk operations against a database. 
// </summary>
// <remarks>
// A stateless session does not implement a first-level cache nor interact with any second-level cache, 
// nor does it implement transactional write-behind or automatic dirty checking,
// nor do operations cascade to associated instances. Collections are
// ignored by a stateless session. 
// </remarks>
// ---
using System;
using System.Data;
using VipSoft.Data.Config;
using VipSoft.Data.Connection;

namespace VipSoft.Data
{
    public interface ISessionFactory : IDisposable
    {
        Settings Settings { get; }

        /// <summary>
        /// Open a <c>ISession</c> on the given connection
        /// </summary>
        /// <param name="conn">A connection provided by the application</param>
        /// <returns>A session</returns>
        /// <remarks>
        /// Note that the second-level cache will be disabled if you supply a ADO.NET connection.
        /// FSORM will not be able to track any statements you might have executed in the same transaction.
        /// Consider implementing your own <see cref="IConnectionProvider" />.
        /// </remarks>
        ISession OpenSession(IDbConnection conn);

        /// <summary>
        /// Create a database connection and open a <c>ISession</c> on it
        /// </summary>
        /// <returns></returns>
        ISession OpenSession();


        /// <summary> Was this <see cref="ISessionFactory"/> already closed?</summary>
        bool IsClosed { get; }

        /// <summary>
        /// Destroy this <c>SessionFactory</c> and release all resources 
        /// connection pools, etc). It is the responsibility of the application
        /// to ensure that there are no open <c>Session</c>s before calling
        /// <c>close()</c>. 
        /// </summary>
        void Close();
    }
}
