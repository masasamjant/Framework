namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents abstract middleware. 
    /// </summary>
    public abstract class Middleware
    {
        /// <summary>
        /// Intializes new instance of the <see cref="Middleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> to process HTTP request in the pipeline.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="next"/> is <c>null</c>.</exception>
        protected Middleware(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException("next");
        }

        /// <summary>
        /// Gets the <see cref="RequestDelegate"/> to process HTTP request.
        /// </summary>
        protected RequestDelegate Next { get; }
    }
}
