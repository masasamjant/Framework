using System.Security.Cryptography.X509Certificates;

namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents provider of <see cref="X509Certificate2"/>.
    /// </summary>
    public abstract class X509CertificateProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="X509CertificateProvider"/> class.
        /// </summary>
        /// <param name="onlyValid"><c>true</c> if should provide only valid certificates; <c>false</c> otherwise.</param>
        protected X509CertificateProvider(bool onlyValid)
        {
            OnlyValid = onlyValid;
        }

        /// <summary>
        /// Gets whether or not should provide only valid certificate.
        /// </summary>
        public bool OnlyValid { get; }

        /// <summary>
        /// Gets <see cref="X509Certificate2"/> certificate.
        /// </summary>
        /// <returns>A <see cref="X509Certificate2"/>.</returns>
        /// <exception cref="X509CertificateException">If certificate is not found.</exception>
        public X509Certificate2 GetCertificate()
        {
            var certificate = FindCertificate();

            if (certificate == null)
            {
                var exception = GetCertificateNotFoundException();
                throw exception;
            }

            return certificate;
        }

        /// <summary>
        /// Tries to find <see cref="X509Certificate2"/> certificate.
        /// </summary>
        /// <returns>A <see cref="X509Certificate2"/> or <c>null</c>, if not found  or not valid when <see cref="OnlyValid"/> is <c>true</c>.</returns>
        /// <exception cref="X509CertificateException">If exception occurs when loading certificate.</exception>
        public abstract X509Certificate2? FindCertificate();

        /// <summary>
        /// Creates <see cref="X509CertificateException"/> to throw when certificate with specified criteria is not found.
        /// </summary>
        /// <returns>A <see cref="X509CertificateException"/>.</returns>
        protected abstract X509CertificateException GetCertificateNotFoundException();
    }
}
