using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents service that will import <see cref="AesCryptoKey"/> from stream.
    /// </summary>
    public sealed class AesCryptoKeyImport : CryptoKeyImport<AesCryptoKey>
    {
        private const int KeyLength = 44;
        private const int IVLength = 24;

        /// <summary>
        /// Imports <see cref="AesCryptoKey"/> from specified stream.
        /// </summary>
        /// <param name="stream">The stream to read imported key.</param>
        /// <returns>A task representing import.</returns>
        /// <exception cref="InvalidOperationException">If import fails.</exception>
        public override async Task<AesCryptoKey> ImportAsync(Stream stream)
        {
            try
            {
                string s;

                var reader = new StreamReader(stream);
                s = await reader.ReadToEndAsync();

                if (s.Length != (KeyLength + IVLength))
                    throw new InvalidOperationException("Stream contains invalid data.");

                string k = s.Left(KeyLength);
                string v = s.Right(IVLength);
                byte[] key = Convert.FromBase64String(k);
                byte[] iv = Convert.FromBase64String(v);
                return new AesCryptoKey(key, iv);
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException && exception.Message == "Stream contains invalid data.")
                    throw;
                else
                    throw new InvalidOperationException("Importing key from stream failed. See inner exception.", exception);
            }
        }
    }
}
