using Masasamjant.Plugins.Runtime;

namespace Masasamjant.Plugins
{
    [TestClass]
    public class RuntimePluginFactoryUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CreatePlugin()
        {
            var info = new PluginInfo(PluginAssemblyPath, PluginTypeName);
            var plugin = RuntimePluginFactory.CreatePlugin(info);
            Assert.IsNotNull(plugin);
            Assert.IsInstanceOfType(plugin, typeof(IRuntimePlugin));
        }
    }
}
