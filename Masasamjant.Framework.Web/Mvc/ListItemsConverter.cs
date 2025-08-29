using Masasamjant.Linq;
using Masasamjant.Resources;
using Masasamjant.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Delegate to function to convert <see cref="IEnumerable{TValue}"/> values to <see cref="IEnumerable{SelectListItem}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="values">The values.</param>
    /// <param name="current">The current value.</param>
    /// <param name="firstItem">The first item to include or <c>null</c>.</param>
    /// <returns>A <see cref="IEnumerable{SelectListItem}"/>.</returns>
    public delegate IEnumerable<SelectListItem> ListItemsConverter<TValue>(IEnumerable<TValue> values, TValue current, SelectListItem? firstItem);

    /// <summary>
    /// Delegate to function to convert <see cref="IEnumerable{TValue}"/> values to <see cref="IEnumerable{SelectListItem}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the source values.</typeparam>
    /// <typeparam name="TResult">The type of the selected value.</typeparam>
    /// <param name="values">The values.</param>
    /// <param name="current">The current value.</param>
    /// <param name="firstItem">The first item to include or <c>null</c>.</param>
    /// <returns>A <see cref="IEnumerable{SelectListItem}"/>.</returns>
    public delegate IEnumerable<SelectListItem> ListItemsConverter<TValue, TResult>(IEnumerable<TValue> values, TResult current, SelectListItem? firstItem);

    /// <summary>
    /// Defines several <see cref="ListItemsConverter{TValue}"/> and <see cref="ListItemsConverter{TValue, TResult}"/> converters.
    /// </summary>
    public static class ListItemsConverters
    {
        /// <summary>
        /// Gets the <see cref="ListItemsConverter{string}"/> converter.
        /// </summary>
        public static ListItemsConverter<string> StringConverter { get; } =
            new ListItemsConverter<string>((values, current, firstItem) => 
            {
                var selectItems = GetSelectListItemList(firstItem);

                foreach (var value in values)
                    selectItems.Add(new SelectListItem(value, value, value == current));
                
                return selectItems.AsEnumerable();
            });

        /// <summary>
        /// Gets the <see cref="ListItemsConverter{bool, string}"/> converter.
        /// </summary>
        public static ListItemsConverter<bool, string> BooleanStringConverter { get; } =
            new ListItemsConverter<bool, string>((values, current, firstItem) => 
            {
                var selectItems = GetSelectListItemList(firstItem);

                foreach (var value in values)
                {
                    var itemValue = value ? bool.TrueString : bool.FalseString;
                    var itemText = BooleanOptionHelper.FromBoolean(value).GetResourceString();
                    selectItems.Add(new SelectListItem(itemText, itemValue, itemValue == current));
                }

                return selectItems.AsEnumerable();
            });

        /// <summary>
        /// Gets the <see cref="ListItemsConverter{SortOrder}"/> converter.
        /// </summary>
        public static ListItemsConverter<SortOrder> SortOrderConverter { get; } =
            new ListItemsConverter<SortOrder>((values, current, firstItem) => 
            {
                var selectItems = GetSelectListItemList(firstItem);

                foreach (var value in values)
                {
                    var itemValue = value.ToString();
                    var itemText = string.Empty;
                    if (value != SortOrder.None)
                        itemText = value.GetResourceString();
                    selectItems.Add(new SelectListItem(itemText, itemValue, value == current));
                }

                return selectItems.AsEnumerable();
            });

        /// <summary>
        /// Gets the <see cref="ListItemsConverter{int}"/> converter.
        /// </summary>
        public static ListItemsConverter<int> Int32Converter { get; } = new ListItemsConverter<int>(ValueConverter);

        /// <summary>
        /// Method that can be used for <see cref="ListItemsConverter{TEnum}"/> converter.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="values">The available <typeparamref name="TEnum"/> values.</param>
        /// <param name="current">The current <typeparamref name="TEnum"/> value.</param>
        /// <param name="firstItem">The first item to include or <c>null</c>.</param>
        /// <returns>A <see cref="IEnumerable{SelectListItem}"/>.</returns>
        public static IEnumerable<SelectListItem> EnumConverter<TEnum>(IEnumerable<TEnum> values, TEnum current, SelectListItem? firstItem) where TEnum : struct, Enum
        {
            var selectItems = GetSelectListItemList(firstItem);

            foreach (var value in values)
            {
                var itemValue = value.ToString();
                var itemText = value.GetResourceStringOrName();
                selectItems.Add(new SelectListItem(itemText, itemValue, value.Equals(current)));
            }

            return selectItems.AsEnumerable();
        }

        /// <summary>
        /// Method that can be used for <see cref="ListItemsConverter{TEnum}"/> converter.
        /// </summary>
        /// <typeparam name="TEnum">The type of the nullable enumeration.</typeparam>
        /// <param name="values">The available <typeparamref name="TEnum"/> values.</param>
        /// <param name="current">The current <typeparamref name="TEnum"/> value.</param>
        /// <param name="firstItem">The first item to include or <c>null</c>.</param>
        /// <returns>A <see cref="IEnumerable{SelectListItem}"/>.</returns>
        public static IEnumerable<SelectListItem> EnumConverter<TEnum>(IEnumerable<TEnum> values, TEnum? current, SelectListItem? firstItem) where TEnum : struct, Enum
        {
            var selectItems = GetSelectListItemList(firstItem);

            foreach (var value in values)
            {
                var itemValue = value.ToString();
                var itemText = value.GetResourceStringOrName();
                selectItems.Add(new SelectListItem(itemText, itemValue, current.HasValue && value.Equals(current.Value)));
            }

            return selectItems.AsEnumerable();
        }

        /// <summary>
        /// Method that can be used for <see cref="ListItemsConverter{T}"/> converter.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="values">The available <typeparamref name="T"/> values.</param>
        /// <param name="current">The current <typeparamref name="T"/> value.</param>
        /// <param name="firstItem">The first item to include or <c>null</c>.</param>
        /// <returns>A <see cref="IEnumerable{SelectListItem}"/>.</returns>
        public static IEnumerable<SelectListItem> ValueConverter<T>(IEnumerable<T> values, T current, SelectListItem? firstItem) where T : struct
        {
            var selectItems = GetSelectListItemList(firstItem);

            foreach (var value in values)
            {
                var itemValue = value.ToString() ?? string.Empty;
                var itemText = value.ToString() ?? string.Empty;
                selectItems.Add(new SelectListItem(itemText, itemValue, value.Equals(current)));
            }

            return selectItems.AsEnumerable();
        }

        /// <summary>
        /// Method that can be used for <see cref="ListItemsConverter{T}"/> converter.
        /// </summary>
        /// <typeparam name="T">The type of the nullable value.</typeparam>
        /// <param name="values">The available <typeparamref name="T"/> values.</param>
        /// <param name="current">The current <typeparamref name="T"/> value.</param>
        /// <param name="firstItem">The first item to include or <c>null</c>.</param>
        /// <returns>A <see cref="IEnumerable{SelectListItem}"/>.</returns>
        public static IEnumerable<SelectListItem> ValueConverter<T>(IEnumerable<T> values, T? current, SelectListItem? firstItem) where T : struct
        {
            var selectItems = GetSelectListItemList(firstItem);

            foreach (var value in values)
            {
                var itemValue = value.ToString() ?? string.Empty;
                var itemText = value.ToString() ?? string.Empty;
                selectItems.Add(new SelectListItem(itemText, itemValue, current.HasValue && value.Equals(current.Value)));
            }

            return selectItems.AsEnumerable();
        }

        private static List<SelectListItem> GetSelectListItemList(SelectListItem? firstItem)
        {
            var selectItems = new List<SelectListItem>();

            if (firstItem != null)
                selectItems.Add(firstItem);

            return selectItems;
        }
    }
}
