namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents a model that has modified and modification time properties.
    /// </summary>
    public interface ISupportModified
    {
        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who modified model.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then modifier is unknown or anonymous user.</remarks>
        string? ModifiedBy { get; }

        /// <summary>
        /// Gets the date and time when model was modified.
        /// </summary>
        /// <remarks>If <c>null</c>, then model has not been modified.</remarks>
        DateTimeOffset? ModifiedAt { get; }
    }
}
