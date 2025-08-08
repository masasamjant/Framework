using Masasamjant.Stubs;
using System.Net.WebSockets;
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

        [TestMethod]
        public void Test_TryLoadType_From_AssemblyFile()
        {
            var assemblyFile = new AssemblyFile(Assembly.GetExecutingAssembly().Location);
            var typeLoader = new TypeLoader();
            Assert.ThrowsException<ArgumentException>(() => typeLoader.TryLoadType(assemblyFile, string.Empty));
            Assert.ThrowsException<ArgumentException>(() => typeLoader.TryLoadType(assemblyFile, "  "));
            var result = typeLoader.TryLoadType(assemblyFile, "NotExist");
            Assert.IsTrue(result.NotFound);

            result = typeLoader.TryLoadType(assemblyFile, typeof(UnitTest).FullName!);
            Assert.IsTrue(result.IsLoaded);
            Assert.AreEqual(typeof(UnitTest), result.Type);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsNull(result.Exception);

            typeLoader = new ErrorTypeLoader();
            result = typeLoader.TryLoadType(assemblyFile, typeof(UnitTest).FullName!);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNull(result.Type);
            Assert.IsTrue(result.IsFaulted);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public void Test_TryLoadType_From_AppDomain()
        {
            var domain = AppDomain.CurrentDomain;
            var typeLoader = new TypeLoader();
            Assert.ThrowsException<ArgumentException>(() => typeLoader.TryLoadType(domain, string.Empty));
            Assert.ThrowsException<ArgumentException>(() => typeLoader.TryLoadType(domain, "  "));
            var result = typeLoader.TryLoadType(domain, "NotExist");
            Assert.IsTrue(result.NotFound);

            result = typeLoader.TryLoadType(domain, typeof(UnitTest).FullName!);
            Assert.IsTrue(result.IsLoaded);
            Assert.AreEqual(typeof(UnitTest), result.Type);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsNull(result.Exception);

            typeLoader = new ErrorTypeLoader();
            result = typeLoader.TryLoadType(domain, typeof(UnitTest).FullName!);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNull(result.Type);
            Assert.IsTrue(result.IsFaulted);
            Assert.IsNotNull(result.Exception);
        }
    }
}
