using Masasamjant.Security.Abstractions;
using System.Security.Cryptography.X509Certificates;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="X509CertificateProvider"/> that provides certificate from specified file.
    /// </summary>
    public class FileX509CertificateProvider : X509CertificateProvider
    {
        private readonly string certificateFilePath;

        /// <summary>
        /// Initializes new instance of the <see cref="FileX509CertificateProvider"/> class.
        /// </summary>
        /// <param name="certificateFilePath">The path of certificate file.</param>
        /// <param name="onlyValid"><c>true</c> to get only valid certificate; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="certificateFilePath"/> is empty or only whitespace.</exception>
        public FileX509CertificateProvider(string certificateFilePath, bool onlyValid)
            : base(onlyValid)
        {
            if (string.IsNullOrEmpty(certificateFilePath))
                throw new ArgumentNullException(nameof(certificateFilePath), "The value is empty or only whitespace.");

            this.certificateFilePath = certificateFilePath;
        }

        /// <summary>
        /// Tries to find <see cref="X509Certificate2"/> certificate.
        /// </summary>
        /// <returns>A <see cref="X509Certificate2"/> or <c>null</c>, if not found  or not valid when <see cref="OnlyValid"/> is <c>true</c>.</returns>
        /// <exception cref="X509CertificateException">If exception occurs when loading certificate.</exception>
        public sealed override X509Certificate2? FindCertificate()
        {
            try
            {
                if (!File.Exists(certificateFilePath))
                    return null;

                var rawData = File.ReadAllBytes(certificateFilePath);
                var certificate = X509CertificateLoader.LoadCertificate(rawData);

                if (OnlyValid && !IsValid(certificate))
                    return null;

                return certificate;
            }
            catch (Exception exception)
            {
                throw new X509CertificateException($"Error when loading certificate data from '{certificateFilePath}'.", exception);
            }
        }

        /// <summary>
        /// Creates <see cref="X509CertificateException"/> to throw when certificate with specified criteria is not found.
        /// </summary>
        /// <returns>A <see cref="X509CertificateException"/>.</returns>
        protected override X509CertificateException GetCertificateNotFoundException()
        {
            return new X509CertificateException($"Certificate file '{certificateFilePath}' does not exist or it contains data of non-valid certificate.");
        }

        /// <summary>
        /// Check if <see cref="X509Certificate2"/> is valid. Default implementation compares <see cref="X509Certificate2.NotBefore"/> 
        /// and <see cref="X509Certificate2.NotAfter"/> to <see cref="DateTime.Today"/>.
        /// </summary>
        /// <param name="certificate">The <see cref="X509Certificate2"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="certificate"/> is considered to be valid; <c>false</c> otherwise.</returns>
        protected virtual bool IsValid(X509Certificate2 certificate)
        {
            DateTime today = DateTime.Today;
            if (certificate.NotBefore > today)
                return false;
            if (certificate.NotAfter <= today)
                return false;
            return true;
        }
    }
}
