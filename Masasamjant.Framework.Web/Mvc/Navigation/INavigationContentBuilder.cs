using Microsoft.AspNetCore.Html;

namespace Masasamjant.Web.Mvc.Navigation
{
    /// <summary>
    /// Represents component that builds HTML navigation markup from using <see cref="NavigationContext"/>.
    /// </summary>
    public interface INavigationContentBuilder
    {
        /// <summary>
        /// Build navigation content using specified <see cref="NavigationContext"/> class.
        /// </summary>
        /// <param name="context">The navigation context.</param>
        /// <returns>The generated HTML content.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="context"/> is <c>null</c>.</exception>
        IHtmlContent BuildNavigation(NavigationContext context);
    }
}
