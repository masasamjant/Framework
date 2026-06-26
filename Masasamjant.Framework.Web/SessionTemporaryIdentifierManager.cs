using Masasamjant.ComponentModel;
using Masasamjant.Security.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="ITemporaryIdentifierManager"/> where scope of identifiers is session.
    /// </summary>
    public sealed class SessionTemporaryIdentifierManager : ITemporaryIdentifierManager
    {
        private readonly TemporaryIdentifierManager identifierManager;

        /// <summary>
        /// Initializes new instance of the <see cref="SessionTemporaryIdentifierManager"/> class.
        /// </summary>
        /// <param name="hashProvider">The hash provider used for generating temporary identifiers.</param>
        public SessionTemporaryIdentifierManager(IStringHashProvider hashProvider)
        {
            identifierManager = new TemporaryIdentifierManager(hashProvider);
        }

        /// <summary>
        /// Get temporary identifier for specified <typeparamref name="T"/> identifier.
        /// </summary>
        /// <typeparam name="T">The type of the identifier.</typeparam>
        /// <param name="sessionIdentifier">The session identifier.</param>
        /// <param name="identifier">The actual identifier.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier<T>(string sessionIdentifier, T identifier) where T : notnull
        {
            return identifierManager.GetTemporaryIdentifier(sessionIdentifier, identifier);
        }

        /// <summary>
        /// Get temporary identifier for specified actual identifiers.
        /// </summary>
        /// <param name="sessionIdentifier">The scope key.</param>
        /// <param name="identifiers">The actual identifier values.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier(string sessionIdentifier, params object[] identifiers)
        {
            return identifierManager.GetTemporaryIdentifier(sessionIdentifier, identifiers);
        }

        /// <summary>
        /// Removes identifiers from specified session.
        /// </summary>
        /// <param name="sessionIdentifier">The session identifier.</param>
        public void RemoveIdentifiers(string sessionIdentifier)
        {
            identifierManager.RemoveIdentifiers(sessionIdentifier);
        }

        /// <summary>
        /// Tries to get the actual identifier for specified temporary identifier.
        /// </summary>
        /// <typeparam name="T">The type of the actual identifier.</typeparam>
        /// <param name="sessionIdentifier">The session identifier.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifier">The actual identifier when returns <c>true</c>; otherwise <c>null</c> or <c>default</c>.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier<T>(string sessionIdentifier, string temporaryIdentifier, [MaybeNullWhen(false)] out T identifier)
        {
            return identifierManager.TryGetIdentifier(sessionIdentifier, temporaryIdentifier, out identifier);
        }

        /// <summary>
        /// Tries to get the actual identifiers for specified temporary identifier.
        /// </summary>
        /// <param name="sessionIdentifier">The session identifier.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifiers">The actual identifiers or empty array.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier(string sessionIdentifier, string temporaryIdentifier, out object[] identifiers)
        {
            return identifierManager.TryGetIdentifier(sessionIdentifier, temporaryIdentifier, out identifiers);
        }
    }
}
