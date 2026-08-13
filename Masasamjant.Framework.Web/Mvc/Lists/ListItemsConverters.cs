using Masasamjant.Linq;
using Masasamjant.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.Mvc.Lists
{
    /// <summary>
    /// Provides list item converters or converter methods.
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
        /// Method that can be used to convert a list of <typeparamref name="TEnum"/> values to a list of <see cref="SelectListItem"/> with the first item, if provided.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="values">The values to convert.</param>
        /// <param name="current">The current value.</param>
        /// <param name="firstItem">The first item to include in the list, if any.</param>
        /// <returns>A list of <see cref="SelectListItem"/>.</returns>
        public static IEnumerable<SelectListItem> EnumConverter<TEnum>(IEnumerable<TEnum> values, TEnum current, SelectListItem? firstItem = null) where TEnum : struct, Enum
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
        /// Method that can be used to convert a list of <typeparamref name="TEnum"/> values to a list of <see cref="SelectListItem"/> with the first item, if provided.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="values">The values to convert.</param>
        /// <param name="current">The current value.</param>
        /// <param name="firstItem">The first item to include in the list, if any.</param>
        /// <returns>A list of <see cref="SelectListItem"/>.</returns>
        public static IEnumerable<SelectListItem> EnumConverter<TEnum>(IEnumerable<TEnum> values, TEnum? current, SelectListItem? firstItem = null) where TEnum : struct, Enum
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
        /// Method to get a list of <see cref="SelectListItem"/> with the first item, if provided.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="values">The values to convert.</param>
        /// <param name="current">The current value.</param>
        /// <param name="firstItem">The first item to include in the list, if any.</param>
        /// <returns>A list of <see cref="SelectListItem"/>.</returns>
        public static IEnumerable<SelectListItem> ValueConverter<T>(IEnumerable<T> values, T current, SelectListItem? firstItem = null) where T : struct
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
        /// Method to get a list of <see cref="SelectListItem"/> with the first item, if provided.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="values">The values to convert.</param>
        /// <param name="current">The current value, if any.</param>
        /// <param name="firstItem">The first item to include in the list, if any.</param>
        /// <returns>A list of <see cref="SelectListItem"/>.</returns>
        public static IEnumerable<SelectListItem> ValueConverter<T>(IEnumerable<T> values, T? current, SelectListItem? firstItem = null) where T : struct
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
