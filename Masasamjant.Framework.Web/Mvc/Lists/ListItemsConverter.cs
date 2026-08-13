using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.Mvc.Lists
{
    /// <summary>
    /// Delegate to function to convert <see cref="IEnumerable{TValue}"/> values to <see cref="IEnumerable{SelectListItem}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="values">The values to convert.</param>
    /// <param name="current">The current value.</param>
    /// <param name="firstItem">The first item in the list or <c>null</c>.</param>
    /// <returns>A collection of <see cref="SelectListItem"/>.</returns>
    public delegate IEnumerable<SelectListItem> ListItemsConverter<TValue>(IEnumerable<TValue> values, TValue current, SelectListItem? firstItem = null);

    /// <summary>
    /// Delegate to function to convert <see cref="IEnumerable{TValue}"/> values to <see cref="IEnumerable{SelectListItem}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="values">The values to convert.</param>
    /// <param name="current">The current value.</param>
    /// <param name="firstItem">The first item in the list or <c>null</c>.</param>
    /// <returns>A collection of <see cref="SelectListItem"/>.</returns>
    public delegate IEnumerable<SelectListItem> ListItemsConverter<TValue, TResult>(IEnumerable<TValue> values, TResult current, SelectListItem? firstItem = null);
}
