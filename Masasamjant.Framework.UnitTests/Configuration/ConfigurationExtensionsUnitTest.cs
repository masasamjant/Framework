using Masasamjant.Resources;
using NSubstitute;

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

        [TestMethod]
        public void Test_GetDisplayName()
        {
            var properties = new List<ConfigurationProperty>();
            var group = new ConfigurationGroup("Name", "Desc", properties);
            var expected = "Name";
            var actual = ConfigurationExtensions.GetDisplayName(group, null);
            Assert.AreEqual(expected, actual);

            group.NameResourceKey = "NameResource";
            actual = ConfigurationExtensions.GetDisplayName(group, null);
            Assert.AreEqual(expected, actual);

            var resourceProvider = Substitute.For<IStringResourceProvider>();
            resourceProvider.GetString("NameResource").Returns((string?)null);
            resourceProvider.GetString("Name").Returns("Nom");

            actual = ConfigurationExtensions.GetDisplayName(group, resourceProvider);
            Assert.AreEqual(expected, actual);

            group.NameResourceKey = "Name";
            actual = ConfigurationExtensions.GetDisplayName(group, resourceProvider);
            Assert.AreEqual("Nom", actual);

            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationExtensions.GetDisplayName(null!, resourceProvider));
        }

        [TestMethod]
        public void Test_GetDisplayDescription()
        {
            var properties = new List<ConfigurationProperty>();
            var group = new ConfigurationGroup("Name", "Desc", properties);
            var expected = "Desc";
            var actual = ConfigurationExtensions.GetDisplayDescription(group, null);
            Assert.AreEqual(expected, actual);
            
            group.DescriptionResourceKey = "DescResource";
            actual = ConfigurationExtensions.GetDisplayDescription(group, null);
            Assert.AreEqual(expected, actual);
            
            var resourceProvider = Substitute.For<IStringResourceProvider>();
            resourceProvider.GetString("DescResource").Returns((string?)null);
            resourceProvider.GetString("Desc").Returns("Description");

            actual = ConfigurationExtensions.GetDisplayDescription(group, resourceProvider);
            Assert.AreEqual(expected, actual);
            
            group.DescriptionResourceKey = "Desc";
            actual = ConfigurationExtensions.GetDisplayDescription(group, resourceProvider);
            Assert.AreEqual("Description", actual);

            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationExtensions.GetDisplayDescription(null!, resourceProvider));
        }
    }
}
