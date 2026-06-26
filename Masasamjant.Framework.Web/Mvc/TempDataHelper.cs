using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides helper methods to <see cref="TempDataDictionary"/> class.
    /// </summary>
    public static class TempDataHelper
    {
        /// <summary>
        /// Try get value of <typeparamref name="T"/> from specified <see cref="TempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="tempData">The <see cref="TempDataDictionary"/> to get the value from.</param>
        /// <param name="key">The key of the value.</param>
        /// <param name="value">The value that was get, if returns <c>true</c>; otherwise, the default value of the <typeparamref name="T"/>.</param>
        /// <returns><c>true</c> if the <see cref="TempDataDictionary"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="tempData"/> is <c>null</c>.</exception>
        public static bool TryGetValue<T>(this TempDataDictionary tempData, string key, [NotNullWhen(true)] out T? value)
        {
            ArgumentNullException.ThrowIfNull(tempData);
            return DataDictionaryHelper.TryGetValue(tempData, key, out value);
        }

        /// <summary>
        /// Gets value of <typeparamref name="T"/> from specified <see cref="TempDataDictionary"/>, if exist or default value, if not.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="tempData">The <see cref="TempDataDictionary"/> to get the value from.</param>
        /// <param name="key">The key of the value.</param>
        /// <param name="defaultValue">The default value to return if the key is not found.</param>
        /// <returns>The value associated with the specified key, if found; otherwise, the default value.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="tempData"/> is <c>null</c>.</exception>
        public static T GetValueOrDefault<T>(this TempDataDictionary tempData, string key, T defaultValue)
            => TryGetValue<T>(tempData, key, out var value) ? value : defaultValue;
    }
}
