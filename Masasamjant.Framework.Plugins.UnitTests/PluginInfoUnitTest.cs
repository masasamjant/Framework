using Masasamjant.Plugins.Runtime;

namespace Masasamjant.Plugins
{
    [TestClass]
    public class PluginInfoUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var info = new PluginInfo(PluginAssemblyPath, PluginTypeName);
            Assert.AreEqual(PluginAssemblyPath, info.AssemblyFilePath);
            Assert.AreEqual(PluginTypeName, info.PluginTypeName);

            Assert.ThrowsExactly<ArgumentNullException>(() => new PluginInfo(null!, PluginTypeName));
            Assert.ThrowsExactly<ArgumentNullException>(() => new PluginInfo("", PluginTypeName));
            Assert.ThrowsExactly<ArgumentNullException>(() => new PluginInfo("  ", PluginTypeName));
            Assert.ThrowsExactly<ArgumentNullException>(() => new PluginInfo(PluginAssemblyPath, null!));
            Assert.ThrowsExactly<ArgumentNullException>(() => new PluginInfo(PluginAssemblyPath, ""));
            Assert.ThrowsExactly<ArgumentNullException>(() => new PluginInfo(PluginAssemblyPath, "  "));
        }

        [TestMethod]
        public void Test_GetAssemblyName()
        {
            var info = new PluginInfo(PluginAssemblyPath, PluginTypeName);
            var assemblyName = info.GetAssemblyName();
            Assert.IsNotNull(assemblyName);
            Assert.AreEqual("Masasamjant.Framework.Plugins.Core", assemblyName.Name);

            var tempFile = Path.GetTempFileName();
            info = new PluginInfo(tempFile, PluginTypeName);
            Assert.ThrowsExactly<InvalidOperationException>(() => info.GetAssemblyName());
            File.Delete(tempFile);
        }
    }
}
