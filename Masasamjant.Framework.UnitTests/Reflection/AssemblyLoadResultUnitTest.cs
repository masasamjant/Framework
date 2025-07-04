using System.Reflection;

namespace Masasamjant.Reflection
{
    [TestClass]
    public class AssemblyLoadResultUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructors()
        {
            var result = new AssemblyLoadResult();
            Assert.IsTrue(result.NotFound);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNull(result.Assembly);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsNull(result.Exception);

            result = new AssemblyLoadResult(Assembly.GetExecutingAssembly());
            Assert.IsFalse(result.NotFound);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsTrue(result.IsLoaded);
            Assert.IsNotNull(result.Assembly);
            Assert.IsNull(result.Exception);

            result = new AssemblyLoadResult(new InvalidOperationException());
            Assert.IsFalse(result.NotFound);
            Assert.IsTrue(result.IsFaulted);
            Assert.IsNull(result.Assembly);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNotNull(result.Exception);
        }
    }
}
