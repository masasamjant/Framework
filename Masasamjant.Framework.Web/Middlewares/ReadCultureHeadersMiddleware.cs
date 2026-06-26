using System.Globalization;

namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents middleware to read culture information from HTTP headers and set current culture and current UI culture.
    /// </summary>
    public sealed class ReadCultureHeadersMiddleware : Middleware
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ReadCultureHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> to process HTTP request in the pipeline.</param>
        /// <param name="currentCultureHttpHeader">The name of HTTP header that contains the current culture information.</param>
        /// <param name="currentUICultureHttpHeader">The name of HTTP header that contains the current UI culture information.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="next"/> is <c>null</c>.</exception>
        /// <remarks>If <paramref name="currentCultureHttpHeader"/> or <paramref name="currentUICultureHttpHeader"/> is <c>null</c>, empty or only whitespace, then header is not read.</remarks>
        public ReadCultureHeadersMiddleware(RequestDelegate next, string? currentCultureHttpHeader = null, string? currentUICultureHttpHeader = null) 
            : base(next)
        {
            CurrentCultureHttpHeader = currentCultureHttpHeader;
            CurrentUICultureHttpHeader = currentUICultureHttpHeader;
        }

        /// <summary>
        /// Gets the name of HTTP header that contains the current culture information. 
        /// If not specified, it will not read the culture information from HTTP headers.
        /// </summary>
        public string? CurrentCultureHttpHeader { get; }

        /// <summary>
        /// Gets the name of HTTP header that contains the current UI culture information. 
        /// If not specified, it will not read the UI culture information from HTTP headers.
        /// </summary>
        public string? CurrentUICultureHttpHeader { get; }

        /// <summary>
        /// Invoked when middleware is executed. 
        /// Read culture information from HTTP headers and set current culture and current UI culture.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the completion of request processing.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            ReadCurrentCulture(context);
            ReadCurrentUICulture(context);
            await Next(context);
        }

        private void ReadCurrentCulture(HttpContext context)
        {
            if (TryReadCultureHeader(context, CurrentCultureHttpHeader, out var cultureName)) 
            {
                if (!string.IsNullOrWhiteSpace(cultureName))
                {
                    if (CultureHelper.IsAvailableCulture(cultureName) && CultureInfo.CurrentCulture.Name != cultureName)
                        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
                }
            }
        }

        private void ReadCurrentUICulture(HttpContext context)
        {
            if (TryReadCultureHeader(context, CurrentUICultureHttpHeader, out var cultureName))
            {
                if (!string.IsNullOrWhiteSpace(cultureName))
                {
                    if (CultureHelper.IsAvailableCulture(cultureName) && CultureInfo.CurrentUICulture.Name != cultureName)
                        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
                }
            }
        }

        private static bool TryReadCultureHeader(HttpContext context,string? headerName, out string? cultureName)
        {
            if (!string.IsNullOrWhiteSpace(headerName) &&
                context.TryGetRequestHeaderValue(headerName, out var values) && values.Count > 0)
            {
                cultureName = values.First();
                return true;
            }

            cultureName = null;
            return false;
        }
    }
}
