namespace Masasamjant.Web.Mvc.Navigation
{
    /// <summary>
    /// Represents navigation context.
    /// </summary>
    public sealed class NavigationContext
    {
        private readonly List<INavigationItem> items = new List<INavigationItem>();

        /// <summary>
        /// Initializes new instance of the <see cref="NavigationContext"/> class.
        /// </summary>
        /// <param name="navigationElements">The navigation elements definition.</param>
        /// <param name="navigationItems">The navigation items associated with the context.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="navigationElements"/> is <c>null</c>.</exception>
        public NavigationContext(NavigationElements navigationElements, IEnumerable<INavigationItem>? navigationItems = null)
        {
            Elements = navigationElements ?? throw new ArgumentNullException(nameof(navigationElements));

            if (navigationItems != null && navigationItems.Any())
                items.AddRange(navigationItems);
        }

        /// <summary>
        /// Gets navigation elements definition.
        /// </summary>
        public NavigationElements Elements { get; }

        /// <summary>
        /// Gets navigation items associated with context.
        /// </summary>
        public IEnumerable<INavigationItem> Items
        {
            get
            {
                foreach (var item in items)
                    yield return item;
            }
        }

        /// <summary>
        /// Adds specified navigation item to context.
        /// </summary>
        /// <param name="navigationItem">The navigation item to add.</param>
        /// <returns>The current <see cref="NavigationContext"/> instance.</returns>
        public NavigationContext Add(INavigationItem navigationItem)
        {
            items.Add(navigationItem);
            return this;
        }

        /// <summary>
        /// Removes specified navigation item from context.
        /// </summary>
        /// <param name="navigationItem">The navigation item to remove.</param>
        /// <returns>The current <see cref="NavigationContext"/> instance.</returns>
        public NavigationContext Remove(INavigationItem navigationItem)
        {
            items.Remove(navigationItem);
            return this;
        }

        /// <summary>
        /// Removes all navigation items from context.
        /// </summary>
        /// <returns>The current <see cref="NavigationContext"/> instance.</returns>
        public NavigationContext RemoveAll()
        {
            items.Clear();
            return this;
        }
    }
}
