using System.Security.Principal;

namespace Masasamjant.Repositories.Abstractions
{
    /// <summary>
    /// Represents provider of current identity.
    /// </summary>
    public interface ICurrentIdentityProvider
    {
        /// <summary>
        /// Gets the identity of current user.
        /// </summary>
        /// <returns>A identity of current user or <c>null</c>.</returns>
        IIdentity? GetCurrentIdentity();
    }
}
