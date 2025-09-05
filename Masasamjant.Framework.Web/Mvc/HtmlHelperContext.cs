namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides extra contextual information to HTML helpers.
    /// </summary>
    public sealed class HtmlHelperContext
    {
        /// <summary>
        /// Initializes new instance of the <see cref="HtmlHelperContext"/> class.
        /// </summary>
        /// <param name="enabled"><c>true</c>  if element is enabled and does not have "disabled" attribute; <c>false</c> otherwise.</param>
        /// <param name="id">The value of "id" attribute.</param>
        /// <param name="title">The value of "title" attribute.</param>
        /// <param name="css">The value of "class" attribute.</param>
        /// <param name="emptyOptionLabel">The text of empty "option" element.</param>
        /// <param name="htmlAttributes">The custom attributes.</param>
        /// <param name="replaceCurrentHtmlAttributes"><c>true</c> if attributes in <paramref name="htmlAttributes"/> replace current attributes; <c>false</c> otherwise.</param>
        public HtmlHelperContext(bool enabled = true, string? id = null, string? title = null, string? css = null, string? emptyOptionLabel = null,
            IDictionary<string, object>? htmlAttributes = null, bool replaceCurrentHtmlAttributes = false)
        {
            IsEnabled = enabled;
            Id = id;
            Title = title;
            CssClass = css;
            EmptyOptionLabel = emptyOptionLabel;
            HtmlAttributes = htmlAttributes != null  ?  new Dictionary<string, object>(htmlAttributes) : new Dictionary<string, object>();
            ReplaceCurrentHtmlAttributes = replaceCurrentHtmlAttributes;
        }

        /// <summary>
        /// Gets or sets  whether or not generated HTML element is enabled.
        /// If <c>true</c>,  then  element has not "disabled" attribute; <c>false</c> otherwise. 
        /// Default is <c>true</c>.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets value of "id" attribute.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets value of "title" attribute.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets value of "class" attribute.
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// Gets or sets empty "option" element text of "select" element.
        /// </summary>
        public string? EmptyOptionLabel { get; set; }

        /// <summary>
        /// Gets or sets custom attributes.
        /// </summary>
        public IDictionary<string, object> HtmlAttributes { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets whether or not attributes from <see cref="HtmlAttributes"/> will replace current attributes.
        /// </summary>
        public bool ReplaceCurrentHtmlAttributes { get; set; }

        /// <summary>
        /// Apply context to specified HTML attributes dictionary.
        /// </summary>
        /// <param name="htmlAttributes">The HTML attributes dictionary to apply context.</param>
        public void ApplyAttributes(IDictionary<string, object> htmlAttributes)
        {
            if (!string.IsNullOrWhiteSpace(Id))
                htmlAttributes["id"] = Id;

            if (!string.IsNullOrWhiteSpace(Title))
                htmlAttributes["title"] = Title;

            if (!IsEnabled)
                htmlAttributes["disabled"] = "disabled";

            if (!string.IsNullOrWhiteSpace(CssClass))
            {
                if (htmlAttributes.TryGetValue("class", out var value))
                { 
                    var css = (string)value;
                    css += " " + CssClass;
                    htmlAttributes["class"] = css;
                }
                else
                    htmlAttributes["class"] = CssClass;
            }

            foreach (var attribute in HtmlAttributes)
            {
                if (!htmlAttributes.ContainsKey(attribute.Key) || ReplaceCurrentHtmlAttributes)
                    htmlAttributes[attribute.Key] = attribute.Value;
            }
        }
    }
}
