namespace Masasamjant.Security
{
    [TestClass]
    public class FileX509CertificateProviderUnitTest : CertificateUnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var certificateFilePath = GetTestCertificateFilePath();
            var provider = new FileX509CertificateProvider(certificateFilePath, true);
            Assert.IsNotNull(provider);
            Assert.IsTrue(provider.OnlyValid);
            provider = new FileX509CertificateProvider(certificateFilePath, false);
            Assert.IsNotNull(provider);
            Assert.IsFalse(provider.OnlyValid);
            Assert.ThrowsException<ArgumentNullException>(() => new FileX509CertificateProvider(null!, true));
            Assert.ThrowsException<ArgumentNullException>(() => new FileX509CertificateProvider(string.Empty, true));
            Assert.ThrowsException<ArgumentNullException>(() => new FileX509CertificateProvider("   ", true));
        }

        [TestMethod]
        public void Test_FindCertificate_File_Not_Exist()
        {
            var certificateFilePath = GetNotFoundTestCertificateFilePath();
            var provider = new FileX509CertificateProvider(certificateFilePath, true);
            var certificate = provider.FindCertificate();
            Assert.IsNull(certificate);
        }

        [TestMethod]
        public void Test_FindCertificate_File_With_Invalid_Data()
        {
            var certificateFilePath = GetInvalidTestCertificateFilePath();
            var provider = new FileX509CertificateProvider(certificateFilePath, true);
            Assert.ThrowsException<X509CertificateException>(() => provider.FindCertificate());
        }

        [TestMethod]
        public void Test_FindCertificate_NonValid()
        {
            var thumbprint = GetExpiredThumbprint();
            var certificateFilePath = GetExpiredTestCertificateFilePath();
            var provider = new FileX509CertificateProvider(certificateFilePath, false);
            var cer = provider.FindCertificate();
            Assert.IsNotNull(cer);
            Assert.AreEqual(thumbprint, cer!.Thumbprint);
        }

        [TestMethod]
        public void Test_FindCertificate_OnlyValid()
        {
            var thumbprint = GetThumbprint();
            var certificateFilePath = GetTestCertificateFilePath();
            var provider = new FileX509CertificateProvider(certificateFilePath, true);
            var cer = provider.FindCertificate();
            Assert.IsNotNull(cer);
            Assert.AreEqual(thumbprint, cer!.Thumbprint);

            certificateFilePath = GetExpiredTestCertificateFilePath();
            provider = new FileX509CertificateProvider(certificateFilePath, true);
            cer = provider.FindCertificate();
            Assert.IsNull(cer);
        }
    }
}
