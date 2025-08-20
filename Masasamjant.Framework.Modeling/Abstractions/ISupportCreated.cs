namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents a model that has creator and creation time properties.
    /// </summary>
    public interface ISupportCreated
    {
        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who created model.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then creator is unknown or anonymous user.</remarks>
        string? CreatedBy { get; }

        /// <summary>
        /// Gets the date and time when model was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }
    }
}
