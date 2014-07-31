namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A FSORM Driver for using the FirebirdSql.Data.Firebird DataProvider.
    /// </summary>
    public class FirebirdDriver : ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirebirdDriver"/> class.
        /// </summary>
		
        /// Thrown when the <c>FirebirdSql.Data.Firebird</c> assembly can not be loaded.
        /// </exception>
        public FirebirdDriver() : base("FirebirdSql.Data.Firebird")
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