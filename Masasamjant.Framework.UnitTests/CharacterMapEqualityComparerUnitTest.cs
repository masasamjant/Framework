namespace Masasamjant
{
    [TestClass]
    public class CharacterMapEqualityComparerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetHashCode()
        {
            var comparer = new CharacterMapEqualityComparer();
            var a = new CharacterMap();
            var b = new CharacterMap();
            Assert.AreEqual(comparer.GetHashCode(a), comparer.GetHashCode(b));

            a.Add('A', '1');
            a.Add('B', '2');
            b.Add('A', '1');
            b.Add('B', '2');
            Assert.AreEqual(comparer.GetHashCode(a), comparer.GetHashCode(b));
        }

        [TestMethod]
        public void Test_Equals()
        {
            var comparer = new CharacterMapEqualityComparer();  
            CharacterMap? a = null;
            CharacterMap? b = null;
            Assert.IsTrue(comparer.Equals(a, b));
            
            a = new CharacterMap();
            Assert.IsFalse(comparer.Equals(a, b));

            a = null;
            b = new CharacterMap();
            Assert.IsFalse(comparer.Equals(a, b));

            a = new CharacterMap();
            b = new CharacterMap();
            Assert.IsTrue(comparer.Equals(a, b));

            a.Add('A', '1');
            Assert.IsFalse(comparer.Equals(a, b));

            b.Add('A', '1');
            Assert.IsTrue(comparer.Equals(a, b));

            b.Add('B', '2');
            Assert.IsFalse (comparer.Equals(a, b));

            b = new AsciiCharacterMap();
            b.Add('A', '1');
            Assert.IsTrue(comparer.Equals(a, b));

            a.Add('B', '2');
            b.Add('C', '3');
            Assert.IsFalse(comparer.Equals(a, b));
        }
    }
}
