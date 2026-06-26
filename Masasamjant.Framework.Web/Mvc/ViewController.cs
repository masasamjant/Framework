using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents base class for MVC controllers that return views.
    /// </summary>
    public class ViewController : Controller
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ViewController"/> class.
        /// </summary>
        protected ViewController()
        { }

        /// <summary>
        /// Gets the <see cref="ISessionStorage"/> associate with controller.
        /// </summary>
        public ISessionStorage SessionStorage
        {
            get { return SessionStorageProvider.GetSessionStorage(); }
        }

        /// <summary>
        /// Gets the <see cref="ISessionStorageProvider"/> associate with controller.
        /// </summary>
        protected virtual ISessionStorageProvider SessionStorageProvider
        {
            get { return new HttpSessionStorageProvider(HttpContext); }
        }
    }
}
