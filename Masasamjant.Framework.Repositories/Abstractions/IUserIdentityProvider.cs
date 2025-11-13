using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Repositories.Abstractions
{
    /// <summary>
    /// Represent service that provides identity of current user.
    /// </summary>
    public interface IUserIdentityProvider
    {
        /// <summary>
        /// Gets the <see cref="IUserIdentity"/> of current user.
        /// </summary>
        /// <returns>A <see cref="IUserIdentity"/>.</returns>
        IUserIdentity GetUserIdentity();
    }
}
