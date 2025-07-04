namespace Masasamjant.Reflection
{
    [TestClass]
    public class ThisTypeUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentException>(() => new ThisType(typeof(ThisType)));
            Assert.ThrowsException<ArgumentException>(() => new ThisType(typeof(NullType)));
            var thisType = new ThisType(typeof(string));
            Assert.AreEqual(typeof(string), thisType.GetActualType());
        }

        [TestMethod]
        public void Test_Equals()
        {
            Type? type;
            var thisType = new ThisType(typeof(string));
            var other = new ThisType(typeof(string));
            type = other;
            Assert.IsTrue(thisType.Equals(other));
            Assert.IsTrue(thisType.Equals(type));
            Assert.IsFalse(thisType.Equals(typeof(string)));

            other = new ThisType(typeof(Random));
            type = other;
            Assert.IsFalse(thisType.Equals(other));
            Assert.IsFalse(thisType.Equals(type));
            Assert.IsFalse(thisType.Equals(typeof(string)));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var thisType = new ThisType(typeof(string));
            var other = new ThisType(typeof(string));
            Assert.IsTrue(thisType.Equals(other) && thisType.GetHashCode() == other.GetHashCode());
        }
    }
}
