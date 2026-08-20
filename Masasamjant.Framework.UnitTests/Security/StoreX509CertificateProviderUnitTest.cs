using System.Security.Cryptography.X509Certificates;

namespace Masasamjant.Security
{
    [TestClass]
    public class StoreX509CertificateProviderUnitTest : CertificateUnitTest
    {
        [TestMethod]
        public void Test_Constructor_With_Thumbprint()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new StoreX509CertificateProvider(StoreName.My, StoreLocation.LocalMachine, null!, true));
            Assert.ThrowsException<ArgumentNullException>(() => new StoreX509CertificateProvider(StoreName.My, StoreLocation.LocalMachine, "", true));
            Assert.ThrowsException<ArgumentNullException>(() => new StoreX509CertificateProvider(StoreName.My, StoreLocation.LocalMachine, "   ", true));
            var thumbprint = GetThumbprint();
            Assert.ThrowsException<ArgumentException>(() => new StoreX509CertificateProvider((StoreName)999, StoreLocation.LocalMachine, thumbprint, true));
            Assert.ThrowsException<ArgumentException>(() => new StoreX509CertificateProvider(StoreName.My, (StoreLocation)999, thumbprint, true));
            var provider = new StoreX509CertificateProvider(StoreName.My, StoreLocation.LocalMachine, thumbprint, true);
            Assert.IsNotNull(provider);
        }

        [TestMethod]
        public void Test_Constructor_With_Subject_Name()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new StoreX509CertificateProvider(null!, StoreName.My, StoreLocation.LocalMachine, true));
            Assert.ThrowsException<ArgumentNullException>(() => new StoreX509CertificateProvider("", StoreName.My, StoreLocation.LocalMachine, true));
            Assert.ThrowsException<ArgumentNullException>(() => new StoreX509CertificateProvider("   ", StoreName.My, StoreLocation.LocalMachine, true));
            var subjectName = GetSubjectName();
            Assert.ThrowsException<ArgumentException>(() => new StoreX509CertificateProvider(subjectName, (StoreName)999, StoreLocation.LocalMachine, true));
            Assert.ThrowsException<ArgumentException>(() => new StoreX509CertificateProvider(subjectName, StoreName.My, (StoreLocation)999, true));
            var provider = new StoreX509CertificateProvider(subjectName, StoreName.My, StoreLocation.LocalMachine, true);
            Assert.IsNotNull(provider);
        }

        [TestMethod]
        public void Test_FindCertificate_Not_Found()
        {
            var thumbprint = GetThumbprint();
            var provider = new StoreX509CertificateProvider(StoreName.My, StoreLocation.CurrentUser, thumbprint, true);
            var certificate = provider.FindCertificate();
            Assert.IsNull(certificate);
        }

        [TestMethod]
        public void Test_FindCertificate_Using_Thumbprint()
        {
            var thumbprint = GetThumbprint();
            var provider = new StoreX509CertificateProvider(StoreName.Root, StoreLocation.CurrentUser, thumbprint, true);
            var cer = provider.FindCertificate();
            Assert.IsNotNull(cer);
            Assert.AreEqual(thumbprint, cer!.Thumbprint);

            thumbprint = GetExpiredThumbprint();
            provider = new StoreX509CertificateProvider(StoreName.Root, StoreLocation.CurrentUser, thumbprint, false);
            cer = provider.FindCertificate();
            Assert.IsNotNull(cer);
            Assert.AreEqual(thumbprint, cer!.Thumbprint);

            thumbprint = GetExpiredThumbprint();
            provider = new StoreX509CertificateProvider(StoreName.Root, StoreLocation.CurrentUser, thumbprint, true);
            cer = provider.FindCertificate();
            Assert.IsNull(cer);
        }

        [TestMethod]
        public void Test_FindCertificate_Using_Subject_Name()
        {
            var subjectName = GetSubjectName();
            var provider = new StoreX509CertificateProvider(subjectName, StoreName.Root, StoreLocation.CurrentUser, true);
            var cer = provider.FindCertificate();
            Assert.IsNotNull(cer);
            Assert.IsTrue(cer!.Subject.Contains(subjectName));

            subjectName = GetExpiredSubjectName();
            provider = new StoreX509CertificateProvider(subjectName, StoreName.Root, StoreLocation.CurrentUser, false);
            cer = provider.FindCertificate();
            Assert.IsNotNull(cer);
            Assert.IsTrue(cer!.Subject.Contains(subjectName));

            subjectName = GetExpiredSubjectName();
            provider = new StoreX509CertificateProvider(subjectName, StoreName.Root, StoreLocation.CurrentUser, true);
            cer = provider.FindCertificate();
            Assert.IsNull(cer);
        }
    }
}
