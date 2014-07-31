namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A ORM Driver for using the Oracle DataProvider.
    /// </summary>
    public class OracleClientDriver : ReflectionBasedDriver
    {
     //   private static readonly SqlType GuidSqlType = new SqlType(DbType.Binary, 16);

        public OracleClientDriver() : base("System.Data.OracleClient")
        {
        }

        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        public override string NamedPrefix
        {
            get { return ":"; }
        }
       
    }
}

/*
 
 protected override void InitializeParameter(IDbDataParameter dbParam, string name, SqlType sqlType)
        {
            if (sqlType.DbType == DbType.Guid)
            {
                base.InitializeParameter(dbParam, name, GuidSqlType);
            }
            else
            {
                base.InitializeParameter(dbParam, name, sqlType);
            }
        } 
 */