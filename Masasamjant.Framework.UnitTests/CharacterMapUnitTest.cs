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
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Construtor_Multi_Character_Mapping()
        {
            var characters = new Dictionary<char, char>()
            {
                { '#', 'A' }, { 'A', 'U' }
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
        [ExpectedException(typeof(ArgumentException))]

        public void Test_Add_Source_Already_Source()
        {
            var map = new CharacterMap();
            map.Add('A', 'X');
            map.Add('A', 'Z');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Add_Source_Already_Destination()
        {
            var map = new CharacterMap();
            map.Add('A', 'X');
            map.Add('X', 'Z');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Add_Destination_Already_Source()
        {
            var map = new CharacterMap();
            map.Add('A', 'X');
            map.Add('F', 'A');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
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
    }
}
