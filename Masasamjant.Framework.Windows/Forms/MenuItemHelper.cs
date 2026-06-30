namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Provides helper methods to work with menu and menu items.
    /// </summary>
    public static class MenuItemHelper
    {
        /// <summary>
        /// Check specified menu items.
        /// </summary>
        /// <param name="menuItems">The menu items to check.</param>
        public static void Check(params ToolStripMenuItem[] menuItems)
        {
            foreach (var menuItem in menuItems)
                menuItem.Checked = true;
        }

        /// <summary>
        /// Uncheck specified menu items.
        /// </summary>
        /// <param name="menuItems">The menu items to uncheck.</param>
        public static void Uncheck(params ToolStripMenuItem[] menuItems)
        {
            foreach (var menuItem in menuItems)
                menuItem.Checked = false;
        }

        /// <summary>
        /// Gets <see cref="ToolStripMenuItem"/> with specified tag.
        /// </summary>
        /// <param name="menu">The menu to search.</param>
        /// <param name="tag">The tag to search for.</param>
        /// <returns>The <see cref="ToolStripMenuItem"/> with the specified tag, or <c>null</c> if not found.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="menu"/> or <paramref name="tag"/> is <c>null</c>.</exception>
        public static ToolStripMenuItem? GetMenuItem(MenuStrip menu, object tag)
        {
            ArgumentNullException.ThrowIfNull(menu);
            return GetMenuItem(menu.Items, tag);
        }

        /// <summary>
        /// Gets <see cref="ToolStripMenuItem"/> with the specified tag.
        /// </summary>
        /// <param name="items">The collection of menu items to search.</param>
        /// <param name="tag">The tag to search for.</param>
        /// <returns>The <see cref="ToolStripMenuItem"/> with the specified tag, or <c>null</c> if not found.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> or <paramref name="tag"/> is <c>null</c>.</exception>
        public static ToolStripMenuItem? GetMenuItem(ToolStripItemCollection items, object tag)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(tag);

            if (items.Count == 0)
                return null;

            foreach (ToolStripItem item in items)
            {
                if (item.Tag == null || item is not ToolStripMenuItem menuItem)
                    continue;

                if (Equals(menuItem.Tag, tag))
                    return menuItem;

                var child = GetMenuItem(menuItem.DropDownItems, tag);

                if (child != null)
                    return child;

            }

            return null;
        }
    }
}
