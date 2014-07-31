using System;

namespace VipSoft.Data.Transaction
{
	/// <summary>
	/// This is used as a marker interface for the different 
	/// transaction context required for each session
	/// </summary>
	public interface ITransactionContext : IDisposable
	{
		bool ShouldCloseSessionOnDistributedTransactionCompleted { get; set; }
	}
}