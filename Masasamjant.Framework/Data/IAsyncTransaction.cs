namespace Masasamjant.Data
{
    /// <summary>
    /// Represents an asynchronous transaction that can be committed or rolled back.
    /// </summary>
    public interface IAsyncTransaction : ITransaction
    {
        /// <summary>
        /// Commit changes made during the transaction.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous commit operation.</returns>
        /// <exception cref="InvalidOperationException">If transaction already committed or rolled back or other internal state prevents commit.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rollback changes made during the transaction.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous rollback operation.</returns>
        /// <exception cref="InvalidOperationException">If transaction already committed or rolled back or other internal state prevents rollback.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
