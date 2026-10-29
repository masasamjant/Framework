namespace Masasamjant.Plugins
{
    /// <summary>
    /// Defines result of plugin execution.
    /// </summary>
    public enum PluginResult : int
    {
        /// <summary>
        /// Plugin execution succeeded.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Plugin execution was ignored.
        /// </summary>
        Ignored = 1,

        /// <summary>
        /// Plugin execution failed.
        /// </summary>
        Failed = 2
    }
}
