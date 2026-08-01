using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.Mvc.Navigation
{
    public class NavigationContentBuilder : INavigationContentBuilder
    {
        public IHtmlContent BuildNavigation(NavigationContext context)
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

        private static TagBuilder BuildNavigationItem(NavigationElements elements, INavigationItem navigationItem)
        {
            var builder = new TagBuilder(elements.NavigationItemElement);
            var css = navigationItem.CssClass;

            if (!string.IsNullOrWhiteSpace(css))
                builder.AddCssClass(css);

            if (!navigationItem.IsEnabled)
            {
                var disabledCss = navigationItem.DisabledCssClass;
                if (!string.IsNullOrWhiteSpace(disabledCss))
                    builder.AddCssClass(disabledCss);
            }

            SetHtmlAttributes(navigationItem, builder);
            SetHrefAttribute(elements, navigationItem, builder);

            builder.InnerHtml.Append(navigationItem.Text);

            return builder;
        }

        private static void SetHtmlAttributes(INavigationItem navigationItem, TagBuilder builder)
        {
            foreach (var attribute in navigationItem.HtmlAttributes)
            {
                var value = attribute.Value?.ToString() ?? string.Empty;
                builder.MergeAttribute(attribute.Key, value);
            }
        }

        private static void SetHrefAttribute(NavigationElements elements, INavigationItem navigationItem, TagBuilder builder)
        {
            if (string.Equals(elements.NavigationItemElement, NavigationElements.DefaultNavigationItemElement, StringComparison.OrdinalIgnoreCase))
            {
                var url = navigationItem.GetNavigationUrl();

                if (!string.IsNullOrWhiteSpace(url))
                    builder.MergeAttribute("href", url);
            }
        }
    }
}
