using Masasamjant.Data;
using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlDocumentTransactionUnitTest : UnitTest
    {
        private const string Xml = @"<?xml version=""1.0"" encoding=""utf-8""?><root><child>value</child></root>";

        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new XmlDocumentTransaction(null!));
            var doc = new XmlDocument();
            var tran = new XmlDocumentTransaction(doc);
            Assert.IsFalse(tran.IsDisposed);
            Assert.AreEqual(TransactionState.Uncommitted, tran.State);
            Assert.AreSame(doc, tran.Document);
        }

        [TestMethod]
        public void Test_Commit_Without_Locking()
        {
            var doc = new XmlDocument();
            var tran = new XmlDocumentTransaction(doc);
            Assert.ThrowsException<InvalidOperationException>(() => tran.Commit());
        }

        [TestMethod]
        public void Test_Commit()
        {
            var doc = new XmlDocument();
            doc.LoadXml(Xml);
            var tran = new XmlDocumentTransaction(doc);
            var lck = XmlDocumentTransactionManager.GetOrCreateLock(doc);
            lck.Lock(tran);
            var root = doc.SelectSingleNode("/root");
            var node = doc.CreateElement("child");
            root!.AppendChild(node);
            tran.Commit();
            var n = doc.SelectNodes("/root/child");
            Assert.IsNotNull(n);
            Assert.AreEqual(2, n!.Count);
            Assert.AreEqual(TransactionState.Committed, tran.State);
            Assert.ThrowsException<InvalidOperationException>(() => tran.Commit());
            tran.Dispose();
            Assert.IsTrue(tran.IsDisposed);
        }

        [TestMethod]
        public void Test_Rollback_Without_Locking()
        {
            var doc = new XmlDocument();
            var tran = new XmlDocumentTransaction(doc);
            Assert.ThrowsException<InvalidOperationException>(() => tran.Rollback());
        }

        [TestMethod]
        public void Test_Rollback()
        {
            var doc = new XmlDocument();
            doc.LoadXml(Xml);
            var tran = new XmlDocumentTransaction(doc);
            var lck = XmlDocumentTransactionManager.GetOrCreateLock(doc);
            lck.Lock(tran);
            var node = doc.CreateElement("child");
            var root = doc.SelectSingleNode("/root");
            root!.AppendChild(node);
            tran.Rollback();
            var n = doc.SelectNodes("/root/child");
            Assert.IsNotNull(n);
            Assert.AreEqual(1, n!.Count);
            Assert.AreEqual(TransactionState.RolledBack, tran.State);
            Assert.ThrowsException<InvalidOperationException>(() => tran.Rollback());
            tran.Dispose();
            Assert.IsTrue(tran.IsDisposed);
        }

        [TestMethod]
        public void Test_Dispose()
        {
            var doc = new XmlDocument();
            var tran = new XmlDocumentTransaction(doc);
            Assert.IsFalse(tran.IsDisposed);
            bool disposed = false;
            tran.Disposed += (s, e) => {
                disposed = true;
            };
            tran.Dispose();
            Assert.IsTrue(disposed);
            Assert.IsTrue(tran.IsDisposed);
            Assert.ThrowsException<ObjectDisposedException>(() => tran.Commit());
            Assert.ThrowsException<ObjectDisposedException>(() => tran.Rollback());
            tran.Dispose();
        }
    }
}
