namespace Masasamjant.Reflection
{
    [TestClass]
    public class AssemblyHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetAssemblyDirectory()
        {
            var assembly = GetType().Assembly;
            var path = AssemblyHelper.GetAssemblyDirectory(assembly);
            Assert.IsNotNull(path);
            Assert.AreEqual(Path.GetDirectoryName(assembly.Location), path);
        }
    }
}
