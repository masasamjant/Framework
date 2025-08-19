using Masasamjant.Modeling.Abstractions;
using Masasamjant.Modeling.Abstractions.Services;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Represents basic implementation of <see cref="IUserIdentityProvider"/> interface.
    /// </summary>
    public class UserIdentityProvider : IUserIdentityProvider
    {
        private readonly Func<IUserIdentity?>? getUserIdentity;
        private readonly IUserIdentity? userIdentity;

        /// <summary>
        /// Initializes new instance of the <see cref="IUserIdentityProvider"/> class 
        /// with delegate to get <see cref="IUserIdentity"/>.
        /// </summary>
        /// <param name="getUserIdentity">The delegate to get <see cref="IUserIdentity"/>.</param>
        public UserIdentityProvider(Func<IUserIdentity?> getUserIdentity)
        {
            this.getUserIdentity = getUserIdentity;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="UserIdentityProvider"/> class 
        /// with provided <see cref="IUserIdentity"/>.
        /// </summary>
        /// <param name="userIdentity">The <see cref="IUserIdentity"/> to provide.</param>
        public UserIdentityProvider(IUserIdentity userIdentity)
        {
            this.userIdentity = userIdentity;
        }

        /// <summary>
        /// Gets the <see cref="IUserIdentity"/> of current user.
        /// </summary>
        /// <returns>A <see cref="IUserIdentity"/>.</returns>
        public IUserIdentity GetUserIdentity()
        {
            if (userIdentity != null)
                return userIdentity;

            if (getUserIdentity != null)
            {
                var identity = getUserIdentity();

                if (identity != null)
                    return identity;
            }

            return new UserIdentity();
        }
    }
}
