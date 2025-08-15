namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents <see cref="IModel"/> that has deleted and deleted time properties.
    /// </summary>
    public interface ISupportDeleted : IModel
    {
        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who marked model as deleted.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then deleter is unknown or anonymous user.</remarks>
        string? DeletedBy { get; }

        /// <summary>
        /// Gets the date and time when model was marked as deleted.
        /// </summary>
        /// <remarks>If <c>null</c>, then model has not been marked as deleted.</remarks>
        DateTimeOffset? DeletedAt { get; }

        /// <summary>
        /// Marks model as deleted.
        /// </summary>
        /// <param name="deletedBy">The identity, like user name or identifier, to identify user who marked model as deleted.</param>
        void Delete(string? deletedBy);
    }
}
