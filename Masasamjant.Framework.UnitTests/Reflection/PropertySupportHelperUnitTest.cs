using System.Reflection;

namespace Masasamjant.Reflection
{
    [TestClass]
    public class PropertySupportHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetBindingFlags()
        {
            Assert.AreEqual(BindingFlags.Default, PropertySupportHelper.GetBindingFlags(PropertySupport.None, false));

            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicSetter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.PublicSetter, false));

            Assert.AreEqual(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublicGetter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublicSetter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublicGetter | PropertySupport.NonPublicSetter, false));

            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.NonPublicGetter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicSetter | PropertySupport.NonPublicSetter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.NonPublicGetter | PropertySupport.PublicSetter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.NonPublicGetter | PropertySupport.NonPublicSetter, false));

            Assert.AreEqual(BindingFlags.Default, PropertySupportHelper.GetBindingFlags(PropertySupport.None, true));

            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicSetter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.PublicSetter, true));

            Assert.AreEqual(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublicGetter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublicSetter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublicGetter | PropertySupport.NonPublicSetter, true));

            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.NonPublicGetter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicSetter | PropertySupport.NonPublicSetter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.NonPublicGetter | PropertySupport.PublicSetter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.PublicGetter | PropertySupport.NonPublicGetter | PropertySupport.NonPublicSetter, true));
        }
    }
}
