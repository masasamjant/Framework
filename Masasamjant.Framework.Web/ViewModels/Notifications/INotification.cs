namespace Masasamjant.Web.ViewModels.Notifications
{
    /// <summary>
    /// Represents notification displaye on web page.
    /// </summary>
    public interface INotification : ISupportCssClass
    {
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        Guid Identifier { get; }

        /// <summary>
        /// Gets the title text or empty string, if no title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the notification message.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the type of notification.
        /// </summary>
        NotificationType NotificationType { get; }

        /// <summary>
        /// Gets timeout in milliseconds when notification should be hide.
        /// </summary>
        int HideTimeout { get; }

        /// <summary>
        /// Gets whether or not close button is visible in notification.
        /// </summary>
        bool ShowCloseButton { get; }
    }
}
