﻿using System.Reflection;

namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to enumeration types.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets custom attribute of <typeparamref name="TAttribute"/> applied to value of <typeparamref name="TEnum"/>.
        /// </summary>
        /// <typeparam name="TEnum">The type of enumeration.</typeparam>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <param name="inherit"><c>true</c> to get inherited attribute; <c>false</c> otherwise.</param>
        /// <returns>A applied <typeparamref name="TAttribute"/> attribute or <c>null</c>.</returns>
        public static TAttribute? GetCustomAttribute<TEnum, TAttribute>(this TEnum value, bool inherit = false)
            where TEnum : struct, Enum
            where TAttribute : Attribute
        {
            var memberInfo = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();

            return memberInfo?.GetCustomAttribute<TAttribute>(inherit);
        }

        /// <summary>
        /// Check if type of <typeparamref name="TEnum"/> enumeration has <see cref="FlagsAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="TEnum">The type of enumeration.</typeparam>
        /// <returns><c>true</c> if type of <typeparamref name="TEnum"/> has <see cref="FlagsAttribute"/>; <c>false</c> otherwise.</returns>
        public static bool IsFlagsEnum<TEnum>() where TEnum : struct, Enum
        {
            return typeof(TEnum).GetCustomAttribute<FlagsAttribute>(false) != null;
        }

        /// <summary>
        /// Check if type of <typeparamref name="TEnum"/> enumeration has <see cref="FlagsAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="TEnum">The type of enumeration.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <returns><c>true</c> if type of <typeparamref name="TEnum"/> has <see cref="FlagsAttribute"/>; <c>false</c> otherwise.</returns>
        public static bool IsFlagsEnum<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            return value.GetType().GetCustomAttribute<FlagsAttribute>(false) != null;
        }

        /// <summary>
        /// Gets count of setted flags in <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TEnum">The type of flags enumeration.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <param name="ignoreDefault"><c>true</c> to ignore default value of <typeparamref name="TEnum"/>; <c>false</c> otherwise.</param>
        /// <returns>A count of setted flags.</returns>
        /// <exception cref="ArgumentException">If type of <typeparamref name="TEnum"/> is not flags enumeration.</exception>
        public static int FlagCount<TEnum>(this TEnum value, bool ignoreDefault = true) where TEnum : struct, Enum
        {
            if (!value.IsFlagsEnum())
                throw new ArgumentException("The type of enumeration must have FlagsAttribute attribute.", nameof(value));

            if (ignoreDefault)
            {
                var defaultValue = default(TEnum);
                return Enum.GetValues<TEnum>().Where(flag => !flag.Equals(defaultValue) && value.HasFlag(flag)).Count();
            }
            else
                return Enum.GetValues<TEnum>().Where(flag => value.HasFlag(flag)).Count();
        }

        /// <summary>
        /// Check if specified value of <typeparamref name="TEnum"/> is equal to specified default flag value.
        /// </summary>
        /// <typeparam name="TEnum">The type of flags enumeration.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <param name="defaultFlag">The default flag value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is equal to <paramref name="defaultFlag"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If type of <typeparamref name="TEnum"/> is not flags enumeration.</exception>
        public static bool IsDefaultFlag<TEnum>(this TEnum value, TEnum defaultFlag = default) where TEnum : struct, Enum
        {
            if (!value.IsFlagsEnum())
                throw new ArgumentException("The type of enumeration must have FlagsAttribute attribute.", nameof(value));

            if (value.FlagCount(false) != 1)
                return false;

            return value.Equals(defaultFlag);
        }

        /// <summary>
        /// Check if specified value of <typeparamref name="TEnum"/> is equal to specified single flag value. 
        /// </summary>
        /// <typeparam name="TEnum">The type of flags enumeration.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <param name="expected">The expected single flag value.</param>
        /// <returns><c>true</c> if <paramref name="expected"/> has single flag set and <paramref name="value"/> is equal to that; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If type of <typeparamref name="TEnum"/> is not flags enumeration.</exception>
        public static bool IsSingleFlagsValue<TEnum>(this TEnum value, TEnum expected) where TEnum : struct, Enum
        {
            if (expected.FlagCount() == 1 && value.FlagCount() == 1)
                return value.HasFlag(expected);

            return false;
        }

        /// <summary>
        /// Gets setted flags of <typeparamref name="TEnum"/> from specified value.
        /// </summary>
        /// <typeparam name="TEnum">The type of flags enumeration.</typeparam>
        /// <param name="value">The <typeparamref name="TEnum"/> value.</param>
        /// <param name="includeDefaultValue"></param>
        /// <returns>A setted flags of <typeparamref name="TEnum"/> from <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentException">If type of <typeparamref name="TEnum"/> is not flags enumeration.</exception>
        public static IEnumerable<TEnum> Flags<TEnum>(this TEnum value, bool includeDefaultValue = false) where TEnum : struct, Enum
        {
            if (!value.IsFlagsEnum())
                throw new ArgumentException("The type of enumeration must have FlagsAttribute attribute.", nameof(value));
            if (includeDefaultValue)
                return Enum.GetValues<TEnum>().Where(flag => value.HasFlag(flag));
            else
                return Enum.GetValues<TEnum>().Where(flag => value.HasFlag(flag) && !flag.Equals(default(TEnum)));
        }

        /// <summary>
        /// Gets the types of that can be underlying enumeration types.
        /// </summary>
        /// <returns>A enumerable of types that can be underlying enumeration types.</returns>
        public static IEnumerable<Type> GetUnderlyingEnumTypes()
        {
            foreach (var type in underlyingEnumTypes)
                yield return type;
        }

        /// <summary>
        /// Check if specified <see cref="Type"/> represents enumeration or underlying enumeration type.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="type"/> represents enumeration or underlying enumeration type; <c>false</c> otherwise.</returns>
        public static bool IsEnumTypeOrUnderlyingEnumType(Type type)
        {
            return type.IsEnum || underlyingEnumTypes.Contains(type);
        }

        private static readonly HashSet<Type> underlyingEnumTypes = new HashSet<Type>()
        {
            typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong)
        };
    }
}
