using System.Text.Encodings.Web;

namespace Masasamjant.Web.ViewModels.Navigation
{
    /// <summary>
    /// Provides helper methods to <see cref="INavigationContentBuilder"/> interface.
    /// </summary>
    public static class NavigationContentBuilderHelper
    {
        /// <summary>
        /// Build navigation using specified <see cref="INavigationContentBuilder"/> and when write content to specified <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="contentBuilder">The <see cref="INavigationContentBuilder"/>.</param>
        /// <param name="context">The <see cref="NavigationContext"/>.</param>
        /// <param name="writer">The <see cref="TextWriter"/>.</param>
        /// <param name="encoder">The <see cref="HtmlEncoder"/> or <c>null</c> to use <see cref="HtmlEncoder.Default"/>.</param>
        public static void BuildAndWrite(this INavigationContentBuilder contentBuilder, NavigationContext context, TextWriter writer, HtmlEncoder? encoder = null)
        {
            var content = contentBuilder.Build(context);
            content.WriteTo(writer, encoder ?? HtmlEncoder.Default);
        }

        /// <summary>
        /// Build navigation using specified <see cref="INavigationContentBuilder"/> and return result as string.
        /// </summary>
        /// <param name="contentBuilder">The <see cref="INavigationContentBuilder"/>.</param>
        /// <param name="context">The <see cref="NavigationContext"/>.</param>
        /// <param name="encoder">The <see cref="HtmlEncoder"/> or <c>null</c> to use <see cref="HtmlEncoder.Default"/>.</param>
        /// <returns>A result of <see cref="INavigationContentBuilder.Build(NavigationContext)"/> as string.</returns>
        public static string Build(this INavigationContentBuilder contentBuilder, NavigationContext context, HtmlEncoder? encoder = null)
        {
            var content = contentBuilder.Build(context);
            return content.ToHtmlString(encoder);
        }
    }
}
