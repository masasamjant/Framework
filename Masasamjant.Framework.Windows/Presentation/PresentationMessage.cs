namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents message presented in view.
    /// </summary>
    public sealed class PresentationMessage
    {
        /// <summary>
        /// Gets the message text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the message title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        public PresentationMessageType MessageType { get; }
    
        /// <summary>
        /// Creates information <see cref="PresentationMessage"/>.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <param name="title">The message title.</param>
        /// <returns>A new information <see cref="PresentationMessage"/> instance.</returns>
        public static PresentationMessage Information(string text, string title = "")
        {
            return new PresentationMessage(text, title, PresentationMessageType.Information);
        }

        /// <summary>
        /// Creates question <see cref="PresentationMessage"/>.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <param name="title">The message title.</param>
        /// <returns>A new question <see cref="PresentationMessage"/> instance.</returns>
        public static PresentationMessage Question(string text, string title = "")
        {
            return new PresentationMessage(text, title, PresentationMessageType.Question);
        }

        /// <summary>
        /// Creates warning <see cref="PresentationMessage"/>.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <param name="title">The message title.</param>
        /// <returns>A new warning <see cref="PresentationMessage"/> instance.</returns>
        public static PresentationMessage Warning(string text, string title = "")
        {
            return new PresentationMessage(text, title, PresentationMessageType.Warning);
        }

        /// <summary>
        /// Creates error <see cref="PresentationMessage"/>.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <param name="title">The message title.</param>
        /// <returns>A new error <see cref="PresentationMessage"/> instance.</returns>
        public static PresentationMessage Error(string text, string title = "")
        {
            return new PresentationMessage(text, title, PresentationMessageType.Error);
        }

        private PresentationMessage(string text, string title, PresentationMessageType messageType)
        {
            Text = text ?? string.Empty;
            Title = title ?? string.Empty;
            MessageType = messageType;
        }
    }
}
