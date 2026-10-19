using Masasamjant.Data;
using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class TransactionalXmlDocumentUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TransactionalXmlDocument(null!));
            var doc = new XmlDocument();
            var tran = new TransactionalXmlDocument(doc);
            Assert.IsFalse(tran.IsDisposed);
            Assert.AreSame(doc, tran.Document);
        }

        [TestMethod]
        public void Test_BeginTransaction()
        {
            var doc = new XmlDocument();
            var transactionalDocument = new TransactionalXmlDocument(doc);
            var transaction = transactionalDocument.BeginTransaction();
            Assert.IsNotNull(transaction);
            Assert.AreEqual(TransactionState.Uncommitted, transaction.State);
            Assert.IsFalse(transaction.IsDisposed);
            Assert.AreSame(doc, transaction.Document);
            Assert.ThrowsException<InvalidOperationException>(() => transactionalDocument.BeginTransaction());  // already active transaction
            var transactionalDocument2 = new TransactionalXmlDocument(doc);
            Assert.ThrowsException<InvalidOperationException>(() => transactionalDocument2.BeginTransaction()); // locked to other transaction
            transaction.Dispose();
            transaction = transactionalDocument.BeginTransaction(); // should work now
            transaction.Dispose();
            transaction = transactionalDocument2.BeginTransaction(); // should work now
            transaction.Dispose();
            transactionalDocument.Dispose();
            transactionalDocument2.Dispose();
        }

        [TestMethod]
        public void Test_Dispose_DisposesActiveTransaction()
        {
            var doc = new XmlDocument();
            var transactionalDocument = new TransactionalXmlDocument(doc);
            var transaction = transactionalDocument.BeginTransaction();
            Assert.IsFalse(transaction.IsDisposed);
            transactionalDocument.Dispose();
            Assert.IsTrue(transaction.IsDisposed);
        }

        [TestMethod]
        public void Test_Dispose()
        {
            var doc = new XmlDocument();
            var transactionalDocument = new TransactionalXmlDocument(doc);
            Assert.IsFalse(transactionalDocument.IsDisposed);
            var transaction = transactionalDocument.BeginTransaction();
            Assert.IsFalse(transaction.IsDisposed);
            transactionalDocument.Dispose();
            Assert.IsTrue(transactionalDocument.IsDisposed);
            Assert.ThrowsException<ObjectDisposedException>(() => { var d = transactionalDocument.Document; });
            Assert.ThrowsException<ObjectDisposedException>(() => transactionalDocument.BeginTransaction());
            Assert.IsTrue(transaction.IsDisposed);
            transactionalDocument.Dispose();
        }
    }
}
