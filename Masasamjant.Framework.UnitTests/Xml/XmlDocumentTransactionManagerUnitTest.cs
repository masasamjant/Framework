using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlDocumentTransactionManagerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetOrCreateLock_ReturnsSameLockForSameDocument()
        {
            var xmlDocument = new XmlDocument();
            var lock1 = XmlDocumentTransactionManager.GetOrCreateLock(xmlDocument);
            var lock2 = XmlDocumentTransactionManager.GetOrCreateLock(xmlDocument);
            Assert.AreSame(lock1, lock2);
        }

        [TestMethod]
        public void Test_GetOrCreateLock_ReturnsDifferentLocksForDifferentDocuments()
        {
            var xmlDocument1 = new XmlDocument();
            var xmlDocument2 = new XmlDocument();
            var lock1 = XmlDocumentTransactionManager.GetOrCreateLock(xmlDocument1);
            var lock2 = XmlDocumentTransactionManager.GetOrCreateLock(xmlDocument2);
            Assert.AreNotSame(lock1, lock2);
        }

        [TestMethod]
        public void Test_GetOrCreateLock_ThrowsArgumentNullExceptionForNullDocument()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocumentTransactionManager.GetOrCreateLock(null!));
        }

        [TestMethod]
        public void Test_GetOrCreateLock_ReturnsNonLocked()
        {
            var doc = new XmlDocument();
            var lck = XmlDocumentTransactionManager.GetOrCreateLock(doc);
            Assert.IsFalse(lck.IsLocked);
        }
    }
}
