namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlSerializationExceptionUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var instance = new object();
            var message = "Testing";
            var inner = new InvalidOperationException();
            var exception = new XmlSerializationException(instance, message, inner);
            Assert.AreSame(instance, exception.Instance);
            Assert.AreEqual(message, exception.Message);
            Assert.AreSame(inner, exception.InnerException);

            exception = new XmlSerializationException(instance, message);
            Assert.AreSame(instance, exception.Instance);
            Assert.AreEqual(message, exception.Message);
            Assert.IsNull(exception.InnerException);
        }
    }
}
