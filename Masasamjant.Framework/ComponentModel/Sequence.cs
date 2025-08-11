namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents abstract sequence of values.
    /// </summary>
    public abstract class Sequence
    {
        /// <summary>
        /// Initializes new instance of <see cref="Sequence"/> class with empty value.
        /// </summary>
        protected Sequence()
        {
            Value = string.Empty;
        }

        /// <summary>
        /// Gets whether or not this represents empty sequence.
        /// </summary>
        public virtual bool IsEmpty
        {
            get { return string.IsNullOrWhiteSpace(Value); }
        }

        /// <summary>
        /// Gets whether or not sequence is full. 
        /// </summary>
        /// <remarks><c>true</c> means that sequence has reached its maximum value.</remarks>
        public bool IsFull { get; protected set; }

        /// <summary>
        /// Gets the value of the sequence as string.
        /// </summary>
        public string Value { get; protected set; }
    }
}
