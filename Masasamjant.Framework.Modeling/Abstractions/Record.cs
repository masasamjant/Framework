namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents abstract <see cref="IRecord"/> impementation.
    /// </summary>
    public abstract class Record : Model, IRecord, IModel, ISupportCreated, ISupportModified, ISupportDeleted
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
        /// Gets the identity, like user name or identifier, to identify user who marked record as deleted.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then deleter is unknown or anonymous user.</remarks>
        public string? DeletedBy { get; protected set; }

        /// <summary>
        /// Gets the date and time when record was marked as deleted.
        /// </summary>
        /// <remarks>If <c>null</c>, then record has not been marked as deleted.</remarks>
        public DateTimeOffset? DeletedAt { get; protected set; }

        /// <summary>
        /// Gets whether or not record is marked as deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return DeletedAt.HasValue; }
        }

        /// <summary>
        /// Marks record as deleted.
        /// </summary>
        /// <param name="deletedBy">The identity, like user name or identifier, to identify user who marked record as deleted.</param>
        public void Delete(string? deletedBy)
        {
            if (IsDeleted)
                return;

            DeletedAt = GetDateTimeOffsetNow();
            DeletedBy = deletedBy;
        }

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
