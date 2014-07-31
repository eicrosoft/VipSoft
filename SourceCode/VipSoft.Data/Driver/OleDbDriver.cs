using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A ORM Driver for using the OleDb DataProvider
    /// </summary>
    /// <remarks>
    /// Always look for a native .NET DataProvider before using the OleDb DataProvider.
    /// </remarks>
    public class OleDbDriver : DriverBase
    {

        public override IDbConnection CreateConnection()
        {
            return new OleDbConnection();
        }

        public override DbParameter CreateParameter()
        {
            return new OleDbParameter();
        }

        public override DbCommandBuilder CreateDbCommandBuilder()
        {
            return new OleDbCommandBuilder();
        }

        public override DbDataAdapter CreateDbDataAdapter()
        {
            return new OleDbDataAdapter();
        }

        public override IDbCommand CreateCommand()
        {
            return new OleDbCommand();
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
            get { return "@"; }
        }

        
    }
}

/*
 
 /// <summary>
        /// OLE DB provider does not support multiple open data readers
        /// </summary>
        public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }
 */