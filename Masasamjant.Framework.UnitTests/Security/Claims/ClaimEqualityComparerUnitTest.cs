using System.Security.Claims;

namespace Masasamjant.Security.Claims
{
    [TestClass]
    public class ClaimEqualityComparerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Equals()
        {
            var comparer = new ClaimEqualityComparer();
            Claim? x = null;
            Claim? y = null;
            Assert.IsTrue(comparer.Equals(x, y));
            
            x = new Claim("Test", "1", "string", "issuer", "org-issuer");
            Assert.IsFalse(comparer.Equals(x, y));
            
            x = null;
            y = new Claim("Test", "1", "string", "issuer", "org-issuer");
            Assert.IsFalse(comparer.Equals(x, y));

            x = new Claim("Test", "1", "string", "issuer", "org-issuer");
            Assert.IsTrue(comparer.Equals(x , new Claim("Test", "1", "string", "issuer", "org-issuer")));
            Assert.IsFalse(comparer.Equals(x, new Claim("Test", "1", "string", "issuer", "issuer")));
            Assert.IsFalse(comparer.Equals(x, new Claim("Test", "1", "string", "org-issuer", "org-issuer")));
            Assert.IsFalse(comparer.Equals(x, new Claim("Test", "1", "int", "issuer", "org-issuer")));
            Assert.IsFalse(comparer.Equals(x, new Claim("Test", "2", "string", "issuer", "org-issuer")));
            Assert.IsFalse(comparer.Equals(x, new Claim("Testing", "1", "string", "issuer", "org-issuer")));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var comparer = new ClaimEqualityComparer();
            var x = new Claim("Test", "1", "string", "issuer", "org-issuer");
            var y = new Claim("Test", "1", "string", "issuer", "org-issuer");
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }
    }
}
