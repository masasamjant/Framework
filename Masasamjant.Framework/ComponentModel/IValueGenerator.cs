namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents a generator that can produce values of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of value to generate.</typeparam>
    public interface IValueGenerator<T>
    {
        /// <summary>
        /// Generates a single value of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new <typeparamref name="T"/> value.</returns>
        T GenerateValue();

        /// <summary>
        /// Generate multiple new values of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="count">The count of values to generate.</param>
        /// <returns>A generated <typeparamref name="T"/> values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="count"/> is less than 0.</exception>
        IEnumerable<T> GenerateValues(int count);

        /// <summary>
        /// Generate multiple new values of <typeparamref name="T"/> and adds them to the provided list.
        /// </summary>
        /// <param name="values">The list to add generated values.</param>
        /// <param name="count">The count of values to generate.</param>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="count"/> is less than 0.</exception>
        void GenerateValues(List<T> values, int count);
    }
}
