namespace Masasamjant.Web.ViewModels.Notifications
{
    /// <summary>
    /// Represents manager of notifications.
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// Add notification to specified key. If several notifications are added to same key, then
        /// list of notifications is created for that key.
        /// </summary>
        /// <param name="notification">The <see cref="INotification"/> to add.</param>
        /// <param name="key">The key, like session or user identifier, to add notification.</param>
        void AddNotification(INotification notification, string key);

        /// <summary>
        /// Gets notifications added for specified key.
        /// </summary>
        /// <param name="key">The key, like session or user identifier, where notifications was added.</param>
        /// <param name="maxCount">The max count of notifications to get or <c>null</c> to get all.</param>
        /// <returns>A notifications added to specified key.</returns>
        IEnumerable<INotification> GetNotifications(string key, int? maxCount = null);

        /// <summary>
        /// Remove notifications added for specified key.
        /// </summary>
        /// <param name="key">The key, like session or user identifier, where notifications was added.</param>
        void RemoveNotifications(string key);
    }
}
