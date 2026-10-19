namespace Masasamjant
{
    /// <summary>
    /// Enumerates contiguous ranges within a ReadOnlySpan<char> that are delimited by a specified separator.
    /// </summary>
    /// <remarks>
    /// This is a ref struct (stack-only) and must not be boxed or captured. Use with foreach to
    /// iterate System.Range values that represent each segment's start (inclusive) and end (exclusive) indices.
    /// </remarks>
    public ref struct CharSpanRangeEnumerator
    {
        private int index;

        /// <summary>
        /// Initializes new <see cref="CharSpanRangeEnumerator"/> with specified characters and separator. 
        /// The enumerator is positioned before the first segment, and <see cref="Current"/> is undefined until <see cref="MoveNext"/> is called.
        /// </summary>
        /// <param name="characters">The span of characters to enumerate.</param>
        /// <param name="separator">The character used to delimit segments.</param>
        public CharSpanRangeEnumerator(ReadOnlySpan<char> characters, char separator)
        {
            Characters = characters;
            Separator = separator;
            index = 0;
            Current = default;
        }

        /// <summary>
        /// Gets the separator character.
        /// </summary>
        internal char Separator { get; }

        /// <summary>
        /// Gets the enumerated read-only span of characters.
        /// </summary>
        internal ReadOnlySpan<char> Characters { get; }

        /// <summary>
        /// Gets the current segment's index range within characters.
        /// </summary>
        public System.Range Current { get; private set; }

        /// <summary>
        /// Return an enumerator that iterates the character span range.
        /// </summary>
        /// <returns>A current <see cref="CharSpanRangeEnumerator"/> that enumerates the elements of the range.</returns>
        public readonly CharSpanRangeEnumerator GetEnumerator() => this;

        /// <summary>
        /// Advances the enumerator to the next segment delimited by the separator and sets <see cref="Current"/> to that segment's index range.
        /// </summary>
        /// <remarks><see cref="Current"/> is set to a System.Range containing the start (inclusive) and end (exclusive)
        /// indexes of the next segment within characters. If no separator is found, <see cref="Current"/> covers the remainder of
        /// characters and the internal index is advanced beyond characters length to terminate enumeration. Consecutive
        /// separators yield empty segments.</remarks>
        /// <returns><c>true</c> if the enumerator was successfully advanced to the next segment; otherwise, <c>false</c>.</returns>
        public bool MoveNext()
        {
            if (index > Characters.Length)
                return false;

            var slice = Characters[index..];
            var next = slice.IndexOf(Separator);

            if (next == -1)
            {
                Current = new System.Range(index, Characters.Length);
                index = Characters.Length + 1;
            }
            else
            { 
                Current = new System.Range(index, index + next);
                index += next + 1;
            }

            return true;
        }
    }
}
