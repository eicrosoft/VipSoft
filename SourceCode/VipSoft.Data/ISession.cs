
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISession.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:5-Nov-2012
// </copyright>
// <summary>
/// The main runtime interface between a .NET application and NHibernate. This is the central
/// API class abstracting the notion of a persistence service.
 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data.Engine;
using VipSoft.Data.Transaction;

namespace VipSoft.Data
{
    /// <summary> 
    /// A command-oriented API for performing bulk operations against a database. 
    /// </summary>
    /// <remarks>
    /// A stateless session does not implement a first-level cache nor interact with any second-level cache, 
    /// nor does it implement transactional write-behind or automatic dirty checking,
    /// nor do operations cascade to associated instances. Collections are
    /// ignored by a stateless session. 
    /// </remarks>
    public interface ISession : IDisposable
    {
        ISessionFactory SessionFactory { get; }

        /// <summary>
        /// Gets the session implementation.
        /// </summary>
        /// <remarks>
        /// This method is provided in order to get the <b>FSORM</b> implementation of the session from wrapper implementions.
        /// Implementors of the <seealso cref="ISession"/> interface should return the FSORM implementation of this method.
        /// </remarks>
        /// <returns>
        /// An FSORM implementation of the <seealso cref="ISessionImplementor"/> interface 
        /// </returns>
        ISessionImplementor GetSessionImplementation();

        /// <summary> 
        /// Returns the current ADO.NET connection associated with this instance.
        /// </summary>
        /// <remarks>
        /// If the session is using aggressive connection release (as in a
        /// CMT environment), it is the application's responsibility to
        /// close the connection returned by this call. Otherwise, the
        /// application should not close the connection.
        /// </remarks>
        IDbConnection Connection { get; }

        /// <summary>
        /// Disconnect the <c>ISession</c> from the current ADO.NET connection.
        /// </summary>
        /// <remarks>
        /// If the connection was obtained by FSORM, close it or return it to the connection
        /// pool. Otherwise return it to the application. This is used by applications which require
        /// long transactions.
        /// </remarks>
        /// <returns>The connection provided by the application or <see langword="null" /></returns>
        IDbConnection Disconnect();

        /// <summary>
        /// Obtain a new ADO.NET connection.
        /// </summary>
        /// <remarks>
        /// This is used by applications which require long transactions
        /// </remarks>
        void Reconnect();

        /// <summary>
        /// Reconnect to the given ADO.NET connection.
        /// </summary>
        /// <remarks>This is used by applications which require long transactions</remarks>
        /// <param name="connection">An ADO.NET connection</param>
        void Reconnect(IDbConnection connection);

        /// <summary> Begin a transaction.</summary>
        ITransaction BeginTransaction();

        /// <summary> Get the current transaction.</summary>
        ITransaction Transaction { get; }

        /// <summary> Close the session and release the ADO.NET connection.</summary>
        IDbConnection Close();

        /// <summary> Insert a entity.</summary>
        /// <param name="entity">A new entity instance </param>
        /// <returns> the identifier of the instance </returns>
        int Insert(params IEntity[] entity);

        /// <summary>
        ///  Insert a entity.
        /// </summary>
        /// <param name="entity">A new transient instance</param>
        /// <returns>the paimarykey of the instance</returns>
        int InsertWithReturnIdentity(IEntity entity);

        /// <summary> Update a entity.</summary>
        /// <param name="entity">A new entity instance</param>
        int Update(IEntity entity);

        /// <summary> Delete a entity. </summary>
        /// <param name="entity">A new entity instance</param>
        int Delete(IEntity entity);

        /// <summary>
        /// Delete entities by primary keys
        /// </summary>
        /// <typeparam name="T">The primary keys</typeparam>
        /// <param name="pKeys"></param>
        /// <returns></returns>
        int Delete<T>(params int[] pKeys);

        /// <summary>
        /// Delete entities with associate talbe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pKeys"></param>
        /// <returns></returns>
        int DeleteWithAssociate<T>(params int[] pKeys);

        /// <summary>
        /// Delete entities with check whether the entities used by associate table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pKeys"></param>
        /// <returns></returns>
        int DeleteWithValidate<T>(params int[] pKeys);

        /// <summary>
        /// Retrieve an entity by primary key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pKey"></param>
        /// <returns> a detached entity instance</returns>
        T Get<T>(int pKey);

        /// <summary> Retrieve an entity. </summary>
        /// <returns> a detached entity instance </returns>
        T Get<T>(IEntity obj);
        T Get<T>(Criteria criteria);

        /// <summary>
        /// Retrieve an entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Get<T>(string sql, CommandType commandType, params DbParameter[] parameters);

        /// <summary>
        /// Get a list of entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<T> Load<T>(IEntity obj);

        List<T> Load<T>();

        List<T> Load<T>(Criteria criteria);
        List<T> Load<T>(Criteria criteria,Order order);



        DataTable ExecuteDataTable(string sql, params DbParameter[] parameters);
        DataTable ExecuteDataTable(string sql, CommandType commandType, params DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>           
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> Load<T>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> Load<T>(string sql, CommandType commandType, params DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery<T>(string sql, CommandType commandType, params DbParameter[] parameters);

        int ExecuteNonQuery<T>(string sql, params DbParameter[] parameters);
         
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar<T>(string sql, CommandType commandType, params DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet<T>(string sql, CommandType commandType, params DbParameter[] parameters);

        /// <summary>
        /// Get parameters for cache by column name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <returns></returns>
        DbParameter[] GetParameters<T>(params string[] columnName);

    }
}
