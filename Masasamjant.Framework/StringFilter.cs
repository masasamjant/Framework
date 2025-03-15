using Masasamjant.Serialization;
using System.Text.Json.Serialization;

namespace Masasamjant
{
    /// <summary>
    /// Represents filter to filter string values.
    /// </summary>
    public class StringFilter : IJsonSerializable
    {
        /// <summary>
        /// Initializes new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="filterType">The <see cref="StringFilterType"/>.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="filterType"/> is not defined.</exception>
        public StringFilter(string filterValue, StringFilterType filterType)
            : this(filterValue, filterType, StringComparison.CurrentCulture)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="filterType">The <see cref="StringFilterType"/>.</param>
        /// <param name="comparison">The <see cref="StringComparison"/>.</param>
        /// <exception cref="ArgumentException">
        /// If value of <paramref name="filterType"/> is not defined.
        /// -or-
        /// If value of <paramref name="comparison"/> is not defined.
        /// </exception>
        public StringFilter(string filterValue, StringFilterType filterType, StringComparison comparison)
        {
            if (!Enum.IsDefined(filterType))
                throw new ArgumentException("The value is not defined.", nameof(filterType));

            if (!Enum.IsDefined(comparison))
                throw new ArgumentException("The value is not defined.", nameof(comparison));

            FilterValue = filterValue;
            FilterType = filterType;
            Comparison = comparison;
        }

        /// <summary>
        /// Initializes new default instance of the <see cref="StringFilter"/> class.
        /// </summary>
        public StringFilter()
        { }

        /// <summary>
        /// Gets the filter value.
        /// </summary>
        [JsonInclude]
        public string FilterValue { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets the <see cref="StringFilterType"/>.
        /// </summary>
        [JsonInclude]
        public StringFilterType FilterType { get; protected set; }

        /// <summary>
        /// Gets the <see cref="StringComparison"/>. Default is <see cref="StringComparison.CurrentCulture"/>.
        /// </summary>
        [JsonInclude]
        public StringComparison Comparison { get; protected set; } = StringComparison.CurrentCulture;

        /// <summary>
        /// Check if specified value match with filter.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if <paramref name="value"/> match with filter; <c>false</c> otherwise.</returns>
        public bool Matches(string value)
        {
            switch (FilterType)
            {
                case StringFilterType.Match:
                    return string.Equals(value, FilterValue, Comparison);
                case StringFilterType.Contains:
                    return value.Contains(FilterValue, Comparison);
                case StringFilterType.StartsWith:
                    return value.StartsWith(FilterValue, Comparison);
                case StringFilterType.EndsWith:
                    return value.EndsWith(FilterValue, Comparison);
                default:
                    throw new NotSupportedException("The filter type is not supported.");
            }
        }

        /// <summary>
        /// Apply filter to specified values returning only those values that match the filter.
        /// </summary>
        /// <param name="values">The initial values.</param>
        /// <returns>A values that match filter.</returns>
        public IEnumerable<string> Apply(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                if (Matches(value))
                    yield return value;
            }
        }
    }
}
