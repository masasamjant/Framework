using System.Security.Claims;
using System.Security.Principal;

namespace Masasamjant.Security.Claims
{
    [TestClass]
    public class ClaimsPrincipalHelperUnitTest : UnitTest
    {
        private readonly ClaimEqualityComparer comparer = new ClaimEqualityComparer();
        private readonly Claim[] claims = [new Claim("Foo", "1", null, "issuer"), new Claim("Bar", "1", null, "issuer"), new Claim("Foo", "2", null, "Boo")];

        [TestMethod]
        public void Test_GetClaims()
        {
            var principal = GetClaimsPrincipal();
            var result = ClaimsPrincipalHelper.GetClaims(principal, "None");
            Assert.IsFalse(result.Any());

            Claim[] expected = [claims[0], claims[2]];
            Claim[] actual = ClaimsPrincipalHelper.GetClaims(principal, "Foo").ToArray();
            CollectionAssert.AreEquivalent(expected, actual, comparer);
        }

        [TestMethod]
        public void Test_GetFirstClaim()
        {
            var principal = GetClaimsPrincipal();
            Claim? actual = ClaimsPrincipalHelper.GetFirstClaim(principal, "None");
            Assert.IsNull(actual);
            Claim? expected = claims[0];
            actual = ClaimsPrincipalHelper.GetFirstClaim(principal, "Foo");
            Assert.IsTrue(comparer.Equals(expected, actual));
        }

        [TestMethod]
        public void Test_GetIssuerClaims()
        {
            var principal = GetClaimsPrincipal();
            var result = ClaimsPrincipalHelper.GetIssuerClaims(principal, "None");
            Assert.IsFalse(result.Any());
            Claim[] expected = [claims[0], claims[1]];
            Claim[] actual = ClaimsPrincipalHelper.GetIssuerClaims(principal, "issuer").ToArray();
            CollectionAssert.AreEquivalent(expected, actual, comparer);
        }

        private ClaimsPrincipal GetClaimsPrincipal()
        {         
            var identity = new ClaimsIdentity(new GenericIdentity("Test"), claims);
            return new ClaimsPrincipal(identity);
        }
    }
}
