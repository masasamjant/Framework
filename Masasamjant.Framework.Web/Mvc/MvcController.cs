using Masasamjant.Web.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents abstract Model-View-Controller controller.
    /// </summary>
    public abstract class MvcController : Controller
    {
        public virtual ISessionStorage SessionStorage
        {
            get { return SessionStorageProvider.GetSessionStorage(); }
        }

        protected virtual ISessionStorageProvider SessionStorageProvider
        {
            get { return new HttpContextSessionStorageProvider(new ControllerHttpContextAccessor(this)); }
        }
    }
}
