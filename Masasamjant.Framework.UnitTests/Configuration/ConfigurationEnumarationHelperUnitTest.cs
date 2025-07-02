using System.Reflection;

namespace Masasamjant.Configuration
{
    [TestClass]
    public class ConfigurationEnumarationHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetConfigurationEnumerationTypes()
        {
            var assembly = typeof(BooleanOption).Assembly;
            var types = ConfigurationEnumarationHelper.GetConfigurationEnumerationTypes(assembly);
            Assert.IsTrue(types.Any());
            Assert.IsTrue(types.Contains(typeof(BooleanOption)));
        }

        [TestMethod]
        public void Test_GetConfigurationEnumerationTypesFromCurrentAppDomain()
        {
            var types = ConfigurationEnumarationHelper.GetConfigurationEnumerationTypesFromCurrentAppDomain();
            Assert.IsTrue(types.Any());
            Assert.IsTrue(types.Contains(typeof(BooleanOption)));
            Func<Assembly, bool> assemblySelector = a => a.FullName != null && a.FullName.Contains(".UnitTests");
            types = ConfigurationEnumarationHelper.GetConfigurationEnumerationTypesFromCurrentAppDomain(assemblySelector);
            Assert.IsFalse(types.Any());
        }

        [TestMethod]
        public void Test_FindConfigurationEnumerationType()
        {
            var assembly = typeof(BooleanOption).Assembly;
            var type = ConfigurationEnumarationHelper.FindConfigurationEnumerationType("BooleanOption", assembly);
            Assert.IsNotNull(type);
            Assert.AreEqual(typeof(BooleanOption), type);
            type = ConfigurationEnumarationHelper.FindConfigurationEnumerationType("NonExistentType", assembly);
            Assert.IsNull(type);
            type = ConfigurationEnumarationHelper.FindConfigurationEnumerationType("PreferredTypeName", assembly);
            Assert.IsNull(type);
        }

        [TestMethod]
        public void Test_FindConfigurationEnumerationTypeFromCurrentAppDomain()
        {
            Func<Assembly, bool> assemblySelector = a => a.FullName != null && a.FullName.Contains(".UnitTests");
            var type = ConfigurationEnumarationHelper.FindConfigurationEnumerationTypeFromCurrentAppDomain("BooleanOption", assemblySelector);
            Assert.IsNull(type);
            assemblySelector = a => a.FullName != null && !a.FullName.Contains(".UnitTests");
            type = ConfigurationEnumarationHelper.FindConfigurationEnumerationTypeFromCurrentAppDomain("BooleanOption", assemblySelector);
            Assert.IsNotNull(type);
        }
    }
}
