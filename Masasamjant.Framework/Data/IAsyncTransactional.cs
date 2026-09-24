namespace Masasamjant.Data
{
    /// <summary>
    /// Represents an object that supports asynchronous transactions.
    /// </summary>
    public interface IAsyncTransactional : ITransactional
    {
        /// <summary>
        /// Begins a transaction over the data source guarded by this instance.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the transaction instance.</returns>
        /// <exception cref="InvalidOperationException">If internal state of transactional does not allow starting a new transaction.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        Task<IAsyncTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
