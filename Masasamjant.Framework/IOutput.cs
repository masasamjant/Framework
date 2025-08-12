namespace Masasamjant
{
    /// <summary>
    /// Represents component that writes text to some output.
    /// </summary>
    public interface IOutput
    {
        /// <summary>
        /// Writes specified value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(string value);

        /// <summary>
        /// Writes specified value as line.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void WriteLine(string value);
        
        /// <summary>
        /// Asyncronously writes specified value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>A task representing asyncronous write.</returns>
        /// <exception cref="NotSupportedException">If asyncronous write is not supported.</exception>
        Task WriteAsync(string value);

        /// <summary>
        /// Asyncronously writes specified value as line.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>A task representing asyncronous write.</returns>
        /// <exception cref="NotSupportedException">If asyncronous write is not supported.</exception>
        Task WriteLineAsync(string value);
    }
}
