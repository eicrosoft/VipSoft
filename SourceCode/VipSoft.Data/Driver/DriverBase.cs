using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using VipSoft.Core.Cache;
using VipSoft.Core.Config;
using VipSoft.Core.Driver;
using VipSoft.Core.Utility;
using VipSoft.FastReflection;

namespace VipSoft.Data.Driver
{
    /// <summary>
    /// Base class for the implementation of IDriver
    /// </summary>
    public abstract class DriverBase : IDriver
    {
        private int commandTimeout;
        private SessionElement dirverSettings;
        private bool prepareSql;

        public virtual void Configure(SessionElement settings)
        {
            dirverSettings = settings;
            // Command timeout, -1
            commandTimeout = settings.CommandTimeOut;
            // Prepare sql enabled, false
            prepareSql = settings.PrepareSqlEnable;
        }

        protected bool IsPrepareSqlEnabled
        {
            get { return prepareSql; }
        }

        public abstract IDbConnection CreateConnection();

        public abstract IDbCommand CreateCommand();

        /// <summary>
        /// Does this Driver require the use of a Named Prefix in the SQL statement.  
        /// </summary>
        /// <remarks>
        /// For example, SqlClient requires <c>select * from simple where simple_id = @simple_id</c>
        /// If this is false, like with the OleDb provider, then it is assumed that  
        /// the <c>?</c> can be a placeholder for the parameter in the SQL statement.
        /// </remarks>
        public abstract bool UseNamedPrefixInSql { get; }

        /// <summary>
        /// Does this Driver require the use of the Named Prefix when trying
        /// to reference the Parameter in the Command's Parameter collection.  
        /// </summary>
        /// <remarks>
        /// This is really only useful when the UseNamedPrefixInSql == true.  When this is true the
        /// code will look like:
        /// <code>IDbParameter param = cmd.Parameters["@paramName"]</code>
        /// if this is false the code will be 
        /// <code>IDbParameter param = cmd.Parameters["paramName"]</code>.
        /// </remarks>
        public abstract bool UseNamedPrefixInParameter { get; }

        /// <summary>
        /// The Named Prefix for parameters.  
        /// </summary>
        /// <remarks>
        /// Sql Server uses <c>"@"</c> and Oracle uses <c>":"</c>.
        /// </remarks>
        public abstract string NamedPrefix { get; }

        /// <summary>
        /// Change the parameterName into the correct format IDbCommand.CommandText
        /// for the ConnectionProvider
        /// </summary>
        /// <param name="parameterName">The unformatted name of the parameter</param>
        /// <returns>A parameter formatted for an IDbCommand.CommandText</returns>
        public virtual string FormatNameForSql(string parameterName)
        {
            return UseNamedPrefixInSql ? string.Format("{0}P_{1}", NamedPrefix, parameterName) : "?";
        }

        public virtual string GetIdentityString
        {
            get { return ""; }
        }

        /// <summary>
        /// Changes the parameterName into the correct format for an IDbParameter
        /// for the Driver.
        /// </summary>
        /// <remarks>
        /// For SqlServerConnectionProvider it will change <c>id</c> to <c>@id</c>
        /// </remarks>
        /// <param name="parameterName">The unformatted name of the parameter</param>
        /// <returns>A parameter formatted for an IDbParameter.</returns>
        protected virtual string FormatNameForParameter(string parameterName)
        {
            return UseNamedPrefixInParameter ? string.Format("{0}P_{1}", NamedPrefix, parameterName) : string.Format("P_{0}", parameterName);
        }

        public virtual bool SupportsMultipleOpenReaders
        {
            get { return true; }
        }

        public virtual bool SupportsBacthInsert
        {
            get { return false; }
        }

        /// <summary>
        /// Does this Driver support IDbCommand.Prepare().
        /// </summary>
        /// <remarks>
        /// <para>
        /// A value of <see langword="false" /> indicates that an exception would be thrown or the 
        /// company that produces the Driver we are wrapping does not recommend using
        /// IDbCommand.Prepare().
        /// </para>
        /// <para>
        /// A value of <see langword="true" /> indicates that calling IDbCommand.Prepare() will function
        /// fine on this Driver.
        /// </para>
        /// </remarks>
        protected virtual bool SupportsPreparingCommands
        {
            get { return true; }
        }

        protected virtual void InitializeParameter(IDbDataParameter dbParam, string name, DbParameter sqlType)
        {
            dbParam.ParameterName = FormatNameForParameter(name);
            dbParam.DbType = sqlType.DbType;
        }

        public IDbDataAdapter GenerateDataAdapter(string commandText, CommandType type, params DbParameter[] parameters)
        {
            IDbCommand command = CreateCommand();
            command.CommandText = commandText;
            command.CommandType = type;
            if (commandTimeout >= 0) command.CommandTimeout = commandTimeout;
            if (parameters != null)
                foreach (var p in parameters) command.Parameters.Add(p);
            IDbDataAdapter result = CreateDbDataAdapter();
            result.SelectCommand = command;
            return result;
        }

        public void PrepareCommand(IDbCommand command)
        {
            if (SupportsPreparingCommands && prepareSql)
            {
                command.Prepare();
            }
        }

        public IDbCommand GenerateCommand(string commandText, CommandType type, params DbParameter[] parameters)
        {
            IDbCommand command = CreateCommand();
            command.CommandText = commandText;
            command.CommandType = type;
            if (commandTimeout >= 0) command.CommandTimeout = commandTimeout;
            if (parameters != null)
                foreach (var p in parameters) command.Parameters.Add(p);
            return command;
        }

        public virtual bool SupportsMultipleQueries
        {
            get { return false; }
        }

        public virtual string MultipleQueriesSeparator
        {
            get { return ";"; }
        }

        public abstract DbCommandBuilder CreateDbCommandBuilder();
        public abstract DbDataAdapter CreateDbDataAdapter();
        public abstract DbParameter CreateParameter();

        public virtual DbParameter CreateParameter(DataRow dataRow, string columnName)
        {
            var dbParameter = CreateParameter();
            var parameterName = string.IsNullOrEmpty(columnName) ? dataRow[SchemaTableColumn.ColumnName].ToString() : columnName;
            dbParameter.ParameterName = FormatNameForParameter(parameterName);
            int size;
            int.TryParse(dataRow[SchemaTableColumn.ColumnSize].ToString(), out size);
            dbParameter.Size = size;
            var dbCommandBuilder = CacheHelper<DbCommandBuilder>.GetCache(string.Format("{0}_DbCommandBuilder_Type_Cache_Key", dirverSettings.Driver), CreateDbCommandBuilder());
            var minfo = dbCommandBuilder.GetType().GetMethod("ApplyParameterInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            var parameters = new Object[4];
            parameters[0] = dbParameter;
            parameters[1] = dataRow;
            parameters[2] = StatementType.Select;
            parameters[3] = false;
            minfo.FastInvoke(dbCommandBuilder, parameters);
            return dbParameter;
        }


        public virtual string CreatePagingSql(int pageSize, int dataStart, string tableName, string primaryKey, string whereStr, string orderByStr)
        { 
            return string.Empty;
        }

    }
}