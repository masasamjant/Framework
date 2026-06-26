using System.Globalization;

namespace Masasamjant.Web
{
    /// <summary>
    /// Provides methods to work with cultures.
    /// </summary>
    public sealed class CultureHelper
    {
        /// <summary>
        /// Check if culture specified by name exist.
        /// </summary>
        /// <param name="cultureName">The name of the culture to check.</param>
        /// <returns><c>true</c> if culture exists; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="cultureName"/> is null, empty, or whitespace.</exception>
        public static bool IsAvailableCulture(string cultureName)
        {
            ValidateCultureName(cultureName);
            return instance.GetAvailableCultures().Any(x => x.Name == cultureName);
        }

        /// <summary>
        /// Get the culture specified by name.
        /// </summary>
        /// <param name="cultureName">The name of the culture to get.</param>
        /// <returns>The <see cref="CultureInfo"/> if found; <c>null</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="cultureName"/> is null, empty, or whitespace.</exception>
        public static CultureInfo? GetCulture(string cultureName)
        {
            ValidateCultureName(cultureName);
            return instance.GetAvailableCultures().FirstOrDefault(x => x.Name == cultureName);
        }

        private static void ValidateCultureName(string cultureName)
        {
            if (string.IsNullOrWhiteSpace(cultureName))
                throw new ArgumentNullException(nameof(cultureName), "Culture name cannot be null, empty or only whitespace.");
        }

        private static readonly CultureHelper instance = new();
        private readonly Lazy<IReadOnlyCollection<CultureInfo>> lazyCultures;

        private CultureHelper()
        {
            lazyCultures = new Lazy<IReadOnlyCollection<CultureInfo>>(() => CultureInfo.GetCultures(CultureTypes.AllCultures).ToList().AsReadOnly());
        }

        private IReadOnlyCollection<CultureInfo> GetAvailableCultures()
        {
            return lazyCultures.Value;
        }
    }
}
