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

            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Getter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Setter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Getter | PropertySupport.Setter, false));

            Assert.AreEqual(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublic | PropertySupport.Getter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublic | PropertySupport.Setter, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublic | PropertySupport.Getter | PropertySupport.Setter, false));

            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Getter | PropertySupport.NonPublic, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Setter | PropertySupport.NonPublic, false));
            Assert.AreEqual(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.NonPublic | PropertySupport.Getter | PropertySupport.Setter, false));

            Assert.AreEqual(BindingFlags.Default, PropertySupportHelper.GetBindingFlags(PropertySupport.None, true));

            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Getter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Setter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Getter | PropertySupport.Setter, true));

            Assert.AreEqual(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublic | PropertySupport.Getter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublic | PropertySupport.Setter, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.NonPublic | PropertySupport.Getter | PropertySupport.Setter, true));

            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Getter | PropertySupport.NonPublic, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.Setter | PropertySupport.NonPublic, true));
            Assert.AreEqual(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty, PropertySupportHelper.GetBindingFlags(PropertySupport.Public | PropertySupport.NonPublic | PropertySupport.Getter | PropertySupport.Setter, true));
        }
    }
}
