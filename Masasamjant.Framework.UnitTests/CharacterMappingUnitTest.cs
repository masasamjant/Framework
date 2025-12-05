namespace Masasamjant
{
    [TestClass]
    public class CharacterMappingUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Source_And_Destination_Cannot_Be_Same()
        {
            new CharacterMapping('a', 'a');
        }

        [TestMethod]
        public void Test_Construction()
        {
            var mapping = new CharacterMapping('A', 'T');
            Assert.AreEqual('A', mapping.Source);
            Assert.AreEqual('T', mapping.Destination);
        }

        [TestMethod]
        public void Test_Equality()
        {
            var mapping = new CharacterMapping('A', 'T');
            Assert.IsFalse(mapping.Equals(null));
            Assert.IsFalse(mapping.Equals(DateTime.Now));
            Assert.IsFalse(mapping.Equals(new CharacterMapping('A', 'B')));
            Assert.IsFalse(mapping.Equals(new CharacterMapping('H', 'T')));
            Assert.IsTrue(mapping.Equals(new CharacterMapping('A', 'T')));
            Assert.AreEqual(mapping.GetHashCode(), new CharacterMapping('A', 'T').GetHashCode());
        }

        [TestMethod]
        public void Test_Clone()
        {
            var mapping = new CharacterMapping('A', 'B');
            var clone = mapping.Clone();
            Assert.AreEqual(mapping.Source, clone.Source);
            Assert.AreEqual(mapping.Destination, clone.Destination);
        }
    }
}
