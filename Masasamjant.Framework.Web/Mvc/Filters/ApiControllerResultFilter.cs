using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Mvc.Filters
{
    /// <summary>
    /// Represents <see cref="IResultFilter"/> that will work with <see cref="IApiController"/> interface 
    /// by notifying controller about result execution.
    /// </summary>
    public sealed class ApiControllerResultFilter : IResultFilter
    {
        /// <summary>
        /// Invoked after result is executed. If <see cref="ResultExecutedContext.Controller"/> is <see cref="IApiController"/>,
        /// the controller's <see cref="IApiController.OnResultExecuted"/> method is called.
        /// </summary>
        /// <param name="context">The context for the result execution.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnResultExecuted(context);
        }

        /// <summary>
        /// Invoked before result is executed. If <see cref="ResultExecutingContext.Controller"/> is <see cref="IApiController"/>,
        /// the controller's <see cref="IApiController.OnResultExecuting"/> method is called.
        /// </summary>
        /// <param name="context">The context for the result execution.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnResultExecuting(context);
        }
    }
}
