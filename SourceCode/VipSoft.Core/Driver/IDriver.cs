using System.Data;
using System.Data.Common;
using VipSoft.Core.Config;

namespace VipSoft.Core.Driver
{
    /// <summary>
    /// A strategy for describing how ORM should interact with the different .NET Data
    /// Providers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>IDriver</c> interface is not intended to be exposed to the application.
    /// Instead it is used internally by ORM to obtain connection objects, command objects, and
    /// to generate and prepare <see cref="IDbCommand">IDbCommands</see>. Implementors should provide a
    /// public default constructor.
    /// </para>
    /// <para>
    /// This is the interface to implement, or you can inherit from <see cref="DriverBase"/> 
    /// if you have an ADO.NET data provider that ORM does not have built in support for.
    /// To use the driver, ORM property <c>driver</c> should be
    /// set to the assembly-qualified name of the driver class.
    /// </para>
    /// <code>
    /// key="driver"
    /// value="FullyQualifiedClassName, AssemblyName"
    /// </code>
    /// </remarks>
    public interface IDriver
    {
        /// <summary>
        /// Configure the driver using <paramref name="settings"/>.
        /// </summary>
        void Configure(SessionElement settings);

        /// <summary>
        /// Creates an uninitialized IDbConnection object for the specific Driver
        /// </summary>
        IDbConnection CreateConnection();

        /// <summary>
        /// Does this Driver support having more than 1 open IDataReader with
        /// the same IDbConnection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A value of <see langword="false" /> indicates that an exception would be thrown if FSORM
        /// attempted to have 2 IDataReaders open using the same IDbConnection.  FSORM
        /// (since this version is a close to straight port of FSORM) relies on the 
        /// ability to recursively open 2 IDataReaders.  If the Driver does not support it
        /// then FSORM will read the values from the IDataReader into an <see cref="NDataReader"/>.
        /// </para>
        /// <para>
        /// A value of <see langword="true" /> will result in greater performance because an IDataReader can be used
        /// instead of the <see cref="NDataReader"/>.  So if the Driver supports it then make sure
        /// it is set to <see langword="true" />.
        /// </para>
        /// </remarks>
        bool SupportsMultipleOpenReaders { get; }

        bool SupportsBacthInsert { get; }

        /// <summary>
        /// Can we issue several select queries in a single query, and get
        /// several result sets back?
        /// </summary>
        bool SupportsMultipleQueries { get; }

        /// <summary>
        /// How we separate the queries when we use multiply queries.
        /// </summary>
        string MultipleQueriesSeparator { get; }

        /// <summary>
        /// Generates an IDbCommand from the SqlString according to the requirements of the DataProvider.
        /// </summary>
        /// <param name="type">The <see cref="CommandType"/> of the command to generate.</param>
        /// <param name="commandText">The SqlString that contains the SQL.</param>
        /// <param name="parameters">The types of the parameters to generate for the command.</param>
        /// <returns>An IDbCommand with the CommandText and Parameters fully set.</returns>
        IDbCommand GenerateCommand(string commandText, CommandType type, params DbParameter[] parameters);

        IDbDataAdapter GenerateDataAdapter(string commandText, CommandType type, params DbParameter[] parameters);


        /// <summary>
        /// Prepare the <paramref name="command" /> by calling <see cref="IDbCommand.Prepare()" />.
        /// May be a no-op if the driver does not support preparing commands, or for any other reason.
        /// </summary>
        /// <param name="command"></param>
        void PrepareCommand(IDbCommand command);

        /// <summary>
        /// Generates an IDbDataParameter for the IDbCommand.  It does not add the IDbDataParameter to the IDbCommand's
        /// Parameter collection.
        /// </summary>
        /// <param name="command">The IDbCommand to use to create the IDbDataParameter.</param>
        /// <param name="name">The name to set for IDbDataParameter.Name</param>
        /// <param name="sqlType">The SqlType to set for IDbDataParameter.</param>
        /// <returns>An IDbDataParameter ready to be added to an IDbCommand.</returns>
        DbParameter CreateParameter(DataRow dataRow, string columnName);


        /// <summary>
        /// Change the parameterName into the correct format IDbCommand.CommandText
        /// for the ConnectionProvider
        /// </summary>
        /// <param name="parameterName">The unformatted name of the parameter</param>
        /// <returns>A parameter formatted for an IDbCommand.CommandText</returns>
        string FormatNameForSql(string parameterName);

        string GetIdentityString { get;}

        /// <summary>
        /// Changes the parameterName into the correct format for an IDbParameter
        /// for the Driver.
        /// </summary>
        /// <remarks>
        /// For SqlServerConnectionProvider it will change <c>id</c> to <c>@id</c>
        /// </remarks>
        /// <param name="parameterName">The unformatted name of the parameter</param>
        /// <returns>A parameter formatted for an IDbParameter.</returns>
       // string FormatNameForParameter(string parameterName);
    }
}