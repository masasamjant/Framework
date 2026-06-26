using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Mvc.Attributes
{
    /// <summary>
    /// Defines what <see cref="IApiController"/> methods <see cref="ApiControllerActionAttribute"/> should invoke.
    /// </summary>
    [Flags]
    public enum ApiControllerActionInvokes : int
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// <see cref="IApiController.OnActionExecuting(ActionExecutingContext)"/>
        /// </summary>
        ActionExecuting = 1,

        /// <summary>
        /// <see cref="IApiController.OnActionExecuted(ActionExecutedContext)"/>
        /// </summary>
        ActionExecuted = 2,

        /// <summary>
        /// <see cref="IApiController.OnResultExecuting(ResultExecutingContext)"/>
        /// </summary>
        ResultExecuting = 4,

        /// <summary>
        /// <see cref="IApiController.OnResultExecuted(ResultExecutedContext)"/>
        /// </summary>
        ResultExecuted = 8
    }
}
