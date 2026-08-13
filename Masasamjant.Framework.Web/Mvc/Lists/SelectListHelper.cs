using Masasamjant.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.Mvc.Lists
{
    /// <summary>
    /// Provides helper methods to work with <see cref="SelectListItem"/>, <see cref="SelectListGroup"/> and <see cref="SelectList"/> classes.
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Creates <see cref="SelectListItem"/> from enumeration of <typeparamref name="TEnum"/>.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="current">The current value.</param>
        /// <param name="firstItem">The item to append as the first item in the list or <c>null</c>.</param>
        /// <param name="getDisabled">A function to determine if an item should be disabled.</param>
        /// <returns>A read-only collection of <see cref="SelectListItem"/>.</returns>
        public static IReadOnlyCollection<SelectListItem> GetSelectListItems<TEnum>(TEnum? current = null, SelectListItem? firstItem = null, Func<TEnum, bool>? getDisabled = null) where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();
            var items = new List<SelectListItem>(values.Length + (firstItem != null ? 1 : 0));

            if (firstItem != null)
                items.Add(firstItem);

            foreach (var value in values)
            {
                var itemValue = value.ToString();
                var itemText = value.GetResourceStringOrName();
                var disabled = getDisabled != null ? getDisabled(value) : false;
                items.Add(new SelectListItem(itemText, itemValue, current.HasValue && value.Equals(current.Value), disabled));
            }

            return items.AsReadOnly();
        }

        /// <summary>
        /// Creates <see cref="SelectListItem"/>s from specified <typeparamref name="T"/> values.
        /// </summary>
        /// <typeparam name="T">The type of the item to create select list item from.</typeparam>
        /// <param name="values">The values.</param>
        /// <param name="getText">A function to get the text for each item.</param>
        /// <param name="getValue">A function to get the value for each item.</param>
        /// <param name="current">The current value.</param>
        /// <param name="comparer">An optional comparer to compare values.</param>
        /// <param name="firstItem">The item to append as the first item in the list or <c>null</c>.</param>
        /// <param name="getDisabled">A function to determine if an item should be disabled.</param>
        /// <returns>A read-only collection of <see cref="SelectListItem"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="values"/>, <paramref name="getText"/>, or <paramref name="getValue"/> is <c>null</c>.</exception>
        public static IReadOnlyCollection<SelectListItem> GetSelectListItems<T>(IEnumerable<T> values, Func<T, string> getText, Func<T, string> getValue, T? current = default, IEqualityComparer<T>? comparer = null, SelectListItem? firstItem = null, Func<T, bool>? getDisabled = null)
        { 
            ArgumentNullException.ThrowIfNull(values);
            ArgumentNullException.ThrowIfNull(getText);
            ArgumentNullException.ThrowIfNull(getValue);

            var items = new List<SelectListItem>(values.Count() + (firstItem != null ? 1 : 0));

            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            if (firstItem != null)
                items.Add(firstItem);

            foreach (var value in values)
            {
                var itemValue = getValue(value);
                var itemText = getText(value);
                var disabled = getDisabled != null ? getDisabled(value) : false;
                items.Add(new SelectListItem(itemText, itemValue, current is not null && comparer.Equals(value, current), disabled));
            }

            return items.AsReadOnly();
        }

        /// <summary>
        /// Gets or creates <see cref="SelectListGroup"/>.
        /// </summary>
        /// <param name="items">The select list items.</param>
        /// <param name="groupName">The name of the group.</param>
        /// <param name="createNewGroup"><c>true</c> to create new group even if one with specified name already exists; <c>false</c> otherwise.</param>
        /// <returns>The existing or newly created <see cref="SelectListGroup"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> or <paramref name="groupName"/> is <c>null</c>.</exception>
        public static SelectListGroup GetOrCreateGroup(IEnumerable<SelectListItem> items, string groupName, bool createNewGroup = false)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(groupName);

            if (createNewGroup || !items.Any())
                return new SelectListGroup() { Name = groupName };

            var group = items.Where(x => x.Group != null && x.Group.Name == groupName).Select(x => x.Group).FirstOrDefault();

            if (group == null)
                group = new SelectListGroup() { Name = groupName };

            return group;
        }

        /// <summary>
        /// Gets different <see cref="SelectListGroup"/>s within specified <see cref="SelectListItem"/>s.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to extract groups from.</param>
        /// <returns>A collection of distinct <see cref="SelectListGroup"/>s.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <c>null</c>.</exception>
        public static IEnumerable<SelectListGroup> GetSelectListGroups(this IEnumerable<SelectListItem> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            static IEnumerable<SelectListGroup> enumerateGroups(IEnumerable<SelectListItem> items)
            {
                var groups = new HashSet<SelectListGroup>();

                foreach (var item in items)
                {
                    if (item.Group == null || groups.Contains(item.Group))
                        continue;
                    else
                    {
                        groups.Add(item.Group);
                        yield return item.Group;
                    }
                }
            }

            return enumerateGroups(items);
        }
        
        /// <summary>
        /// Selects <see cref="SelectListItem"/>s that match the specified predicate, and deselects all others.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to select.</param>
        /// <param name="isSelected">A predicate to determine which items should be selected.</param>
        /// <returns>A collection of <see cref="SelectListItem"/>s with updated selection state.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> or <paramref name="isSelected"/> is <c>null</c>.</exception>
        public static IEnumerable<SelectListItem> Select(this IEnumerable<SelectListItem> items, Func<SelectListItem, bool> isSelected)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(isSelected);

            static IEnumerable<SelectListItem> enumerateSelected(IEnumerable<SelectListItem> items, Func<SelectListItem, bool> isSelected)
            {
                foreach (var item in items)
                {
                    if (isSelected(item))
                    {
                        item.Selected = true;
                        yield return item;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }

            return enumerateSelected(items, isSelected);
        }

        /// <summary>
        /// Gets <see cref="SelectListItem"/>s that belong to the <see cref="SelectListGroup"/> specified by name.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to filter.</param>
        /// <param name="groupName">The name of the <see cref="SelectListGroup"/> to filter by.</param>
        /// <returns>A <see cref="SelectListItem"/>s that belong to the specified group.</returns>
        public static IEnumerable<SelectListItem> GetSelectGroupItems(this IEnumerable<SelectListItem> items, string groupName)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(groupName);

            return items.Where(item => item.Group != null && item.Group.Name == groupName);
        }

        /// <summary>
        /// Gets <see cref="SelectListItem"/>s that belong to the specified <see cref="SelectListGroup"/>.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to filter.</param>
        /// <param name="group">The <see cref="SelectListGroup"/> to filter by.</param>
        /// <returns>A <see cref="SelectListItem"/>s that belong to the specified group.</returns>
        public static IEnumerable<SelectListItem> GetSelectGroupItems(this IEnumerable<SelectListItem> items, SelectListGroup group)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(group);

            return items.Where(item => item.Group != null && ReferenceEquals(item.Group, group));
        }

        /// <summary>
        /// Enable <see cref="SelectListItem"/>s that match the specified predicate, and disable all others.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to enable.</param>
        /// <param name="isEnabled">A predicate to determine which items should be enabled.</param>
        public static void EnableItems(this IEnumerable<SelectListItem> items, Func<SelectListItem, bool> isEnabled)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(isEnabled);

            foreach (var item in items)
            {
                if (isEnabled(item))
                    item.Disabled = false;
                else
                    item.Disabled = true;
            }
        }

        /// <summary>
        /// Enable <see cref="SelectListItem"/>s in the specified group and disable items in other groups. 
        /// If the group is <c>null</c>, enable items that are not in any group and disable items that are in a group.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to enable.</param>
        /// <param name="group">The <see cref="SelectListGroup"/> to enable items for, or <c>null</c> to enable items not in any group.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <c>null</c>.</exception>
        public static void EnableItems(this IEnumerable<SelectListItem> items, SelectListGroup? group)
        {
            if (group != null)
                EnableItems(items, item => item.Group != null && ReferenceEquals(item.Group, group));
            else
                EnableItems(items, item => item.Group == null);
        }

        /// <summary>
        /// Disable <see cref="SelectListItem"/>s that match the specified predicate, and enable all others.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to disable.</param>
        /// <param name="isDisabled">A predicate to determine which items should be disabled.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> or <paramref name="isDisabled"/> is <c>null</c>.</exception>
        public static void DisableItems(this IEnumerable<SelectListItem> items, Func<SelectListItem, bool> isDisabled)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(isDisabled);

            foreach (var item in items)
            {
                if (isDisabled(item))
                    item.Disabled = true;
                else
                    item.Disabled = false;
            }
        }

        /// <summary>
        /// Disable <see cref="SelectListItem"/>s in the specified group and enable items in other groups. 
        /// If <paramref name="group"/> is <c>null</c>, disable items that are not in any group and enable items that are in a group.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to disable.</param>
        /// <param name="group">The <see cref="SelectListGroup"/> to disable items for, or <c>null</c> to disable items not in any group.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <c>null</c>.</exception>
        public static void DisableItems(this IEnumerable<SelectListItem> items, SelectListGroup? group)
        {
            if (group != null)
                DisableItems(items, item => item.Group != null && ReferenceEquals(item.Group, group));
            else
                DisableItems(items, item => item.Group == null);
        }

        /// <summary>
        /// Gets the read-only collection of selected items.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to get the selected items from.</param>
        /// <returns>A read-only collection of selected <see cref="SelectListItem"/>s.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <c>null</c>.</exception>
        public static IReadOnlyCollection<SelectListItem> GetSelectedItems(this IEnumerable<SelectListItem> items)
        {
            ArgumentNullException.ThrowIfNull(items);
            return items.Where(item => item.Selected).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets single <see cref="SelectListItem"/> with the specified value.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to search.</param>
        /// <param name="value">The value of the <see cref="SelectListItem"/> to find.</param>
        /// <returns>The <see cref="SelectListItem"/> with the specified value.</returns>
        /// <exception cref="InvalidOperationException">If no item or more than one item with the specified value is found.</exception>
        public static SelectListItem Single(this IEnumerable<SelectListItem> items, string value)
        {
            var matches = items.Where(x => x.Value == value).Take(2).ToList();

            if (matches.Count == 0)
                throw new InvalidOperationException("No item with specified value.");
            else if (matches.Count > 1)
                throw new InvalidOperationException("More than one item with specified value.");
            else
                return matches[0];
        }

        /// <summary>
        /// Gets single <see cref="SelectListItem"/> with the specified value, or <c>null</c> if no item is found.
        /// </summary>
        /// <param name="items">The collection of <see cref="SelectListItem"/>s to search.</param>
        /// <param name="value">The value of the <see cref="SelectListItem"/> to find.</param>
        /// <returns>The <see cref="SelectListItem"/> with the specified value, or <c>null</c> if no item is found.</returns>
        /// <exception cref="InvalidOperationException">If more than one item with the specified value is found.</exception>
        public static SelectListItem? SingleOrDefault(this IEnumerable<SelectListItem> items, string value)
        {
            var matches = items.Where(x => x.Value == value).Take(2).ToList();

            if (matches.Count == 0)
                return null;
            else if (matches.Count > 1)
                throw new InvalidOperationException("More than one item with specified value.");
            else
                return matches[0];
        }

        /// <summary>
        /// Regroups <see cref="SelectListItem"/>s that belong to specified old group to the specified new group.
        /// </summary>
        /// <param name="items">The selected list items.</param>
        /// <param name="oldGroup">The old group.</param>
        /// <param name="newGroup">The new group.</param>
        /// <returns>A read-only collection of items that were moved to the new group.</returns>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="oldGroup"/> and <paramref name="newGroup"/> are the same.</exception>
        public static IReadOnlyCollection<SelectListItem> Regroup(this IEnumerable<SelectListItem> items, SelectListGroup oldGroup, SelectListGroup newGroup)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(oldGroup);
            ArgumentNullException.ThrowIfNull(newGroup);

            if (ReferenceEquals(oldGroup, newGroup))
                throw new ArgumentException("Old group and new group cannot be the same.", nameof(newGroup));

            var result = new List<SelectListItem>();

            foreach (var item in items)
            {
                if (ReferenceEquals(item.Group, oldGroup))
                {
                    item.Group = newGroup;
                    result.Add(item);
                }
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Group select list items that do not have a group assigned to them.
        /// </summary>
        /// <param name="items">The select list items to group.</param>
        /// <param name="group">The group to assign to ungrouped items.</param>
        /// <returns>A read-only collection of items added to group.</returns>
        public static IReadOnlyCollection<SelectListItem> Group(this IEnumerable<SelectListItem> items, SelectListGroup group)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(group);

            var result = new List<SelectListItem>();

            foreach (var item in items)
            {
                if (item.Group == null)
                {
                    item.Group = group;
                    result.Add(item);
                }
            }

            return result.AsReadOnly();
        }
    }
}
