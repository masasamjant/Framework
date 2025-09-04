using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web
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
        /// <param name="tempData">The <see cref="TempDataDictionary"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value from <paramref name="tempData"/> if returns <c>true</c>.</param>
        /// <returns><c>true</c> if <paramref name="value"/> was get from <paramref name="tempData"/>; <c>false</c> otherwise.</returns>
        public static bool TryGetValue<T>(this TempDataDictionary tempData, string key, [NotNullWhen(true)] out T? value)
            => DataDictionaryHelper.TryGetValue(tempData, key, out value);

        /// <summary>
        /// Gets value of <typeparamref name="T"/> from specified <see cref="TempDataDictionary"/>, if exist or default value, if not.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="tempData">The <see cref="TempDataDictionary"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value to return if value not in <paramref name="tempData"/>.</param>
        /// <returns>A <typeparamref name="T"/> value from <paramref name="tempData"/> or <paramref name="defaultValue"/>.</returns>
        public static T GetValueOrDefault<T>(this TempDataDictionary tempData, string key, T defaultValue)
            => DataDictionaryHelper.GetValueOrDefault(tempData, key, defaultValue);
    }
}
