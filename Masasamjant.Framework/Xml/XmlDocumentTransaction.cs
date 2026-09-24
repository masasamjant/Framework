using Masasamjant.Data;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents a transaction for an XML document, allowing changes to be made and either committed or rolled back.
    /// Instance of this class is not thread-safe. If multiple threads are accessing the same XML document, they must synchronize access to the document and its transaction.
    /// </summary>
    /// <remarks>Transaction should be disposed immediately after use to release document associated with transaction.</remarks>
    public sealed class XmlDocumentTransaction : ITransaction, IDisposable
    {
        /// <summary>
        /// Notifies when the transaction has been disposed.
        /// </summary>
        internal event EventHandler? Disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentTransaction"/> class with the specified XML document.
        /// </summary>
        /// <param name="document">The XML document to be managed by the transaction.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="document"/> is null.</exception>
        internal XmlDocumentTransaction(XmlDocument document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Xml = document.OuterXml;
            IsDisposed = false;
        }

        /// <summary>
        /// Finalizes the current instance.
        /// </summary>
        ~XmlDocumentTransaction()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the transaction state.
        /// </summary>
        public TransactionState State { get; private set; } = TransactionState.Uncommitted;

        internal XmlDocument? Document { get; private set; }

        private string? Xml { get; set; }

        [MemberNotNullWhen(false, nameof(Document))]
        internal bool IsDisposed { get; private set; }

        /// <summary>
        /// Commit changes made during the transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// If transaction already committed or rolled back.
        /// -or-
        /// If this transaction instance does not own the transaction lock for the document.
        /// </exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public void Commit()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            CheckIsUncommitted();

            var documentLock = XmlDocumentTransactionManager.GetOrCreateLock(Document);

            lock (documentLock)
            {
                if (!documentLock.IsLockOwner(this))
                    throw new InvalidOperationException("This transaction instance does not own the transaction lock for the document.");

                State = TransactionState.Committed;
                Xml = null;
            }
        }

        /// <summary>
        /// Rollback changes made during the transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">If transaction already committed or rolled back or other internal state prevents rollback.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public void Rollback()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            CheckIsUncommitted();

            var documentLock = XmlDocumentTransactionManager.GetOrCreateLock(Document);
            
            lock (documentLock)
            {
                if (!documentLock.IsLockOwner(this))
                    throw new InvalidOperationException("This transaction instance does not own the transaction lock for the document.");

                RestoreXml(Document);
                State = TransactionState.RolledBack;
                Xml = null;
            }
        }

        /// <summary>Disposes the current instance.</summary>
        /// <remarks>If the transaction is uncommitted when disposed, it will automatically rollback changes made to the XML document.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            // If transaction is uncommitted when disposed, then rollback changes.
            if (State == TransactionState.Uncommitted)
            {
                var documentLock = XmlDocumentTransactionManager.GetOrCreateLock(Document);

                lock (documentLock)
                {
                    if (documentLock.IsLockOwner(this))
                    {
                        RestoreXml(Document);
                        State = TransactionState.RolledBack;
                    }
                }
            }

            Xml = null;
            Document = null;
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        private void RestoreXml(XmlDocument document)
        {
            if (string.IsNullOrWhiteSpace(Xml))
            {
                if (document.ChildNodes.Count > 0)
                    document.RemoveAll();
            }
            else
                document.LoadXml(Xml);
        }

        private void CheckIsUncommitted()
        {
            if (State != TransactionState.Uncommitted)
                throw new InvalidOperationException("Transaction has already been committed or rolled back.");
        }
    }
}
