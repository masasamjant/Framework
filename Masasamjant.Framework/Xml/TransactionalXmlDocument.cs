using Masasamjant.Data;
using System.Xml;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents a transactional XML document that allows changes to be made within a transaction scope, which can be committed or rolled back.
    /// </summary>
    public sealed class TransactionalXmlDocument : ITransactional, IDisposable
    {
        private XmlDocumentTransaction? currentTransaction;
        private XmlDocument? document;

        /// <summary>
        /// Intializes new instance of the <see cref="TransactionalXmlDocument"/> class with the specified XML document.
        /// </summary>
        /// <param name="document">The XML document to be managed by this transactional instance.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="document"/> is null.</exception>
        public TransactionalXmlDocument(XmlDocument document)
        {
            this.document = document ?? throw new ArgumentNullException(nameof(document));
        }

        /// <summary>
        /// Finalizes the current instance.
        /// </summary>
        ~TransactionalXmlDocument()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the associated XML document that is managed by this transactional instance.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public XmlDocument Document
        {
            get
            {
                if (IsDisposed || document == null)
                    throw new ObjectDisposedException(GetType().FullName);

                return document;
            }
        }

        /// <summary>
        /// Gets if instance is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Begins a transaction over the data source guarded by this instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">If internal state of transactional does not allow starting a new transaction.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public XmlDocumentTransaction BeginTransaction()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);

            if (currentTransaction != null)
                throw new InvalidOperationException("A transaction with this document is already in progress. Previous transaction must be disposed before beginning new one.");

            var transaction = new XmlDocumentTransaction(Document);
            var documentLock = XmlDocumentTransactionManager.GetOrCreateLock(Document);

            lock (documentLock)
            {
                if (documentLock.IsLocked)
                    throw new InvalidOperationException("Another transaction has already obtained the lock for this document.");

                documentLock.Lock(transaction);
                currentTransaction = transaction;
                currentTransaction.Disposed += OnCurrentTransactionDisposed;
                return currentTransaction;
            }
        }

        /// <summary>
        /// Disposes the current instance and releases any resources associated with it.
        /// </summary>
        /// <remarks>If there is active transaction, it will be disposed also.</remarks>
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

            if (disposing)
            {
                var transaction = currentTransaction;

                if (transaction != null && !transaction.IsDisposed)
                    transaction.Dispose();
            }

            document = null;
        }

        private void OnCurrentTransactionDisposed(object? sender, EventArgs e)
        {
            if (currentTransaction != null)
            {
                var doc = document;
                
                if (doc != null)
                {
                    var documentLock = XmlDocumentTransactionManager.GetOrCreateLock(doc);

                    lock (documentLock)
                    {
                        if (documentLock.IsLockOwner(currentTransaction))
                            documentLock.Release(currentTransaction);
                    }
                }

                currentTransaction.Disposed -= OnCurrentTransactionDisposed;
                currentTransaction = null;
            }
        }

        ITransaction ITransactional.BeginTransaction()
        {
            return BeginTransaction();
        }
    }
}
