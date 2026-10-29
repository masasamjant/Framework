namespace Masasamjant.Plugins
{
    /// <summary>
    /// Represents plugin request.
    /// </summary>
    public class PluginRequest : IPluginRequest
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PluginRequest"/> class.
        /// </summary>
        public PluginRequest()
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="PluginRequest"/> class.
        /// </summary>
        /// <param name="data">The request data as string.</param>
        public PluginRequest(string? data)
        {
            Data = data;
        }

        /// <summary>
        /// Get the request data as string.
        /// </summary>
        /// <remarks>Depending on implementation this can be simple value like "1", delimited string or more complex XML or JSON string.</remarks>
        public string? Data { get; internal set; }
    }
}
