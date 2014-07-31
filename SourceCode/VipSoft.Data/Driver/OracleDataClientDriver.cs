namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A ORM Driver for using the Oracle.DataAccess DataProvider
    /// </summary>

    public class OracleDataClientDriver : ReflectionBasedDriver
    {
        //private static readonly SqlType GuidSqlType = new SqlType(DbType.Binary, 16);
        /// <summary>
        /// Initializes a new instance of <see cref="OracleDataClientDriver"/>.
        /// </summary>
		
        /// Thrown when the <c>Oracle.DataAccess</c> assembly can not be loaded.
        /// </exception>
        public OracleDataClientDriver(): base("Oracle.DataAccess.Client")
        {
        }

        /// <summary></summary>
        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        /// <summary></summary>
        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        /// <summary></summary>
        public override string NamedPrefix
        {
            get { return ":"; }
        }

		
		
    }
}

/*
 
 /// <remarks>
		/// This adds logic to ensure that a DbType.Boolean parameter is not created since
		/// ODP.NET doesn't support it.
		/// </remarks>
		protected override void InitializeParameter(IDbDataParameter dbParam, string name, SqlType sqlType)
		{
			// if the parameter coming in contains a boolean then we need to convert it 
			// to another type since ODP.NET doesn't support DbType.Boolean
			switch (sqlType.DbType)
			{
				case DbType.Boolean:
					base.InitializeParameter(dbParam, name, SqlTypeFactory.Int16);
					break;
				case DbType.Guid:
					base.InitializeParameter(dbParam, name, GuidSqlType);
					break;
				default:
					base.InitializeParameter(dbParam, name, sqlType);
					break;
			}
		}

 #region IEmbeddedBatcherFactoryProvider Members

		System.Type IEmbeddedBatcherFactoryProvider.BatcherFactoryClass
		{
			get { return typeof (OracleDataClientBatchingBatcherFactory); }
		}

		#endregion
 
 */