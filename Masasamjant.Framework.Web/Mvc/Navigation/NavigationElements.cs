namespace Masasamjant.Web.Mvc.Navigation
{
    /// <summary>
    /// Represents definition of HTML elements used in navigation.
    /// </summary>
    public sealed class NavigationElements
    {
        /// <summary>
        /// Default element of navigation item.
        /// </summary>
        public const string DefaultNavigationItemElement = "a";

        public NavigationElements(string navigationContainerElement, string? navigationItemContainerElement)
            : this(navigationContainerElement, navigationItemContainerElement, DefaultNavigationItemElement)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="NavigationElements"/> class.
        /// </summary>
        /// <param name="navigationContainerElement">The root element of navigation.</param>
        /// <param name="navigationItemContainerElement">The container element of navigation items.</param>
        /// <param name="navigationItemElement">The single navigation item element.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="navigationContainerElement"/> or <paramref name="navigationItemElement"/> is <c>null</c>, empty or whitespace.</exception>
        public NavigationElements(string navigationContainerElement, string? navigationItemContainerElement, string navigationItemElement)
        {
            if (string.IsNullOrWhiteSpace(navigationContainerElement))
                throw new ArgumentNullException(nameof(navigationContainerElement), "The navigation container element is empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(navigationItemElement))
                throw new ArgumentNullException(nameof(navigationItemElement), "The navigation item element is empty or only whitespace.");

            NavigationContainerElement = navigationContainerElement.Trim().ToLowerInvariant();
            NavigationItemContainerElement = string.IsNullOrWhiteSpace(navigationItemContainerElement) ? null : navigationItemContainerElement.Trim().ToLowerInvariant();
            NavigationItemElement = navigationItemElement.Trim().ToLowerInvariant();
        }

        /// <summary>
        /// Gets the root element of navigation.
        /// </summary>
        public string NavigationContainerElement { get; }

        /// <summary>
        /// Gets the container element of navigation items. 
        /// If this property is <c>null</c>, then navigation items are appended directly to <see cref="NavigationContainerElement"/>.
        /// </summary>
        public string? NavigationItemContainerElement { get; }

        /// <summary>
        /// Gets the single navigation item element.
        /// </summary>
        public string NavigationItemElement { get; }

        /// <summary>
        /// Gets or sets the CSS class of navigation container element.
        /// </summary>
        /// <remarks>Default value is empty string.</remarks>
        public string NavigationContainerElementCssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the CSS class of navigation item container element.
        /// </summary>
        /// <remarks>Default value is empty string.</remarks>
        public string NavigationItemContainerElementCssClass { get; set; } = string.Empty;
    }
}
