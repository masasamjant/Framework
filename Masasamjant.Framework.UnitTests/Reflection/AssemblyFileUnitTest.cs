using Masasamjant.Stubs;
using System.Reflection;

namespace Masasamjant.Reflection
{
    [TestClass]
    public class AssemblyFileUnitTest : UnitTest
    {
        private static readonly string? filePath = Assembly.GetExecutingAssembly().Location;
        private static readonly string? notExistFilePath = Path.Combine(AssemblyHelper.GetAssemblyDirectory(Assembly.GetExecutingAssembly())!, "Not-Exists.dll");

        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new AssemblyFile(string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => new AssemblyFile("   "));
            Assert.ThrowsException<ArgumentException>(() => new AssemblyFile(Path.GetFileName(filePath)!));
            Assert.ThrowsException<NotSupportedException>(() => new AssemblyFile(Path.ChangeExtension(filePath, "txt")!));
            var assemblyFile = new AssemblyFile(filePath!);
            Assert.AreEqual(filePath, assemblyFile.FullAssemblyPath);
            Assert.AreEqual(Path.GetFileName(filePath), assemblyFile.FileName);
            Assert.AreEqual(Path.GetDirectoryName(filePath), assemblyFile.DirectoryPath);
            Assert.AreEqual(true, assemblyFile.Exists);
            Assert.AreEqual(AssemblyType.Library, assemblyFile.AssemblyType);
            Assert.AreEqual(false, assemblyFile.UseCache);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var assemblyFile = new AssemblyFile(filePath!);
            var other = new AssemblyFile(filePath!);
            Assert.IsTrue(assemblyFile.Equals(other));
            other = new AssemblyFile(notExistFilePath!);
            Assert.IsFalse(assemblyFile.Equals(other));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var assemblyFile = new AssemblyFile(filePath!);
            var other = new AssemblyFile(filePath!);
            Assert.IsTrue(assemblyFile.Equals(other) && assemblyFile.GetHashCode() == other.GetHashCode());
        }

        [TestMethod]
        public void Test_ToString()
        {
            var assemblyFile = new AssemblyFile(filePath!);
            Assert.AreEqual(filePath, assemblyFile.ToString());
        }

        [TestMethod]
        public void Test_TryLoad()
        {
            var assemblyFile = new AssemblyFile(filePath!);
            var result = assemblyFile.TryLoad();
            Assert.IsTrue(result.IsLoaded);
            Assert.IsFalse(result.IsFaulted);
            Assert.IsNotNull(result.Assembly);
            Assert.IsNull(result.Exception);
            var errorAssemblyFile = new ErrorAssemblyFile(filePath!);
            result = errorAssemblyFile.TryLoad();
            Assert.IsTrue(result.IsFaulted);
            Assert.IsFalse(result.IsLoaded);
            Assert.IsNotNull(result.Exception);
            Assert.IsNull(result.Assembly);
        }

        [TestMethod]
        public void Test_AssemblyCache()
        {
            bool invoked = false;
            Func<Assembly> getAssembly = () => {
                invoked = true;
                return Assembly.GetExecutingAssembly();
            };
            var assemblyFile = new AssemblyFile(filePath!);
            var result = AssemblyFile.AssemblyCache.TryGetAssembly(assemblyFile, getAssembly);
            Assert.IsTrue(invoked);
            Assert.IsTrue(result.IsLoaded);
            Assert.IsNotNull(result.Assembly);
            invoked = false;
            result = AssemblyFile.AssemblyCache.TryGetAssembly(assemblyFile, getAssembly);
            Assert.IsFalse(invoked);
            Assert.IsTrue(result.IsLoaded);
            Assert.IsNotNull(result.Assembly);
        }

        [TestMethod]
        public void Test_UseCache()
        {
            var assemblyFile = new AssemblyFile(filePath!);
            assemblyFile.UseCache = true;
            var result = assemblyFile.TryLoad();
            Assert.IsTrue(result.IsLoaded);
            Assert.IsNotNull(result.Assembly);
            result = assemblyFile.TryLoad();
            Assert.IsTrue(result.IsLoaded);
            Assert.IsNotNull(result.Assembly);
        }
    }
}
