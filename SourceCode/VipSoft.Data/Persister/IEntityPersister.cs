using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using VipSoft.Core.Driver;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data.Engine;

namespace VipSoft.Data.Persister
{
    /// <summary>
    /// Concrete <c>IEntityPersister</c>s implement mapping and persistence logic for a particular class.
    /// </summary>
    /// <remarks>
    /// Implementors must be threadsafe (preferably immutable) and must provide a constructor of type
    /// matching the signature of: (PersistentClass, SessionFactoryImplementor)
    /// </remarks>
    public interface IEntityPersister
    {
        ISessionFactoryImplementor Factory { get; }

        IDriver Driver { get; }

        ClassMetadata ClassMetadata { get; }

        void SetDbParamter(ISessionImplementor session, List<DbParameter> paramters, string columnName, object vaule);

        /// <summary>
        /// 得到表的参数类型
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        IDictionary<string, DbParameter> GetParameter(ISessionImplementor session);

        DbParameter[] GetParameters(ISessionImplementor session, params string[] columnName);

        /// <summary>
        /// Persist an instance, using a natively generated identifier (optional operation)
        /// </summary>
        int Insert(ISessionImplementor session, params IEntity[] obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        int InsertWithReturnIdentity(ISessionImplementor session, IEntity obj);
        
        /// <summary>
        /// Delete a persistent instance
        /// </summary>
        int Delete(ISessionImplementor session, IEntity obj);

        /// <summary>
        /// Delete a persistent instance
        /// </summary>
        int Delete(ISessionImplementor session, params int[] pKeys);

        int DeleteWithAssociate(ISessionImplementor session, params int[] pKeys);

        int DeleteWithValidate(ISessionImplementor session, params int[] pKeys);

        /// <summary>
        /// Update a persistent instance
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="session">The session.</param>
        int Update(ISessionImplementor session, IEntity obj);

        T Get<T>(ISessionImplementor session, int id);

        T Get<T>(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters);

        /// <summary>
        /// Load an instance of the persistent class.
        /// </summary>
        T Get<T>(ISessionImplementor session, IEntity model);
        T Get<T>(ISessionImplementor session, Criteria criteria);


        #region MyRegion
        
        List<T> Load<T>(ISessionImplementor session);
        List<T> Load<T>(ISessionImplementor session,Criteria criteria);
        List<T> Load<T>(ISessionImplementor session, Criteria criteria,Order order);                                      
        #endregion

        List<T> Load<T>(ISessionImplementor session, IEntity model);

        List<T> Load<T>(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters);

        int ExecuteNonQuery(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters);

        object ExecuteScalar(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters);

        DataSet ExecuteDataSet(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters);
    }
}
