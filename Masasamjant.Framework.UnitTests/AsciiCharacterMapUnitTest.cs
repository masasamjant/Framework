namespace Masasamjant
{
    [TestClass]
    public class AsciiCharacterMapUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var values = new Dictionary<char, char>
            {
                { 'A', 'B' },
                { 'C', 'D' },
                { '1', '2' }
            };
            var map = new AsciiCharacterMap(values);
            Assert.AreEqual('B', map.GetDestination('A'));
            Assert.AreEqual('D', map.GetDestination('C'));
            Assert.AreEqual('2', map.GetDestination('1'));
            values.Add('E', 'Ä');
            Assert.ThrowsException<ArgumentException>(() => new AsciiCharacterMap(values));
            values.Remove('E');
            values.Add('Ä', 'E');
            Assert.ThrowsException<ArgumentException>(() => new AsciiCharacterMap(values));
        }

        [TestMethod]
        public void Test_AddMapping()
        {
            var map = new AsciiCharacterMap();
            var mapping = map.Add('A', 'B');
            Assert.IsTrue(map.Contains(mapping));
            mapping = map.Add('1', '2');
            Assert.IsTrue(map.Contains(mapping));
            Assert.ThrowsException<CharacterMappingException>(() => map.Add('E', 'Ä'));
            Assert.ThrowsException<CharacterMappingException>(() => map.Add('Ä', 'E'));
        }

        [TestMethod]
        public void Test_Clone()
        {
            var map = new AsciiCharacterMap(new Dictionary<char, char> 
            {
                { 'A', 'a' },
                { 'B', 'b'  }
            });
            map.SetReadOnly();
            var other = map.Clone();
            Assert.IsInstanceOfType<AsciiCharacterMap>(other);
            Assert.AreNotSame(map, other);
            Assert.IsFalse(other.IsReadOnly);
            CollectionAssert.AreEqual(map.Mappings.ToArray(), other.Mappings.ToArray());
        }
    }
}
