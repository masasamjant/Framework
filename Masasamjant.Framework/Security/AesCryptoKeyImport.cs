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
        /// <exception cref="ArgumentException">If <paramref name="stream"/> is not readable.</exception>
        /// <exception cref="InvalidOperationException">If import fails.</exception>
        public override async Task<AesCryptoKey> ImportAsync(Stream stream)
        {
            ValidateCanRead(stream);

            try
            {
                string value = await ReadImportValueAsync(stream);
                return CreateCryptoKeyFromImportValue(value);
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException && exception.Message == "Stream contains invalid data.")
                    throw;
                else
                    throw new InvalidOperationException("Importing key from stream failed. See inner exception.", exception);
            }
        }

        private static async Task<string> ReadImportValueAsync(Stream stream)
        {
            var reader = new StreamReader(stream);
            var value = await reader.ReadToEndAsync();
            if (value.Length != (KeyLength + IVLength))
                throw new InvalidOperationException("Stream contains invalid data.");
            return value;
        }

        private static AesCryptoKey CreateCryptoKeyFromImportValue(string value)
        {
            byte[] key = Convert.FromBase64String(value.Left(KeyLength));
            byte[] iv = Convert.FromBase64String(value.Right(IVLength));
            return new AesCryptoKey(key, iv);
        }
    }
}
