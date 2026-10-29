namespace Masasamjant.Plugins
{
    /// <summary>
    /// Represents plugin response.
    /// </summary>
    public class PluginResponse : IPluginResponse
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PluginResponse"/> class.
        /// </summary>
        /// <param name="result">The result of the plugin execution.</param>
        /// <param name="data">The response data.</param>
        public PluginResponse(PluginResult result, string? data)
        { 
            Result = result;
            Data = data;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="PluginResponse"/> class.
        /// </summary>
        /// <param name="exception">The exception that caused the plugin to fail.</param>
        public PluginResponse(Exception exception)
        {
            Result = PluginResult.Failed;
            Error = exception.Message;
        }

        /// <summary>
        /// Gets the result of the plugin execution.
        /// </summary>
        public PluginResult Result { get; internal set; } = PluginResult.Ignored;

        /// <summary>
        /// Gets the error message, if available.
        /// </summary>
        public string? Error { get; internal set; }

        /// <summary>
        /// Gets the response data.
        /// </summary>
        /// <remarks>Depending on implementation this can be simple value like "1", delimited string or more complex XML or JSON string.</remarks>
        public string? Data { get; internal set; }
    }
}
