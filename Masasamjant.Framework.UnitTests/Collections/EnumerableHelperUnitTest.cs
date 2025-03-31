namespace Masasamjant.Collections
{
    [TestClass]
    public class EnumerableHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsNullOrEmpty()
        {
            IEnumerable<int>? source = null;
            Assert.IsTrue(EnumerableHelper.IsNullOrEmpty(source));
            source = new List<int>();
            Assert.IsTrue(EnumerableHelper.IsNullOrEmpty(source));
            source = new List<int>() { 1, 2 };
            Assert.IsFalse(EnumerableHelper.IsNullOrEmpty(source));
        }

        [TestMethod]
        public void Test_ForEach()
        {
            var source = new List<int>() { 1, 2, 3 };
            var result = new List<int>();
            var action = new Action<int>(result.Add);
            EnumerableHelper.ForEach(source, action);
            CollectionAssert.AreEqual(source, result);

            result.Clear();
            Func<int, bool> match = item => item > 1;
            EnumerableHelper.ForEach(source, match, action);
            CollectionAssert.AreEqual(new int[] { 2, 3 }, result);
        }

        [TestMethod]
        public void Test_ContainsAny()
        {
            var source = new List<int>() { 1, 2, 3 };
            Assert.IsFalse(EnumerableHelper.ContainsAny(source, new int[] { 4, 5, 6 }));
            Assert.IsTrue(EnumerableHelper.ContainsAny(source, new int[] { 4, 5, 3 }));
        }

        [TestMethod]
        public void Test_ContainsAll()
        {
            var source = new List<int>() { 1, 2, 3 };
            Assert.IsFalse(EnumerableHelper.ContainsAll(source, new int[] { 1, 2, 6 }));
            Assert.IsTrue(EnumerableHelper.ContainsAll(source, new int[] { 1, 2, 3 }));
        }

        [TestMethod]
        public void Test_Batch()
        {
            var source = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var size = -1;
            var batches = EnumerableHelper.Batch(source, size);
            Assert.AreEqual(0, batches.Count());
            size = 0;
            batches = EnumerableHelper.Batch(source, size);
            Assert.AreEqual(0, batches.Count());
            size = 2;
            batches = EnumerableHelper.Batch(source, size);
            Assert.AreEqual(5, batches.Count());
            int f = 1;
            int s = 2;
            foreach (var batch in batches)
            {
                CollectionAssert.AreEqual(new int[] { f, s }, batch.ToArray());
                f += 2;
                s += 2;
            }
            size = 3;
            batches = EnumerableHelper.Batch(source, size);
            Assert.AreEqual(4, batches.Count());
            f = 1;
            s = 2;
            foreach (var batch in batches)
            {
                var arr = batch.ToArray();
                if (arr.Length == 3)
                {
                    CollectionAssert.AreEqual(new int[] { f, s, s + 1 }, arr);
                    f += 3;
                    s += 3;
                }
                else
                    CollectionAssert.AreEqual(new int[] { f }, arr);
            }
        }
    }
}
