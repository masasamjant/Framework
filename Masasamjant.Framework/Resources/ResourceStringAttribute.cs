using System.Reflection;

namespace Masasamjant.Resources
{
    /// <summary>
    /// Attribute to mark field, property, class, struct or enum have resource string to display with value or type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class ResourceStringAttribute : Attribute
    {
        private string? resourceValue;

        /// <summary>
        /// Initializes new instance of the <see cref="ResourceStringAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceType">The resource type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> is empty or contains only whitespace characters.</exception>
        public ResourceStringAttribute(string resourceKey, Type resourceType)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
                throw new ArgumentNullException(nameof(resourceKey), "The resource key cannot be empty or only whitespace characters.");

            ResourceKey = resourceKey;
            ResourceType = resourceType;
            UseNonPublicResource = false;
        }

        /// <summary>
        /// Gets the resource key.
        /// </summary>
        public string ResourceKey { get; }

        /// <summary>
        /// Gets the resource type.
        /// </summary>
        public Type ResourceType { get; }

        /// <summary>
        /// Gets or sets if can use non-public member to get resource value. 
        /// Default value is <c>false</c>.
        /// </summary>
        public bool UseNonPublicResource { get; set; }

        /// <summary>
        /// Gets the resource value.
        /// </summary>
        /// <exception cref="ResourceException">If reading of resource value fails.</exception>
        public string ResourceValue
        {
            get
            {
                if (resourceValue == null)
                    resourceValue = GetResourceValue();

                return resourceValue;
            }
        }

        private string GetResourceValue()
        {
            try
            {
                var property = ResourceType.GetProperty(ResourceKey, BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

                bool nonPublic = false;

                if (property == null && UseNonPublicResource)
                {
                    property = ResourceType.GetProperty(ResourceKey, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetProperty);
                    nonPublic = property != null;
                }

                if (property == null)
                    throw new ResourceException(ResourceKey, ResourceType, $"The type does not have static property of {ResourceKey}.");

                if (!property.PropertyType.Equals(typeof(string)))
                    throw new ResourceException(ResourceKey, ResourceType, $"The {ResourceKey} property is not string.");

                var getMethod = property.GetGetMethod(nonPublic);

                if (getMethod == null)
                    throw new ResourceException(ResourceKey, ResourceType, $"The {ResourceType} does not have static get property of {ResourceKey}.");

                var value = getMethod.Invoke(null, null);

                return value is string s ? s : string.Empty;
            }
            catch (Exception exception)
            {
                if (exception is ResourceException)
                    throw;
                else
                    throw new ResourceException(ResourceKey, ResourceType, "The unexpected exception when getting value of specified resource.", exception);
            }
        }
    }
}
