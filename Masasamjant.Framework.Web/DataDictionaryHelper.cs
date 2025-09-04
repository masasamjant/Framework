using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web
{
    internal static class DataDictionaryHelper
    {
        internal static bool TryGetValue<T>(IDictionary<string, object?> dictionary, string key, [NotNullWhen(true)] out T? value)
        {
            if (dictionary.TryGetValue(key, out var result) && result is T)
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        internal static T GetValueOrDefault<T>(IDictionary<string, object?> dictionary, string key, T defaultValue)
            => TryGetValue<T>(dictionary, key, out var value) ? value : defaultValue;
    }
}
