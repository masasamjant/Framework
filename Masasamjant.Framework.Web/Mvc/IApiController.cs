using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents API controller. This interface must be implemented to use <see cref="ApiControllerActionFilter"/> and <see cref="ApiControllerResultFilter"/>
    /// to notify controller about action and result execution.
    /// </summary>
    public interface IApiController
    {
        /// <summary>
        /// Invoked after action is executed.
        /// </summary>
        /// <param name="context">The context for the action execution.</param>
        void OnActionExecuted(ActionExecutedContext context);

        /// <summary>
        /// Invoked before action is executed.
        /// </summary>
        /// <param name="context">The context for the action execution.</param>
        void OnActionExecuting(ActionExecutingContext context);

        /// <summary>
        /// Invoked after result is executed.
        /// </summary>
        /// <param name="context">The context for the result execution.</param>
        void OnResultExecuted(ResultExecutedContext context);

        /// <summary>
        /// Invoked before result is executed.
        /// </summary>
        /// <param name="context">The context for the result execution.</param>
        void OnResultExecuting(ResultExecutingContext context);
    }
}
