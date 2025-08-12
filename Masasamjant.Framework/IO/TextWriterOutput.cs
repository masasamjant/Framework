namespace Masasamjant.IO
{
    /// <summary>
    /// Represents <see cref="IOutput"/> that writes text to specified <see cref="TextWriter"/>.
    /// </summary>
    public sealed class TextWriterOutput : IOutput
    {
        private readonly TextWriter writer;

        /// <summary>
        /// Initializes new instance of the <see cref="TextWriterOutput"/> class.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to write.</param>
        public TextWriterOutput(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Writes specified value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(string value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Asyncronously writes specified value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>A task representing asyncronous write.</returns>
        /// <exception cref="NotSupportedException">If asyncronous write is not supported.</exception>
        public Task WriteAsync(string value)
        {
            return writer.WriteAsync(value);
        }

        /// <summary>
        /// Writes specified value as line.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLine(string value)
        {
            writer.WriteLine(value);
        }

        /// <summary>
        /// Asyncronously writes specified value as line.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>A task representing asyncronous write.</returns>
        /// <exception cref="NotSupportedException">If asyncronous write is not supported.</exception>
        public Task WriteLineAsync(string value)
        {
            return writer.WriteLineAsync(value);
        }
    }
}
