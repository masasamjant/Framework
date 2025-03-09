namespace Masasamjant.Configuration
{
    [TestClass]
    public class ConfigurationExtensionsUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ConfigurationException))]
        public void Test_GetValue_Section_Not_Exist()
        {
            var configuration = GetConfiguration(new Dictionary<string, string?>());
            ConfigurationExtensions.GetValue(configuration, "Key", "Section");
        }

        [TestMethod]
        public void Test_GetValue_Single_Section()
        {
            var configuration = GetConfiguration(new Dictionary<string, string?>() 
            {
                { "Section:Key", "Value" },
            });
            var expected = "Value";
            var actual = ConfigurationExtensions.GetValue(configuration, "Key", "Section");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetValue_No_Sections()
        {
            var configuration = GetConfiguration(new Dictionary<string, string?>()
            {
                { "Key", "Value" },
            });
            var expected = "Value";
            var actual = ConfigurationExtensions.GetValue(configuration, "Key");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationException))]
        public void Test_GetValue_Invalid_Section()
        {
            var configuration = GetConfiguration(new Dictionary<string, string?>()
            {
                { "Section:Key", "Value" },
            });
            var sectionKeys = new string[] { "Section", "" };
            ConfigurationExtensions.GetValue(configuration, "Key", sectionKeys);
        }

        [TestMethod]
        public void Test_GetValue_Multiple_Sections()
        {
            var configuration = GetConfiguration(new Dictionary<string, string?>()
            {
                { "Root:Section:Key", "Value" },
            });
            var sectionKeys = new string[] { "Root", "Section" };
            var expected = "Value";
            var actual = ConfigurationExtensions.GetValue(configuration, "Key", sectionKeys);
            Assert.AreEqual(expected, actual);
        }
    }
}
