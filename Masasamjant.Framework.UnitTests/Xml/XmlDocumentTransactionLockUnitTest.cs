using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlDocumentTransactionLockUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Default_Constructor()
        {
            var doc = new XmlDocument();
            var tran = new XmlDocumentTransaction(doc);
            var lock1 = new XmlDocumentTransactionLock();
            Assert.IsNotNull(lock1);
            Assert.IsFalse(lock1.IsLocked);
            Assert.IsFalse(lock1.IsLockOwner(tran));
        }

        [TestMethod]
        public void Test_Lock()
        {
            var doc = new XmlDocument();
            var tran = new XmlDocumentTransaction(doc);
            var lock1 = new XmlDocumentTransactionLock();
            Assert.IsFalse(lock1.IsLockOwner(tran));
            lock1.Lock(tran);
            Assert.IsTrue(lock1.IsLockOwner(tran));
            lock1.Lock(tran);
            Assert.IsTrue(lock1.IsLockOwner(tran));
            var doc2 = new XmlDocument();
            var tran2 = new XmlDocumentTransaction(doc2);
            Assert.IsFalse(lock1.IsLockOwner(tran2));
            Assert.ThrowsException<InvalidOperationException>(() => lock1.Lock(tran2));
        }

        [TestMethod]
        public void Test_Release()
        {
            var doc = new XmlDocument();
            var tran = new XmlDocumentTransaction(doc);
            var lock1 = new XmlDocumentTransactionLock();
            Assert.IsFalse(lock1.IsLockOwner(tran));
            lock1.Lock(tran);
            Assert.IsTrue(lock1.IsLockOwner(tran));
            var doc2 = new XmlDocument();
            var tran2 = new XmlDocumentTransaction(doc2);
            Assert.IsFalse(lock1.IsLockOwner(tran2));
            Assert.ThrowsException<InvalidOperationException>(() => lock1.Release(tran2));
            lock1.Release(tran);
            Assert.IsFalse(lock1.IsLockOwner(tran));
        }
    }
}
