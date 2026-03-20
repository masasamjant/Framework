using System.Text;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents component to write log messages to specified string builder.
    /// </summary>
    public class StringBuilderLogWriter : TextLogWriter
    {
        /// <summary>
        /// Initializes new instance of the <see cref="StringBuilderLogWriter"/> class that use <see cref="DefaultLogMessageFormatter"/> to format messages.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append log messages.</param>
        public StringBuilderLogWriter(StringBuilder builder)
            : base(new StringWriter(builder))
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="StringBuilderLogWriter"/> class that use specified <see cref="ILogMessageFormatter"/> to format messages.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append log messages.</param>
        /// <param name="formatter">The <see cref="ILogMessageFormatter"/> to format messages.</param>
        public StringBuilderLogWriter(StringBuilder builder, ILogMessageFormatter formatter)
            : base(new StringWriter(builder), formatter)
        { }
    }
}
