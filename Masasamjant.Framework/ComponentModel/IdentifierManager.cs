using Masasamjant.Security;
using Masasamjant.Security.Abstractions;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents simple <see cref="IIdentifierManager"/> that keeps identifiers in memory.
    /// </summary>
    /// <remarks>Instance of this class is thread-safe and can be used as singleton instance.</remarks>
    public sealed class IdentifierManager : IIdentifierManager
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, object[]>> temporaryIdentifiers;
        private readonly IStringHashProvider hashProvider;

        /// <summary>
        /// Initializes new default instance of the <see cref="IdentifierManager"/> class that creates Base-64 SHA1 temporary identifiers.
        /// </summary>
        public IdentifierManager()
            : this(new Base64SHA1Provider())
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="IdentifierManager"/> class with specified <see cref="IStringHashProvider"/> to compute temporary identifiers.
        /// </summary>
        /// <param name="hashProvider">The <see cref="IStringHashProvider"/> to compute temporary identifiers.</param>
        public IdentifierManager(IStringHashProvider hashProvider)
        {
            this.temporaryIdentifiers = new ConcurrentDictionary<string, ConcurrentDictionary<string, object[]>>();
            this.hashProvider = hashProvider;
        }

        /// <summary>
        /// Get temporary identifier for specified <typeparamref name="T"/> identifier.
        /// </summary>
        /// <typeparam name="T">The type of the identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="identifier">The actual identifier.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier<T>(string key, T identifier) where T : notnull
            => CreateTemporaryIdentifier(key, [identifier]);

        /// <summary>
        /// Get temporary identifier for specified actual identifiers.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="identifiers">The actual identifier values.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier(string key, params object[] identifiers)
            => CreateTemporaryIdentifier(key, identifiers);

        /// <summary>
        /// Removes identifiers from specified key.
        /// </summary>
        /// <param name="key">The scope key.</param>
        public void RemoveIdentifiers(string key)
        {
            if (temporaryIdentifiers.TryRemove(key, out var identifiers))
                identifiers.Clear();
        }

        /// <summary>
        /// Tries to get the actual identifier for specified temporary identifier.
        /// </summary>
        /// <typeparam name="T">The type of the actual identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifier">The actual identifier when returns <c>true</c>; otherwise <c>null</c> or <c>default</c>.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier<T>(string key, string temporaryIdentifier, [MaybeNullWhen(false)] out T identifier)
        {
            if (TryGetIdentifier(key, temporaryIdentifier, out var identifiers) &&
                identifiers.Length == 1 && identifiers[0] is T result)
            {
                identifier = result;
                return true;
            }

            identifier = default;
            return false;
        }

        /// <summary>
        /// Tries to get the actual identifiers for specified temporary identifier.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifiers">The actual identifiers or empty array.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier(string key, string temporaryIdentifier, out object[] identifiers)
        {
            if (temporaryIdentifiers.TryGetValue(key, out var identifiersDictionary))
            {
                if (identifiersDictionary.TryGetValue(temporaryIdentifier, out var result))
                {
                    identifiers = result;
                    return true;
                }
            }

            identifiers = [];
            return false;
        }

        private string CreateTemporaryIdentifier(string key, object[] identifiers)
        {
            if (identifiers.Length == 0)
                throw new ArgumentException("There must be at least one identifier value.", nameof(identifiers));

            var value = key + string.Concat(identifiers.Select(x => x.ToString()));
            var temporaryIdentifier = hashProvider.CreateHash(value);
            var identifiersDictionary = temporaryIdentifiers.GetOrAdd(key, new ConcurrentDictionary<string, object[]>());
            identifiersDictionary.AddOrUpdate(temporaryIdentifier, identifiers, (k, v) => identifiers);
            return temporaryIdentifier;
        }
    }
}
