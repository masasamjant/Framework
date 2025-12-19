using System.Text;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents component to write log messages to specified string builder.
    /// </summary>
    public class StringBuilderLogWriter : TextLogWriter
    {
        /// <summary>
        /// Initializes new instance of the <see cref="StringBuilderLogWriter"/> class.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append log messages.</param>
        public StringBuilderLogWriter(StringBuilder builder) 
            : base(new StringWriter(builder))
        { }
    }
}
