namespace Masasamjant.Resources
{
    /// <summary>
    /// Represents exception thrown when using resource fails.
    /// </summary>
    public class ResourceException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ResourceException"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceType">The resource type.</param>
        public ResourceException(string resourceKey, Type resourceType)
            : this(resourceKey, resourceType, "The unexpected exception using specified resource.")
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ResourceException"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceType">The resource type.</param>
        /// <param name="message">The exception message.</param>
        public ResourceException(string resourceKey, Type resourceType, string message)
            : this(resourceKey, resourceType, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ResourceException"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceType">The resource type.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ResourceException(string resourceKey, Type resourceType, string message, Exception? innerException)
            : base(message, innerException)
        {
            ResourceKey = resourceKey;
            ResourceType = resourceType;
        }

        /// <summary>
        /// Gets the resource key.
        /// </summary>
        public string ResourceKey { get; }

        /// <summary>
        /// Gets the resource type.
        /// </summary>
        public Type ResourceType { get; }
    }
}
