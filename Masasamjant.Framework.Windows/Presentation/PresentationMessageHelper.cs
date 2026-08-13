namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Provides helper methods for displaying presentation messages.
    /// </summary>
    public static class PresentationMessageHelper
    {
        /// <summary>
        /// Show message box based on the provided presentation message.
        /// </summary>
        /// <param name="message">The presentation message.</param>
        /// <param name="owner">The owner window or <c>null</c>.</param>
        /// <returns>A <see cref="DialogResult"/> of displaying message box.</returns>
        public static DialogResult ShowMessageBox(this PresentationMessage message, IWin32Window? owner = null)
        {
            if (owner != null)
                return MessageBox.Show(owner, message.Text, message.Title, GetMessageBoxButtons(message.MessageType), GetMessageBoxIcon(message.MessageType));
            else
                return MessageBox.Show(message.Text, message.Title, GetMessageBoxButtons(message.MessageType), GetMessageBoxIcon(message.MessageType));
        }

        private static MessageBoxButtons GetMessageBoxButtons(PresentationMessageType messageType)
        {
            return messageType switch
            {
                PresentationMessageType.Information => MessageBoxButtons.OK,
                PresentationMessageType.Question => MessageBoxButtons.YesNo,
                PresentationMessageType.Warning => MessageBoxButtons.RetryCancel,
                PresentationMessageType.Error => MessageBoxButtons.OK,
                _ => MessageBoxButtons.OK
            };
        }

        private static MessageBoxIcon GetMessageBoxIcon(PresentationMessageType messageType)
        {
            return messageType switch
            {
                PresentationMessageType.Information => MessageBoxIcon.Information,
                PresentationMessageType.Question => MessageBoxIcon.Question,
                PresentationMessageType.Warning => MessageBoxIcon.Warning,
                PresentationMessageType.Error => MessageBoxIcon.Error,
                _ => MessageBoxIcon.None
            };
        }
    }
}
