using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using VipSoft.Core.Cache;
using VipSoft.Core.Driver;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data.Engine;
using VipSoft.FastReflection;
using log4net;

namespace VipSoft.Data.Persister
{
    public class EntityPersister : IEntityPersister
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(EntityPersister));

        private readonly ISessionFactoryImplementor factory;

        private readonly ClassMetadata classMetadata;

        private readonly IDriver driver;

        private readonly string parameterCacheKey;

        public EntityPersister(ClassMetadata persistentClass, ISessionFactoryImplementor sessionFactoryImplementor)
        {
            factory = sessionFactoryImplementor;
            driver = sessionFactoryImplementor.ConnectionProvider.Driver;
            classMetadata = persistentClass;
            parameterCacheKey = string.Format("{0}_{1}_PARAMETER_CACHE_KEY", factory.Settings.SessionFactoryName, classMetadata.TableName);
        }

        public ISessionFactoryImplementor Factory { get { return factory; } }

        public IDriver Driver { get { return driver; } }

        public virtual ClassMetadata ClassMetadata { get { return classMetadata; } }

        public void SetDbParamter(ISessionImplementor session, List<DbParameter> paramters, string columnName, object vaule)
        {
            DbParameter result;
            GetParameter(session).TryGetValue(columnName, out result);
            if (result == null) return;
            var p = (DbParameter)((ICloneable)(result)).Clone();
            p.Size = result.Size;
            p.Value = vaule ?? DBNull.Value;
            paramters.Add(p);
        }

        public int Insert(ISessionImplementor session, params IEntity[] list)
        {
            //IPersistenceContext persistenceContext = session.PersistenceContext;
            var batcher = session.Batcher;
            //persistenceContext.BeforeLoad();
            var result = 0;
            try
            {
                if (list.Length == 1)
                {
                    var sql = Map.CreateInsertSql(session, this, list[0]);

                    var cmd = driver.GenerateCommand(sql.CommandText, CommandType.Text, sql.Parameters);
                    result = batcher.ExecuteNonQuery(cmd);
                }
                else
                {
                    var sqls = Map.CreateInsertSql(this, list);
                    foreach (var sql in sqls)
                    {
                        result += batcher.ExecuteNonQuery(driver.GenerateCommand(sql, CommandType.Text));
                    }
                }
            }
            finally
            {
                batcher.CloseCommands();
                //persistenceContext.AfterLoad();
            }
            return result;
        }

        public int InsertWithReturnIdentity(ISessionImplementor session, IEntity obj)
        {

            //IPersistenceContext persistenceContext = session.PersistenceContext;
            var batcher = session.Batcher;
            //persistenceContext.BeforeLoad();
            var result = 0;
            try
            {
                if (obj != null)
                {
                    var sql = Map.CreateInsertReturnIdentitySql(session, this, obj);

                    var cmd = driver.GenerateCommand(sql.CommandText, CommandType.Text, sql.Parameters);
                    result = int.Parse(batcher.ExecuteScalar(cmd).ToString());
                }
            }
            finally
            {
                batcher.CloseCommands();
                //persistenceContext.AfterLoad();
            }
            return result;
        }

        public int Delete(ISessionImplementor session, IEntity obj)
        {
            var sql = Map.CreateDeleteSql(session, this, obj);

            return ExecuteNonQuery(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public int Delete(ISessionImplementor session, params int[] pKeys)
        {
            var sql = Map.CreateDeleteSql(session, this, pKeys);

            return ExecuteNonQuery(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public int DeleteWithAssociate(ISessionImplementor session, params int[] pKeys)
        {
            var sql = Map.CreateDeleteWithAssociateSql(session, this, pKeys);
            return ExecuteNonQuery(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public int DeleteWithValidate(ISessionImplementor session, params int[] pKeys)
        {
            var sql = Map.CreateDeleteWithValidateSql(session, this, pKeys);
            return ExecuteNonQuery(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public int Update(ISessionImplementor session, IEntity obj)
        {
            var sql = Map.CreateUpdateSql(session, this, obj);
            return ExecuteNonQuery(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public T Get<T>(ISessionImplementor session, int pKey)
        {
            var sql = Map.CreateSelectSql(session, this, pKey);
            return Get<T>(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public T Get<T>(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters)
        {
            //IPersistenceContext persistenceContext = session.PersistenceContext;
            //persistenceContext.BeforeLoad();
            var batcher = session.Batcher;
            T result = default(T);
            IDataReader rs = null;
            try
            {
                rs = batcher.ExecuteReader(driver.GenerateCommand(sql, commandType, parameters));
                if (rs.Read())
                {
                    result = PopulateEntity<T>(rs);
                }
            }
            finally
            {
                batcher.CloseReader(rs);
                //persistenceContext.AfterLoad();
            }
            return result;
        }

        public T Get<T>(ISessionImplementor session, IEntity model)
        {
            //IPersistenceContext persistenceContext = session.PersistenceContext;
            //persistenceContext.BeforeLoad();
            var batcher = session.Batcher;
            T result = default(T);
            IDataReader rs = null;
            try
            {
                var sql = Map.CreateSelectSql(session, this, model);
                rs = batcher.ExecuteReader(driver.GenerateCommand(sql.CommandText, CommandType.Text, sql.Parameters));
                if (rs.Read())
                {
                    result = PopulateEntity<T>(rs);
                }
            }
            finally
            {
                batcher.CloseReader(rs);
                // persistenceContext.AfterLoad();
            }
            return result;
        }

        public T Get<T>(ISessionImplementor session, Criteria criteria)
        {
            //IPersistenceContext persistenceContext = session.PersistenceContext;
            //persistenceContext.BeforeLoad();
            var batcher = session.Batcher;
            T result = default(T);
            IDataReader rs = null;
            try
            {
                //var sql = Map.CreateSelectSql(session, this, model);
                var sql = Map.CreateSelectSql2(session, this, criteria, null);
                rs = batcher.ExecuteReader(driver.GenerateCommand(sql.CommandText, CommandType.Text, sql.Parameters));
                if (rs.Read())
                {
                    result = PopulateEntity<T>(rs);
                }
            }
            finally
            {
                batcher.CloseReader(rs);
                // persistenceContext.AfterLoad();
            }
            return result;
        }

        public List<T> Load<T>(ISessionImplementor session)
        {
            var sql = Map.CreateSelectSql(session, this, null);
            return Load<T>(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public List<T> Load<T>(ISessionImplementor session, Criteria criteria)
        {
            var sql = Map.CreateSelectSql2(session, this, criteria, null);
            return Load<T>(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public List<T> Load<T>(ISessionImplementor session, Criteria criteria, Order order)
        {
            var sql = Map.CreateSelectSql2(session, this, criteria, order);
            return Load<T>(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public List<T> Load<T>(ISessionImplementor session, IEntity model)
        {
            var sql = Map.CreateSelectSql(session, this, model);
            return Load<T>(session, sql.CommandText, CommandType.Text, sql.Parameters);
        }

        public List<T> Load<T>(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters)
        {
            //IPersistenceContext persistenceContext = session.PersistenceContext;
            //persistenceContext.BeforeLoad();
            var batcher = session.Batcher;
            List<T> result;
            IDataReader rs = null;
            try
            {
                rs = batcher.ExecuteReader(driver.GenerateCommand(sql, commandType, parameters));
                result = new List<T>();
                while (rs.Read())
                {
                    var obj = PopulateEntity<T>(rs);
                    if (!Equals(obj, default(T))) result.Add(obj);
                }
            }
            finally
            {
                batcher.CloseReader(rs);
                //persistenceContext.AfterLoad();
            }
            return result;
        }

        public int ExecuteNonQuery(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters)
        {
            // IPersistenceContext persistenceContext = session.PersistenceContext;
            var batcher = session.Batcher;
            //persistenceContext.BeforeLoad();
            int result;
            try
            {
                result = batcher.ExecuteNonQuery(driver.GenerateCommand(sql, commandType, parameters));
            }
            finally
            {
                batcher.CloseCommands();
                // persistenceContext.AfterLoad();
            }
            return result;
        }

        public object ExecuteScalar(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters)
        {
            //IPersistenceContext persistenceContext = session.PersistenceContext;
            var batcher = session.Batcher;
            //persistenceContext.BeforeLoad();
            object result;
            try
            {
                result = batcher.ExecuteScalar(driver.GenerateCommand(sql, commandType, parameters));
            }
            finally
            {
                batcher.CloseCommands();
                //persistenceContext.AfterLoad();
            }
            return result;
        }

        public DataSet ExecuteDataSet(ISessionImplementor session, string sql, CommandType commandType, params DbParameter[] parameters)
        {
            //IPersistenceContext persistenceContext = session.PersistenceContext;
            var batcher = session.Batcher;
            //persistenceContext.BeforeLoad();
            DataSet result;
            try
            {
                result = batcher.ExecuteDataSet(driver.GenerateDataAdapter(sql, commandType, parameters));
            }
            finally
            {
                batcher.CloseCommands();
                //persistenceContext.AfterLoad();
            }
            return result;
        }

        private T PopulateEntity<T>(IDataRecord reader)
        {
            T result;
            try
            {
                result = Activator.CreateInstance<T>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var propertyInfo = classMetadata.ColumnInfos.Find(t => (t.Name == null ? "" : t.Name.ToUpper()) == reader.GetName(i).ToUpper());
                    if (propertyInfo != null)
                    {
                        var value = reader.GetValue(i);
                        if (value != DBNull.Value) propertyInfo.PropertyInfo.FastSetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType));

                    }
                }
            }
            catch (Exception e)
            {
                log.DebugFormat("ExecuteNonQuery took {0} ms", e);
                log.Error("Could not PopulateEntity: ", e);
                result = default(T);
            }
            return result;
        }

        public IDictionary<string, DbParameter> GetParameter(ISessionImplementor session)
        {
            var result = CacheHelper<IDictionary<string, DbParameter>>.GetCache(parameterCacheKey);
            if (result == null)
            {
                result = new Dictionary<string, DbParameter>();
                var sql = string.Format("SELECT * FROM {0} WHERE 1=0", classMetadata.TableName);
                var cmd = driver.GenerateCommand(sql, CommandType.Text, null);
                var schemaTable = session.Batcher.ExecuteSchemaTable(cmd);
                foreach (var columnInfo in classMetadata.ColumnInfos)
                {
                    if (!columnInfo.IsColumn) continue;
                    var schemaRows = schemaTable.Select(string.Format("{0}='{1}'", SchemaTableColumn.ColumnName, columnInfo.Name));
                    if (schemaRows.Length <= 0) continue;
                    var columns = string.Format("{0};{1}", columnInfo.Name, columnInfo.SameParameters).Split(';');
                    for (var i = 0; i < columns.Length; i++)
                    {
                        if (string.IsNullOrEmpty(columns[i])) continue;
                        var parameter = driver.CreateParameter(schemaRows[0], columns[i]);
                        parameter.Direction = columnInfo.ParameterDirection;
                        result[columns[i]] = parameter;
                    }
                }
                CacheHelper<IDictionary<string, DbParameter>>.SetCache(parameterCacheKey, result);
            }
            return result;
        }

        public DbParameter[] GetParameters(ISessionImplementor session, params string[] columnName)
        {
            var result = new List<DbParameter>();
            var parmeters = GetParameter(session);
            if (columnName.Length == 0)
            {
                foreach (var parmeter in parmeters) result.Add(parmeter.Value);
            }
            else
            {
                foreach (var s in columnName)
                {
                    DbParameter pa;
                    parmeters.TryGetValue(s, out pa);
                    if (pa != null) result.Add(pa);
                }
            }
            return result.ToArray();
        }
    }
}
