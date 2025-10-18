using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents service that will export <see cref="AesCryptoKey"/> to stream.
    /// </summary>
    public sealed class AesCryptoKeyExport : CryptoKeyExport<AesCryptoKey>
    {
        /// <summary>
        /// Exports specified <see cref="AesCryptoKey"/> to specified stream.
        /// </summary>
        /// <param name="key">The crypto key.</param>
        /// <param name="stream">The stream to export key.</param>
        /// <returns>A task representing export.</returns>
        /// <remarks>It is responsibility of caller to secure data exported to stream.</remarks>
        /// <exception cref="InvalidOperationException">If export fails.</exception>
        public override async Task ExportAsync(AesCryptoKey key, Stream stream)
        {
            try
            {
                string k = Convert.ToBase64String(key.Key);
                string v = Convert.ToBase64String(key.IV);
                string s = string.Concat(k, v);
                var writer = new StreamWriter(stream);
                await writer.WriteAsync(s);
                await writer.FlushAsync();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Exporting key to specified stream failed. See inner exception.", exception);
            }
        }
    }
}
