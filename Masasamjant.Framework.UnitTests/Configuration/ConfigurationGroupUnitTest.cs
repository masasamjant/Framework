namespace Masasamjant.Configuration
{
    [TestClass]
    public class ConfigurationGroupUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var properties = new List<ConfigurationProperty>();
            var group = new ConfigurationGroup("TestGroup", "Desc", properties);
            Assert.AreEqual("TestGroup", group.Name);
            Assert.AreEqual("Desc", group.Description);
            Assert.AreEqual(0, group.Properties.Count);
            
            properties.Add(new ConfigurationProperty("Prop1", "Value1"));
            group = new ConfigurationGroup("TestGroup", "Desc", properties);
            Assert.AreEqual(1, group.Properties.Count);
        
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationGroup(null!, "Desc", properties));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationGroup("", "Desc", properties));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationGroup("  ", "Desc", properties));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationGroup("TestGroup", "Desc", null!));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationGroup("TestGroup", null!, properties));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationGroup("TestGroup", "", properties));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationGroup("TestGroup", "  ", properties));
        }
    }
}
