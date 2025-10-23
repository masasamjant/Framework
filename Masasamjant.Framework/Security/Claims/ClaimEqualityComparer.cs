using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Masasamjant.Security.Claims
{
    /// <summary>
    /// Equality comparer of <see cref="Claim"/> class.
    /// </summary>
    public class ClaimEqualityComparer : EqualityComparer<Claim>
    {
        /// <summary>
        /// Check if claims are equal. The default impelementation check if type, value type, value, issue and original issuer
        /// of claims are equal, then claims are equal.
        /// </summary>
        /// <param name="x">The first claim.</param>
        /// <param name="y">The second claim.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="x"/> and <paramref name="y"/> are same reference or if they have same type, issuer and original issuer;
        /// <c>false</c> otherwise.
        /// </returns>
        public override bool Equals(Claim? x, Claim? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null) 
                return false;

            return x.Type == y.Type &&
                   x.ValueType == y.ValueType &&
                   x.Value == y.Value &&
                   x.Issuer == y.Issuer &&
                   x.OriginalIssuer == y.OriginalIssuer;
        }

        /// <summary>
        /// Gets hash code for claim. The default implementation computes hash from type, value type, value, issuer and original issuer.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns>A hash code.</returns>
        public override int GetHashCode([DisallowNull] Claim claim)
        {
            return HashCode.Combine(claim.Type, claim.ValueType, claim.Value, claim.Issuer, claim.OriginalIssuer);
        }
    }
}
