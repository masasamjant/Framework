namespace Masasamjant.Configuration
{
    [TestClass]
    public class ConfigurationPropertyUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var property = new ConfigurationProperty("TestProperty", "This is a test property.", "TestValue", false);
            Assert.AreEqual("TestProperty", property.Name);
            Assert.AreEqual("This is a test property.", property.Description);
            Assert.AreEqual("TestValue", property.Value);
            Assert.IsFalse(property.IsReadOnly);

            property = new ConfigurationProperty("ReadOnlyProperty", "This is a read-only property.", "ReadOnlyValue", true);
            Assert.AreEqual("ReadOnlyProperty", property.Name);
            Assert.AreEqual("This is a read-only property.", property.Description);
            Assert.AreEqual("ReadOnlyValue", property.Value);
            Assert.IsTrue(property.IsReadOnly);

            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationProperty(null!, "Description"));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationProperty("Name", null!));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationProperty("", "Description"));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationProperty("Name", ""));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationProperty("   ", "Description"));
            Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationProperty("Name", "   "));
        }

        [TestMethod]
        public void Test_Value_Setter()
        {
            var property = new ConfigurationProperty("TestProperty", "This is a test property.", "TestValue", false);
            property.Value = "NewValue";
            Assert.AreEqual("NewValue", property.Value);
            
            var readOnlyProperty = new ConfigurationProperty("ReadOnlyProperty", "This is a read-only property.", "ReadOnlyValue", true);
            Assert.ThrowsException<InvalidOperationException>(() => readOnlyProperty.Value = "NewValue");
        }

        [TestMethod]
        public void Test_SetReadOnly()
        {
            var property = new ConfigurationProperty("TestProperty", "This is a test property.", "TestValue", false);
            Assert.IsFalse(property.IsReadOnly);
            property.SetReadOnly();
            Assert.IsTrue(property.IsReadOnly);
        }
    }
}
