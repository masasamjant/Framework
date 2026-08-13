using Masasamjant.Web.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Mvc.Attributes
{
    /// <summary>
    /// Represents <see cref="ActionFilterAttribute"/> to use with <see cref="IApiController"/>. 
    /// This can be used if <see cref="ApiControllerActionFilter"/> and <see cref="ApiControllerResultFilter"/> are not used. 
    /// Provides more selective use of <see cref="IApiController"/> methods, when action or result is executed.
    /// </summary>
    public sealed class ApiControllerActionAttribute : ActionFilterAttribute
    {
        private readonly ApiControllerActionInvokes invokeFlags;

        /// <summary>
        /// Initializes new default instance of the <see cref="ApiControllerActionInvokes"/> class
        /// that invokes all <see cref="IApiController"/> methods.
        /// </summary>
        public ApiControllerActionAttribute()
            : this(ApiControllerActionInvokes.ActionExecuting | ApiControllerActionInvokes.ActionExecuted | ApiControllerActionInvokes.ResultExecuting | ApiControllerActionInvokes.ResultExecuted)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ApiControllerActionAttribute"/> class.
        /// </summary>
        /// <param name="invokeFlags">The flags that determine which <see cref="IApiController"/> methods to invoke.</param>
        public ApiControllerActionAttribute(ApiControllerActionInvokes invokeFlags)
        {
            this.invokeFlags = invokeFlags;
        }

        /// <summary>
        /// Invoked after action is executed. If <see cref="ActionExecutedContext.Controller"/> is <see cref="IApiController"/> and 
        /// <see cref="ApiControllerActionInvokes.ActionExecuted"/> was set, then invokes <see cref="IApiController.OnActionExecuted(ActionExecutedContext)"/>.
        /// </summary>
        /// <param name="context">The context for the action executed.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (context.Controller is IApiController controller &&
                Invoke<ApiControllerActionFilter>(context, ApiControllerActionInvokes.ActionExecuted))
            {
                controller.OnActionExecuted(context);
            }
        }

        /// <summary>
        /// Invoked before action is executed. If <see cref="ActionExecutingContext.Controller"/> is <see cref="IApiController"/> and 
        /// <see cref="ApiControllerActionInvokes.ActionExecuting"/> was set, then invokes <see cref="IApiController.OnActionExecuting(ActionExecutingContext)"/>.
        /// </summary>
        /// <param name="context">The context for the action executing.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (context.Controller is IApiController controller &&
                Invoke<ApiControllerActionFilter>(context, ApiControllerActionInvokes.ActionExecuting))
            {
                controller.OnActionExecuting(context);
            }
        }

        /// <summary>
        /// Invoked after result is executed. If <see cref="ResultExecutedContext.Controller"/> is <see cref="IApiController"/> and 
        /// <see cref="ApiControllerActionInvokes.ResultExecuted"/> was set, then invokes <see cref="IApiController.OnResultExecuted(ResultExecutedContext)"/>.
        /// </summary>
        /// <param name="context">The context for the result executed.</param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (context.Controller is IApiController controller &&
                Invoke<ApiControllerResultFilter>(context, ApiControllerActionInvokes.ResultExecuted))
            {
                controller.OnResultExecuted(context);
            }
        }

        /// <summary>
        /// Invoked before result is executed. If <see cref="ResultExecutingContext.Controller"/> is <see cref="IApiController"/> and 
        /// <see cref="ApiControllerActionInvokes.ResultExecuting"/> was set, then invokes <see cref="IApiController.OnResultExecuting(ResultExecutingContext)"/>.
        /// </summary>
        /// <param name="context">The context for the result executing.</param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (context.Controller is IApiController controller &&
                Invoke<ApiControllerResultFilter>(context, ApiControllerActionInvokes.ResultExecuting))
            {
                controller.OnResultExecuting(context);
            }
        }

        private bool Invoke<TFilter>(FilterContext context, ApiControllerActionInvokes flag) where TFilter : IFilterMetadata
        {
            // The filter has been applied to the action, so do not invoke thru attribute.
            if (context.ContainsFilter<TFilter>())
                return false;

            if (invokeFlags.Equals(ApiControllerActionInvokes.None))
                return false;

            return invokeFlags.HasFlag(flag);
        }
    }
}
