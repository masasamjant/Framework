namespace Masasamjant.Web.Mvc.Notifications
{
    /// <summary>
    /// Represents manager of notifications.
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// Add notification to specified key. If several notifications are added to same key, then they will be stored in the order they were added.
        /// </summary>
        /// <param name="notification">The notification to add.</param>
        /// <param name="key">The key to which the notification should be added.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> or <paramref name="notification"/> is <c>null</c>.</exception>
        void AddNotification(Notification notification, string key);

        /// <summary>
        /// Gets read-only collection of notifications added for specified key.
        /// </summary>
        /// <param name="key">The key for which to get notifications.</param>
        /// <param name="maxCount">The maximum number of notifications to return or <c>null</c> to return all notifications.</param>
        /// <returns>A read-only collection of notifications.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="maxCount"/> has value and that value is negative.</exception>
        IReadOnlyCollection<Notification> GetNotifications(string key, int? maxCount = null);

        /// <summary>
        /// Remove notifications added for specified key.
        /// </summary>
        /// <param name="key">The key for which to remove notifications.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is <c>null</c>.</exception>
        void RemoveNotifications(string key);
    }
}
