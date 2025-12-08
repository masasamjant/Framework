namespace Masasamjant.Collections
{
    [TestClass]
    public class QueueHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ForEachDequeue()
        {
            var q = new Queue<int>([1, 2, 3]);
            var items = new List<int>();
            var action = new Action<int>(items.Add);
            QueueHelper.ForEachDequeue(q, action);
            Assert.IsTrue(q.Count == 0);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, items);
        }

        [TestMethod]
        public void Test_EnqueueRange()
        {
            var q = new Queue<int>();
            var items = new[] { 1, 2, 3 };
            QueueHelper.EnqueueRange(q, items);
            Assert.IsTrue(q.Count == 3);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, q.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_DequeueRange_When_Count_Is_Negative()
        {
            var q = new Queue<int>();
            QueueHelper.DequeueRange(q, -1);
        }

        [TestMethod]
        public void Test_DequeueRange()
        {
            var q = new Queue<int>([1, 2, 3, 4, 5]);

            // Count is 0, dequeue nothing.
            var items = QueueHelper.DequeueRange(q, 0);
            Assert.IsFalse(items.Any());
            Assert.IsTrue(q.Count == 5);

            // No count, dequeue all.
            items = QueueHelper.DequeueRange(q);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, items.ToArray());
            Assert.IsTrue(q.Count == 0);

            // Dequeue specified count
            q = new Queue<int>([1, 2, 3, 4, 5]);
            items = QueueHelper.DequeueRange(q, 3);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, items.ToArray());
            CollectionAssert.AreEqual(new[] { 4, 5 }, q.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Queue_Split_When_Size_Less_Than_One()
        {
            var q = new Queue<int>();
            QueueHelper.Split(q, 0);
        }

        [TestMethod]
        public void Test_Split()
        {
            var q = new Queue<int>();
            var sq = QueueHelper.Split(q, 1);
            Assert.IsFalse(sq.Any());
            q.EnqueueRange(new int[] { 1, 2, 3 });
            sq = QueueHelper.Split(q, 4);
            Assert.IsTrue(sq.Count() == 1);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, sq.First().ToArray());
            Assert.IsTrue(q.Count == 0);
            q.EnqueueRange(new int[] { 1, 2, 3 });
            sq = QueueHelper.Split(q, 1);
            Assert.IsTrue(sq.Count() == 3);
            int n = 1;
            foreach (var x in sq)
            {
                CollectionAssert.AreEqual(new[] { n }, x.ToArray());
                n++;
            }
        }

        [TestMethod]
        public void Test_DequeueUntil()
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            Predicate<int> stopPredicate = item => item == 3;

            var expected = new int[] { 1, 2 };
            var actual = QueueHelper.DequeueUntil(queue, stopPredicate).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            expected = new int[] { 3, 4 };
            actual = QueueHelper.DequeueUntil(queue, 5).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_EnqueueMatching()
        {
            var queue = new Queue<int>();
            var items = new List<int>() { 1, 2, 3, 4, 5 };
            Func<int, bool> enqueuePredicate = item => item >= 3;
            QueueHelper.EnqueueMatches(queue, items, enqueuePredicate);
            var expected = new int[] { 3, 4, 5 };
            var actual = queue.ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
