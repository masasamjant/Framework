namespace Masasamjant.Security
{
    /// <summary>
    /// Represents exception thrown when error related to certificates occurs.
    /// </summary>
    public class X509CertificateException : Exception
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="X509CertificateException"/> class.
        /// </summary>
        public X509CertificateException()
            : this("Unexpected exception occurred while using certificate.")
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="X509CertificateException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public X509CertificateException(string message)
            : this(message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="X509CertificateException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public X509CertificateException(string message, Exception? innerException)
            : base(message, innerException)
        { }
    }
}
