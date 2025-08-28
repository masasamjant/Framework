using System.Collections.Concurrent;

namespace Masasamjant.Web.ViewModels.Notifications
{
    /// <summary>
    /// Represents <see cref="INotificationManager"/> that keeps notifications in memory.
    /// </summary>
    /// <remarks>Instance of this class is thread-safe and can be used as singleton instance.</remarks>
    public sealed class NotificationManager : INotificationManager
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<INotification>> notifications;

        /// <summary>
        /// Initializes new instance of the <see cref="NotificationManager"/> class.
        /// </summary>
        public NotificationManager()
        {
            notifications = new ConcurrentDictionary<string, ConcurrentQueue<INotification>>();
        }

        /// <summary>
        /// Add notification to specified key. If several notifications are added to same key, then
        /// list of notifications is created for that key.
        /// </summary>
        /// <param name="notification">The <see cref="INotification"/> to add.</param>
        /// <param name="key">The key, like session or user identifier, to add notification.</param>
        public void AddNotification(INotification notification, string key)
        {
            var queue = notifications.GetOrAdd(key, new ConcurrentQueue<INotification>());
            queue.Enqueue(notification);
        }

        /// <summary>
        /// Gets notifications added for specified key.
        /// </summary>
        /// <param name="key">The key, like session or user identifier, where notifications was added.</param>
        /// <param name="maxCount">The max count of notifications to get or <c>null</c> to get all.</param>
        /// <returns>A notifications added to specified key.</returns>
        public IEnumerable<INotification> GetNotifications(string key, int? maxCount = null)
        {
            var result = new List<INotification>();

            if (notifications.TryGetValue(key, out var queue))
            { 
                while ((maxCount == null || result.Count < maxCount.Value) && queue.TryDequeue(out var notification))
                    result.Add(notification);
            }

            return result;
        }

        /// <summary>
        /// Remove notifications added for specified key.
        /// </summary>
        /// <param name="key">The key, like session or user identifier, where notifications was added.</param>
        public void RemoveNotifications(string key)
        {
            if (notifications.TryGetValue(key, out var queue))
                queue.Clear();
        }
    }
}
