using System.Reflection;
using System.Runtime.CompilerServices;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Provides helper methods for reflection.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Creates <see cref="ThisType"/> from specified <see cref="Type"/>.   
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A <see cref="ThisType"/>.</returns>
        public static ThisType ToThisType(this Type type)
        {
            if (type is ThisType thisType)
                return thisType;

            return new ThisType(type);
        }

        /// <summary>
        /// Check if specified <see cref="Type"/> is <see cref="ThisType"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is <see cref="ThisType"/>; <c>false</c> otherwise.</returns>
        public static bool IsThisType(this Type type)
        {
            return type is ThisType;
        }

        /// <summary>
        /// Creates <see cref="NullType"/> from specified <see cref="Type"/>.   
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A <see cref="NullType"/>.</returns>
        public static NullType ToNullType(this Type type)
        {
            if (type is NullType nullType)
                return nullType;

            return new NullType(type);
        }

        /// <summary>
        /// Check if specified <see cref="Type"/> is <see cref="NullType"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is <see cref="NullType"/>; <c>false</c> otherwise.</returns>
        public static bool IsNullType(this Type type)
        {
            return type is NullType;
        }

        /// <summary>
        /// Gets extension methods of an specified <see cref="Type"/> in specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="assembly">The <see cref="Assembly"/> to search for extension methods or <c>null</c> to use assembly of <paramref name="type"/>.</param>
        /// <returns>A found extension methods of <paramref name="type"/>.</returns>
        public static IEnumerable<MethodInfo> GetExtensionMethods(Type type, Assembly? assembly = null)
        {
            if (assembly == null)
                assembly = type.Assembly;

            var methods = from t in assembly.GetTypes()
                          where !t.IsGenericType && !t.IsNested
                          from m in t.GetMethods(BindingFlags.Static | BindingFlags.Public)
                          where m.IsDefined(typeof(ExtensionAttribute), false)
                          where m.GetParameters()[0].ParameterType.Equals(type)
                          select m;

            foreach (var method in methods)
                yield return method;
        }

        /// <summary>
        /// Gets the extension method specified by name of an specified <see cref="Type"/> in specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The method name.</param>
        /// <param name="assembly">The <see cref="Assembly"/> to search for extension methods or <c>null</c> to use assembly of <paramref name="type"/>.</param>
        /// <param name="parameterTypes">The array of parameter types or <c>null</c>. Use this to specify what overload method should be retrieve.</param>
        /// <returns>A found extension method or <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="parameterTypes"/> contains <see cref="ThisType"/>, but its not the first item.</exception>
        /// <remarks>If <c>this</c> type is included to <paramref name="parameterTypes"/>, then it must be the first type in array and specified using <see cref="ThisType"/>.</remarks>
        public static MethodInfo? GetExtensionMethod(Type type, string name, Assembly? assembly = null, Type[]? parameterTypes = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "The name cannot be empty or only whitespace.");

            bool includeThisType;

            if (parameterTypes == null || parameterTypes.Length == 0)
            {
                parameterTypes = [new ThisType(type)];
                includeThisType = true;
            }
            else
            {
                includeThisType = parameterTypes[0].IsThisType();

                // If this type is included, then its actual type must be same as 'type'.
                if (includeThisType && !((ThisType)parameterTypes[0]).GetActualType().Equals(type))
                    throw new ArgumentException("When the first type is 'ThisType', then its actual type must be the same as 'type'.", nameof(parameterTypes));

                // Only the first type can be 'ThisType', so check rest of the types.
                if (parameterTypes.Length > 1)
                {
                    for (int index = 1; index < parameterTypes.Length; index++)
                    {
                        if (parameterTypes[index].IsThisType())
                            throw new ArgumentException("The 'ThisType' must be the first type in array.", nameof(parameterTypes));
                    }
                }
            }

            int length = includeThisType ? parameterTypes.Length : parameterTypes.Length + 1;

            var methods = (from m in GetExtensionMethods(type, assembly)
                            where m.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                            m.GetParameters().Length > 0 &&
                            m.GetParameters()[0].ParameterType.Equals(type) &&
                            m.GetParameters().Length == length
                            select m).ToList();

            if (methods.Count == 0)
                return null;
            
            if (methods.Count == 1)
                return methods[0];

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();

                bool found = true;

                for (int index = 0; index < parameters.Length; index++)
                {
                    if (!parameters[index + 1].ParameterType.Equals(parameterTypes[index]))
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                    return method;
            }

            return null;
        }

        /// <summary>
        /// Gets <see cref="PropertyInfo"/> of the instance property specified by name. The property name can be specified as simple name i.e. "Property1"
        /// or as a path to the property i.e. "Property1.Property2.Property3".
        /// </summary>
        /// <param name="instance">The target instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertySupport">The <see cref="PropertySupport"/>.</param>
        /// <returns>A <see cref="PropertyInfo"/> or <c>null</c> if no such property exist.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="propertyName"/> is empty or only whitespace.</exception>
        public static PropertyInfo? GetProperty(object instance, string propertyName, PropertySupport propertySupport)
        {
            ValidatePropertyName(propertyName);

            if (propertySupport.Equals(PropertySupport.None))
                return null;

            var bindingFlags = propertySupport.GetBindingFlags(true);

            PropertyInfo? property;

            if (propertyName.Contains('.'))
                property = GetPropertyRecursive(instance, propertyName.Split('.', StringSplitOptions.RemoveEmptyEntries), bindingFlags);
            else
                property = instance.GetType().GetProperty(propertyName, bindingFlags);

            return property;
        }

        /// <summary>
        /// Gets <see cref="PropertyInfo"/> of the instance property specified by name. The property name can be specified as simple name i.e. "Property1" 
        /// or as a path to the property i.e. "Property1.Property2.Property3".
        /// </summary>
        /// <param name="instance">The target instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertySupport">The <see cref="PropertySupport"/>.</param>
        /// <param name="owner">The owner instance of the property, if property found.</param>
        /// <returns>A <see cref="PropertyInfo"/> or <c>null</c> if no such property exist.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="propertyName"/> is empty or only whitespace.</exception>
        public static PropertyInfo? GetProperty(object instance, string propertyName, PropertySupport propertySupport, out object? owner)
        {
            ValidatePropertyName(propertyName);

            if (propertySupport.Equals(PropertySupport.None))
            {
                owner = null;
                return null;
            }

            var bindingFlags = propertySupport.GetBindingFlags(true);

            PropertyInfo? property;

            if (propertyName.Contains('.'))
                property = GetPropertyRecursive(instance, propertyName.Split('.', StringSplitOptions.RemoveEmptyEntries), bindingFlags, out owner);
            else
            {
                property = instance.GetType().GetProperty(propertyName, bindingFlags);
                owner = property != null ? instance : null;
            }

            return property;
        }

        public static object? GetPropertyValue(object instance, string propertyName, bool allowNonPublic = false)
        {
            var propertySupport = allowNonPublic ? PropertySupport.PublicGetter | PropertySupport.NonPublicGetter : PropertySupport.PublicGetter;
            var property = GetProperty(instance, propertyName, propertySupport, out var owner);
            if (property == null)
                return null;
            return property.GetValue(owner);
        }

        public static void SetPropertyValue(object instance, string propertyName, object? value, bool allowNonPublic = false)
        {
            var propertySupport = allowNonPublic ? PropertySupport.PublicSetter | PropertySupport.NonPublicSetter : PropertySupport.PublicSetter;
            var property = GetProperty(instance, propertyName, propertySupport, out var owner);
            if (property == null)
                return;
            property.SetValue(owner, value);
        }

        public static object? InvokeMethod(object instance, string methodName, object?[]? parameters)
        {
            var method = GetMethod(instance.GetType(), methodName, ref parameters);
            return method.Invoke(instance, parameters);
        }

        private static PropertyInfo? GetPropertyRecursive(object instance, IEnumerable<string> propertyNames, BindingFlags bindingFlags)
        {
            var declaringType = instance.GetType();
            PropertyInfo? property = null;

            foreach (var propertyName in propertyNames)
            {
                property = declaringType.GetProperty(propertyName, bindingFlags);

                if (property == null)
                    return null;

                declaringType = property.PropertyType;
            }

            return property;
        }

        private static PropertyInfo? GetPropertyRecursive(object instance, IEnumerable<string> propertyNames, BindingFlags bindingFlags, out object? owner)
        {
            if (!propertyNames.Any())
            {
                owner = null;
                return null;
            }

            var lastPropertyName = propertyNames.Last();

            var declaringType = instance.GetType();
            PropertyInfo? property = null;
            owner = instance;

            foreach (var propertyName in propertyNames)
            {
                property = declaringType.GetProperty(propertyName, bindingFlags);

                if (property == null)
                {
                    owner = null;
                    return null;
                }

                if (propertyName != lastPropertyName)
                    owner = property.GetValue(owner, null);

                declaringType = property.PropertyType;
            }

            return property;
        }

        private static MethodInfo GetMethod(Type type, string methodName, ref object?[]? parameters)
        {
            ValidateMethodName(methodName);

            if (parameters == null)
                parameters = [];

            var newParameters = new object?[parameters.Length];

            var parameterTypes = new Type[parameters.Length];

            for (int index = 0; index < parameters.Length; index++)
            {
                var parameter = parameters[index];

                if (parameter == null)
                    throw new ArgumentException($"The null reference parameters must be specified using '{TypeHelper.GetTypeName(typeof(NullType))}'.", nameof(parameters));
                
                if (parameter is NullType nullType)
                {
                    parameterTypes[index] = nullType.GetActualType();
                    newParameters[index] = null;
                }
                else
                {
                    parameterTypes[index] = parameter.GetType();
                    newParameters[index] = parameter;
                }
            }

            var method = type.GetMethod(methodName, parameterTypes);

            if (method == null)
                throw new InvalidOperationException($"The type '{TypeHelper.GetTypeName(type)}' does not have public method of '{methodName}'.");

            parameters = newParameters;
            
            return method;
        }

        private static void ValidatePropertyName(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "The property name cannot be empty or only whitespace.");
        }

        private static void ValidateMethodName(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentNullException(nameof(methodName), "The method name cannot be empty or only whitespace.");
        }
    }
}
