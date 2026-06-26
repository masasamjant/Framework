using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Mvc.Filters
{
    /// <summary>
    /// Represents <see cref="IActionFilter"/> that will work with <see cref="IApiController"/> interface 
    /// by notifying controller about action execution.
    /// </summary>
    public sealed class ApiControllerActionFilter : IActionFilter
    {
        /// <summary>
        /// Invoked after action is executed. If <see cref="ActionExecutedContext.Controller"/> is <see cref="IApiController"/>,
        /// the controller's <see cref="IApiController.OnActionExecuted"/> method is called.
        /// </summary>
        /// <param name="context">The context for the action execution.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnActionExecuted(context);
        }
        
        /// <summary>
        /// Invoked before action is executed. If <see cref="ActionExecutingContext.Controller"/> is <see cref="IApiController"/>,
        /// the controller's <see cref="IApiController.OnActionExecuting"/> method is called.
        /// </summary>
        /// <param name="context">The context for the action execution.</param> 
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnActionExecuting(context);
        }
    }
}
