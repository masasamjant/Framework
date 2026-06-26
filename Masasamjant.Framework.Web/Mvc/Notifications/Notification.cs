using Masasamjant.Serialization;
using System.Text.Json.Serialization;

namespace Masasamjant.Web.Mvc.Notifications
{
    /// <summary>
    /// Represents notification displayed in view.
    /// </summary>
    public class Notification : ViewModel, IJsonSerializable
    {
        /// <summary>
        /// Minium value of <see cref="HideTimeoutSeconds"/> is 0 seconds.
        /// </summary>
        public const int MinHideTimeoutSeconds = 0;

        /// <summary>
        /// Maximum value of <see cref="HideTimeoutSeconds"/> is 300 seconds (5 minutes).
        /// </summary>
        public const int MaxHideTimeoutSeconds = 300;

        /// <summary>
        /// Initializes new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="notificationType">The notification type.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="hideTimeoutSeconds">The time, in seconds, after which the notification should be hidden.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="notificationType"/> is not defined.</exception>
        public Notification(NotificationType notificationType, string? title, string? message, int hideTimeoutSeconds)
            : this(notificationType, title, message, false, hideTimeoutSeconds)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="notificationType">The notification type.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="showCloseButton"><c>true</c> if should show close button; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="notificationType"/> is not defined.</exception>
        public Notification(NotificationType notificationType, string? title, string? message, bool showCloseButton)
            : this(notificationType, title, message, showCloseButton, MaxHideTimeoutSeconds)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="notificationType">The notification type.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="showCloseButton"><c>true</c> if should show close button; <c>false</c> otherwise.</param>
        /// <param name="hideTimeoutSeconds">The time, in seconds, after which the notification should be hidden.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="notificationType"/> is not defined.</exception>
        public Notification(NotificationType notificationType, string? title, string? message, bool showCloseButton, int hideTimeoutSeconds)
        {
            NotificationType = Enum.IsDefined(notificationType) ? notificationType : throw new ArgumentException("Notification type not defined.", nameof(notificationType));
            Title = string.IsNullOrWhiteSpace(title) ? string.Empty : title.Trim();
            Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message.Trim();
            HideTimeoutSeconds = Math.Min(Math.Max(MinHideTimeoutSeconds, hideTimeoutSeconds), MaxHideTimeoutSeconds);
            ShowCloseButton = showCloseButton || HideTimeoutSeconds == MinHideTimeoutSeconds;
        }

        /// <summary>
        /// Gets the notification title.
        /// </summary>
        [JsonInclude]
        public string Title { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the notification message.
        /// </summary>
        [JsonInclude]
        public string Message { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        [JsonInclude]
        public NotificationType NotificationType { get; internal set; }

        /// <summary>
        /// Gets if or not close button should be visible. <c>true</c> if explicitly set or if <see cref="HideTimeoutSeconds"/>
        /// is equal to <see cref="MinHideTimeoutSeconds"/> and <c>false</c> otherwise.
        /// </summary>
        [JsonInclude]
        public bool ShowCloseButton { get; internal set; }

        /// <summary>
        /// Gets the time, in seconds, after the notification should be hidden. If value is 0, then should not be hidden if user 
        /// closes manually or <see cref="MaxHideTimeoutSeconds"/> is reached.
        /// </summary>
        [JsonInclude]
        public int HideTimeoutSeconds { get; internal set; }

        /// <summary>
        /// Gets if or not notification should be visible. <c>true</c> if notification has title or message; <c>false</c> otherwise.
        /// </summary>
        [JsonIgnore]
        public bool IsVisible
        {
            get { return !string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Message); }
        }
    }
}
