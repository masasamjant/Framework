namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeHelper
    {
        private const string TypeScopedKeySeparator = "+";

        /// <summary>
        /// Check if specified <see cref="Type"/> implements specified interface type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="interfaceType">The interface type.</param>
        /// <returns><c>true</c> if interfaces of <paramref name="type"/> includes <paramref name="interfaceType"/> or <paramref name="type"/> is <paramref name="interfaceType"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
        public static bool Implements(Type type, Type interfaceType)
        {
            if (!interfaceType.IsInterface)
                throw new ArgumentException("The type must represent interface.", nameof(interfaceType));

            return type.Equals(interfaceType) || type.GetInterfaces().Any(t => t.Equals(interfaceType));
        }

        /// <summary>
        /// Check if specified <see cref="Type"/> is of specified type or derived from it.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="expected">The expected type.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is <paramref name="expected"/> or derived from it; <c>false</c> otherwise.</returns>
        public static bool IsOfType(this Type type, Type expected)
        {
            return type.Equals(expected) || expected.IsAssignableFrom(type);
        }

        /// <summary>
        /// Get the name of specified <see cref="Type"/>. The name is assembly qualified name if available, 
        /// otherwise full name if available or name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// A assembly qualified name if available, or full name if available, or name.
        /// -or-
        /// A empty string if <paramref name="type"/> is <c>null</c>.
        /// </returns>
        public static string GetTypeName(Type? type)
        {
            if (type == null)
                return string.Empty;

            return type.AssemblyQualifiedName ?? type.FullName ?? type.Name;
        }

        /// <summary>
        /// Gets the preferred name of specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="preferred">The preferred type name.</param>
        /// <returns>
        /// A assembly qualified name if available and <paramref name="preferred"/> is <see cref="PreferredTypeName.AssemblyQualifiedName"/>.
        /// -or-
        /// A full name if available and <paramref name="preferred"/> is <see cref="PreferredTypeName.FullName"/>.
        /// -or-
        /// A name if available and <paramref name="preferred"/> is <see cref="PreferredTypeName.Name"/>.
        /// -or-
        /// A assembly qualified name if available, or full name if available, or name.
        /// -or-
        /// A empty string if <paramref name="type"/> is <c>null</c>.
        /// </returns>
        public static string GetTypeName(Type? type, PreferredTypeName preferred)
        {
            if (type == null)
                return string.Empty;

            if (preferred == PreferredTypeName.AssemblyQualifiedName && !string.IsNullOrWhiteSpace(type.AssemblyQualifiedName))
                return type.AssemblyQualifiedName;

            if (preferred == PreferredTypeName.FullName && !string.IsNullOrWhiteSpace(type.FullName))
                return type.FullName;

            if (preferred == PreferredTypeName.Name && !string.IsNullOrWhiteSpace(type.Name))
                return type.Name;

            return GetTypeName(type);
        }

        /// <summary>
        /// Get the hierarchy of types for specified <see cref="Type"/>. The first type in the hierarchy is the specified type itself, 
        /// if <paramref name="includeSelf"/> is <c>true</c>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="includeSelf"><c>true</c> to include <paramref name="type"/> to hierarchy; <c>false</c> otherwise.</param>
        /// <returns>A hierarcy of types.</returns>
        public static IEnumerable<Type> GetTypeHierarchy(this Type type, bool includeSelf = true)
        {
            if (includeSelf)
                yield return type;
            while (type.BaseType != null)
            {
                type = type.BaseType;
                yield return type;
            }
        }

        /// <summary>
        /// Check if specified <see cref="Type"/> represents concrete type i.e. not abstract, not interface and not generic type definition.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if <paramref name="type"/> represents concrete type; <c>false</c> otherwise.</returns>
        public static bool IsConcrete(this Type type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition)
                return false;

            return true;
        }

        /// <summary>
        /// Get the type scoped key for specified <see cref="Type"/>. 
        /// The type scoped key is a combination of type name and specified key.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The initial key.</param>
        /// <returns>A type scoped key.</returns>
        public static string GetTypeScopedKey(this Type type, string key)
        {
            return GetTypeName(type, PreferredTypeName.FullName) + TypeScopedKeySeparator + key;
        }

        /// <summary>
        /// Get the type scoped key for specified <see cref="object"/>.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="key">The initial key.</param>
        /// <returns>A type scoped key.</returns>
        public static string GetTypeScopedKey(object instance, string key) => GetTypeScopedKey(instance.GetType(), key);

        /// <summary>
        /// Get the original key from specified type scoped key.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="typedKey">The type scoped key.</param>
        /// <returns>A original key or <paramref name="typedKey"/>.</returns>
        public static string GetOriginalKey(this Type type, string typedKey)
        {
            var typeName = GetTypeName(type, PreferredTypeName.FullName);

            if (typedKey.StartsWith(typeName))
            {
                var key = typedKey.Remove(0, typeName.Length);
                if (key.StartsWith(TypeScopedKeySeparator) && key.Length > 1)
                    return key.Substring(1);
            }

            return typedKey;
        }

        /// <summary>
        /// Get the original key from specified type scoped key.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="typedKey">The type scoped key.</param>
        /// <returns>A original key or <paramref name="typedKey"/>.</returns>
        public static string GetOriginalKey(object instance, string typedKey) => GetOriginalKey(instance.GetType(), typedKey);
    }
}
