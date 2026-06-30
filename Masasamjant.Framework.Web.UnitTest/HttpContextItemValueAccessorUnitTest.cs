namespace Masasamjant.Web
{
    [TestClass]
    public class HttpContextItemValueAccessorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_SetHttpValue_GetHttpValue()
        {
            string key = "key";
            string value = "value";
            var context = CreateHttpContext();
            var accessor = new HttpContextItemValueAccessor();
            accessor.SetHttpValue(context, key, value);
            var actual = accessor.GetHttpValue(context, key);
            Assert.AreEqual(value, actual);
        }

        [TestMethod]
        public void Test_GetHttpValue()
        {
            string key = "key";
            var context = CreateHttpContext();
            var accessor = new HttpContextItemValueAccessor();
            var value = accessor.GetHttpValue(context, key);
            Assert.IsNull(value);
            context.Items[key] = "value";
            value = accessor.GetHttpValue(context, key);
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void Test_SetHttpValue()
        {
            string key = "key";
            string value = "value";
            var context = CreateHttpContext();
            var accessor = new HttpContextItemValueAccessor();
            accessor.SetHttpValue(context, key, value);
            var actual = context.Items[key];
            Assert.AreEqual(value, actual);
        }
    }
}
