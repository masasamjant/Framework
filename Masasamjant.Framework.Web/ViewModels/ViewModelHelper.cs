using Masasamjant.Resources;

namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Provides helper methods to <see cref="ViewModel"/> class.
    /// </summary>
    public static class ViewModelHelper
    {
        /// <summary>
        /// Helper method to get resource string of specified <typeparamref name="TEnum"/> value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="model">The view model.</param>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <returns>A resource string of <paramref name="value"/> or <paramref name="value"/> as string.</returns>
        public static string GetResourceString<TEnum>(this ViewModel model, TEnum value) where TEnum : struct, Enum
            => ResourceHelper.GetResourceString(value) ?? value.ToString();

        /// <summary>
        /// Helper method to get resource string of specified <typeparamref name="TEnum"/> value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="model">The view model.</param>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <returns>A resource string of <paramref name="value"/> or <paramref name="value"/> as string or empty string, if <paramref name="value"/> has no value.</returns>
        public static string GetResourceString<TEnum>(this ViewModel model, TEnum? value) where TEnum : struct, Enum
            => value.HasValue ? GetResourceString(model, value.Value) : string.Empty;
    }
}
