using System.Security.Principal;

namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents a model that supports activation.
    /// </summary>
    public interface ISupportActivation
    {
        /// <summary>
        /// Gets the current status of activity.
        /// </summary>
        ActiveStatus ActiveStatus { get; }

        /// <summary>
        /// Gets the date and time when model's activation status was last changed.
        /// </summary>
        DateTimeOffset? ActiveStatusChangedAt { get; }

        /// <summary>
        /// Gets the identity, like user name or identifier, to identify user who last changed model's activation status.
        /// </summary>
        string? ActiveStatusChangedBy { get; }

        /// <summary>
        /// Change model's activation status.
        /// </summary>
        /// <param name="activeStatus">The new activation status.</param>
        /// <param name="changedBy">The identity, like user name or identifier, to identify user who changed model's activation status.</param>
        void ChangeActivateStatus(ActiveStatus activeStatus, string? changedBy);

        /// <summary>
        /// Change model's activation status.
        /// </summary>
        /// <param name="activeStatus">The new activation status.</param>
        /// <param name="identity">The identity of the user who changed model's activation status.</param>
        void ChangeActivateStatus(ActiveStatus activeStatus, IIdentity? identity);
    }
}
