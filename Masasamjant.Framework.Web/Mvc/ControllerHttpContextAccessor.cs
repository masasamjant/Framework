using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents <see cref="IHttpContextAccessor"/> that access HTTP context of specified <see cref="ControllerBase"/>.
    /// </summary>
    public sealed class ControllerHttpContextAccessor : IHttpContextAccessor
    {
        private readonly HttpContext context;

        /// <summary>
        /// Initializes new instance of the <see cref="ControllerHttpContextAccessor"/> class.
        /// </summary>
        /// <param name="controller">The <see cref="ControllerBase"/> of accessed HTTP context.</param>
        public ControllerHttpContextAccessor(ControllerBase controller)
        {
            context = controller.HttpContext;
        }

        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        /// <exception cref="NotSupportedException">If changing HTTP context is attempted.</exception>
        public HttpContext? HttpContext
        {
            get { return context; }
            set { throw new NotSupportedException("Controller HTTP context accessor does not support setting the context."); }
        }
    }
}
