using Masasamjant.Plugins.Runtime;

namespace Masasamjant.Plugins
{
    [TestClass]
    public class PluginLoadContextUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var info = new PluginInfo(PluginAssemblyPath, PluginTypeName);
            var context = new PluginLoadContext(info);
            var assembly = context.Load();      
            Assert.IsNotNull(assembly);
            context.Unload();
        }
    }
}
