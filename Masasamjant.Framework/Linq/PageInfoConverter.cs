using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents converter to convert <see cref="PageInfo"/> to string and back.
    /// </summary>
    public sealed class PageInfoConverter : TypeConverter
    {
        private const char Separator = '|';

        /// <summary>
        /// Check if can convert <see cref="PageInfo"/> from specified source type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns><c>true</c> if can convert from <paramref name="sourceType"/>; <c>false</c> otherwise.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType.Equals(typeof(PageInfo)) || sourceType.Equals(typeof(string));
        }

        /// <summary>
        /// Check if can convert <see cref="PageInfo"/> to specified destination type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns><c>true</c> if <paramref name="destinationType"/> is type of <see cref="string"/>; <c>false</c> otherwise.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return destinationType != null && destinationType.Equals(typeof(string));
        }

        /// <summary>
        /// Convert specified value to <see cref="PageInfo"/> instance.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/>.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>A converted <see cref="PageInfo"/> or <c>null</c>.</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is PageInfo pageInfo)
                return pageInfo;
            else if (value is string str)
            {
                var parts = str.Split(Separator, StringSplitOptions.TrimEntries);

                if (parts.Length == 3)
                {
                    if (int.TryParse(parts[0], out int index) && index >= 0 &&
                        int.TryParse(parts[1], out int size) && size >= 0 &&
                        int.TryParse(parts[2], out int totalCount) && totalCount >= 0)
                    {
                        return new PageInfo(index, size)
                        {
                            TotalCount = totalCount
                        };
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Convert <see cref="PageInfo"/> value to specified destination type.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/>.</param>
        /// <param name="value">The <see cref="PageInfo"/> to convert.</param>
        /// <param name="destinationType">The destination type. Must be type of <see cref="string"/>.</param>
        /// <returns>A converted value or <c>null</c>.</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType.Equals(typeof(string)) && value is PageInfo pageInfo)
            {
                var values = new[] 
                { 
                    pageInfo.Index.ToString(), 
                    pageInfo.Size.ToString(), 
                    pageInfo.TotalCount.ToString() 
                };

                return string.Join(Separator, values);
            }

            return null;
        }
    }
}
