using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using VipSoft.Core.Utility;

namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A ORM Driver for using the SqlClient DataProvider
    /// </summary>
    public class SqlClientDriver : DriverBase
    {
        /// <summary>
        /// Creates an uninitialized <see cref="IDbConnection" /> object for 
        /// the SqlClientDriver.
        /// </summary>
        /// <value>An unitialized <see cref="System.Data.SqlClient.SqlConnection"/> object.</value>
        public override IDbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }

        /// <summary>
        /// Creates an uninitialized <see cref="IDbCommand" /> object for 
        /// the SqlClientDriver.
        /// </summary>
        /// <value>An unitialized <see cref="System.Data.SqlClient.SqlCommand"/> object.</value>
        public override IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override DbCommandBuilder CreateDbCommandBuilder()
        {
            return new SqlCommandBuilder();
        }

        public override DbDataAdapter CreateDbDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public override bool SupportsBacthInsert
        {
            get { return false; }
        }

        /// <summary>
        /// MsSql requires the use of a Named Prefix in the SQL statement.  
        /// </summary>
        /// <remarks>
        /// <see langword="true" /> because MsSql uses "<c>@</c>".
        /// </remarks>
        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        /// <summary>
        /// MsSql requires the use of a Named Prefix in the Parameter.  
        /// </summary>
        /// <remarks>
        /// <see langword="true" /> because MsSql uses "<c>@</c>".
        /// </remarks>
        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        /// <summary>
        /// The Named Prefix for parameters.  
        /// </summary>
        /// <value>
        /// Sql Server uses <c>"@"</c>.
        /// </value>
        public override string NamedPrefix
        {
            get { return "@"; }
        }

        /// <summary>
        /// sql use to return autoincrease id
        /// </summary>
        public override string GetIdentityString
        {
            get
            {
                return "select @@identity";
            }
        }

        public override bool SupportsMultipleQueries
        {
            get { return true; }
        }

        public override string CreatePagingSql(int pageSize, int dataStart, string tableName, string primaryKey, string whereStr, string orderByStr)
        {
            //var totalSql = "SELECT COUNT(1) FROM YouJia_UserInfo";
            //Object result = GetScalar(totalSql);
            //totalItemCount = result == null ? 0 : ConvertHandler.ToInt32(result);
            //            var sql = string.Format(@"SELECT u2.n, U.* FROM YouJia_UserInfo U,
            //                                    (SELECT TOP {0} row_number() OVER (ORDER BY Name DESC) n, CODE FROM YouJia_UserInfo)
            //                                     u2 WHERE U.CODE = u2.CODE AND u2.n >= {1} ORDER BY u2.n ASC", 
            //                                    ((@pageIndex - 1) * @pageSize) + @pageSize, ((@pageIndex - 1) * @pageSize) + 1);
            //=================
            //{3} ORDER BY Name DESC
            //{4} primaryKey
            //{4} whereStr
            var sql = string.Format(@"SELECT u2.n, U.* FROM {0} U,
                                    (SELECT TOP {1} row_number() OVER ({3}) n, {4} FROM {0} {5})
                                     u2 WHERE U.{4} = u2.{4} AND u2.n > {2} ORDER BY u2.n ASC",
                                   tableName, dataStart + pageSize, dataStart, orderByStr, primaryKey, whereStr);
            return sql;
        }
    }
}

/*
 *   /// <summary>
        /// The SqlClient driver does NOT support more than 1 open IDataReader
        /// with only 1 IDbConnection.
        /// </summary>
        /// <value><see langword="false" /> - it is not supported.</value>
        /// <remarks>
        /// MS SQL Server 2000 (and 7) throws an exception when multiple IDataReaders are 
        /// attempted to be opened.  When SQL Server 2005 comes out a new driver will be 
        /// created for it because SQL Server 2005 is supposed to support it.
        /// </remarks>
        public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }
 
        // Used from SqlServerCeDriver as well
        public static void SetParameterSizes(IDataParameterCollection parameters, SqlType[] parameterTypes)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                SetVariableLengthParameterSize((IDbDataParameter) parameters[i], parameterTypes[i]);
            }
        }

 *         private const int MaxAnsiStringSize = 8000;
        private const int MaxBinarySize = MaxAnsiStringSize;
        private const int MaxStringSize = MaxAnsiStringSize / 2;
        private const int MaxBinaryBlobSize = int.MaxValue;
        private const int MaxStringClobSize = MaxBinaryBlobSize / 2;
        private const byte MaxPrecision = 28;
        private const byte MaxScale = 5;

        private static void SetDefaultParameterSize(IDbDataParameter dbParam, SqlType sqlType)
        {
            switch (dbParam.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    dbParam.Size = MaxAnsiStringSize;
                    break;

                case DbType.Binary:
                    if (sqlType is BinaryBlobSqlType)
                    {
                        dbParam.Size = MaxBinaryBlobSize;
                    }
                    else
                    {
                        dbParam.Size = MaxBinarySize;
                    }
                    break;
                case DbType.Decimal:
                    dbParam.Precision = MaxPrecision;
                    dbParam.Scale = MaxScale;
                    break;
                case DbType.String:
                case DbType.StringFixedLength:
                    if (sqlType is StringClobSqlType)
                    {
                        dbParam.Size = MaxStringClobSize;
                    }
                    else
                    {
                        dbParam.Size = MaxStringSize;
                    }
                    break;
            }
        }

        private static void SetVariableLengthParameterSize(IDbDataParameter dbParam, SqlType sqlType)
        {
            SetDefaultParameterSize(dbParam, sqlType);

            // Override the defaults using data from SqlType.
            if (sqlType.LengthDefined)
            {
                dbParam.Size = sqlType.Length;
            }

            if (sqlType.PrecisionDefined)
            {
                dbParam.Precision = sqlType.Precision;
                dbParam.Scale = sqlType.Scale;
            }
        }

        public override IDbCommand GenerateCommand(CommandType type, SqlString sqlString, SqlType[] parameterTypes)
        {
            IDbCommand command = base.GenerateCommand(type, sqlString, parameterTypes);
            if (IsPrepareSqlEnabled)
            {
                SetParameterSizes(command.Parameters, parameterTypes);
            }
            return command;
        }
 * 
  #region IEmbeddedBatcherFactoryProvider Members

        System.Type IEmbeddedBatcherFactoryProvider.BatcherFactoryClass
        {
            get { return typeof(SqlClientBatchingBatcherFactory); }
        }

        #endregion
 
 */