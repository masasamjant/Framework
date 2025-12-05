namespace Masasamjant
{
    [TestClass]
    public class CharacterMapUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Contructor()
        {
            var map = new CharacterMap();
            Assert.AreEqual(0, map.Count);
            Assert.IsFalse(map.Sources.Any());
            Assert.IsFalse(map.Destinations.Any());
            var characters = new Dictionary<char, char>()
            {
                { '#', 'A' }, { '&', 'B' }, { '%' , 'C' }
            };
            map = new CharacterMap(characters);
            Assert.AreEqual(3, map.Count);
            CollectionAssert.AreEqual(new[] { '#', '&', '%' }, map.Sources.ToArray());
            CollectionAssert.AreEqual(new[] { 'A', 'B', 'C' }, map.Destinations.ToArray());
            var expected = new[]
            {
                new CharacterMapping('#', 'A'),
                new CharacterMapping('&', 'B'),
                new CharacterMapping('%', 'C')
            };
            var actual = map.Mappings.ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Construtor_Multi_Destination_Character_Mapping()
        {
            var characters = new Dictionary<char, char>()
            {
                { '#', 'A' }, { '%', 'A' }
            };
            new CharacterMap(characters);
        }

        [TestMethod]
        public void Test_Add()
        {
            var map = new CharacterMap();
            var mapping = map.Add('A', 'B');
            Assert.AreEqual(1, map.Count);
            Assert.AreEqual(mapping, new CharacterMapping('A', 'B'));
            var next = map.Add('A', 'B');
            Assert.AreEqual(mapping, next);
            Assert.AreEqual(1, map.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CharacterMappingException))]

        public void Test_Add_Source_Already_Source()
        {
            var map = new CharacterMap();
            map.Add('A', 'X');
            map.Add('A', 'Z');
        }

        [TestMethod]
        [ExpectedException(typeof(CharacterMappingException))]
        public void Test_Add_Destination_Already_Destination()
        {
            var map = new CharacterMap();
            map.Add('A', 'X');
            map.Add('F', 'X');
        }

        [TestMethod]
        public void Clear()
        {
            var map = new CharacterMap();
            var mapping = map.Add('A', 'B');
            Assert.AreEqual(1, map.Count);
            map.Clear();
            Assert.AreEqual(0, map.Count);
        }

        [TestMethod]
        public void Test_Remove()
        {
            var map = new CharacterMap();
            var mapping = map.Add('A', 'B');
            Assert.IsFalse(map.Remove('A', 'C'));
            Assert.IsTrue(map.Remove(mapping));
        }

        [TestMethod]
        public void Test_Contains()
        {
            var map = new CharacterMap();
            var mapping = map.Add('A', 'B');
            Assert.IsFalse(map.Contains('A', 'C'));
            Assert.IsTrue(map.Contains(mapping));
        }

        [TestMethod]
        public void Test_GetMapping()
        {
            var map = new CharacterMap();
            map.Add('A', 'X');
            var mapping = map.GetMapping('B');
            Assert.IsTrue(mapping == null);
            var a = map.GetMapping('A');
            var x = map.GetMapping('X');
            Assert.AreEqual(a, x);
        }

        [TestMethod]
        public void Test_ReadOnly()
        {
            var map = new CharacterMap();
            Assert.IsFalse(map.IsReadOnly);
            map.Add('A', 'B');
            map.SetReadOnly();
            Assert.IsTrue(map.IsReadOnly);
            Assert.ThrowsException<InvalidOperationException>(() => map.Add('C', 'D'));
            Assert.ThrowsException<InvalidOperationException>(() => map.Clear());
            Assert.ThrowsException<InvalidOperationException>(() => map.Remove('A', 'B'));
        }

        [TestMethod]
        public void Test_GetDestination()
        {
            var map = new CharacterMap(new Dictionary<char, char>() { { 'A', '@' } });
            var dest = map.GetDestination('A');
            Assert.AreEqual('@', dest);
            dest = map.GetDestination('@');
            Assert.IsFalse(dest.HasValue);
            dest = map.GetDestination('H');
            Assert.IsFalse(dest.HasValue);
        }

        [TestMethod]
        public void Test_GetSource()
        {
            var map = new CharacterMap(new Dictionary<char, char>() { { 'A', '@' } });
            var dest = map.GetSource('A');
            Assert.IsFalse(dest.HasValue);
            dest = map.GetSource('@');
            Assert.AreEqual('A', dest);
            dest = map.GetSource('H');
            Assert.IsFalse(dest.HasValue);
        }

        [TestMethod]
        public void Test_ToDictionary()
        {
            var map = new CharacterMap(new Dictionary<char, char>() 
            { 
                { 'A', '@' },
                { 'B', '#' }
            });
            var dictionary = map.ToDictionary();
            Assert.IsTrue(dictionary.Count == 2);
            Assert.AreEqual('@', dictionary['A']);
            Assert.AreEqual('#', dictionary['B']);

            map = new CharacterMap();
            dictionary = map.ToDictionary();
            Assert.IsTrue(dictionary.Count == 0);
        }

        [TestMethod]
        public void Test_Clone()
        {
            var map = new CharacterMap(new Dictionary<char, char>()
            {
                { 'A', '@' },
                { 'B', '#' }
            });
            map.SetReadOnly();
            var clone = map.Clone();
            Assert.AreNotSame(map, clone);
            Assert.IsFalse(clone.IsReadOnly);
            CollectionAssert.AreEqual(map.Mappings.ToArray(), clone.Mappings.ToArray());
        }
    }
}
