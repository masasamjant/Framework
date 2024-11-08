namespace Masasamjant.Http.Abstractions
{
    /// <summary>
    /// Represents listener of HTTP request execution.
    /// </summary>
    public interface IHttpClientListener
    {
        /// <summary>
        /// Invoked before specified HTTP request is executed.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task OnExecutingAsync(HttpRequest request);

        /// <summary>
        /// Invoked after specified HTTP request is executed.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task OnExecutedAsync(HttpRequest request);

        /// <summary>
        /// Invoked if exception occurs when executing specified HTTP request.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <param name="exception">The occurred exception.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task OnErrorAsync(HttpRequest request, Exception exception);
    }
}
