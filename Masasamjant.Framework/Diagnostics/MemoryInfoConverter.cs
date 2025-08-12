using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents <see cref="TypeConverter"/> to convert <see cref="MemoryInfo"/> to <see cref="string"/> and back.
    /// </summary>
    public sealed class MemoryInfoConverter : TypeConverter
    {
        private const char Separator = '|';

        /// <summary>
        /// Check if can convert <see cref="MemoryInfo"/> from specified source type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns><c>true</c> if can convert from <paramref name="sourceType"/>; <c>false</c> otherwise.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(MemoryInfo) || sourceType == typeof(string);
        }

        /// <summary>
        /// Check if can convert <see cref="MemoryInfo"/> to specified destination type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns><c>true</c> if <paramref name="destinationType"/> is type of <see cref="string"/>; <c>false</c> otherwise.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return destinationType != null && (destinationType == typeof(string) || destinationType == typeof(MemoryInfo));
        }

        /// <summary>
        /// Convert specified value to <see cref="MemoryInfo"/> instance.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/>.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>A converted <see cref="MemoryInfo"/> or <c>null</c>.</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is MemoryInfo info)
                return info;
            else if (value is string s)
            {
                var parts = s.Split(Separator, StringSplitOptions.TrimEntries);

                if (parts.Length == 2 &&
                    byte.TryParse(parts[0], out var start) && start >= 0 &&
                    byte.TryParse(parts[1], out var end) && end >= 0)
                {
                    return new MemoryInfo(start, end);
                }
            }

            return null;
        }

        /// <summary>
        /// Convert <see cref="MemoryInfo"/> value to specified destination type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/>.</param>
        /// <param name="value">The <see cref="MemoryInfo"/> to convert.</param>
        /// <param name="destinationType">The destination type. Must be type of <see cref="string"/>.</param>
        /// <returns>A converted value or <c>null</c>.</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is MemoryInfo info)
            {
                if (destinationType.Equals(typeof(string)))
                {
                    var parts = new[] { info.StartBytes, info.EndBytes };
                    return string.Join(Separator, parts);
                }
                else if (destinationType.Equals(typeof(MemoryInfo)))
                    return info;
            }

            return null;
        }
    }
}
