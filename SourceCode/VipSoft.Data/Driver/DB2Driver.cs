using System;

namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A FSORM Driver for using the IBM.Data.DB2 DataProvider.
    /// </summary>
    public class DB2Driver : ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DB2Driver"/> class.
        /// </summary>
		
        /// Thrown when the <c>IBM.Data.DB2</c> assembly can not be loaded.
        /// </exception>
        public DB2Driver() : base("IBM.Data.DB2")
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
            get { return String.Empty; }
        }

       
    }
}
/* public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }
 */