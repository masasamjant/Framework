using Masasamjant.Modeling.Abstractions;
using Masasamjant.Serialization;
using System.Text.Json.Serialization;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Represents identity of the user.
    /// </summary>
    public class UserIdentity : IUserIdentity, IJsonSerializable
    {
        /// <summary>
        /// Initializes new instance of the <see cref="UserIdentity"/> class with specified identity.
        /// </summary>
        /// <param name="identity">The user identity, like name, identifier or email address, as string.</param>
        /// <remarks>If <paramref name="identity"/> is only whitespace, then it is replaced with empty string.</remarks>
        public UserIdentity(string identity)
        {
            Identity = string.IsNullOrWhiteSpace(identity) ? string.Empty : identity;
        }

        /// <summary>
        /// Initializes new default instance of the <see cref="UserIdentity"/> class that 
        /// represents anonymous user.
        /// </summary>
        public UserIdentity() 
        { }

        /// <summary>
        /// Gets the user identity as string. This can be name, identifier, email address etc. 
        /// The value should be something that uniquely identifies user. 
        /// If returns empty string, then user is considered to be anonymous.
        /// </summary>
        [JsonInclude]
        public string Identity { get; internal set; } = string.Empty;
    }
}
