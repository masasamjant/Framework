namespace Masasamjant
{
    [TestClass]
    public class GuidGeneratorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GenerateValue()
        {
            var generator = new GuidGenerator();
            var value = generator.GenerateValue();
            Assert.AreNotEqual(Guid.Empty, value);  
        }

        [TestMethod]
        public void Test_GenerateValues()
        {
            var generator = new GuidGenerator();
            var values = generator.GenerateValues(10);
            Assert.AreEqual(10, values.Count());
            var first = values.First();
            Assert.IsTrue(values.All(v => v != Guid.Empty));
            Assert.IsTrue(values.Skip(1).All(v => v != first));
            
            var list = new List<Guid>(10);
            generator.GenerateValues(list, 10);
            Assert.AreEqual(10, list.Count);
            first = list.First();
            Assert.IsTrue(list.All(v => v != Guid.Empty));
            Assert.IsTrue(list.Skip(1).All(v => v != first));
        }
    }
}
