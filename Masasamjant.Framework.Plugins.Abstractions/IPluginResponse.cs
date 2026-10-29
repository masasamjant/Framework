namespace Masasamjant.Plugins
{
    /// <summary>
    /// Represents plugin response.
    /// </summary>
    public interface IPluginResponse
    {
        /// <summary>
        /// Gets the result of the plugin execution.
        /// </summary>
        PluginResult Result { get; }
        
        /// <summary>
        /// Gets the error message, if available.
        /// </summary>
        string Error { get; }

        /// <summary>
        /// Gets the response data.
        /// </summary>
        /// <remarks>Depending on implementation this can be simple value like "1", delimited string or more complex XML or JSON string.</remarks>
        string Data { get; }
    }
}
