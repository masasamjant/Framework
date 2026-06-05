using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents service that will import <see cref="AesCryptoKey"/> from stream in raw bytes.
    /// </summary>
    public class AesCryptoKeyImport : CryptoKeyImport<AesCryptoKey>
    {
        /// <summary>
        /// Imports <see cref="AesCryptoKey"/> from specified stream.
        /// </summary>
        /// <param name="stream">The stream to read imported key.</param>
        /// <returns>A task representing import.</returns>
        /// <exception cref="ArgumentException">If <paramref name="stream"/> is not readable.</exception>
        /// <exception cref="InvalidOperationException">If import fails.</exception>
        public sealed override async Task<AesCryptoKey> ImportAsync(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            ValidateCanRead(stream);

            try
            {
                byte[] value = await ReadImportDataAsync(stream);
                return CreateCryptoKeyFromImportData(value);
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException && exception.Message == "Stream contains invalid data.")
                    throw;
                else
                    throw new InvalidOperationException("Importing key from stream failed. See inner exception.", exception);
            }
        }

        /// <summary>
        /// Reads import data from stream. By default, it reads bytes until the end of the stream and returns them as an array.
        /// Derived classes can override this method to implement custom reading logic, such as reading a specific number of bytes 
        /// or applying additional processing to the data before returning it.
        /// </summary>
        /// <param name="stream">The stream containing exported data.</param>
        /// <returns>A task representing the asynchronous operation, with a byte array containing the imported data.</returns>
        protected virtual async Task<byte[]> ReadImportDataAsync(Stream stream)
        {
            var bytes = new List<byte>();
            byte[] buffer = new byte[AesCryptoKey.KeyLength + AesCryptoKey.IVLength];
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                bytes.AddRange(buffer);

            return bytes.ToArray();
        }

        /// <summary>
        /// Creates <see cref="AesCryptoKey"/> from import data.
        /// </summary>
        /// <param name="data">The import data obtained from <see cref="ReadImportDataAsync(Stream)"/>.</param>
        /// <returns>A <see cref="AesCryptoKey"/>.</returns>
        protected virtual AesCryptoKey CreateCryptoKeyFromImportData(byte[] data)
        {
            byte[] key = data.Take(AesCryptoKey.KeyLength).ToArray();
            byte[] iv = data.Skip(AesCryptoKey.KeyLength).Take(AesCryptoKey.IVLength).ToArray();
            return new AesCryptoKey(key, iv);
        }
    }
}
