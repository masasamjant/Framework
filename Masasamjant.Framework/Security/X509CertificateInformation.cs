using System.Security.Cryptography.X509Certificates;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents information about an X509 certificate.
    /// </summary>
    public sealed class X509CertificateInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateInformation"/> class with the specified certificate file path.
        /// </summary>
        /// <param name="certificateFilePath">The path to the certificate file.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="certificateFilePath"/> is <c>null</c>, empty or only whitespace.</exception>
        public X509CertificateInformation(string certificateFilePath)
        {
            if (string.IsNullOrWhiteSpace(certificateFilePath))
                throw new ArgumentNullException(nameof(certificateFilePath), "The certificate file path cannot be null, empty or only whitespace.");

            CertificateFilePath = certificateFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateInformation"/> class with the specified thumbprint, store name and store location.
        /// </summary>
        /// <param name="thumbprint">The thumbprint of the certificate.</param>
        /// <param name="storeName">The name of the store where the certificate is located.</param>
        /// <param name="storeLocation">The location of the store where the certificate is located.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="thumbprint"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="storeName"/> or <paramref name="storeLocation"/> is not defined.</exception>
        public X509CertificateInformation(string thumbprint, StoreName storeName, StoreLocation storeLocation)
        {
            if (string.IsNullOrWhiteSpace(thumbprint))
                throw new ArgumentNullException(nameof(thumbprint), "The certificate thumbprint cannot be null, empty or only whitespace.");

            Thumbprint = thumbprint;
            StoreName = ValidateStoreName(storeName);
            StoreLocation = ValidateStoreLocation(storeLocation);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateInformation"/> class with the specified subject name, store name and store location.
        /// </summary>
        /// <param name="storeName">The name of the store where the certificate is located.</param>
        /// <param name="storeLocation">The location of the store where the certificate is located.</param>
        /// <param name="subjectName">The subject name of the certificate.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="subjectName"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="storeName"/> or <paramref name="storeLocation"/> is not defined.</exception>
        public X509CertificateInformation(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            if (string.IsNullOrWhiteSpace(subjectName))
                throw new ArgumentNullException(nameof(subjectName), "The certificate subject name cannot be null, empty or only whitespace.");

            SubjectName = subjectName;
            StoreName = ValidateStoreName(storeName);
            StoreLocation = ValidateStoreLocation(storeLocation);
        }

        /// <summary>
        /// Gets the path to the certificate file. 
        /// If this property is set, the certificate will be loaded from the file instead of the store.
        /// </summary>
        public string? CertificateFilePath { get; }

        /// <summary>
        /// Gets the subject name of the certificate.
        /// This property is used when loading the certificate from the store.
        /// </summary>
        public string? SubjectName { get; }

        /// <summary>
        /// Gets the thumbprint of the certificate.
        /// This property is used when loading the certificate from the store.
        /// </summary>
        public string? Thumbprint { get; }

        /// <summary>
        /// Gets the store name of the certificate. 
        /// This property is used when loading the certificate from the store.
        /// </summary>
        public StoreName? StoreName { get; }

        /// <summary>
        /// Gets the store location of the certificate. 
        /// This property is used when loading the certificate from the store.
        /// </summary>
        public StoreLocation? StoreLocation { get; }

        /// <summary>
        /// Gets <see cref="X509Certificate2"/> represented by this information.
        /// </summary>
        /// <param name="onlyValid"><c>true</c> to get only valid certificate; <c>false</c> otherwise.</param>
        /// <returns><see cref="X509Certificate2"/> represented by this information.</returns>
        /// <exception cref="FileNotFoundException">If represents certificate in file and the certificate file does not exist.</exception>
        /// <exception cref="X509CertificateException">If the certificate cannot be found or is not valid when <paramref name="onlyValid"/> is <c>true</c>.</exception>
        public X509Certificate2 GetCertificate(bool onlyValid = true)
        {
            if (string.IsNullOrWhiteSpace(CertificateFilePath))
            {
                return GetStoreCertificate(onlyValid);
            }
            else
            {
                return GetFileCertificate(onlyValid);
            }
        }

        private X509Certificate2 GetFileCertificate(bool onlyValid)
        {
            if (!File.Exists(CertificateFilePath))
                throw new FileNotFoundException($"The certificate file '{CertificateFilePath}' does not exist.", CertificateFilePath);

            var provider = new FileX509CertificateProvider(CertificateFilePath, onlyValid);
            var certificate = provider.FindCertificate();
            if (certificate == null)
                throw new X509CertificateException($"The certificate file '{CertificateFilePath}' does not contain data of valid certificate.");
            return certificate;
        }

        private X509Certificate2 GetStoreCertificate(bool onlyValid)
        {
            var storeName = StoreName!.Value;
            var storeLocation = StoreLocation!.Value;

            var provider = CreateStoreProvider(storeName, storeLocation, onlyValid);
            var certificate = provider.FindCertificate();

            if (certificate == null)
                throw new X509CertificateException("The certificate store does not contain data of valid certificate.");

            return certificate; 
        }

        private StoreX509CertificateProvider CreateStoreProvider(StoreName storeName, StoreLocation storeLocation, bool onlyValid)
        {
            if (string.IsNullOrWhiteSpace(Thumbprint))
            {
                if (string.IsNullOrWhiteSpace(SubjectName))
                    throw new InvalidOperationException("Either Thumbprint or SubjectName must be specified to get certificate from store.");

                return new StoreX509CertificateProvider(SubjectName, storeName, storeLocation, onlyValid);
            }
            else
            {
                return new StoreX509CertificateProvider(storeName, storeLocation, Thumbprint, onlyValid);
            }
        }

        private static StoreName ValidateStoreName(StoreName storeName)
        {
            if (!Enum.IsDefined(typeof(StoreName), storeName))
                throw new ArgumentException($"The store name '{storeName}' is not valid.", nameof(storeName));

            return storeName;
        }

        private static StoreLocation ValidateStoreLocation(StoreLocation storeLocation)
        {
            if (!Enum.IsDefined(typeof(StoreLocation), storeLocation))
                throw new ArgumentException($"The store location '{storeLocation}' is not valid.", nameof(storeLocation));

            return storeLocation;
        }
    }
}
