namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents abstract generator of values of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of value to generate.</typeparam>
    public abstract class ValueGenerator<T> : IValueGenerator<T>
    {
        /// <summary>
        /// Generates a single value of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new <typeparamref name="T"/> value.</returns>
        public abstract T GenerateValue();

        /// <summary>
        /// Generate multiple new values of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="count">The count of values to generate.</param>
        /// <returns>A generated <typeparamref name="T"/> values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="count"/> is less than 0.</exception>
        public IEnumerable<T> GenerateValues(int count)
        {
            ValidateCount(count);
                
            IEnumerable<T> Generate()
            {
                if (count == 0)
                {
                    yield break;
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        yield return GenerateValue();
                    }
                }
            }

            return Generate();
        }

        /// <summary>
        /// Generate multiple new values of <typeparamref name="T"/> and adds them to the provided list.
        /// </summary>
        /// <param name="values">The list to add generated values.</param>
        /// <param name="count">The count of values to generate.</param>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="count"/> is less than 0.</exception>
        public void GenerateValues(List<T> values, int count)
        {
            foreach (var value in GenerateValues(count))
            {
                values.Add(value);
            }
        }

        private static void ValidateCount(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "The value must be non-negative.");
            }
        }
    }
}
