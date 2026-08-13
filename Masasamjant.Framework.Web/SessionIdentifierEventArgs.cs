namespace Masasamjant.Web
{
    /// <summary>
    /// Arguments for event associated with session.
    /// </summary>
    public class SessionIdentifierEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="SessionIdentifierEventArgs"/> class.
        /// </summary>
        /// <param name="sessionIdentifier">The unique session identifier.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="sessionIdentifier"/> is <c>null</c>.</exception>
        public SessionIdentifierEventArgs(string sessionIdentifier)
        {
            ArgumentNullException.ThrowIfNull(sessionIdentifier);
            SessionIdentifier = sessionIdentifier;
        }

        /// <summary>
        /// Gets the session identifier.
        /// </summary>
        public string SessionIdentifier { get; }
    }
}
