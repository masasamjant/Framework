namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents <see cref="Middleware"/> that transfers value between HTTP context stores.
    /// </summary>
    public sealed class ValueAccessorMiddleware : Middleware
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ValueAccessorMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> to process HTTP request in the pipeline.</param>
        /// <param name="getValueKey">The key of the value to get from the HTTP context.</param>
        /// <param name="setValueKey">The key of the value to set in the HTTP context.</param>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public ValueAccessorMiddleware(RequestDelegate next, string getValueKey, string setValueKey)
            : base(next)
        {
            GetValueKey = getValueKey ?? throw new ArgumentNullException(nameof(getValueKey));
            SetValueKey = setValueKey ?? throw new ArgumentNullException(nameof(setValueKey));
        }

        /// <summary>
        /// Gets the key of value to get.
        /// </summary>
        public string GetValueKey { get; }

        /// <summary>
        /// Gets the key of value to set.
        /// </summary>
        public string SetValueKey { get; }

        /// <summary>
        /// Invoked when middleware is executed. Gets value from specified <see cref="IHttpContextValueGetter"/> 
        /// and sets it using <see cref="IHttpContextValueSetter"/>.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="valueGetter">The <see cref="IHttpContextValueGetter"/> to get value from HTTP context.</param>
        /// <param name="valueSetter">The <see cref="IHttpContextValueSetter"/> to set value to HTTP context.</param>   
        /// <returns>A task that represents the completion of request processing.</returns>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public async Task InvokeAsync(HttpContext context, IHttpContextValueGetter valueGetter, IHttpContextValueSetter valueSetter)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(valueGetter);
            ArgumentNullException.ThrowIfNull(valueSetter);

            var value = valueGetter.GetHttpValue(context, GetValueKey);

            if (value != null)
                valueSetter.SetHttpValue(context, SetValueKey, value);

            await Next(context);
        }
    }
}
