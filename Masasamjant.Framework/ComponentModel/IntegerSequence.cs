namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents sequence of integer values.
    /// </summary>
    public sealed class IntegerSequence : Sequence, IEquatable<IntegerSequence>
    {
        /// <summary>
        /// Gets <see cref="IntegerSequence"/> of an empty sequence.
        /// </summary>
        public static IntegerSequence Empty => new IntegerSequence(string.Empty, false);

        /// <summary>
        /// Creates new <see cref="IntegerSequence"/> that represents zero value.
        /// </summary>
        /// <param name="length">The length of the sequence value.</param>
        /// <returns>A <see cref="IntegerSequence"/> of zero.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="length"/> is less than 1.</exception>
        public static IntegerSequence Create(int length)
        {
            ValidateLength(length);
            var value = new string('0', length);
            return new IntegerSequence(value, false);
        }

        /// <summary>
        /// Creates new <see cref="IntegerSequence"/> with specified initial value.
        /// </summary>
        /// <param name="initial">The initial sequence value.</param>
        /// <param name="length">The length of the sequence value.</param>
        /// <returns>A <see cref="IntegerSequence"/> with specified initial value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="initial"/> is less than 0.
        /// -or-
        /// If value of <paramref name="length"/> is less than 1.
        /// </exception>
        public static IntegerSequence Create(int initial, int length)
        {
            ValidateLength(length);
            ValidateInitial(initial);

            var value = initial.ToString();

            if (value.Length < length)
                value = value.PadLeft(length, '0');

            bool isFull = value.All(c => c == '9');

            return new IntegerSequence(value, isFull);
        }

        /// <summary>
        /// Creates new <see cref="IntegerSequence"/> with specified initial value.
        /// </summary>
        /// <param name="initial">The initial sequence value.</param>
        /// <param name="length">The length of the sequence value.</param>
        /// <returns>A <see cref="IntegerSequence"/> with specified initial value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="initial"/> is less than 0.
        /// -or-
        /// If value of <paramref name="length"/> is less than 1.
        /// </exception>
        public static IntegerSequence Create(long initial, int length)
        {
            ValidateLength(length);
            ValidateInitial(initial);

            var value = initial.ToString();

            if (value.Length < length)
                value = value.PadLeft(length, '0');

            bool isFull = value.All(c => c == '9');

            return new IntegerSequence(value, isFull);
        }

        /// <summary>
        /// Creates new <see cref="IntegerSequence"/> with specified value.
        /// </summary>
        /// <param name="value">The integer value as string.</param>
        /// <returns>A <see cref="IntegerSequence"/> with specified value.</returns>
        /// <exception cref="ArgumentException">If any character of <paramref name="value"/> is not ASCII digit.</exception>
        public static IntegerSequence Create(string value)
        {
            bool isFull = true;

            foreach (var c in value)
            {
                if (!char.IsAsciiDigit(c))
                    throw new ArgumentException("Each character must be integer number from 0 to 9.", nameof(value));

                int i = int.Parse(c.ToString());

                if (i < 9)
                    isFull = false;
            }

            return new IntegerSequence(value, isFull);
        }

        /// <summary>
        /// Gets <see cref="IntegerSequence"/> that represents next value in sequence.
        /// </summary>
        /// <returns>A <see cref="IntegerSequence"/> that represents next value in sequence.</returns>
        /// <exception cref="InvalidOperationException">If this sequence is full.</exception>
        public IntegerSequence Next()
        {
            if (IsFull)
                throw new InvalidOperationException("The current sequence is full.");

            var array = ToArray(Value);

            for (int index = array.Length - 1; index >= 0; index--)
            {
                if (array[index] == 0)
                {
                    array[index]++;

                    if (index < (array.Length - 1))
                    {
                        for (int j = index + 1; j < array.Length; j++)
                            array[j] = 0;
                    }

                    break;
                }
                else if (array[index] < 9)
                {
                    array[index]++;

                    if (index < (array.Length - 1))
                        array[index + 1] = 0;

                    break;
                }
            }

            bool isFull = array.All(x => x == 9);
            var value = string.Concat(array);
            return new IntegerSequence(value, isFull);
        }

        /// <summary>
        /// Check if other <see cref="IntegerSequence"/> is equal with this. Sequences are equal 
        /// when they have same value.
        /// </summary>
        /// <param name="other">The other sequence.</param>
        /// <returns><c>true</c> if this and <paramref name="other"/> are equal; <c>false</c> otherwise.</returns>
        public bool Equals(IntegerSequence? other)
        {
            return other != null && string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Check if object instance is <see cref="IntegerSequence"/> and equal with this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="IntegerSequence"/> and equal with this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as IntegerSequence);
        }

        /// <summary>
        /// Gets hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Gets string presentation, <see cref="Value"/>.
        /// </summary>
        /// <returns>A value of the sequence.</returns>
        public override string ToString()
        {
            return Value;
        }

        private IntegerSequence(string value, bool isFull)
            : base()
        {
            Value = value;
            IsFull = isFull;
        }

        private static void ValidateLength(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "The length must be greater than or equal to 1.");
        }

        private static void ValidateInitial(long initial)
        {
            if (initial < 0)
                throw new ArgumentOutOfRangeException(nameof(initial), initial, "The initial value must be greater than or equal to 0.");
        }

        private static int[] ToArray(string value)
        {
            var values = value.ToStringArray();
            var array = new int[values.Length];
            for (int index = 0; index < array.Length; index++)
                array[index] = int.Parse(values[index]);
            return array;
        }
    }
}
