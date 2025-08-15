using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Provides helper methods to <see cref="IUserIdentity"/> interface.
    /// </summary>
    public static class UserIdentityHelper
    {
        /// <summary>
        /// Check if specified <see cref="IUserIdentity"/> represents anonymous user i.e. <see cref="IUserIdentity.Identity"/> is empty string.
        /// </summary>
        /// <param name="userIdentity">The <see cref="IUserIdentity"/>.</param>
        /// <returns><c>true</c> if <paramref name="userIdentity"/> represents anonymous user; <c>false</c> otherwise.</returns>
        public static bool IsAnonymous(this IUserIdentity userIdentity)
        {
            return string.IsNullOrWhiteSpace(userIdentity.Identity);
        }
    }
}
