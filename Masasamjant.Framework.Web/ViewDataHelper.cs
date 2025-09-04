using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods to <see cref="ViewDataDictionary"/> class.
    /// </summary>
    public static class ViewDataHelper
    {
        /// <summary>
        /// Try get value of <typeparamref name="T"/> from specified <see cref="ViewDataDictionary"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="viewData">The <see cref="ViewDataDictionary"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value from <paramref name="viewData"/> if returns <c>true</c>.</param>
        /// <returns><c>true</c> if <paramref name="value"/> was get from <paramref name="viewData"/>; <c>false</c> otherwise.</returns>
        public static bool TryGetValue<T>(this ViewDataDictionary viewData, string key, [NotNullWhen(true)] out T? value)
            => DataDictionaryHelper.TryGetValue(viewData, key, out value);

        /// <summary>
        /// Gets value of <typeparamref name="T"/> from specified <see cref="ViewDataDictionary"/>, if exist or default value, if not.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="viewData">The <see cref="ViewDataDictionary"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value to return if value not in <paramref name="viewData"/>.</param>
        /// <returns>A <typeparamref name="T"/> value from <paramref name="viewData"/> or <paramref name="defaultValue"/>.</returns>
        public static T GetValueOrDefault<T>(this ViewDataDictionary viewData, string key, T defaultValue)
            => DataDictionaryHelper.GetValueOrDefault(viewData, key, defaultValue);
    }
}
