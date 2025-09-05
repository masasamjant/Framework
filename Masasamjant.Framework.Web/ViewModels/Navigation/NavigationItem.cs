using System.Text;

namespace Masasamjant.Web.ViewModels.Navigation
{
    /// <summary>
    /// Represents navigation item.
    /// </summary>
    public class NavigationItem : INavigationItem
    {
        private string url;

        /// <summary>
        /// Initializes new instance of the <see cref="NavigationItem"/> with specified values.
        /// </summary>
        /// <param name="text">The item text.</param>
        /// <param name="url">The URL to navigate.</param>
        /// <param name="routeParameters">The route parameters.</param>
        /// <param name="cssClass">The element CSS class.</param>
        /// <param name="disabledCssClass">The disabled element CSS class.</param>
        /// <param name="enabled"><c>true</c> if item is enabled; <c>false</c> otherwise.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="url"/> is not <c>null</c> and not absolute URI of HTTP or HTTPS scheme.</exception>
        public NavigationItem(string? text = null, string? url = null, IDictionary<string, object?>? routeParameters = null, 
            string? cssClass = null, string? disabledCssClass = null, bool enabled = true, IDictionary<string, object?>? htmlAttributes = null)
        {
            if (url != null && !UrlHelper.IsValidHttpUrl(url))
                throw new ArgumentException("The value must be absolute URI with http or https scheme.", nameof(url));
                
            Text = text ?? string.Empty;
            this.url = url ?? string.Empty;
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
        /// Gets or sets the URL to navigatate.
        /// </summary>
        /// <exception cref="ArgumentException">If value of <paramref name="value"/> is not <c>null</c> and not absolute URI of HTTP or HTTPS scheme.</exception>
        public string Url 
        {
            get { return url; }
            set
            {
                if (value.Length > 0 && !UrlHelper.IsValidHttpUrl(value))
                    throw new ArgumentException("The value must be absolute URI with http or https scheme.", nameof(Url));

                url = value;
            }
        }

        /// <summary>
        /// Gets route aka query string parameters.
        /// </summary>
        public IDictionary<string, object?> RouteParameters { get; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to navigation item element.
        /// </summary>
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to element when navigation item element is disabled.
        /// </summary>
        public string DisabledCssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether navigation item is in enabled state or not.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets HTML attributes.
        /// </summary>
        public IDictionary<string, object?> HtmlAttributes { get; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets the full navigation URL with route parameters.
        /// </summary>
        /// <returns>A full navigation URL with route parameters.</returns>
        public string GetFullUrl()
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

            var sb = new StringBuilder();

            foreach (var parameter in parameters)
            {
                var key = parameter.Key;
                var value = parameter.Value?.ToString() ?? string.Empty;
                sb.Append($"&{key}={value}");
            }

            if (url.EndsWith('?'))
                return string.Concat(url, sb.ToString().AsSpan(1));
            else if (url.Contains('?'))
                return url + sb.ToString();
            else
                return string.Concat(url, "?", sb.ToString().AsSpan(1));
        }
    }
}
