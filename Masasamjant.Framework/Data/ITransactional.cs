namespace Masasamjant.Data
{
    /// <summary>
    /// Represents an object that supports transactions.
    /// </summary>
    public interface ITransactional : IDisposable
    {
        /// <summary>
        /// Begins a transaction over the data source guarded by this instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">If internal state of transactional does not allow starting a new transaction.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        ITransaction BeginTransaction();
    }
}
