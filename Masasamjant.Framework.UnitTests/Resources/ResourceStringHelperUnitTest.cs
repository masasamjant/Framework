using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant.Resources
{
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
    }
}
