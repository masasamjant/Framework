using Masasamjant.Stubs;
using System.Reflection;

namespace Masasamjant.Reflection
{
    [TestClass]
    public class TypeLoaderUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_TryLoadType_From_Assembly()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var typeLoader = new TypeLoader();
            Assert.ThrowsException<ArgumentException>(() => typeLoader.TryLoadType(assembly, string.Empty));
            Assert.ThrowsException<ArgumentException>(() => typeLoader.TryLoadType(assembly, "  "));
            var result = typeLoader.TryLoadType(assembly, "NotExist");
            Assert.IsTrue(result.NotFound);

            result = typeLoader.TryLoadType(assembly, typeof(UnitTest).FullName!);
            Assert.IsTrue(result.IsLoaded);
            Assert.AreEqual(typeof(UnitTest), result.Type);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsNull(result.Exception);

            typeLoader = new ErrorTypeLoader();
            result = typeLoader.TryLoadType(assembly, typeof(UnitTest).FullName!);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNull(result.Type);
            Assert.IsTrue(result.IsFaulted);
            Assert.IsNotNull(result.Exception);
        }
    }
}
