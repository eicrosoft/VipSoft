namespace VipSoft.Data.Driver
{
    /// <summary>
    /// ORM driver for the SQLite.NET data provider.
    /// <p>
    /// Author: <a href="mailto:ib@stalker.ro"> Ioan Bizau </a>
    /// </p>
    /// </summary>
    /// <remarks>
    /// <p>
    /// In order to use this Driver you must have the SQLite.NET.dll Assembly available for ORM to load it.
    /// You must also have the SQLite.dll and SQLite3.dll libraries.
    /// </p>
    /// <p>
    /// Please check <a href="http://www.sqlite.org/"> http://www.sqlite.org/ </a> for more information regarding SQLite.
    /// </p>
    /// </remarks>
    public class SQLiteDriver : ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SQLiteDriver"/>.
        /// </summary>
		
        /// Thrown when the <c>SQLite.NET</c> assembly can not be loaded.
        /// </exception>
        public SQLiteDriver() : base("Finisar.SQLite")
        {
        }

        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        public override bool SupportsBacthInsert
        {
            get { return true; }
        }

        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        public override string NamedPrefix
        {
            get { return "@"; }
        }

        public override string GetIdentityString
        {
            get
            {
                return "sqlite3_last_insert_rowid();";
            }
        }

      
    }
}

/*
   public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }
 
 */