
namespace VipSoft.Data.Driver
{
    /// <summary>
    /// The PostgreSQL data provider provides a database driver for PostgreSQL.
    /// <p>
    /// Author: <a href="mailto:oliver@weichhold.com">Oliver Weichhold</a>
    /// </p>
    /// </summary>
    /// <remarks>
    /// <p>
    /// In order to use this Driver you must have the Npgsql.dll Assembly available for 
    /// FSORM to load it.
    /// </p>
    /// <p>
    /// Please check the products website 
    /// <a href="http://www.postgresql.org/">http://www.postgresql.org/</a>
    /// for any updates and or documentation.
    /// </p>
    /// <p>
    /// The homepage for the .NET DataProvider is: 
    /// <a href="http://pgfoundry.org/projects/npgsql">http://pgfoundry.org/projects/npgsql</a>. 
    /// </p>
    /// </remarks>
    public class NpgsqlDriver : ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpgsqlDriver"/> class.
        /// </summary>
		
        /// Thrown when the <c>Npgsql</c> assembly can not be loaded.
        /// </exception>
        public NpgsqlDriver() : base("Npgsql")
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

        protected override bool SupportsPreparingCommands
        {
            // NOTE: Npgsql1.0 and 2.0-preview apparently doesn't correctly  support prepared commands.
            // The following exception is thrown on insert statements:
            // Npgsql.NpgsqlException : ERROR: 42601: cannot insert multiple commands into a prepared statement
            get { return false; }
        }

        public override bool SupportsMultipleQueries
        {
            get { return true; }
        }
    }
}

/*
         public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }
 
 */