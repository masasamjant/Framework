using System.Globalization;

namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to work with numbers.
    /// </summary>
    public static class NumberHelper
    {
        private static readonly char[] starts = "-1234567890".ToCharArray();
        private static readonly char[] ends = "1234567890".ToCharArray();

        /// <summary>
        /// Try parse complex string to <see cref="int"/> value. The <paramref name="value"/> like
        /// "&lt; -25" is parsed to value -25.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>A parsed <see cref="int"/> or <c>null</c>, if cannot parse.</returns>
        public static int? ParseInt32(string value)
        {
            value = value.Trim();
            if (value.Length == 0)
                return null;

            if (int.TryParse(value, out int result))
                return result;

            // Trim so that has valid start and end characters.
            value = TrimToValidStartAndEnd(value);

            if (int.TryParse(value, out result))
                return result;

            // Check if value start with more than one '-' and trim expect 1.
            value = RemoveMultipleMinusSigns(value);

            if (int.TryParse(value, out result))
                return result;

            value = value.Remove(starts);

            if (int.TryParse(value, out result))
                return result;

            // Last thing to check is if '-' is somewhere in the middle of the string.
            value = RemoveMiddleMinus(value);

            if (int.TryParse(value, out result))
                return result;

            // Could not parse.
            return null;
        }

        /// <summary>
        /// Try parse complex string to <see cref="long"/> value. The <paramref name="value"/> like
        /// "&lt; -25" is parsed to value -25.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>A parsed <see cref="long"/> or <c>null</c>, if cannot parse.</returns>
        public static long? ParseInt64(string value)
        {
            value = value.Trim();
            if (value.Length == 0)
                return null;

            if (long.TryParse(value, out long result))
                return result;

            // Trim so that has valid start and end characters.
            value = TrimToValidStartAndEnd(value);

            if (long.TryParse(value, out result))
                return result;

            // Check if value start with more than one '-' and trim expect 1.
            value = RemoveMultipleMinusSigns(value);

            if (long.TryParse(value, out result))
                return result;

            value = value.Remove(starts);

            if (long.TryParse(value, out result))
                return result;

            // Last thing to check is if '-' is somewhere in the middle of the string.
            value = RemoveMiddleMinus(value);

            if (long.TryParse(value, out result))
                return result;

            // Could not parse.
            return null;
        }

        /// <summary>
        /// Gets the highest decimal digits amongst the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to get decimal separator or <c>null</c> to use current culture.</param>
        /// <returns>A highest digits.</returns>
        public static int GetHighestDigits(IEnumerable<double> values, CultureInfo? culture = null)
        {
            int digits = 0;
            
            var separator = GetCultureInfo(culture).NumberFormat.NumberDecimalSeparator;
            
            foreach (var value in values)
            {
                var str = value.ToString(GetCultureInfo(culture));
                if (str.Contains(separator))
                {
                    str = str.Split(separator)[1];
                    if (str.Length > digits)
                        digits = str.Length;
                }
            }

            return digits;
        }

        /// <summary>
        /// Gets the highest decimal digits amongst the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to get decimal separator or <c>null</c> to use current culture.</param>
        /// <returns>A highest digits.</returns>
        public static int GetHighestDigits(IEnumerable<decimal> values, CultureInfo? culture = null)
        {
            int digits = 0;

            var separator = GetCultureInfo(culture).NumberFormat.NumberDecimalSeparator;

            foreach (var value in values)
            {
                var str = value.ToString(GetCultureInfo(culture));
                if (str.Contains(separator))
                {
                    str = str.Split(separator)[1];
                    if (str.Length > digits)
                        digits = str.Length;
                }
            }

            return digits;
        }

        /// <summary>
        /// Gets positive values, greater than or equal to 0, from specified values.
        /// </summary>
        /// <param name="values">The initial values.</param>
        /// <returns>A positive values.</returns>
        public static IEnumerable<int> GetPositives(this IEnumerable<int> values)
        {
            return values.Where(value => value >= 0);
        }

        /// <summary>
        /// Gets positive values, greater than or equal to 0, from specified values.
        /// </summary>
        /// <param name="values">The initial values.</param>
        /// <returns>A positive values.</returns>
        public static IEnumerable<long> GetPositives(this IEnumerable<long> values)
        {
            return values.Where(value => value >= 0);
        }

        /// <summary>
        /// Gets negative values, less than 0, from specified values.
        /// </summary>
        /// <param name="values">The initial values.</param>
        /// <returns>A negative values.</returns>
        public static IEnumerable<int> GetNegatives(this IEnumerable<int> values)
        {
            return values.Where(value => value < 0);
        }

        /// <summary>
        /// Gets negative values, less than 0, from specified values.
        /// </summary>
        /// <param name="values">The initial values.</param>
        /// <returns>A negative values.</returns>
        public static IEnumerable<long> GetNegatives(this IEnumerable<long> values)
        {
            return values.Where(value => value < 0);
        }

        private static CultureInfo GetCultureInfo(CultureInfo? culture)
        {
            return culture ?? CultureInfo.CurrentCulture;
        }

        private static string TrimToValidStartAndEnd(string value)
        {
            value = value.TrimUntilStartsWith(starts);
            value = value.TrimUntilEndsWith(ends);
            return value;
        }

        private static string RemoveMultipleMinusSigns(string value)
        {
            int count = value.LeftCount('-');
            if (count > 1)
                value = value.Remove(0, count - 1);
            return value;
        }

        private static string RemoveMiddleMinus(string value)
        {
            bool negative = value.StartsWith('-');
            var parts = value.Split('-', StringSplitOptions.TrimEntries);
            value = string.Concat(parts);
            if (negative)
                value = "-" + value;
            return value;
        }
    }
}
