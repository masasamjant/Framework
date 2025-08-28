
namespace Masasamjant.Web.ViewModels.Notifications
{
    /// <summary>
    /// Represents view model that implements <see cref="INotification"/> interface.
    /// </summary>
    public class NotificationViewModel : ViewModel, INotification
    {
        /// <summary>
        /// Initializes new instance of the <see cref="NotificationViewModel"/> class.
        /// </summary>
        /// <param name="message">The notification message.</param>
        /// <param name="title">The title text or empty string, if no title.</param>
        /// <param name="notificationType">The notification type. Default is <see cref="NotificationType.Information"/>.</param>
        /// <param name="showCloseButton"><c>true</c> if close button is visible; <c>false</c> otherwise.</param>
        /// <param name="hideTimeout">The timeout in milliseconds when notification should be hide.</param>
        public NotificationViewModel(string message, string? title = null, NotificationType notificationType = NotificationType.Information, bool showCloseButton = false, int hideTimeout = 0)
        {
            Identifier = Guid.NewGuid();
            Message = message;
            Title = title ?? string.Empty;
            NotificationType = Enum.IsDefined(notificationType) ? notificationType : NotificationType.Information;
            ShowCloseButton = showCloseButton || hideTimeout <= 0;
            HideTimeout = hideTimeout;
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Identifier { get; }

        /// <summary>
        /// Gets the title text or empty string, if no title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the notification message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the type of notification.
        /// </summary>
        public NotificationType NotificationType { get; }

        /// <summary>
        /// Gets timeout in milliseconds when notification should be hide.
        /// </summary>
        public int HideTimeout { get; }

        /// <summary>
        /// Gets whether or not close button is visible in notification.
        /// </summary>
        public bool ShowCloseButton { get; }

        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to element.
        /// </summary>
        public string CssClass { get; set; } = string.Empty;
    }
}
