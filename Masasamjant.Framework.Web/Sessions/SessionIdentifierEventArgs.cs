namespace Masasamjant.Web.Sessions
{
    /// <summary>
    /// Represents argument of events related to session.
    /// </summary>
    public class SessionIdentifierEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="SessionIdentifierEventArgs"/> class.
        /// </summary>
        /// <param name="sessionIdentifier">The session identifier as string.</param>
        public SessionIdentifierEventArgs(string sessionIdentifier)
        {
            SessionIdentifier = sessionIdentifier;
        }

        /// <summary>
        /// Gets the session identifier as string.
        /// </summary>
        public string SessionIdentifier { get; }
    }
}
