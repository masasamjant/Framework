using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents component to manage temporary identifiers to hide actual identifiers.
    /// </summary>
    public interface ITemporaryIdentifierManager
    {
        /// <summary>
        /// Tries to get the actual identifier for specified temporary identifier.
        /// </summary>
        /// <typeparam name="T">The type of the actual identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifier">The actual identifier when returns <c>true</c>; otherwise <c>null</c> or <c>default</c>.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        bool TryGetIdentifier<T>(string key, string temporaryIdentifier, [MaybeNullWhen(false)] out T identifier);

        /// <summary>
        /// Tries to get the actual identifiers for specified temporary identifier.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifiers">The actual identifiers or empty array.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        bool TryGetIdentifier(string key, string temporaryIdentifier, out object[] identifiers);

        /// <summary>
        /// Get temporary identifier for specified <typeparamref name="T"/> identifier.
        /// </summary>
        /// <typeparam name="T">The type of the identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="identifier">The actual identifier.</param>
        /// <returns>A temporary identifier.</returns>
        string GetTemporaryIdentifier<T>(string key, T identifier) where T : notnull;

        /// <summary>
        /// Get temporary identifier for specified actual identifiers.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="identifiers">The actual identifier values.</param>
        /// <returns>A temporary identifier.</returns>
        string GetTemporaryIdentifier(string key, params object[] identifiers);

        /// <summary>
        /// Removes identifiers from specified key.
        /// </summary>
        /// <param name="key">The scope key.</param>
        void RemoveIdentifiers(string key);
    }
}
