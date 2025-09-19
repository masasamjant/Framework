using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Masasamjant
{
    /// <summary>
    /// Represents <see cref="TypeConverter"/> that converts <see cref="Timing"/> to string and back.
    /// </summary>
    public sealed class TimingConverter : TypeConverter
    {
        private const char Separator = '|';

        /// <summary>
        /// Check if can convert from specified source type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns><c>true</c> if <paramref name="sourceType"/> is type of <see cref="string"/> or <see cref="Timing"/>; <c>false</c> otherwise.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType.Equals(typeof(string)) || sourceType.Equals(typeof(Timing));
        }

        /// <summary>
        /// Check if can convert <see cref="Timing"/> to specified destination type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns><c>true</c> if <paramref name="destinationType"/> is type of <see cref="string"/> or <see cref="Timing"/>; <c>false</c> otherwise.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return destinationType != null && (destinationType.Equals(typeof(Timing)) || destinationType.Equals(typeof(string)));
        }

        /// <summary>
        /// Converts specified object to <see cref="Timing"/>.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="culture">The conversion culture.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Timing"/> converted from <paramref name="value"/> or <c>null</c>, if not convertable.</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is Timing timing)
                return timing;
            else if (value is string str)
            {
                var parts = str.Split(Separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (culture == null)
                    culture = CultureInfo.InvariantCulture;

                if (parts.Length == 2)
                {
                    if (DateTime.TryParse(parts[0], culture, out var startDateTime) &&
                        DateTime.TryParse(parts[1], culture, out var endDateTime))
                        return new Timing(startDateTime, endDateTime);
                }
            }
            
            return null;
        }

        /// <summary>
        /// Converts specified <see cref="Timing"/> to instance of specified destination type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="culture">The conversion culture.</param>
        /// <param name="value">The <see cref="Timing"/> to convert.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>A instance of <paramref name="destinationType"/> or <c>null</c>, if not convertable.</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is Timing timing)
            {
                if (destinationType.Equals(typeof(Timing)))
                    return timing;
                else if (destinationType.Equals(typeof(string)))
                {
                    if (culture == null)
                        culture = CultureInfo.InvariantCulture;

                    return string.Join(Separator, timing.StartDateTime.ToString(culture), timing.EndDateTime.ToString(culture));
                }
            }

            return null;
        }
    }
}
