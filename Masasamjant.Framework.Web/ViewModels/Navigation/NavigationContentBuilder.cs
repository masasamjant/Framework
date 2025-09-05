using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.ViewModels.Navigation
{
    /// <summary>
    /// Represents component that builds HTML navigation markup from using <see cref="NavigationContext"/>.
    /// </summary>
    public class NavigationContentBuilder  : INavigationContentBuilder
    {
        /// <summary>
        /// Build navigation content using specified <see cref="NavigationContext"/> class.
        /// </summary>
        /// <param name="context">The navigation context.</param>
        /// <returns>A <see cref="IHtmlContent"/> of navigation markup.</returns>
        public virtual IHtmlContent Build(NavigationContext context)
        {
            var rootBuilder = new TagBuilder(context.Elements.NavigationContainerElement);
            var navigationContainerElementCssClass = context.Elements.NavigationContainerElementCssClass;

            if (!string.IsNullOrWhiteSpace(navigationContainerElementCssClass))
                rootBuilder.AddCssClass(navigationContainerElementCssClass);

            foreach (var navigationItem in context.Items)
            {
                if (string.IsNullOrWhiteSpace(context.Elements.NavigationItemContainerElement))
                {
                    var navigationItemBuilder = BuildNavigationItem(context.Elements, navigationItem);
                    rootBuilder.InnerHtml.AppendHtml(navigationItemBuilder);
                }
                else
                {
                    var navigationItemContainerBuilder = new TagBuilder(context.Elements.NavigationItemContainerElement);
                    var navigationItemContainerElementCssClass = context.Elements.NavigationItemContainerElementCssClass;

                    if (!string.IsNullOrWhiteSpace(navigationItemContainerElementCssClass))
                        navigationItemContainerBuilder.AddCssClass(navigationItemContainerElementCssClass);

                    var navigationItemBuilder = BuildNavigationItem(context.Elements, navigationItem);
                    navigationItemContainerBuilder.InnerHtml.AppendHtml(navigationItemBuilder);
                    rootBuilder.InnerHtml.AppendHtml(navigationItemContainerBuilder);
                }
            }

            return rootBuilder;
        }

        /// <summary>
        /// Build HTML markup of specified navigation item.
        /// </summary>
        /// <param name="elements">The <see cref="NavigationElements"/>.</param>
        /// <param name="navigationItem">The <see cref="INavigationItem"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of navigation item.</returns>
        protected virtual IHtmlContent BuildNavigationItem(NavigationElements elements, INavigationItem navigationItem)
        {
            var builder = new TagBuilder(elements.NavigationItemElement);
            var cssClass = navigationItem.CssClass;

            if (!string.IsNullOrWhiteSpace(cssClass))
                builder.AddCssClass(cssClass);

            if (!navigationItem.IsEnabled)
            {
                var disabledCssClass = navigationItem.DisabledCssClass;
                if (!string.IsNullOrWhiteSpace(disabledCssClass))
                    builder.AddCssClass(disabledCssClass);
            }

            SetHtmlAttributes(navigationItem, builder);
            SetHrefAttribute(elements, navigationItem, builder);

            builder.InnerHtml.Append(navigationItem.Text);
            return builder;
        }

        /// <summary>
        /// Sets navigation item HTML attributes.
        /// </summary>
        /// <param name="navigationItem">The <see cref="INavigationItem"/>.</param>
        /// <param name="builder">The <see cref="TagBuilder"/>.</param>
        protected static void SetHtmlAttributes(INavigationItem navigationItem, TagBuilder builder)
        {
            foreach (var attribute in navigationItem.HtmlAttributes)
            {
                var value = attribute.Value?.ToString() ?? string.Empty;
                builder.MergeAttribute(attribute.Key, value);
            }
        }

        /// <summary>
        /// Sets href attribute value if navigation item element of specified navigation item is <see cref="NavigationElements.DefaultNavigationItemElement"/>.
        /// </summary>
        /// <param name="elements">The <see cref="NavigationElements"/>.</param>
        /// <param name="navigationItem">The <see cref="INavigationItem"/>.</param>
        /// <param name="builder">The <see cref="TagBuilder"/>.</param>
        protected static void SetHrefAttribute(NavigationElements elements, INavigationItem navigationItem, TagBuilder builder)
        {
            if (string.Equals(elements.NavigationItemElement, NavigationElements.DefaultNavigationItemElement, StringComparison.OrdinalIgnoreCase))
            {
                string url = navigationItem.GetFullUrl();

                if (string.IsNullOrWhiteSpace(url))
                    builder.MergeAttribute("href", "#");
                else
                    builder.MergeAttribute("href", url);
            }
        }
    }
}
