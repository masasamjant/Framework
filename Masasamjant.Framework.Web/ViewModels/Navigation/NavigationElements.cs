namespace Masasamjant.Web.ViewModels.Navigation
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

        /// <summary>
        /// Initializes new instance of the <see cref="NavigationElements"/> class where navigation item element is <see cref="DefaultNavigationItemElement"/>. 
        /// If <paramref name="navigationItemContainerElement"/> is empty or only whitespace, then navigation items are appended directly to <paramref name="navigationContainerElement"/>.
        /// </summary>
        /// <param name="navigationContainerElement">The name of navigation container element.</param>
        /// <param name="navigationItemContainerElement">The name of navigation item container element.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="navigationContainerElement"/> or <paramref name="navigationItemElement"/> is empty or only whitespace.</exception>
        public NavigationElements(string navigationContainerElement, string navigationItemContainerElement)
            : this(navigationContainerElement, navigationItemContainerElement, DefaultNavigationItemElement)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="NavigationElements"/> class. If <paramref name="navigationItemContainerElement"/> is empty or only whitespace, 
        /// then navigation items are appended directly to <paramref name="navigationContainerElement"/>.
        /// </summary>
        /// <param name="navigationContainerElement">The name of navigation container element.</param>
        /// <param name="navigationItemContainerElement">The name of navigation item container element.</param>
        /// <param name="navigationItemElement">The name of navigation item element.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="navigationContainerElement"/> or <paramref name="navigationItemElement"/> is empty or only whitespace.</exception>
        public NavigationElements(string navigationContainerElement, string navigationItemContainerElement, string navigationItemElement)
        {
            if (string.IsNullOrWhiteSpace(navigationContainerElement))
                throw new ArgumentNullException(nameof(navigationContainerElement), "The navigation container element is empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(navigationItemElement))
                throw new ArgumentNullException(nameof(navigationItemElement), "The navigation item element is empty or only whitespace.");

            NavigationContainerElement = navigationContainerElement.Trim().ToLowerInvariant();
            NavigationItemContainerElement = string.IsNullOrWhiteSpace(navigationItemContainerElement) ? string.Empty : navigationItemContainerElement.Trim().ToLowerInvariant();
            NavigationItemElement = navigationItemElement.Trim().ToLowerInvariant();
        }

        /// <summary>
        /// Gets the name of navigation container element.
        /// </summary>
        public string NavigationContainerElement { get; }

        /// <summary>
        /// Gets the name of navigation item container element.
        /// </summary>
        public string NavigationItemContainerElement { get; }

        /// <summary>
        /// Gets the name of navigation item element.
        /// </summary>
        public string NavigationItemElement { get; }

        /// <summary>
        /// Gets or sets value of "class" attribute of navigation container element.
        /// </summary>
        public string NavigationContainerElementCssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of "class" attribute of navigation item container element.
        /// </summary>
        public string NavigationItemContainerElementCssClass { get; set; } = string.Empty;
    }
}
