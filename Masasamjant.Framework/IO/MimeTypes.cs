using Masasamjant.Resources.Files;
using System.Text;

namespace Masasamjant.IO
{
    /// <summary>
    /// Provides common MIME types defined in this framework.
    /// </summary>
    public static class MimeTypes
    {
        /// <summary>
        /// Gets all MIME types defined in this framework.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MimeType> GetMimeTypes()
        {
            foreach (var mimeType in Items)
                yield return mimeType;
        }

        /// <summary>
        /// Gets the MIME type associated with specfied file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>A <see cref="MimeType"/> associated with file extension or <c>null</c>.</returns>
        public static MimeType? GetMimeType(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
                return null;

            return Items.FirstOrDefault(x => string.Equals(x.FileExtension, fileExtension.EnsureStartsWith('.'), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets all MIME types with specified value.
        /// eve
        /// </summary>
        /// <param name="value">The MIME type value.</param>
        /// <returns>A <see cref="MimeType"/>s with specified value or empty.</returns>
        /// <remarks>Some MIME type values like 'text/html' might be associated with different file extensions like '.html' and '.htm'.</remarks>
        public static IEnumerable<MimeType> GetMimeTypes(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return [];

            return Items.Where(x => string.Equals(x.Value, value, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets dictionary of file extension as key and MIME type value as value.
        /// </summary>
        /// <returns>A read-only dictionary of file extension as key and MIME type value as value.</returns>
        public static IReadOnlyDictionary<string, string> GetExtensionValueMap()
        {
            var dictionary = Items.ToDictionary((x) => x.FileExtension, (x) => x.Value);
            return dictionary.AsReadOnly();
        }

        private static readonly Lazy<List<MimeType>> lazyMimeTypes = new(LoadMimeTypes);

        private static List<MimeType> Items => lazyMimeTypes.Value;

        private static List<MimeType> LoadMimeTypes()
        {
            var result = new List<MimeType>(80);
            var csv = Encoding.UTF8.GetString(MimesResource.Data);
            
            using (var reader = new StringReader(csv))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values.Length != 3)
                        throw new InvalidOperationException($"Resource '{typeof(MimesResource)}' contains invalid line '{line}'.");

                    var value = values[2];
                    var extension = values[0];
                    var description = values[1];
                    result.Add(new MimeType(value, extension, description));
                }
            }

            return result;
        }
    }
}
