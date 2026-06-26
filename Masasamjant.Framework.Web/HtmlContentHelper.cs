using Microsoft.AspNetCore.Html;
using System.Text;
using System.Text.Encodings.Web;

namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods to <see cref="IHtmlContent"/> interface.
    /// </summary>
    public static class HtmlContentHelper
    {
        /// <summary>
        /// Get content of specified <see cref="IHtmlContent"/> as string.
        /// </summary>
        /// <param name="content">The HTML content to convert to a string.</param>
        /// <param name="encoder">The HTML encoder to use. If <c>null</c>, then <see cref="HtmlEncoder.Default"/> is used.</param>
        /// <returns>The HTML content as a string.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <c>null</c>.</exception>
        public static string ToHtmlString(this IHtmlContent content, HtmlEncoder? encoder = null)
        {
            ArgumentNullException.ThrowIfNull(content);

            var builder = new StringBuilder();

            using (var writer = new StringWriter(builder))
            {
                content.WriteTo(writer, encoder ?? HtmlEncoder.Default);
                writer.Flush();
            }

            return builder.ToString();
        }
    }
}
