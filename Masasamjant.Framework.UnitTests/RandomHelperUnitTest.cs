namespace Masasamjant
{
    [TestClass]
    public class RandomHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CreateRandom()
        {
            var random = RandomHelper.CreateRandom();
            Assert.IsNotNull(random);
        }

        [TestMethod]
        public void Test_GetString()
        {
            var random = RandomHelper.CreateRandom();
            string result = random.GetString(10);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Length);
            result = random.GetString(0);
            Assert.AreEqual(string.Empty, result);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => random.GetString(-1));
        }

        [TestMethod]
        public void Test_GetBytes()
        {
            var random = RandomHelper.CreateRandom();
            var result = random.GetBytes(10);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Length);
            result = random.GetBytes(0);
            Assert.AreEqual(0, result.Length);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => random.GetBytes(-1));
        }

        [TestMethod]
        public void Test_GetValue()
        {
            var random = RandomHelper.CreateRandom();
            var values = new List<int>();
            Assert.ThrowsException<ArgumentException>(() => RandomHelper.GetValue(random, values));
            values.Add(1);
            Assert.AreEqual(1, RandomHelper.GetValue(random, values));
            values.AddRange(new int[] { 2, 3, 4, 5, 6 });
            var result = RandomHelper.GetValue(random, values);
            Assert.IsTrue(values.Contains(result)); 
        }
    }
}
