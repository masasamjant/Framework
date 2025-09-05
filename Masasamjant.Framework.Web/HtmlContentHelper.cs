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
        /// Gets content of specified <see cref="IHtmlContent"/> as string.
        /// </summary>
        /// <param name="content">The <see cref="IHtmlContent"/>.</param>
        /// <param name="encoder">The <see cref="HtmlEncoder"/> or <c>null</c> to use <see cref="HtmlEncoder.Default"/>.</param>
        /// <returns>A content of <paramref name="content"/> as string.</returns>
        public static string ToHtmlString(this IHtmlContent content, HtmlEncoder? encoder = null)
        {
            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                content.WriteTo(writer, encoder ?? HtmlEncoder.Default);
                writer.Flush();
            }

            return sb.ToString();
        }
    }
}
