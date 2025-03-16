using System.Globalization;

namespace Masasamjant.Globalization
{
    [TestClass]
    public class CultureContextUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CultureContext()
        {
            CultureInfo c = CultureInfo.CurrentCulture;
            CultureInfo u = CultureInfo.CurrentUICulture;

            try
            {
                CultureInfo enUS = new CultureInfo("en-US");
                CultureInfo.CurrentCulture = enUS;
                CultureInfo.CurrentUICulture = enUS;
                CultureInfo fi = new CultureInfo("fi-FI");

                using (var context = new CultureContext(fi))
                {
                    Assert.IsTrue(CultureInfo.CurrentCulture.Equals(fi));
                    Assert.IsTrue(CultureInfo.CurrentUICulture.Equals(fi));
                }

                Assert.IsTrue(CultureInfo.CurrentCulture.Equals(enUS));
                Assert.IsTrue(CultureInfo.CurrentUICulture.Equals(enUS));

                using (var context = new CultureContext(fi, null))
                {
                    Assert.IsTrue(CultureInfo.CurrentCulture.Equals(fi));
                    Assert.IsTrue(CultureInfo.CurrentUICulture.Equals(enUS));
                }

                Assert.IsTrue(CultureInfo.CurrentCulture.Equals(enUS));
                Assert.IsTrue(CultureInfo.CurrentUICulture.Equals(enUS));

                using (var context = new CultureContext(null, fi))
                {
                    Assert.IsTrue(CultureInfo.CurrentCulture.Equals(enUS));
                    Assert.IsTrue(CultureInfo.CurrentUICulture.Equals(fi));
                }

                Assert.IsTrue(CultureInfo.CurrentCulture.Equals(enUS));
                Assert.IsTrue(CultureInfo.CurrentUICulture.Equals(enUS));
            }
            finally
            {
                CultureInfo.CurrentCulture = c;
                CultureInfo.CurrentUICulture = u;
            }
        }
    }
}
