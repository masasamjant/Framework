using Microsoft.AspNetCore.Html;

namespace Masasamjant.Web.ViewModels.Navigation
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
        /// <returns>A <see cref="IHtmlContent"/> of navigation markup.</returns>
        IHtmlContent Build(NavigationContext context);
    }
}
