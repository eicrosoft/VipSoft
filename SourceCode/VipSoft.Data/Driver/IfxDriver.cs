namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A FSORM Driver for using the Informix DataProvider
    /// </summary>
    public class IfxDriver : ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IfxDriver"/> class.
        /// </summary>
		
        /// Thrown when the <c>IBM.Data.Informix</c> assembly can not be loaded.
        /// </exception>
        public IfxDriver(): base("IBM.Data.Informix")
        {
        }

        public override bool UseNamedPrefixInSql
        {
            get { return false; }
        }

        public override bool UseNamedPrefixInParameter
        {
            get { return false; }
        }

        public override string NamedPrefix
        {
            get { return ":"; }
        }
    }
}