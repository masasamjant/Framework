using System.Security.Principal;
using System.Text.Json.Serialization;

namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents entity that implements <see cref="ISupportCreated"/> and <see cref="ISupportModified"/> interfaces.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class Record<TIdentifier> : Entity<TIdentifier>, IRecord<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who created model.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then creator is unknown or anonymous user.</remarks>
        [JsonInclude]
        public string? CreatedBy { get; protected set; }

        /// <summary>
        /// Gets the date and time when model was created.
        /// </summary>
        [JsonInclude]
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who modified model.
        /// </summary>
        /// <remarks>If <c>null</c> or empty, then modifier is unknown or anonymous user.</remarks>
        [JsonInclude]
        public string? ModifiedBy { get; protected set; }

        /// <summary>
        /// Gets the date and time when model was modified.
        /// </summary>
        /// <remarks>If <c>null</c>, then model has not been modified.</remarks>
        [JsonInclude]
        public DateTimeOffset? ModifiedAt { get; protected set; }

        /// <summary>
        /// Invoked when model instance is added to non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity to identify who is performing addition.</param>
        protected override void OnAdd(IIdentity? identity)
        {
            CreatedBy = identity?.Name;
            CreatedAt = GetDateTimeOffsetNow();
            base.OnAdd(identity);
        }

        /// <summary>
        /// Invoked when model instance is updated in non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity to identify who is performing update.</param>
        protected override void OnUpdate(IIdentity? identity)
        {
            ModifiedBy = identity?.Name;
            ModifiedAt = GetDateTimeOffsetNow();
            base.OnUpdate(identity);
        }
    }
}
