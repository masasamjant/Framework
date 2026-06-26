using System.Collections.Concurrent;

namespace Masasamjant.Web.Mvc.Notifications
{
    /// <summary>
    /// Represents <see cref="INotificationManager"/> that keeps notifications in memory.
    /// </summary>
    public sealed class NotificationManager : INotificationManager
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<Notification>> notifications;

        /// <summary>
        /// Initializes new instance of the <see cref="NotificationManager"/> class.
        /// </summary>
        public NotificationManager()
        {
            notifications = new ConcurrentDictionary<string, ConcurrentQueue<Notification>>();
        }

        /// <summary>
        /// Add notification to specified key. If several notifications are added to same key, then they will be stored in the order they were added.
        /// </summary>
        /// <param name="notification">The notification to add.</param>
        /// <param name="key">The key to which the notification should be added.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> or <paramref name="notification"/> is <c>null</c>.</exception>
        public void AddNotification(Notification notification, string key)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(notification);

            var queue = notifications.GetOrAdd(key, new ConcurrentQueue<Notification>());
            queue.Enqueue(notification);
        }

        /// <summary>
        /// Gets read-only collection of notifications added for specified key.
        /// </summary>
        /// <param name="key">The key for which to get notifications.</param>
        /// <param name="maxCount">The maximum number of notifications to return or <c>null</c> to return all notifications.</param>
        /// <returns>A read-only collection of notifications.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="maxCount"/> has value and that value is negative.</exception>
        public IReadOnlyCollection<Notification> GetNotifications(string key, int? maxCount = null)
        {
            if (maxCount.HasValue && maxCount.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(maxCount), maxCount, "Max count of notifications to get must be greater than or equal to 0.");

            var result = new List<Notification>();

            if (notifications.TryGetValue(key, out var queue))
            {
                while ((maxCount == null || result.Count < maxCount.Value) && queue.TryDequeue(out var notification))
                    result.Add(notification);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Remove notifications added for specified key.
        /// </summary>
        /// <param name="key">The key for which to remove notifications.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is <c>null</c>.</exception>
        public void RemoveNotifications(string key)
        {
            if (notifications.TryRemove(key, out var queue))
                queue.Clear();
        }
    }
}
