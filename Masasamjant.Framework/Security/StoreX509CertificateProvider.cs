using Masasamjant.Security.Abstractions;
using System.Security.Cryptography.X509Certificates;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="X509CertificateProvider"/> that provides certificate from specified store either by thumbprint or subject name.
    /// </summary>
    public sealed class StoreX509CertificateProvider : X509CertificateProvider
    {
        private readonly string subjectName;
        private readonly string thumbprint;
        private readonly X509FindType findType;
        private readonly StoreName storeName;
        private readonly StoreLocation storeLocation;
        private readonly bool findByThumbprint;

        /// <summary>
        /// Initializes new instance of the <see cref="StoreX509CertificateProvider"/> class that provides certificate by thumbprint.
        /// </summary>
        /// <param name="storeName">The store name.</param>
        /// <param name="storeLocation">The store location.</param>
        /// <param name="thumbprint">The certificate thumbprint.</param>
        /// <param name="onlyValid"><c>true</c> if should provide only valid certificates; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="storeName"/> or <paramref name="storeLocation"/> is not defined.</exception>
        public StoreX509CertificateProvider(StoreName storeName, StoreLocation storeLocation, string thumbprint, bool onlyValid)
            : this(null, thumbprint, X509FindType.FindByThumbprint, storeName, storeLocation, onlyValid)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="StoreX509CertificateProvider"/> class that provides certificate by subject name.
        /// </summary>
        /// <param name="subjectName">The subject name.</param>
        /// <param name="storeName">The store name.</param>
        /// <param name="storeLocation">The store location.</param>
        /// <param name="onlyValid"><c>true</c> if should provide only valid certificates; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="storeName"/> or <paramref name="storeLocation"/> is not defined.</exception>
        public StoreX509CertificateProvider(string subjectName, StoreName storeName, StoreLocation storeLocation, bool onlyValid)
            : this(subjectName, null, X509FindType.FindBySubjectName, storeName, storeLocation, onlyValid)
        { }

        private StoreX509CertificateProvider(string? subjectName, string? thumbprint, X509FindType findType, StoreName storeName, StoreLocation storeLocation, bool onlyValid)
            : base(onlyValid)
        {
            this.subjectName = subjectName ?? string.Empty;
            this.thumbprint = thumbprint ?? string.Empty;
            this.findType = findType;
            this.storeName = Enum.IsDefined(storeName) ? storeName : throw new ArgumentException("The value is not defined.", nameof(storeName));
            this.storeLocation = Enum.IsDefined(storeLocation) ? storeLocation : throw new ArgumentException("The value is not defined.", nameof(storeLocation));
            this.findByThumbprint = FindByThumbprint();
        }

        /// <summary>
        /// Tries to find <see cref="X509Certificate2"/> certificate.
        /// </summary>
        /// <returns>A <see cref="X509Certificate2"/> or <c>null</c>, if not found  or not valid when <see cref="OnlyValid"/> is <c>true</c>.</returns>
        /// <exception cref="X509CertificateException">If exception occurs when loading certificate.</exception>
        public override X509Certificate2? FindCertificate()
        {
            var findValue = findByThumbprint ? thumbprint : subjectName;
            var store = new X509Store(storeName, storeLocation);
            
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(findType, findValue, OnlyValid);
                var certificate = certificates.FirstOrDefault();
                return certificate;
            }
            catch (Exception exception)
            {
                throw new X509CertificateException($"Exception occurred when attempt to get cerficate '{findValue}' from '{storeName}\\{storeLocation}'.", exception);
            }
            finally 
            {
                if (store.IsOpen)
                    store.Close();
            }
        }

        /// <summary>
        /// Creates <see cref="X509CertificateException"/> to throw when certificate with specified criteria is not found.
        /// </summary>
        /// <returns>A <see cref="X509CertificateException"/>.</returns>
        protected override X509CertificateException GetCertificateNotFoundException()
        {
            var findValue = findByThumbprint ? thumbprint : subjectName;
            var message = findByThumbprint 
                ? $"Certificate with thumbprint '{findValue}' not found from '{storeName}\\{storeLocation}'." 
                : $"Certificate with subject name '{findValue}' not found from '{storeName}\\{storeLocation}'.";
            return new X509CertificateException(message);
        }

        private bool FindByThumbprint()
        {
            if (findType == X509FindType.FindByThumbprint)
                return true;
            else if (findType == X509FindType.FindBySubjectName)
                return false;
            else
                throw new NotSupportedException($"The find type '{findType}' is not supported.");
        }
    }
}
