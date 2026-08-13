using System.Text;

namespace Masasamjant.Web.Mvc.Navigation
{
    /// <summary>
    /// Represents navigation item view model.
    /// </summary>
    public sealed class NavigationItem : INavigationItem
    {
        private string url = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationItem"/> class with specified values.
        /// </summary>
        /// <param name="text">The text of the navigation item.</param>
        /// <param name="url">The URL of the navigation item.</param>
        /// <param name="routeParameters">The route parameters of the navigation item.</param>
        /// <param name="cssClass">The CSS class of the navigation item.</param>
        /// <param name="disabledCssClass">The CSS class of the navigation item when disabled.</param>
        /// <param name="enabled">Whether the navigation item is enabled.</param>
        /// <param name="htmlAttributes">The HTML attributes of the navigation item.</param>
        /// <exception cref="ArgumentException">If <paramref name="url"/> is not <c>null</c> and not valid absolute HTTP or HTTPS URL.</exception>
        public NavigationItem(string? text = null, string? url = null, IDictionary<string, object?>? routeParameters = null,
            string? cssClass = null, string? disabledCssClass = null, bool enabled = true, IDictionary<string, object?>? htmlAttributes = null)
        {
            if (url != null && !UrlHelper.IsValidHttpUrl(url))
                throw new ArgumentException("The value must be absolute URI with http or https scheme.", nameof(url));

            Text = text ?? string.Empty;
            Url = url ?? string.Empty;
            if (routeParameters != null && routeParameters.Count > 0)
                RouteParameters = new Dictionary<string, object?>(routeParameters);
            CssClass = cssClass ?? string.Empty;
            DisabledCssClass = disabledCssClass ?? string.Empty;
            IsEnabled = enabled;
            if (htmlAttributes != null && htmlAttributes.Count > 0)
                HtmlAttributes = new Dictionary<string, object?>(htmlAttributes);
        }

        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL to navigate.
        /// </summary>
        /// <exception cref="ArgumentException">If the set value is not a valid absolute HTTP or HTTPS URL.</exception>
        public string Url
        {
            get => url;
            set
            {
                if (value.Length > 0 && !UrlHelper.IsValidHttpUrl(value))
                    throw new ArgumentException("The value must be absolute URI with http or https scheme.", nameof(Url));

                this.url = value;
            }
        }

        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to root element.
        /// </summary>
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to root element when it is disabled.
        /// </summary>
        public string DisabledCssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether or not the view model is in enabled state. 
        /// If <c>false</c>, then HTML elements bound to this view model should be disabled, hidden or inactive, depending on the design.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets the HTML attributes dictionary.
        /// </summary>
        public IDictionary<string, object?> HtmlAttributes { get; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets the route parameters to append to the navigation URL. 
        /// The key is the parameter name, and the value is the parameter value.
        /// </summary>
        public IDictionary<string, object?> RouteParameters { get; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets the full navigation URL with route parameters.
        /// </summary>
        /// <returns>The full navigation URL.</returns>
        public string GetNavigationUrl()
        {
            var url = Url;

            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            var parameters = RouteParameters.ToDictionary();

            if (parameters.Count == 0)
            {
                if (url.EndsWith('?'))
                    return url.TrimEnd('?');
                
                return url;
            }

            var builder = GetParametersBuilder(parameters);
            return BuildNavigationUrl(url, builder);
        }

        private static StringBuilder GetParametersBuilder(IDictionary<string, object?> parameters)
        {
            var builder = new StringBuilder();
            foreach (var parameter in parameters)
            {
                var key = parameter.Key;
                var value = parameter.Value?.ToString() ?? string.Empty;
                builder.Append($"&{key}={value}");
            }
            return builder;
        }

        private static string BuildNavigationUrl(string url, StringBuilder parametersBuilder)
        {
            if (url.EndsWith('?'))
                return string.Concat(url, parametersBuilder.ToString().AsSpan(1));
            else if (url.Contains('?'))
                return url + parametersBuilder.ToString();
            else
                return string.Concat(url, "?", parametersBuilder.ToString().AsSpan(1));
        }
    }
}