using System.Security.Claims;

namespace Masasamjant.Security.Claims
{
    /// <summary>
    /// Provides helper methods to <see cref="ClaimsPrincipal"/> class.
    /// </summary>
    public static class ClaimsPrincipalHelper
    {
        /// <summary>
        /// Gets the first claim with specified type.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/>.</param>
        /// <param name="type">The type of claim to get.</param>
        /// <returns>A first claim with specified type or <c>null</c>.</returns>
        public static Claim? GetFirstClaim(this ClaimsPrincipal principal, string type)
            => GetClaims(principal, type).FirstOrDefault();

        /// <summary>
        /// Gets all claims with specified type.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/>.</param>
        /// <param name="type">The type of claim to get.</param>
        /// <returns>A claims with specified type.</returns>
        public static IEnumerable<Claim> GetClaims(this ClaimsPrincipal principal, string type)
            => principal.Claims.Where(claim => claim.Type == type);

        /// <summary>
        /// Gets all claims issued by specified issuer.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/>.</param>
        /// <param name="issuer">The issuer.</param>
        /// <returns>A claims issued by specied issuer.</returns>
        public static IEnumerable<Claim> GetIssuerClaims(this ClaimsPrincipal principal, string issuer)
            => principal.Claims.Where(claim => claim.Issuer == issuer);
    }
}
