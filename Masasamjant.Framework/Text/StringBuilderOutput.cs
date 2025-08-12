using System.Text;

namespace Masasamjant.Text
{
    /// <summary>
    /// Represents <see cref="IOutput"/> that writes text to specified <see cref="StringBuilder"/>.
    /// </summary>
    public sealed class StringBuilderOutput : IOutput
    {
        private readonly StringBuilder builder;

        /// <summary>
        /// Initializes new instance of the <see cref="StringBuilderOutput"/> class.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to write.</param>
        public StringBuilderOutput(StringBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Writes specified value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(string value)
        {
            builder.Append(value);
        }

        /// <summary>
        /// Writes specified value as line.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLine(string value)
        {
            builder.AppendLine(value);
        }

        /// <summary>
        /// Asyncronously writes specified value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>A task representing asyncronous write.</returns>
        /// <exception cref="NotSupportedException">If asyncronous write is not supported.</exception>
        public Task WriteAsync(string value)
        {
            Write(value);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Asyncronously writes specified value as line.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>A task representing asyncronous write.</returns>
        /// <exception cref="NotSupportedException">If asyncronous write is not supported.</exception>
        public Task WriteLineAsync(string value)
        {
            WriteLine(value);
            return Task.CompletedTask;
        }
    }
}
