using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Provides helper methods to work with type converters.
    /// </summary>
    public static class TypeConverterHelper
    {
        /// <summary>
        /// Check if specified type has <see cref="TypeConverterAttribute"/> attribute.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="attribute">The found <see cref="TypeConverterAttribute"/> attribute, if returns <c>true</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if type has <see cref="TypeConverterAttribute"/> attribute; <c>false</c> otherwise.</returns>
        public static bool HasTypeConverter(Type type, [MaybeNullWhen(false)] out TypeConverterAttribute attribute)
        {
            attribute = type.GetCustomAttribute<TypeConverterAttribute>(true);
            return attribute != null;
        }

        /// <summary>
        /// Tries to create <see cref="TypeConverter"/> instance from specified <see cref="TypeConverterAttribute"/>.
        /// </summary>
        /// <param name="attribute">The <see cref="TypeConverterAttribute"/>.</param>
        /// <returns>A <see cref="TypeConverter"/> or <c>null</c>.</returns>
        public static TypeConverter? CreateTypeConverter(this TypeConverterAttribute attribute)
        {
            var converterType = Type.GetType(attribute.ConverterTypeName);

            if (converterType == null)
                return null;

            return Activator.CreateInstance(converterType) as TypeConverter;
        }
    }
}
