namespace Masasamjant.Reflection
{
    [TestClass]
    public class TypeLoadResultUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructors()
        {
            var result = new TypeLoadResult();
            Assert.IsTrue(result.NotFound);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNull(result.Type);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsNull(result.Exception);

            result = new TypeLoadResult(typeof(string));
            Assert.IsFalse(result.NotFound);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsTrue(result.IsLoaded);
            Assert.IsNotNull(result.Type);
            Assert.IsNull(result.Exception);

            result = new TypeLoadResult(new InvalidOperationException());
            Assert.IsFalse(result.NotFound);
            Assert.IsTrue(result.IsFaulted);
            Assert.IsNull(result.Type);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNotNull(result.Exception);
        }
    }
}
