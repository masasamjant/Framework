using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents service that will export <see cref="AesCryptoKey"/> to stream in raw bytes.
    /// </summary>
    /// <remarks>It is responsibility of caller to secure data exported to stream.</remarks>
    public class AesCryptoKeyExport : CryptoKeyExport<AesCryptoKey>
    {
        /// <summary>
        /// Exports specified <see cref="AesCryptoKey"/> to specified stream.
        /// </summary>
        /// <param name="key">The crypto key.</param>
        /// <param name="stream">The stream to export key.</param>
        /// <returns>A task representing export.</returns>
        /// <exception cref="ArgumentException">If <paramref name="stream"/> is not writable.</exception>
        /// <exception cref="InvalidOperationException">If export fails.</exception>
        public sealed override async Task ExportAsync(AesCryptoKey key, Stream stream)
        {
            ValidateCanWrite(stream);
            
            try
            {
                var buffer = GetExportData(key);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Exporting key to specified stream failed. See inner exception.", exception);
            }
        }

        /// <summary>
        /// Gets the export data. Default impelementation combines key and IV into single byte array. 
        /// Derived classes can override this method to change export format for example to encrypt export data before writing to stream.
        /// </summary>
        /// <param name="key">The AES crypto key to export.</param>
        /// <returns>A export data.</returns>
        protected virtual byte[] GetExportData(AesCryptoKey key)
        {
            return ArrayHelper.Combine(key.Key, key.IV);
        }
    }
}
