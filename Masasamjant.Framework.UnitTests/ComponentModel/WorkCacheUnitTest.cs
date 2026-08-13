namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class WorkCacheUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_When_Work_Function_Null_Then_Throw_Exception()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new WorkCache<int, int>(null!));
        }

        [TestMethod]
        public void Test_When_Work_Not_Cached_Then_Perform_Work()
        {
            int workerCallCount = 0;
            Func<int, int> worker = input =>
            {
                workerCallCount++;
                return input * 2;
            };
            var cache = new WorkCache<int, int>(worker);
            int result1 = cache.Perform(5);
            Assert.AreEqual(10, result1);
            Assert.AreEqual(1, workerCallCount);
            int result2 = cache.Perform(5);
            Assert.AreEqual(10, result2);
            Assert.AreEqual(1, workerCallCount);
        }

        [TestMethod]
        public void Test_When_Optional_Key_Provided_Then_Cache_Separately()
        {
            int workerCallCount = 0;
            Func<int, int> worker = input =>
            {
                workerCallCount++;
                return input * 2;
            };
            var cache = new WorkCache<int, int>(worker);
            int result1 = cache.Perform(5, "key1");
            Assert.AreEqual(10, result1);
            Assert.AreEqual(1, workerCallCount);
            int result2 = cache.Perform(5, "key2");
            Assert.AreEqual(10, result2);
            Assert.AreEqual(2, workerCallCount);
        }

        [TestMethod]
        public void Test_When_Cleared_Then_Removes_Cache_Results()
        {
            int workerCallCount = 0;
            Func<int, int> worker = input =>
            {
                workerCallCount++;
                return input * 2;
            };
            var cache = new WorkCache<int, int>(worker);
            int result1 = cache.Perform(5);
            Assert.AreEqual(10, result1);
            Assert.AreEqual(1, workerCallCount);
            cache.ClearCache();
            int result2 = cache.Perform(5);
            Assert.AreEqual(10, result2);
            Assert.AreEqual(2, workerCallCount);
        }
    }
}
