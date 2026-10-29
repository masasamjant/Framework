namespace Masasamjant.Plugins
{
    /// <summary>
    /// Provides default implementation of <see cref="IPlugin"/> interface. 
    /// This class can be used as a base class for custom plugin implementations.
    /// </summary>
    public class Plugin : IPlugin, IDisposable
    {
        private long disposed = 0L;

        /// <summary>
        /// Gets the plugin name. Default name is "Plugin". 
        /// Override this property to provide a custom name for the plugin.
        /// </summary>
        public virtual string Name 
        {
            get { return "Plugin"; }
        }

        /// <summary>
        /// Gets the description of the plugin. Default description is "Base class for IPlugin implementations.". 
        /// Override this property to provide a custom description for the plugin.
        /// </summary>
        public virtual string Description 
        { 
            get { return "Base class for IPlugin implementations."; }
        }

        /// <summary>
        /// Gets if plugin is disposed.
        /// </summary>
        protected bool IsDisposed
        {
            get { return Interlocked.Read(ref disposed) != 0L; }
        }

        /// <summary>
        /// Disposes plugin and releases the resources it holds.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Executes plugin with specified request. Default implementation returns <see cref="PluginResult.Ignored"/> always.
        /// </summary>
        /// <param name="request">The plugin request.</param>
        /// <returns>A plugin response.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="request"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">If the plugin is disposed.</exception>
        public virtual IPluginResponse Execute(IPluginRequest request)
        {
            if (IsDisposed)
                return new PluginResponse(new ObjectDisposedException(GetType().FullName));

            if (request == null)
                return new PluginResponse(new ArgumentNullException(nameof(request)));

            return new PluginResponse(PluginResult.Ignored, null);
        }

        /// <summary>
        /// Disposes plugin and releases the resource it holds.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            Interlocked.CompareExchange(ref disposed, 1L, 0L);
        }
    }
}
