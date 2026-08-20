using System.Security.Cryptography.X509Certificates;

namespace Masasamjant.Security
{
    [TestClass]
    public class X509CertificateInformationUnitTest : CertificateUnitTest
    {
        [TestMethod]
        public void Test_Constructor_With_File_Path()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation(null!));
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation(string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation("   "));
            
            var certificateFilePath = GetNotFoundTestCertificateFilePath();
            var information = new X509CertificateInformation(certificateFilePath);
            Assert.IsNotNull(information);
            Assert.AreEqual(certificateFilePath, information.CertificateFilePath);
            Assert.IsFalse(information.StoreName.HasValue);
            Assert.IsFalse(information.StoreLocation.HasValue);
            Assert.IsNull(information.Thumbprint);
            Assert.IsNull(information.SubjectName);

            certificateFilePath = GetTestCertificateFilePath();
            information = new X509CertificateInformation(certificateFilePath);
            Assert.IsNotNull(information);
            Assert.AreEqual(certificateFilePath, information.CertificateFilePath);
            Assert.IsFalse(information.StoreName.HasValue);
            Assert.IsFalse(information.StoreLocation.HasValue);
            Assert.IsNull(information.Thumbprint);
            Assert.IsNull(information.SubjectName);
        }

        [TestMethod]
        public void Test_Constructor_With_Thumbprint()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation(null!, StoreName.My, StoreLocation.CurrentUser));
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation(string.Empty, StoreName.My, StoreLocation.CurrentUser));
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation("   ", StoreName.My, StoreLocation.CurrentUser));
            var thumbprint = GetThumbprint();
            Assert.ThrowsException<ArgumentException>(() => new X509CertificateInformation(thumbprint, (StoreName)999, StoreLocation.CurrentUser));
            Assert.ThrowsException<ArgumentException>(() => new X509CertificateInformation(thumbprint, StoreName.My, (StoreLocation)999));
            var information = new X509CertificateInformation(thumbprint, StoreName.My, StoreLocation.CurrentUser);
            Assert.IsNotNull(information);
            Assert.AreEqual(thumbprint, information.Thumbprint);
            Assert.AreEqual(StoreName.My, information.StoreName);
            Assert.AreEqual(StoreLocation.CurrentUser, information.StoreLocation);
            Assert.IsNull(information.CertificateFilePath);
            Assert.IsNull(information.SubjectName);
        }

        [TestMethod]
        public void Test_Constructor_With_SubjectName()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation(StoreName.My, StoreLocation.CurrentUser, null!));
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation(StoreName.My, StoreLocation.CurrentUser, string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => new X509CertificateInformation(StoreName.My, StoreLocation.CurrentUser, "   "));
            var subjectName = GetSubjectName();
            Assert.ThrowsException<ArgumentException>(() => new X509CertificateInformation((StoreName)999, StoreLocation.CurrentUser, subjectName));
            Assert.ThrowsException<ArgumentException>(() => new X509CertificateInformation(StoreName.My, (StoreLocation)999, subjectName));
            var information = new X509CertificateInformation(StoreName.My, StoreLocation.CurrentUser, subjectName);
            Assert.IsNotNull(information);
            Assert.AreEqual(subjectName, information.SubjectName);
            Assert.AreEqual(StoreName.My, information.StoreName);
            Assert.AreEqual(StoreLocation.CurrentUser, information.StoreLocation);
            Assert.IsNull(information.CertificateFilePath);
            Assert.IsNull(information.Thumbprint);
        }

        [TestMethod]
        public void Test_GetCertificate_From_File()
        {
            var certificateFilePath = GetNotFoundTestCertificateFilePath();
            var information = new X509CertificateInformation(certificateFilePath);
            Assert.ThrowsException<FileNotFoundException>(() => information.GetCertificate());

            certificateFilePath = GetExpiredTestCertificateFilePath();
            information = new X509CertificateInformation(certificateFilePath);
            Assert.ThrowsException<X509CertificateException>(() => information.GetCertificate());

            var certificate = information.GetCertificate(false);
            Assert.IsNotNull(certificate);
            Assert.AreEqual(GetExpiredThumbprint(), certificate.Thumbprint);

            certificateFilePath = GetTestCertificateFilePath();
            information = new X509CertificateInformation(certificateFilePath);
            certificate = information.GetCertificate();
            Assert.IsNotNull(certificate);
            Assert.AreEqual(GetThumbprint(), certificate.Thumbprint);
        }

        [TestMethod]
        public void Test_GetCertificate_From_Store()
        {
            var thumbprint = GetThumbprint();
            var information = new X509CertificateInformation(thumbprint, StoreName.Root, StoreLocation.CurrentUser);
            var certificate = information.GetCertificate();
            Assert.IsNotNull(certificate);
            Assert.AreEqual(thumbprint, certificate.Thumbprint);

            thumbprint = GetExpiredThumbprint();
            information = new X509CertificateInformation(thumbprint, StoreName.Root, StoreLocation.CurrentUser);
            Assert.ThrowsException<X509CertificateException>(() => information.GetCertificate());
            certificate = information.GetCertificate(false);
            Assert.IsNotNull(certificate);
            Assert.AreEqual(thumbprint, certificate.Thumbprint);

            var subjectName = GetSubjectName();
            information = new X509CertificateInformation(StoreName.Root, StoreLocation.CurrentUser, subjectName);
            certificate = information.GetCertificate();
            Assert.IsNotNull(certificate);
            Assert.IsTrue(certificate.Subject.Contains(subjectName));

            subjectName = GetExpiredSubjectName();
            information = new X509CertificateInformation(StoreName.Root, StoreLocation.CurrentUser, subjectName);
            Assert.ThrowsException<X509CertificateException>(() => information.GetCertificate());
            certificate = information.GetCertificate(false);
            Assert.IsNotNull(certificate);
            Assert.IsTrue(certificate.Subject.Contains(subjectName));
        }
    }
}
