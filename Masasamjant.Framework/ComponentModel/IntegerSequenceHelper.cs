namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Provides helper methods to <see cref="IntegerSequence"/> class.
    /// </summary>
    public static class IntegerSequenceHelper
    {
        /// <summary>
        /// Check if <see cref="IntegerSequence"/> represents zero. Sequence is consired to be zero when
        /// it is not empty and all characters of value are '0'.
        /// </summary>
        /// <param name="sequence">The <see cref="IntegerSequence"/>.</param>
        /// <returns><c>true</c> if all characters of value of <paramref name="sequence"/> are '0'; <c>false</c> otherwise.</returns>
        public static bool IsZero(this IntegerSequence sequence)
        {
            return !sequence.IsEmpty && sequence.Value.All(c => c == '0');
        }

        /// <summary>
        /// Tries to get value of <see cref="IntegerSequence"/> as <see cref="int"/> value.
        /// </summary>
        /// <param name="sequence">The <see cref="IntegerSequence"/>.</param>
        /// <param name="result">The result if return <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if can create <see cref="int"/> from value of <paramref name="sequence"/>; <c>false</c> otherwise.</returns>
        public static bool TryGetInt32(this IntegerSequence sequence, out int? result)
        {
            result = null;

            if (sequence.IsEmpty)
                return false;

            if (IsZero(sequence))
                result = 0;
            else if (int.TryParse(sequence.Value, out var i))
                result = i;

            return result.HasValue;
        }

        /// <summary>
        /// Tries to get value of <see cref="IntegerSequence"/> as <see cref="long"/> value.
        /// </summary>
        /// <param name="sequence">The <see cref="IntegerSequence"/>.</param>
        /// <param name="result">The result if return <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if can create <see cref="long"/> from value of <paramref name="sequence"/>; <c>false</c> otherwise.</returns>
        public static bool TryGetInt64(this IntegerSequence sequence, out long? result)
        {
            result = null;

            if (sequence.IsEmpty)
                return false;

            if (IsZero(sequence))
                result = 0L;
            else if (long.TryParse(sequence.Value, out var l))
                result = l;

            return result.HasValue;
        }

        /// <summary>
        /// Tries to get value of <see cref="IntegerSequence"/> as <see cref="double"/> value.
        /// </summary>
        /// <param name="sequence">The <see cref="IntegerSequence"/>.</param>
        /// <param name="result">The result if return <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if can create <see cref="double"/> from value of <paramref name="sequence"/>; <c>false</c> otherwise.</returns>
        public static bool TryGetDouble(this IntegerSequence sequence, out double? result)
        {
            result = null;

            if (sequence.IsEmpty)
                return false;

            if (IsZero(sequence))
                result = DoubleHelper.Zero;
            else if (double.TryParse(sequence.Value, out var d))
                result = d;

            return result.HasValue;
        }

        /// <summary>
        /// Gets sequence value as double number, if convertible at least to double.
        /// </summary>
        /// <param name="sequence">The <see cref="IntegerSequence"/>.</param>
        /// <returns>A double number of sequence value or <c>null</c>, if represents empty sequence.</returns>
        public static double? ToNumber(this IntegerSequence sequence)
        {
            if (TryGetDouble(sequence, out var d))
            {
                return d;
            }

            return null;
        }
    }
}
