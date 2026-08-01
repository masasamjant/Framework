using System.Reflection;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents abstract view model that implements <see cref="ISupportCssClass"/>, <see cref="ISupportDisabledCssClass"/>, <see cref="ISupportDisplayOrder"/>, <see cref="ISupportHtmlAttributes"/> and <see cref="ISupportEnabledState"/>.
    /// </summary>
    public abstract class ViewModel : ISupportCssClass, ISupportDisabledCssClass, ISupportDisplayOrder, ISupportHtmlAttributes, ISupportEnabledState
    {
        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to root element. 
        /// </summary>
        /// <remarks>Default value is empty string.</remarks>
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to root element when it is disabled.
        /// </summary>
        /// <remarks>Default value is empty string.</remarks>
        public string DisabledCssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display order of the view model.
        /// </summary>
        /// <remarks>Default value is 0.</remarks>
        public int DisplayOrder { get; set; } = 0;

        /// <summary>
        /// Gets the HTML attributes dictionary.
        /// </summary>
        public IDictionary<string, object?> HtmlAttributes { get; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets or sets whether or not the view model is in enabled state. 
        /// If <c>false</c>, then HTML elements bound to this view model should be disabled, hidden or inactive, depending on the design.
        /// </summary>
        /// <remarks>Default value is <c>true</c>.</remarks>
        public bool IsEnabled { get; set; } = true;

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
