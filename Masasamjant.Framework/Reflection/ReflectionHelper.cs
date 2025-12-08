using System.Reflection;
using System.Runtime.CompilerServices;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Provides helper methods for reflection.
    /// </summary>
    public static class ReflectionHelper
    {
        private const char PropertyPathSeparator = '.';

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
        /// <param name="parameterType">The type.</param>
        /// <param name="assembly">The <see cref="Assembly"/> to search for extension methods or <c>null</c> to use assembly of <paramref name="parameterType"/>.</param>
        /// <returns>A found extension methods of <paramref name="parameterType"/>.</returns>
        public static IEnumerable<MethodInfo> GetExtensionMethods(Type parameterType, Assembly? assembly = null)
        {
            if (assembly == null)
                assembly = parameterType.Assembly;

            var methods = from t in assembly.GetTypes()
                          where !t.IsGenericType && !t.IsNested
                          from m in t.GetMethods(BindingFlags.Static | BindingFlags.Public)
                          where m.IsDefined(typeof(ExtensionAttribute), false)
                          where m.GetParameters()[0].ParameterType.Equals(parameterType)
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
                ValidateExtensionMethodParameterTypes(parameterTypes, includeThisType, type);
            }

            int length = includeThisType ? parameterTypes.Length : parameterTypes.Length + 1;
            var methods = GetExtensionMethodsByName(type, assembly, name, length);
            var method = GetExtensionMethodByParameterTypes(methods, parameterTypes);
            return method;
        }

        private static void ValidateExtensionMethodParameterTypes(Type[] parameterTypes, bool includeThisType, Type thisType)
        {
            // If this type is included, then its actual type must be same as 'type'.
            if (includeThisType && !((ThisType)parameterTypes[0]).GetActualType().Equals(thisType))
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

        private static List<MethodInfo> GetExtensionMethodsByName(Type parameterType, Assembly? assembly, string methodName, int parametersLength)
        {
            return (from m in GetExtensionMethods(parameterType, assembly)
                    where m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase) &&
                    m.GetParameters().Length > 0 &&
                    m.GetParameters()[0].ParameterType.Equals(parameterType) &&
                    m.GetParameters().Length == parametersLength
                    select m).ToList();
        }

        private static MethodInfo? GetExtensionMethodByParameterTypes(List<MethodInfo> candidateMethods, Type[] parameterTypes)
        {
            if (candidateMethods.Count == 0)
                return null;

            if (candidateMethods.Count == 1)
                return candidateMethods[0];

            foreach (var cancidateMethod in candidateMethods)
            {
                var parameters = cancidateMethod.GetParameters();

                if (MethodParameterTypesMatch(parameters, parameterTypes))
                    return cancidateMethod;
            }

            return null;
        }

        private static bool MethodParameterTypesMatch(ParameterInfo[] methodParameters, Type[] expectedParameterTypes)
        {
            for (int index = 0; index < methodParameters.Length; index++)
            {
                if (!methodParameters[index + 1].ParameterType.Equals(expectedParameterTypes[index]))
                {
                    return false;
                }
            }

            return true;
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

            if (propertyName.Contains(PropertyPathSeparator))
                property = GetPropertyRecursive(instance, propertyName.Split(PropertyPathSeparator, StringSplitOptions.RemoveEmptyEntries), bindingFlags);
            else
                property = instance.GetType().GetProperty(propertyName, bindingFlags);

            // Check if property should be ignored in reflection.
            if (property != null && property.HasIngorePropertyAttribute())
                return null;

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

            if (propertyName.Contains(PropertyPathSeparator))
                property = GetPropertyRecursive(instance, propertyName.Split(PropertyPathSeparator, StringSplitOptions.RemoveEmptyEntries), bindingFlags, out owner);
            else
            {
                property = instance.GetType().GetProperty(propertyName, bindingFlags);
                owner = property != null ? instance : null;
            }

            // Check if property should be ignored in reflection.
            if (property != null && property.HasIngorePropertyAttribute())
            {
                owner = null;
                return null;
            }

            return property;
        }

        /// <summary>
        /// Gets property value.
        /// </summary>
        /// <param name="instance">The instance to get property.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="allowNonPublic"><c>true</c> allow using non-public property; <c>false</c> otherwise.</param>
        /// <returns>A property value or <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="propertyName"/> is empty or only whitespace.</exception>
        public static object? GetPropertyValue(object instance, string propertyName, bool allowNonPublic = false)
        {
            var propertySupport = allowNonPublic ? PropertySupport.Public | PropertySupport.NonPublic : PropertySupport.Public;
            propertySupport |= PropertySupport.Getter;

            var property = GetProperty(instance, propertyName, propertySupport, out var owner);
            if (property == null)
                return null;
            return property.GetValue(owner);
        }

        /// <summary>
        /// Sets property value.
        /// </summary>
        /// <param name="instance">The instance to set property.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="allowNonPublic"><c>true</c> to allow using non-public property; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="propertyName"/> is empty or only whitespace.</exception>
        /// <exception cref="TargetInvocationException">If invocation of property setter fails.</exception>
        public static void SetPropertyValue(object instance, string propertyName, object? value, bool allowNonPublic = false)
        {
            var propertySupport = allowNonPublic ? PropertySupport.Public | PropertySupport.NonPublic : PropertySupport.Public;
            propertySupport |= PropertySupport.Setter;

            var property = GetProperty(instance, propertyName, propertySupport, out var owner);
            if (property == null)
                return;
            try
            {
                property.SetValue(owner, value);
            }
            catch (Exception exception)
            {
                throw new TargetInvocationException($"Invoking setter of '{propertyName}' property failed. See inner exception.", exception);
            }
        }

        /// <summary>
        /// Invoke method of specified object instance.
        /// </summary>
        /// <param name="instance">The instance to invoke method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="parameters">The method parameters.</param>
        /// <returns>A result of method invoke.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="methodName"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <c>null</c> reference parameters are not defined using <see cref="NullType"/>.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="instance"/> do not have public instance method with name specified by <paramref name="methodName"/>.</exception>
        /// <exception cref="TargetInvocationException">If invocation of method fails.</exception>
        public static object? InvokeMethod(object instance, string methodName, object?[]? parameters)
        {
            var method = GetMethod(instance.GetType(), methodName, ref parameters);
            try
            {
                return method.Invoke(instance, parameters);
            }
            catch (Exception exception)
            {
                throw new TargetInvocationException($"Invoking method '{methodName}' failed. See inner exception.", exception);
            }
        }

        /// <summary>
        /// Gets methods of specified type and base type or interfaces, if <paramref name="inherited"/> is <c>true</c>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="binding">The binding to get method.</param>
        /// <param name="inherited"><c>true</c> to get also methods of interfaces if <paramref name="type"/> represents interface; <c>false</c> otherwise.</param>
        /// <returns>A found methods.</returns>
        public static IEnumerable<MethodInfo> GetMethods(this Type type, BindingFlags binding, bool inherited)
        { 
            var methods = new List<MethodInfo>();

            // First get methods in current type.
            var currentMethods = type.GetMethods(binding);
            if (currentMethods.Length > 0)
                methods.AddRange(currentMethods);

            // Get inherited methods if so requested and type is interface.
            if (inherited && type.IsInterface)
            {
                var interfaceTypes = type.GetInterfaces();

                foreach (var interfaceType in interfaceTypes)
                {
                    currentMethods = interfaceType.GetMethods(binding);
                    if (currentMethods.Length > 0)
                        methods.AddRange(currentMethods);
                }
            }

            return methods.AsEnumerable();
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

            BuildMethodParameters(parameters, newParameters, parameterTypes);

            var method = type.GetMethod(methodName, parameterTypes);

            if (method == null)
                throw new InvalidOperationException($"The type '{TypeHelper.GetTypeName(type)}' does not have public method of '{methodName}'.");

            parameters = newParameters;
            
            return method;
        }

        private static void BuildMethodParameters(object?[] parameters, object?[] newParameters, Type[] parameterTypes)
        {
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
        }

        private static MethodInfo? GetMethodRecursive(Type type, string methodName, Type[] parameterTypes)
        {
            if (type.Equals(typeof(object)))
                return null;

            if (type.IsInterface)
            { 
                var interfaceTypes = type.GetInterfaces();

                foreach (var interfaceType in interfaceTypes) 
                {
                    var method = GetMethodRecursive(interfaceType, methodName, parameterTypes);

                    if (method != null)
                        return method;
                }
            }
            else if (type.BaseType != null)
                return GetMethodRecursive(type.BaseType, methodName, parameterTypes);

            return null;
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
