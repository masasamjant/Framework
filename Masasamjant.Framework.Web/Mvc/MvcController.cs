using Masasamjant.Web.Sessions;
using Masasamjant.Web.ViewModels.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents abstract Model-View-Controller controller.
    /// </summary>
    public abstract class MvcController : Controller
    {
        private INotificationManager? notificationManager;

        /// <summary>
        /// Gets the <see cref="ISessionStorage"/>.
        /// </summary>
        public virtual ISessionStorage SessionStorage
        {
            get { return SessionStorageProvider.GetSessionStorage(); }
        }

        /// <summary>
        /// Gets the <see cref="ISessionStorageProvider"/>.
        /// </summary>
        protected virtual ISessionStorageProvider SessionStorageProvider
        {
            get { return new HttpContextSessionStorageProvider(new ControllerHttpContextAccessor(this)); }
        }

        /// <summary>
        /// Gets the <see cref="INotificationManager"/>.
        /// </summary>
        protected virtual INotificationManager NotificationManager
        {
            get
            {
                if (notificationManager == null)
                    notificationManager = new NotificationManager();

                return notificationManager;
            }
        }
    }
}
