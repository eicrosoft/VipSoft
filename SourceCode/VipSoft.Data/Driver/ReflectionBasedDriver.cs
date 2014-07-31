using System.Data;
using System.Data.Common;

namespace VipSoft.Data.Driver
{
	public abstract class ReflectionBasedDriver : DriverBase
	{
        private readonly DbProviderFactory dbProviderFactory;
        /// <summary>
        /// Initializes a new instance of <see cref="ReflectionBasedDriver" /> with
        /// type names that are loaded from the specified assembly.
        /// </summary>
        /// <param name="connectionTypeName">Connection type name.</param>

        protected ReflectionBasedDriver(string connectionTypeName)
        {
            dbProviderFactory = DbProviderFactories.GetFactory(connectionTypeName);
        }

        public override IDbConnection CreateConnection()
        {
            return dbProviderFactory.CreateConnection();
        }

        public override IDbCommand CreateCommand()
        {
            return dbProviderFactory.CreateCommand();
        }

        public override DbCommandBuilder CreateDbCommandBuilder()
        {
            return dbProviderFactory.CreateCommandBuilder();
        }

        public override DbDataAdapter CreateDbDataAdapter()
        {
            return dbProviderFactory.CreateDataAdapter();
        }

        public override DbParameter CreateParameter()
        {
            return dbProviderFactory.CreateParameter();
        }
	}
}