using System.Reflection;

namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Represents abstract view model.
    /// </summary>
    public abstract class ViewModel
    {
        /// <summary>
        /// Gets types in specified assembly that inherits from <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>A types that inherit from <see cref="ViewModel"/>.</returns>
        public static IEnumerable<Type> GetViewModelTypes(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (TypeHelper.IsOfType(type, typeof(ViewModel)))
                    yield return type;
            }
        }

        /// <summary>
        /// Gets the default format user in <see cref="GetDateTimeString(DateTimeOffset, string?)"/> and <see cref="GetDateTimeString(DateTimeOffset?, string?)"/> methods, 
        /// if else is not specified.
        /// Default value is "G".
        /// </summary>
        protected virtual string DefaultDateTimeFormatString
        {
            get { return "G"; }
        }

        /// <summary>
        /// Gets string presentation of <see cref="DateTimeOffset"/> value using specified format.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> value.</param>
        /// <param name="format">The format string or <c>null</c> if not format or empty or whitespace to use <see cref="DefaultDateTimeFormatString"/>.</param>
        /// <returns>A <paramref name="datetime"/> formatted to string.</returns>
        protected virtual string GetDateTimeString(DateTimeOffset datetime, string? format = null)
        {
            if (format == null)
                return datetime.ToString();

            if (string.IsNullOrWhiteSpace(format))
                format = DefaultDateTimeFormatString;

            return datetime.ToString(format);
        }

        /// <summary>
        /// Gets string presentation of <see cref="DateTimeOffset"/> value using specified format.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> value.</param>
        /// <param name="format">The format string or <c>null</c> if not format or empty or whitespace to use <see cref="DefaultDateTimeFormatString"/>.</param>
        /// <returns>A <paramref name="datetime"/> formatted to string or empty, if <paramref name="datetime"/> does not have value.</returns>
        protected virtual string GetDateTimeString(DateTimeOffset? datetime, string? format = null)
            => datetime.HasValue ? GetDateTimeString(datetime.Value, format) : string.Empty;
    }
}
