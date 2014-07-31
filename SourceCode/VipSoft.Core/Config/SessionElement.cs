// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionElement.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:5-Nov-2012
// </copyright>
// <summary>
//   SessionElement
// </summary>
// ---

using System.Configuration;

namespace VipSoft.Core.Config
{
    public class SessionElement : ConfigurationElement
    {
        public SessionElement() { }

        public SessionElement(string name, string connectionString, string connectionProvider, string driver)
        {
            Name = name;
            ConnectionString = connectionString;
            ConnectionProvider = connectionProvider;
            Driver = driver;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "Not Supplied")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("connectionString", IsRequired = true, DefaultValue = "Not Supplied")]
        public string ConnectionString
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }

        [ConfigurationProperty("connectionProvider", IsRequired = false)]
        public string ConnectionProvider
        {
            get { return (string)this["connectionProvider"]; }
            set { this["connectionProvider"] = value; }
        }

        [ConfigurationProperty("driver", IsRequired = true)]
        public string Driver
        {
            get { return (string)this["driver"]; }
            set { this["driver"] = value; }
        }

        [ConfigurationProperty("connectionReleaseMode", IsRequired = false, DefaultValue = "auto")]
        public string ConnectionReleaseMode
        {
            get { return (string)this["connectionReleaseMode"]; }
            set { this["connectionReleaseMode"] = value; }
        }

        [ConfigurationProperty("transactionFactory", IsRequired = false, DefaultValue = "VipSoft.Data.Transaction.AdoNetWithDistrubtedTransactionFactory")]
        public string TransactionFactory
        {
            get { return (string)this["transactionFactory"]; }
            set { this["transactionFactory"] = value; }
        }

        [ConfigurationProperty("commandTimeOut", IsRequired = false, DefaultValue = 0)]
        public int CommandTimeOut
        {
            get { return (int)this["commandTimeOut"]; }
            //set { this["commandTimeOut"] = value; }
        }

        //[ConfigurationProperty("defaultBatchFetchSize", IsRequired = false, DefaultValue = 1)]
        //public int DefaultBatchFetchSize
        //{
        //    get { return (int)this["defaultBatchFetchSize"]; }
        //    set { this["defaultBatchFetchSize"] = value; }
        //}

        [ConfigurationProperty("batchSize", IsRequired = false, DefaultValue = 10000)]
        public int BatchSize
        {
            get { return (int)this["batchSize"]; }
            set { this["batchSize"] = value; }
        }

        [ConfigurationProperty("isolationString", IsRequired = false, DefaultValue = "")]
        public string IsolationString
        {
            get { return (string)this["isolationString"]; }
            set { this["isolationString"] = value; }
        }

        [ConfigurationProperty("prepareSqlEnable", IsRequired = false, DefaultValue = true)]
        public bool PrepareSqlEnable
        {
            get { return (bool)this["prepareSqlEnable"]; }
            set { this["prepareSqlEnable"] = value; }
        }

        [ConfigurationProperty("isTransactional", IsRequired = false, DefaultValue = true)]
        public bool IsTransactional
        {
            get { return (bool)this["isTransactional"]; }
            set { this["isTransactional"] = value; }
        }     
    }
}