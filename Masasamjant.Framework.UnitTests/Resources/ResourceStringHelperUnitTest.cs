using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant.Resources
{
    public enum ResourceEnum : int
    {
        [ResourceString("Value0", typeof(PublicUnitTestResource))]
        Value0 = 0,
        [ResourceString("Value1", typeof(PublicUnitTestResource))]
        Value1 = 1,
        [ResourceString("Value2", typeof(PublicUnitTestResource))]
        Value2 = 2
    }

    [Flags]
    public enum ResourceFlags : int
    {
        None = 0,
        [ResourceString("Value1", typeof(PublicUnitTestResource))]
        Value1 = 1,
        [ResourceString("Value2", typeof(PublicUnitTestResource))]
        Value2 = 2,
        [ResourceString("Value4", typeof(PublicUnitTestResource))]
        Value4 = 4,
        [ResourceString("Value8", typeof(PublicUnitTestResource))]
        Value8 = 8
    }

    [TestClass]
    public class ResourceStringHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetResourceString_Of_Type()
        {
            Assert.AreEqual(null, ResourceHelper.GetResourceString(typeof(DateTime)));
            Assert.AreEqual("User", ResourceHelper.GetResourceString(typeof(TestUser)));
        }

        [TestMethod]
        public void Test_GetResourceStringOrName_Of_Type()
        {
            Assert.AreEqual("DateTime", ResourceHelper.GetResourceStringOrName(typeof(DateTime)));
            Assert.AreEqual("User", ResourceHelper.GetResourceStringOrName(typeof(TestUser)));
        }

        [TestMethod]
        public void Test_GetResourceString_Of_Property()
        {
            var ageProperty = typeof(TestUser).GetProperty("Age");
            var nameProperty = typeof(TestUser).GetProperty("Name");
            Assert.AreEqual(null, ResourceHelper.GetResourceString(ageProperty!));
            Assert.AreEqual("Name", ResourceHelper.GetResourceString(nameProperty!));
        }

        [TestMethod]
        public void Test_GetResourceString_Of_Field()
        {
            var ageField = typeof(TestUser).GetField("age", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField);
            var nameField = typeof(TestUser).GetField("name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField);
            Assert.AreEqual(null, ResourceHelper.GetResourceString(ageField!));
            Assert.AreEqual("Name", ResourceHelper.GetResourceString(nameField!));
        }

        [TestMethod]
        public void Test_GetResourceString_Of_Enum()
        {
            Assert.AreEqual(null, ResourceHelper.GetResourceString(DateTimeKind.Local));
            Assert.AreEqual("Value 0", ResourceHelper.GetResourceString(ResourceEnum.Value0));
            Assert.AreEqual("Value 1,Value 2,Value 4", ResourceHelper.GetResourceString(ResourceFlags.None | ResourceFlags.Value1 | ResourceFlags.Value2 | ResourceFlags.Value4));
            Assert.AreEqual(null, ResourceHelper.GetResourceString(ResourceFlags.None));
        }

        [TestMethod]
        public void Test_GetResourceStringOrName_Of_Enum()
        {
            Assert.AreEqual("Local", ResourceHelper.GetResourceStringOrName(DateTimeKind.Local));
            Assert.AreEqual("Value 0", ResourceHelper.GetResourceStringOrName(ResourceEnum.Value0));
            Assert.AreEqual("Value 1,Value 2,Value 4", ResourceHelper.GetResourceStringOrName(ResourceFlags.None | ResourceFlags.Value1 | ResourceFlags.Value2 | ResourceFlags.Value4));
            Assert.AreEqual("None", ResourceHelper.GetResourceStringOrName(ResourceFlags.None));
        }
    }
}
