using System;

namespace VipSoft.Data.Driver
{
    /// <summary>
    /// A FSORM Driver for using the Ingres DataProvider
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class IngresDriver : ReflectionBasedDriver
    {
        public IngresDriver() : base("Ingres.Client") {}

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