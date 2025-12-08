using System.Text;

namespace Masasamjant.Text
{
    /// <summary>
    /// Arguments for event occurring before <see cref="StringBuilder"/> is cleared.
    /// </summary>
    public sealed class StringBuilderClearingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="StringBuilderClearingEventArgs"/> class.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to be cleared.</param>
        public StringBuilderClearingEventArgs(StringBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// Gets the <see cref="StringBuilder"/> to be cleared.
        /// </summary>
        public StringBuilder Builder { get; }
    }
}
