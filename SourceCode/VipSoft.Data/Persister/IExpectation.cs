using System.Data;

namespace VipSoft.Data.Persister
{
    public interface IExpectation
    {
        void VerifyOutcomeNonBatched(int rowCount, IDbCommand statement);
        bool CanBeBatched { get; }

        /// <summary>
        /// Expected row count. Valid only for batchable expectations.
        /// </summary>
        int ExpectedRowCount { get; }
    }
}