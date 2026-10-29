namespace Masasamjant.Plugins.Runtime
{
    /// <summary>
    /// Represents a runtime-loaded plug-in that wraps an <see cref="IPlugin"/> instance and delegates execution requests.
    /// </summary>
    internal sealed class RuntimePlugin : IRuntimePlugin
    {
        private PluginLoadContext? context;
        private IPlugin? plugin;
        private bool disposed;

        /// <summary>
        /// Initializes new instance of the <see cref="RuntimePlugin"/> class.
        /// </summary>
        /// <param name="context">The plugin load context.</param>
        /// <param name="plugin">The plugin instance.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="plugin"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="plugin"/> is a <see cref="RuntimePlugin"/> instance.</exception>
        internal RuntimePlugin(PluginLoadContext context, IPlugin plugin)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(plugin);

            this.context = context;
            this.plugin = plugin;
            this.disposed = false;
        }

        /// <summary>
        /// Finalizes current instance.
        /// </summary>
        ~RuntimePlugin()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the plugin name.
        /// </summary>
        public string Name
        {
            get { return plugin?.Name ?? "Runtime Plugin"; }
        }   

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        public string Description
        {
            get { return plugin?.Description ?? "Runtime Plugin"; }
        }

        /// <summary>
        /// Executes plugin with specified request.
        /// </summary>
        /// <param name="request">The plugin request.</param>
        /// <returns>A plugin response.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="request"/> is invalid.</exception>
        public IPluginResponse Execute(IPluginRequest request)
        {
            ObjectDisposedException.ThrowIf(disposed, this);
            
            if (plugin == null)
                throw new InvalidOperationException("Plugin is not initialized.");
            
            return plugin.Execute(request);
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (CanDispose())
            {
                if (disposing && plugin != null)
                {
                    if (plugin is IDisposable disposable)
                        disposable.Dispose();
                }

                Clean();
            }
        }

        private void Clean()
        {
            try
            {
                if (context != null && context.IsCollectible)
                    context.Unload();
            }
            catch
            {
                return;
            }
            finally
            {
                context = null;
                plugin = null;
            }
        }

        private bool CanDispose()
        {
            if (disposed)
                return false;
            disposed = true;
            return true;
        }
    }
}
