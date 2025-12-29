namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents <see cref="IRecord{TIdentifier}"/> that is identified by <typeparamref name="TIdentifier"/>.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class Record<TIdentifier> : Model<TIdentifier>, IRecord<TIdentifier>, ISupportCreated, ISupportModified
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who created record.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then creator is unknown or anonymous user.</remarks>
        public string? CreatedBy { get; protected set; }

        /// <summary>
        /// Gets the date and time when record was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who modified record.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then modifier is unknown or anonymous user.</remarks>
        public string? ModifiedBy { get; protected set; }

        /// <summary>
        /// Gets the date and time when record was modified.
        /// </summary>
        /// <remarks>If <c>null</c>, then record has not been modified.</remarks>
        public DateTimeOffset? ModifiedAt { get; protected set; }

        /// <summary>
        /// Invoked when record instance is added to non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing addition.</param>
        protected override void OnAdd(string? identity)
        {
            CreatedAt = GetDateTimeOffsetNow();
            CreatedBy = identity;
        }

        /// <summary>
        /// Invoked when record instance is updated in non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing update.</param>
        protected override void OnUpdate(string? identity)
        {
            ModifiedAt = GetDateTimeOffsetNow();
            ModifiedBy = identity;
        }
    }
}
