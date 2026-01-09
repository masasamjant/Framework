namespace Masasamjant
{
    [TestClass]
    public class StringArgumentExceptionUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var exception = new StringArgumentException("Message", "Value", "Name");
            Assert.AreEqual("Message (Parameter 'Name')", exception.Message);
            Assert.AreEqual("Name", exception.ParamName);
            Assert.AreEqual("Value", exception.ParamValue);
            Assert.IsNull(exception.InnerException);

            var inner = new InvalidOperationException();
            exception = new StringArgumentException("Message", "Value", "Name", inner);
            Assert.AreEqual("Message (Parameter 'Name')", exception.Message);
            Assert.AreEqual("Name", exception.ParamName);
            Assert.AreEqual("Value", exception.ParamValue);
            Assert.AreSame(inner, exception.InnerException);
        }
    }
}
