namespace Masasamjant.Web.ViewModels.Navigation
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
        /// <param name="elements">The <see cref="NavigationElements"/>.</param>
        /// <param name="items">The navigation items.</param>
        public NavigationContext(NavigationElements elements, IEnumerable<INavigationItem> items)
        {
            Elements = elements;
            if (items.Any())
                this.items.AddRange(items);
        }
        
        /// <summary>
        /// Gets the <see cref="NavigationElements"/> to create markup of <see cref="Items"/>.
        /// </summary>
        public NavigationElements Elements { get; }

        /// <summary>
        /// Gets the navigation items.
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
        /// Add specified <see cref="INavigationItem"/> to context.
        /// </summary>
        /// <param name="item">The <see cref="INavigationItem"/> to add.</param>
        /// <returns>A current context.</returns>
        public NavigationContext AddItem(INavigationItem item) 
        {
            items.Add(item);
            return this;
        }

        /// <summary>
        /// Remove specified <see cref="INavigationItem"/> from context.
        /// </summary>
        /// <param name="item">The <see cref="INavigationItem"/> to remove.</param>
        /// <returns>A current context.</returns>
        public NavigationContext RemoveItem(INavigationItem item)
        {
            items.Remove(item);
            return this;
        }

        /// <summary>
        /// Remove all navigation items from context.
        /// </summary>
        public void ClearItems()
        {
            items.Clear();
        }
    }
}
