namespace Masasamjant.Data
{
    /// <summary>
    /// Represents a transaction that can be committed or rolled back.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Gets the current state.
        /// </summary>
        TransactionState State { get; }

        /// <summary>
        /// Commit changes made during the transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">If transaction already committed or rolled back or other internal state prevents commit.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        void Commit();

        /// <summary>
        /// Rollback changes made during the transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">If transaction already committed or rolled back or other internal state prevents rollback.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        void Rollback();
    }
}
