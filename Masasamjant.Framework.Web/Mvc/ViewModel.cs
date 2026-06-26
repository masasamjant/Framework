using System.ComponentModel;
using System.Reflection;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents abstract view model.
    /// </summary>
    public abstract class ViewModel
    {
        /// <summary>
        /// Gets types in specified assembly that inherits from <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="assembly">The assembly to search for view model types.</param>
        /// <returns>An enumerable of types that inherit from <see cref="ViewModel"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="assembly"/> is <c>null</c>.</exception>
        public static IEnumerable<Type> GetViewModelTypes(Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(assembly);

            var t = typeof(ViewModel);

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsOfType(t))
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Gets the default format used in <see cref="GetDateTimeString(DateTime, string?)"/> and <see cref="GetDateTimeString(DateTime?, string?)"/> methods,
        /// as well as in <see cref="GetDateTimeString(DateTimeOffset, string?)"/> and <see cref="GetDateTimeString(DateTimeOffset?, string?)"/> methods. This format is used, 
        /// if format passed to those methods is empty or only whitespace.
        /// </summary>
        /// <remarks>Default value in base class is "G".</remarks>
        protected virtual string DefaultDateTimeFormatString
        { 
            get { return "G"; }
        }

        /// <summary>
        /// Gets string representation of <see cref="DateTime"/> value using specified format.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> value to format.</param>
        /// <param name="format">The format string to use.</param>
        /// <returns>A string representation of the <see cref="DateTime"/> value.</returns>
        /// <remarks>
        /// If <paramref name="format"/> is <c>null</c>, then format is not used. 
        /// If empty or whitespace, then <see cref="DefaultDateTimeFormatString"/> is used.
        /// </remarks>
        protected virtual string GetDateTimeString(DateTime datetime, string? format = null)
        {
            if (format == null)
                return datetime.ToString();

            if (string.IsNullOrWhiteSpace(format))
                format = DefaultDateTimeFormatString;

            return datetime.ToString(format);
        }

        /// <summary>
        /// Gets string representation of <see cref="DateTime"/> value using specified format.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> value to format.</param>
        /// <param name="format">The format string to use.</param>
        /// <returns>A string representation of the <see cref="DateTime"/> value or empty string.</returns>
        /// <remarks>
        /// If <paramref name="format"/> is <c>null</c>, then format is not used. 
        /// If empty or whitespace, then <see cref="DefaultDateTimeFormatString"/> is used. 
        /// If <paramref name="datetime"/> has no value, then empty string is returned.
        /// </remarks>
        protected virtual string GetDateTimeString(DateTime? datetime, string? format = null)
            => datetime.HasValue ? GetDateTimeString(datetime.Value, format) : string.Empty;

        /// <summary>
        /// Gets string representation of <see cref="DateTimeOffset"/> value using specified format.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> value to format.</param>
        /// <param name="format">The format string to use.</param>
        /// <returns>A string representation of the <see cref="DateTimeOffset"/> value.</returns>
        /// <remarks>
        /// If <paramref name="format"/> is <c>null</c>, then format is not used. 
        /// If empty or whitespace, then <see cref="DefaultDateTimeFormatString"/> is used.
        /// </remarks>
        protected virtual string GetDateTimeString(DateTimeOffset datetime, string? format = null)
        {
            if (format == null)
                return datetime.ToString();

            if (string.IsNullOrWhiteSpace(format))
                format = DefaultDateTimeFormatString;

            return datetime.ToString(format);
        }

        /// <summary>
        /// Gets string representation of <see cref="DateTimeOffset"/> value using specified format.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> value to format.</param>
        /// <param name="format">The format string to use.</param>
        /// <returns>A string representation of the <see cref="DateTimeOffset"/> value or empty string.</returns>
        /// <remarks>
        /// If <paramref name="format"/> is <c>null</c>, then format is not used. 
        /// If empty or whitespace, then <see cref="DefaultDateTimeFormatString"/> is used. 
        /// If <paramref name="datetime"/> has no value, then empty string is returned.
        /// </remarks>
        protected virtual string GetDateTimeString(DateTimeOffset? datetime, string? format = null)
            => datetime.HasValue ? GetDateTimeString(datetime.Value, format) : string.Empty;
    }
}
