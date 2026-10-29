namespace Masasamjant.Plugins
{
    /// <summary>
    /// Represents plugin request.
    /// </summary>
    public interface IPluginRequest
    {
        /// <summary>
        /// Get the request data as string.
        /// </summary>
        /// <remarks>Depending on implementation this can be simple value like "1", delimited string or more complex XML or JSON string.</remarks>
        string Data { get; }
    }
}
