using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Provides helper and extension methods to <see cref="PropertyInfo"/> class.
    /// </summary>
    public static class PropertyInfoHelper
    {
        /// <summary>
        /// Check if specified <see cref="PropertyInfo"/> represents index property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns><c>true</c> if <paramref name="property"/> represents index property; <c>false</c> otherwise.</returns>
        public static bool IsIndexProperty(this PropertyInfo property)
        {
            return property.GetIndexParameters().Length > 0;
        }

        /// <summary>
        /// Check if type of specified property implements <see cref="IEnumerable"/> interface. If <paramref name="ignoreString"/> is <c>true</c> and 
        /// property type is <see cref="string"/>, then returns <c>false</c>; otherwise returns <c>true</c> for strings.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="ignoreString"><c>true</c> to ignore string type in check; <c>false</c> otherwise.</param>
        /// <returns><c>true</c> of property type of <paramref name="property"/> implements <see cref="IEnumerable"/>; <c>false</c> otherwise.</returns>
        public static bool IsEnumerable(this PropertyInfo property, bool ignoreString = false)
        {
            if (property.PropertyType.Equals(typeof(string)) && ignoreString)
                return false;

            return TypeHelper.Implements(property.PropertyType, typeof(IEnumerable));
        }

        /// <summary>
        /// Check if specified property has <see cref="IgnorePropertyAttribute"/> attribute.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns><c>true</c> if <paramref name="property"/> has <see cref="IgnorePropertyAttribute"/> attribute; <c>false</c> otherwise.</returns>
        public static bool HasIngorePropertyAttribute(this PropertyInfo property) => HasCustomAttribute<IgnorePropertyAttribute>(property, false);

        /// <summary>
        /// Check if specified property has <typeparamref name="TAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="inherited"><c>true</c> if attribute can be inherited one; <c>false</c> otherwise.</param>
        /// <returns><c>true</c> if <paramref name="property"/> has <typeparamref name="TAttribute"/> attribute; <c>false</c> otherwise.</returns>
        public static bool HasCustomAttribute<TAttribute>(this PropertyInfo property, bool inherited = false) where TAttribute : Attribute
        {
            return property.GetCustomAttribute<TAttribute>(inherited) != null;
        }

        /// <summary>
        /// Check if specified property has <typeparamref name="TAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="attribute">The <typeparamref name="TAttribute"/> instance, if returns <c>true</c>; <c>null</c> otherwise.</param>
        /// <param name="inherited"><c>true</c> if attribute can be inherited one; <c>false</c> otherwise.</param>
        /// <returns><c>true</c> if <paramref name="property"/> has <typeparamref name="TAttribute"/> attribute; <c>false</c> otherwise.</returns>
        public static bool HasCustomAttribute<TAttribute>(this PropertyInfo property, [NotNullWhen(true)] out TAttribute? attribute, bool inherited = false) where TAttribute : Attribute
        {
            attribute = property.GetCustomAttribute<TAttribute>(inherited);
            return attribute != null;
        }

        /// <summary>
        /// Gets expression to access getter of specified property.
        /// </summary>
        /// <typeparam name="T">The property declaring type.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>A expression to access getter.</returns>
        /// <exception cref="ArgumentException">If the property is not from declaring type.</exception>
        public static Expression<Func<T, TValue?>> GetGetterExpression<T, TValue>(this PropertyInfo property)
        {
            ValidatePropertyDeclaringType<T>(property);
            var (parameterExpression, memberExpression) = GetPropertyMemberExpressions(property);
            return Expression.Lambda<Func<T, TValue?>>(memberExpression, parameterExpression);
        }

        /// <summary>
        /// Gets expression to access getter of specified property.
        /// </summary>
        /// <typeparam name="T">The property declaring type.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>A expression to access getter.</returns>
        /// <exception cref="ArgumentException">If the property is not from declaring type.</exception>
        public static Expression<Func<T, object?>> GetGetterExpression<T>(this PropertyInfo property)
        {
            ValidatePropertyDeclaringType<T>(property);
            var (parameterExpression, memberExpression) = GetPropertyMemberExpressions(property);
            return Expression.Lambda<Func<T, object?>>(memberExpression, parameterExpression);
        }

        private static (ParameterExpression ParameterExpression, MemberExpression MemberExpression) GetPropertyMemberExpressions(PropertyInfo property)
        {
            var parameterExpression = Expression.Parameter(property.DeclaringType!, "x");
            var memberExpression = Expression.Property(parameterExpression, property);
            return (parameterExpression, memberExpression);
        }

        private static void ValidatePropertyDeclaringType<T>(PropertyInfo property)
        {
            if (property.DeclaringType == null || !TypeHelper.IsOfType(property.DeclaringType, typeof(T)))
                throw new ArgumentException($"Property is not from the declaring type of {TypeHelper.GetTypeName(typeof(T))}.", nameof(property));
        }
    }
}
