namespace Masasamjant
{
    /// <summary>
    /// Provies helper methods for working with <see cref="BooleanOption"/>.
    /// </summary>
    public static class BooleanOptionHelper
    {
        /// <summary>
        /// Check if specified <see cref="BooleanOption"/> is <see cref="BooleanOption.True"/>.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <returns><c>true</c> if <paramref name="option"/> is <see cref="BooleanOption.True"/>; <c>false</c> otherwise.</returns>
        public static bool IsTrue(this BooleanOption option)
        {
            return option == BooleanOption.True;
        }

        /// <summary>
        /// Check if specified <see cref="BooleanOption"/> is <see cref="BooleanOption.False"/>.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <returns><c>true</c> if <paramref name="option"/> is <see cref="BooleanOption.False"/>; <c>false</c> otherwise.</returns>
        public static bool IsFalse(this BooleanOption option)
        {
            return option == BooleanOption.False;
        }

        /// <summary>
        /// Check if specified <see cref="BooleanOption"/> is <see cref="BooleanOption.Default"/>.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <returns><c>true</c> if <paramref name="option"/> is <see cref="BooleanOption.Default"/>; <c>false</c> otherwise.</returns>
        public static bool IsDefault(this BooleanOption option)
        {
            return option == BooleanOption.Default;
        }

        /// <summary>
        /// Creates <see cref="BooleanOption"/> from specified nullable <see cref="bool"/> value. 
        /// If <paramref name="value"/> is <c>null</c>, then returns <see cref="BooleanOption.Default"/>.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value.</param>
        /// <returns>A <see cref="BooleanOption"/>.</returns>
        public static BooleanOption FromBoolean(bool? value)
        {
            return value.HasValue ? FromBoolean(value.Value) : BooleanOption.Default;
        }

        /// <summary>
        /// Creates <see cref="BooleanOption"/> from specified <see cref="bool"/> value.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value.</param>
        /// <returns>A <see cref="BooleanOption"/>.</returns>
        public static BooleanOption FromBoolean(bool value)
        {
            return value ? BooleanOption.True : BooleanOption.False;
        }

        /// <summary>
        /// Convert <see cref="BooleanOption"/> to nullable <see cref="bool"/> value.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <returns>
        /// A <c>true</c> if <paramref name="option"/> is <see cref="BooleanOption.True"/>.
        /// -or-
        /// A <c>false</c> if <paramref name="option"/> is <see cref="BooleanOption.False"/>.
        /// -or-
        /// A <c>null</c> otherwise.
        /// </returns>
        public static bool? ToBoolean(this BooleanOption option)
        {
            switch (option)
            {
                case BooleanOption.True:
                    return true;
                case BooleanOption.False:
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Convert <see cref="BooleanOption"/> to <see cref="bool"/> value.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <param name="valueForDefault">The replacement of <see cref="BooleanOption.Default"/>.</param>
        /// <returns>
        /// A <c>true</c> if <paramref name="option"/> is <see cref="BooleanOption.True"/>.
        /// -or-
        /// A <c>false</c> if <paramref name="option"/> is <see cref="BooleanOption.False"/>.
        /// -or-
        /// A <paramref name="valueForDefault"/> otherwise.
        /// </returns>
        public static bool ToBoolean(this BooleanOption option, bool valueForDefault)
        {
            switch (option)
            {
                case BooleanOption.True:
                    return true;
                case BooleanOption.False:
                    return false;
                default:
                    return valueForDefault;
            }
        }

        /// <summary>
        /// Converts <see cref="BooleanOption"/> to <see cref="bool"/> so that <see cref="BooleanOption.Default"/> is <c>true</c>.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <returns><c>true</c> if <paramref name="option"/> is <see cref="BooleanOption.True"/> or <see cref="BooleanOption.Default"/>; <c>false</c> otherwise.</returns>
        public static bool TrueIfDefault(this BooleanOption option)
        {
            return !IsFalse(option);
        }

        /// <summary>
        /// Converts <see cref="BooleanOption"/> to <see cref="bool"/> so that <see cref="BooleanOption.Default"/> is <c>false</c>.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <returns><c>false</c> if <paramref name="option"/> is <see cref="BooleanOption.False"/> or <see cref="BooleanOption.Default"/>; <c>true</c> otherwise.</returns>
        public static bool FalseIfDefault(this BooleanOption option)
        {
            return IsTrue(option);
        }

        /// <summary>
        /// Perform logical AND operation on two <see cref="BooleanOption"/>s.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> and <paramref name="right"/> are <see cref="BooleanOption.True"/>; <c>false</c> otherwise.
        /// </returns>
        public static bool And(BooleanOption left, BooleanOption right)
            => left.IsTrue() && right.IsTrue();

        /// <summary>
        /// Perform logical AND operation on two <see cref="BooleanOption"/>s.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <param name="valueForDefault">The value to replace <see cref="BooleanOption.Default"/>.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> and <paramref name="right"/> are <see cref="BooleanOption.True"/> or <see cref="BooleanOption.Default"/> and <paramref name="valueForDefault"/> is <c>true</c>; <c>false</c> otherwise.
        /// </returns>
        public static bool And(BooleanOption left, BooleanOption right, bool valueForDefault)
        {
            if (valueForDefault)
                return left.TrueIfDefault() && right.TrueIfDefault();
            else
                return left.FalseIfDefault() && right.FalseIfDefault();
        }

        /// <summary>
        /// Perform logical OR operation on two <see cref="BooleanOption"/>s.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> or <paramref name="right"/> are <see cref="BooleanOption.True"/>; <c>false</c> otherwise.
        /// </returns>
        public static bool Or(BooleanOption left, BooleanOption right)
            => left.IsTrue() || right.IsTrue();

        /// <summary>
        /// Perform logical OR operation on two <see cref="BooleanOption"/>s.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <param name="valueForDefault">The value to replace <see cref="BooleanOption.Default"/>.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> or <paramref name="right"/> are <see cref="BooleanOption.True"/> or <see cref="BooleanOption.Default"/> and <paramref name="valueForDefault"/> is <c>true</c>; <c>false</c> otherwise.
        /// </returns>
        public static bool Or(BooleanOption left, BooleanOption right, bool valueForDefault)
        {
            if (valueForDefault)
                return left.TrueIfDefault() || right.TrueIfDefault();
            else
                return left.FalseIfDefault() || right.FalseIfDefault();
        }

        /// <summary>
        /// Gets the opposite value of <see cref="BooleanOption"/>.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <returns>
        /// A <see cref="BooleanOption.Default"/>, if <paramref name="option"/> is <see cref="BooleanOption.Default"/>.
        /// -or-
        /// A <see cref="BooleanOption.False"/>, if <paramref name="option"/> is <see cref="BooleanOption.True"/>.
        /// -or-
        /// A <see cref="BooleanOption.True"/>, if <paramref name="option"/> is <see cref="BooleanOption.False"/>.
        /// </returns>
        public static BooleanOption Opposite(this BooleanOption option)
        {
            if (option.IsTrue())
                return BooleanOption.False;
            if (option.IsFalse())
                return BooleanOption.True;
            return BooleanOption.Default;
        }

        /// <summary>
        /// Gets the opposite value of <see cref="BooleanOption"/>.
        /// </summary>
        /// <param name="option">The <see cref="BooleanOption"/>.</param>
        /// <param name="valueForDefault">The value to replace <see cref="BooleanOption.Default"/>.</param>
        /// <returns>
        /// A <see cref="BooleanOption.False"/>, if <paramref name="option"/> is <see cref="BooleanOption.True"/>.
        /// -or-
        /// A <see cref="BooleanOption.False"/>, if <paramref name="option"/> is <see cref="BooleanOption.Default"/> and <paramref name="valueForDefault"/> <c>true</c>.
        /// -or-
        /// A <see cref="BooleanOption.True"/>, if <paramref name="option"/> is <see cref="BooleanOption.False"/>.
        /// -or-
        /// A <see cref="BooleanOption.True"/>, if <paramref name="option"/> is <see cref="BooleanOption.Default"/> and <paramref name="valueForDefault"/> is <c>false</c>.
        /// </returns>
        public static BooleanOption Opposite(this BooleanOption option, bool valueForDefault)
            => ToBoolean(option, valueForDefault) ? BooleanOption.False : BooleanOption.True;

        /// <summary>
        /// Check if all <see cref="BooleanOption"/>s are the expected one.
        /// </summary>
        /// <param name="options">The <see cref="BooleanOption"/>s to check.</param>
        /// <param name="expected">The expected <see cref="BooleanOption"/>.</param>
        /// <returns><c>true</c> if all in <paramref name="options"/> are <paramref name="expected"/>; <c>false</c> otherwise.</returns>
        public static bool All(this IEnumerable<BooleanOption> options, BooleanOption expected)
        {
            // By default if empty, return false.
            bool result = false;

            foreach (var option in options)
            {
                if (option != expected)
                    return false;
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Check if any <see cref="BooleanOption"/> is the expected one.
        /// </summary>
        /// <param name="options">The <see cref="BooleanOption"/>s to check.</param>
        /// <param name="expected">The expected <see cref="BooleanOption"/>.</param>
        /// <returns><c>true</c> if any in <paramref name="options"/> is <paramref name="expected"/>; <c>false</c> otherwise.</returns>
        public static bool Any(this IEnumerable<BooleanOption> options, BooleanOption expected)
        {
            foreach (var option in options)
            {
                if (option == expected)
                    return true;
            }

            return false;
        }
    }
}
