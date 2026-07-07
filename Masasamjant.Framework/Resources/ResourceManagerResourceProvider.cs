using System.Globalization;
using System.Resources;

namespace Masasamjant.Resources
{
    /// <summary>
    /// Represents resource provider that provides resource from <see cref="ResourceManager"/>.
    /// </summary>
    public sealed class ResourceManagerResourceProvider : IStringResourceProvider, IObjectResourceProvider, IStreamResourceProvider
    {
        private readonly ResourceManager? resourceManager;
        private readonly Func<ResourceManager>? resourceManagerFactory;

        /// <summary>
        /// Initializes new instance of the <see cref="ResourceManagerResourceProvider"/> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager to get resources from.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceManager"/> is <c>null</c>.</exception>
        public ResourceManagerResourceProvider(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
        }

        /// <summary>
        /// Initializes new instance of the <see cref="ResourceManagerResourceProvider"/> class.
        /// </summary>
        /// <param name="resourceManagerFactory">The factory function to create the resource manager.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceManagerFactory"/> is <c>null</c>.</exception>
        public ResourceManagerResourceProvider(Func<ResourceManager> resourceManagerFactory)
        {
            this.resourceManagerFactory = resourceManagerFactory ?? throw new ArgumentNullException(nameof(resourceManagerFactory));
        }

        /// <summary>
        /// Gets string resource specified by key using current UI culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The string resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> is <c>null</c>.</exception>
        public string? GetString(string resourceKey, string? defaultValue = null)
        {
            return GetString(resourceKey, CultureInfo.CurrentUICulture, defaultValue);
        }

        /// <summary>
        /// Gets string resource specified by key using specified culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="culture">The culture to use.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The string resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> or <paramref name="culture"/> is <c>null</c>.</exception>
        public string? GetString(string resourceKey, CultureInfo culture, string? defaultValue = null)
        {
            ValidateResourceKey(resourceKey);
            var resourceManager = GetResourceManager();
            var resourceValue = resourceManager.GetString(resourceKey, culture);
            return resourceValue ?? defaultValue;
        }

        /// <summary>
        /// Gets object resource specified by key using current UI culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The object resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> is <c>null</c>.</exception>
        public object? GetObject(string resourceKey, object? defaultValue = null)
        {
            return GetObject(resourceKey, CultureInfo.CurrentUICulture, defaultValue);
        }

        /// <summary>
        /// Gets object resource specified by key using specified culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="culture">The culture to use.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The object resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> or <paramref name="culture"/> is <c>null</c>.</exception>
        public object? GetObject(string resourceKey, CultureInfo culture, object? defaultValue = null)
        {
            ValidateResourceKey(resourceKey);
            var resourceManager = GetResourceManager();
            var resourceValue = resourceManager.GetObject(resourceKey, culture);
            return resourceValue ?? defaultValue;
        }

        /// <summary>
        /// Gets the stream resource for the specified resource name using current UI culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns>The stream resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> is <c>null</c>.</exception>
        public Stream? GetStream(string resourceKey)
        {
            return GetStream(resourceKey, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the stream resource for the specified resource name using the specified culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="culture">The culture to use.</param>
        /// <returns>The stream resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> or <paramref name="culture"/> is <c>null</c>.</exception>
        public Stream? GetStream(string resourceKey, CultureInfo culture)
        {
            ValidateResourceKey(resourceKey);
            var resourceManager = GetResourceManager();
            var resourceStream = resourceManager.GetStream(resourceKey, culture);
            return resourceStream;
        }

        private static void ValidateResourceKey(string resourceKey)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
                throw new ArgumentNullException(nameof(resourceKey), "Resource key is null, empty or only whitespace.");
        }

        private ResourceManager GetResourceManager()
        {
            if (resourceManager == null)
            {
                if (resourceManagerFactory != null)
                {
                    var manager = resourceManagerFactory();

                    if (manager != null)
                        return manager;
                }

                throw new InvalidOperationException("Resource manager is not available or not created.");
            }

            return resourceManager;
        }
    }
}
