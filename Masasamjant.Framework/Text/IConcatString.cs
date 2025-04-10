namespace Masasamjant.Text
{
    /// <summary>
    /// Represents component that concatenates strings.
    /// </summary>
    public interface IConcatString
    {
        /// <summary>
        /// Gets if represents empty contact string.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Concatenates values into single string.
        /// </summary>
        /// <returns>A concated string.</returns>
        string Concat();

        /// <summary>
        /// Parse concat string from specified value.
        /// </summary>
        /// <param name="value">The value to parse concat string.</param>
        /// <exception cref="FormatException">If the format of <paramref name="value"/> is not correct.</exception>
        void ParseFrom(string value);
    }
}
