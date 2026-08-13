using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents <see cref="IHttpContextAccessor"/> that provides HTTP context of <see cref="ControllerBase"/>.
    /// </summary>
    public sealed class ControllerHttpContextAccessor : IHttpContextAccessor
    {
        private readonly ControllerBase controller;

        /// <summary>
        /// Initializes new instance of the <see cref="ControllerHttpContextAccessor"/> class.
        /// </summary>
        /// <param name="controller">The controller from which to access the HTTP context.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="controller"/> is <c>null</c>.</exception>
        public ControllerHttpContextAccessor(ControllerBase controller)
        {
            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        /// <summary>
        /// Gets the HTTP context from the associated controller.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown when attempting to set the HTTP context.</exception>
        public HttpContext? HttpContext
        {
            get => controller.HttpContext;
            set => throw new NotSupportedException("Setting HTTP context is not supported.");
        }
    }
}
