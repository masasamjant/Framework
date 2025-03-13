using System.Reflection;

namespace Masasamjant.Resources
{
    /// <summary>
    /// Provides helper methods related to resources.
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A resource string or <c>null</c>.</returns>
        public static string? GetResourceString<T>() => GetResourceString(typeof(T));

        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to type of <typeparamref name="T"/> or name of the type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A resource string or name of <typeparamref name="T"/>.</returns>
        public static string GetResourceStringOrName<T>() => GetResourceStringOrName(typeof(T));

        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A resource string or <c>null</c>.</returns>
        public static string? GetResourceString(this Type type)
        {
            var resourceStringAttribute = type.GetCustomAttribute<ResourceStringAttribute>(false);
            return resourceStringAttribute?.ResourceValue;
        }

        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to specified type or name of the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A resource string or name of the type.</returns>
        public static string GetResourceStringOrName(this Type type)
        {
            var resourceStringAttribute = type.GetCustomAttribute<ResourceStringAttribute>(false);
            return resourceStringAttribute?.ResourceValue ?? type.Name;
        }

        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to specified <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="property">The <see cref="PropertyInfo"/>.</param>
        /// <returns>A resource string or <c>null</c>.</returns>
        public static string? GetResourceString(this PropertyInfo property)
        {
            var resourceStringAttribute = property.GetCustomAttribute<ResourceStringAttribute>(false);
            return resourceStringAttribute?.ResourceValue;
        }

        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to specified <see cref="FieldInfo"/>.
        /// </summary>
        /// <param name="field">The <see cref="FieldInfo"/>.</param>
        /// <returns>A resource string or <c>null</c>.</returns>
        public static string? GetResourceString(this FieldInfo field)
        {
            var resourceStringAttribute = field.GetCustomAttribute<ResourceStringAttribute>(false);
            return resourceStringAttribute?.ResourceValue;
        }

        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to value of <typeparamref name="TEnum"/>. If <typeparamref name="TEnum"/> is 
        /// flags enum, then gets resource string of each setted flag concatenated into single string where each resource string is separated by comma.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <returns>A resource string or <c>null</c>.</returns>
        public static string? GetResourceString<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            if (value.IsFlagsEnum())
            {
                var resources = new List<string>();

                foreach (var flag in value.Flags(true))
                {
                    if (value.FlagCount(false) == 1)
                    {
                        var resourceStringAttribute = value.GetCustomAttribute<TEnum, ResourceStringAttribute>();
                        return resourceStringAttribute?.ResourceValue;
                    }
                    else
                    {
                        if (value.IsDefaultFlag())
                            continue;

                        var resourceStringAttribute = flag.GetCustomAttribute<TEnum, ResourceStringAttribute>();
                        if (resourceStringAttribute != null)
                            resources.Add(resourceStringAttribute.ResourceValue);
                    }
                }

                if (resources.Count == 0)
                    return null;

                return string.Join(',', resources);
            }
            else
            {
                var resourceStringAttribute = value.GetCustomAttribute<TEnum, ResourceStringAttribute>(false);
                return resourceStringAttribute?.ResourceValue;
            }
        }

        /// <summary>
        /// Gets resource string of <see cref="ResourceStringAttribute"/> applied to value of <typeparamref name="TEnum"/> or name of value. If <typeparamref name="TEnum"/> is 
        /// flags enum, then gets resource string of each setted flag concatenated into single string where each resource string is separated by comma.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <returns>A resource string or name of value.</returns>
        public static string GetResourceStringOrName<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            if (value.IsFlagsEnum())
            {
                var resources = new List<string>();

                foreach (var flag in value.Flags(true))
                {
                    if (value.FlagCount(false) == 1)
                    {
                        var resourceString = flag.GetResourceString();
                        return resourceString ?? Enum.GetName(flag) ?? flag.ToString();
                    }
                    else
                    {
                        if (flag.IsDefaultFlag())
                            continue;

                        var resourceString = flag.GetResourceString() ?? Enum.GetName(flag) ?? flag.ToString();
                        resources.Add(resourceString);
                    }
                }

                if (resources.Count == 0)
                    return string.Empty;

                return string.Join(',', resources);
            }
            else
            {
                var resourceString = value.GetResourceString();
                return resourceString ?? Enum.GetName(value) ?? value.ToString();
            }
        }
    }
}
