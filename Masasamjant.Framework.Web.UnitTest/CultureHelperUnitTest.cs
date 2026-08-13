using System.Globalization;

namespace Masasamjant.Web
{
    [TestClass]
    public class CultureHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsAvailableCulture()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (var culture in cultures)
            {
                if (string.IsNullOrWhiteSpace(culture.Name))
                    continue;

                CultureHelper.IsAvailableCulture(culture.Name);
            }

            Assert.IsFalse(CultureHelper.IsAvailableCulture("No-Such-Culture"));
        }

        [TestMethod]
        public void Test_GetCulture()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            
            foreach (var culture in cultures)
            {
                if (string.IsNullOrWhiteSpace(culture.Name))
                    continue;

                var other = CultureHelper.GetCulture(culture.Name);
                Assert.IsNotNull(other);
                Assert.AreEqual(culture.Name, other.Name);
            }

            Assert.IsNull(CultureHelper.GetCulture("No-Such-Culture"));
        }
    }
}
