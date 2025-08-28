using Masasamjant.Web.Sessions;

namespace Masasamjant.Web.Components
{
    /// <summary>
    /// Represents events that <see cref="ISessionIdentifierManager"/> can react.
    /// </summary>
    public interface ISessionIdentifierManagerEvents
    {
        /// <summary>
        /// Event to notify <see cref="ISessionIdentifierManager"/> to remove identifiers associated with specified session.
        /// </summary>
        event EventHandler<SessionIdentifierEventArgs>? RemoveIdentifiers;
    }
}
