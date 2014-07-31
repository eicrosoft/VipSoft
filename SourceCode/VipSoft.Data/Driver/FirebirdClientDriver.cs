namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A FSORM Driver for using the Firebird data provider located in
    /// <c>FirebirdSql.Data.FirebirdClient</c> assembly.
    /// </summary>
    public class FirebirdClientDriver : ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirebirdDriver"/> class.
        /// </summary>
		
        /// Thrown when the <c>FirebirdSql.Data.Firebird</c> assembly can not be loaded.
        /// </exception>
        public FirebirdClientDriver() : base("FirebirdSql.Data.FirebirdClient")
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
            get { return "@"; }
        }
    }
}