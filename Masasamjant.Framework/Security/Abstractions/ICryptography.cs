namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents cryptography component that creates cryptography keys.
    /// </summary>
    /// <typeparam name="TCryptoKey">The type of the cryptography key.</typeparam>
    public interface ICryptography<TCryptoKey> where TCryptoKey : CryptoKey
    {
        /// <summary>
        /// Creates new <typeparamref name="TCryptoKey"/> instance from specified password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>A cryptography key of <typeparamref name="TCryptoKey"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        TCryptoKey CreateCryptoKey(string password, Salt salt);
    }
}
