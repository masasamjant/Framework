namespace Masasamjant.Reflection
{
    [TestClass]
    public class NullTypeUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentException>(() => new NullType(typeof(ThisType)));
            Assert.ThrowsException<ArgumentException>(() => new NullType(typeof(NullType)));
            var nullType = new NullType(typeof(string));
            Assert.AreEqual(typeof(string), nullType.GetActualType());
        }

        [TestMethod]
        public void Test_Equals()
        {
            Type? type;
            var nullType = new NullType(typeof(string));
            var other = new NullType(typeof(string));
            type = other;
            Assert.IsTrue(nullType.Equals(other));
            Assert.IsTrue(nullType.Equals(type));
            Assert.IsFalse(nullType.Equals(typeof(string)));

            other = new NullType(typeof(Random));
            type = other;
            Assert.IsFalse(nullType.Equals(other));
            Assert.IsFalse(nullType.Equals(type));
            Assert.IsFalse(nullType.Equals(typeof(string)));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var nullType = new NullType(typeof(string));
            var other = new NullType(typeof(string));
            Assert.IsTrue(nullType.Equals(other) && nullType.GetHashCode() == other.GetHashCode());
        }
    }
}
