namespace Masasamjant.Resources
{
    [TestClass]
    public class ResourceStringAttributeUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Resource_Key_Is_Empty()
        {
            new ResourceStringAttribute("", typeof(PublicUnitTestResource));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Resource_Key_Is_WhiteSpace()
        {
            new ResourceStringAttribute("   ", typeof(PublicUnitTestResource));
        }

        [TestMethod]
        public void Test_Constructor()
        {
            var attribute = new ResourceStringAttribute("Key", typeof(PublicUnitTestResource));
            Assert.AreEqual("Key", attribute.ResourceKey);
            Assert.AreEqual(typeof(PublicUnitTestResource), attribute.ResourceType);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceException))]
        public void Test_Resource_Not_Exist()
        {
            var attribute = new ResourceStringAttribute("Foo", typeof(PublicUnitTestResource));
            var value = attribute.ResourceValue;
        }

        [TestMethod]
        public void Test_Public_Resource()
        {
            var attribute = new ResourceStringAttribute("Key", typeof(PublicUnitTestResource))
            {
                UseNonPublicResource = false
            };
            Assert.AreEqual("Value", attribute.ResourceValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceException))]
        public void Test_Reading_Non_Public_Resource_Fails()
        {
            var attribute = new ResourceStringAttribute("Key", typeof(UnitTestResource))
            {
                UseNonPublicResource = false
            };
            Assert.AreEqual("Value", attribute.ResourceValue);
        }

        [TestMethod]
        public void Test_Reading_Non_Public_Resource()
        {
            var attribute = new ResourceStringAttribute("Key", typeof(UnitTestResource))
            {
                UseNonPublicResource = true
            };
            Assert.AreEqual("Value", attribute.ResourceValue);
        }
    }
}
