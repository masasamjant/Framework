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

        [TestMethod]
        public void Test_Transfer()
        {
            var source = new Queue<int>([1, 2, 3]);
            var destination = QueueHelper.Transfer(source);
            Assert.IsTrue(source.Count == 0);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, destination.ToArray());
        }

        [TestMethod]
        public void Test_TransferTo_Queue()
        {
            var source = new Queue<int>([1, 2, 3]);
            var destination = new Queue<int>();
            QueueHelper.TransferTo(source, destination);
            Assert.IsTrue(source.Count == 0);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, destination.ToArray());
        }

        [TestMethod]
        public void Test_TransferTo_Collection()
        {
            var source = new Queue<int>([1, 2, 3]);
            var destination = new List<int>();
            QueueHelper.TransferTo(source, destination);
            Assert.IsTrue(source.Count == 0);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, destination.ToArray());
        }

        [TestMethod]
        public void Test_TransferTo_Stack()
        {
            var source = new Queue<int>([1, 2, 3]);
            var destination = new Stack<int>();
            QueueHelper.TransferTo(source, destination);
            Assert.IsTrue(source.Count == 0);
            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, destination.ToArray());
        }

        [TestMethod]
        public void Test_TransferTo_Selected_To_Queue()
        {
            var source = new Queue<int>([1, 2, 3, 4, 5]);
            var destination = new Queue<int>();
            Func<int, bool> predicate = item => item % 2 == 0;
            QueueHelper.TransferTo(source, destination, predicate);
            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, source.ToArray());
            CollectionAssert.AreEqual(new[] { 2, 4 }, destination.ToArray());
        }

        [TestMethod]
        public void Test_TransferTo_Selected_To_Collection()
        {
            var source = new Queue<int>([1, 2, 3, 4, 5]);
            var destination = new List<int>();
            Func<int, bool> predicate = item => item % 2 == 0;
            QueueHelper.TransferTo(source, destination, predicate);
            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, source.ToArray());
            CollectionAssert.AreEqual(new[] { 2, 4 }, destination.ToArray());
        }

        [TestMethod]
        public void Test_TransferTo_Selected_To_Stack()
        {
            var source = new Queue<int>([1, 2, 3, 4, 5]);
            var destination = new Stack<int>();
            Func<int, bool> predicate = item => item % 2 == 0;
            QueueHelper.TransferTo(source, destination, predicate);
            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, source.ToArray());
            CollectionAssert.AreEqual(new[] { 4, 2 }, destination.ToArray());
        }

        [TestMethod]
        public void Test_Requeue_With_InsertBefore_InsertAfter()
        {
            var queue = new Queue<int>([2, 3, 4]);
            var insertBefore = new int[] { 1 };
            var insertAfter = new int[] { 5 };
            QueueHelper.Requeue(queue, insertBefore, insertAfter);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, queue.ToArray());
        }

        [TestMethod]
        public void Test_Create()
        {
            Assert.ThrowsException<ArgumentNullException>(() => QueueHelper.Create((IDictionary<int, int>)null!));

            var items = new Dictionary<int, int>()
            {
                { 0, 1 },
            };

            Assert.ThrowsException<ArgumentException>(() => QueueHelper.Create(items));
            items.Clear();
            items.Add(1, 1);
            items.Add(2, 2);
            items.Add(3, 3);
            var queue = QueueHelper.Create(items);
            Assert.AreEqual(3, queue.Count);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, queue.ToArray());
        }

        [TestMethod]
        public void Test_Requeue_With_Specified_Items()
        {
            var queue = new Queue<int>();
            var insertItems = new Dictionary<int, int>();
            QueueHelper.Requeue(queue, insertItems);
            Assert.IsTrue(queue.Count == 0);
            insertItems.Add(1, 1);
            insertItems.Add(3, 2);
            insertItems.Add(8, 8);
            QueueHelper.Requeue(queue, insertItems);
            CollectionAssert.AreEqual(new[] { 1, 2, 8 }, queue.ToArray());
            queue.Clear();
            queue.Enqueue(5);
            queue.Enqueue(6);
            QueueHelper.Requeue(queue, insertItems);
            CollectionAssert.AreEqual(new[] { 1, 5, 2, 6, 8 }, queue.ToArray());
        }

        [TestMethod]
        public void Test_Requeue_With_Provided_Positions()
        {
            var queue = new Queue<int>();
            Func<int, int> positionProvider = item => 
            {
                if (item == 2)
                    return 3;

                if (item == 3)
                    return 2;

                return item;
            };

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            QueueHelper.Requeue(queue, positionProvider);
            CollectionAssert.AreEqual(new[] { 1, 3, 2 }, queue.ToArray());
        }

        [TestMethod]
        public void Test_GetPosition() 
        {
            var queue = new Queue<int>([1, 2, 3]);
            Assert.AreEqual(1, QueueHelper.GetPosition(queue, 1));
            Assert.AreEqual(2, QueueHelper.GetPosition(queue, 2));
            Assert.AreEqual(3, QueueHelper.GetPosition(queue, 3));
            Assert.AreEqual(0, QueueHelper.GetPosition(queue, 4));
        }

        [TestMethod]
        public void Test_GetPositions()
        {
            var queue = new Queue<int>([1, 2, 3]);
            var positions = QueueHelper.GetPositions(queue, null);
            Assert.IsTrue(positions[1] == 1);
            Assert.IsTrue(positions[2] == 2);
            Assert.IsTrue(positions[3] == 3);
            positions = QueueHelper.GetPositions(queue, [1, 3]);
            Assert.IsTrue(positions[1] == 1);
            Assert.IsTrue(positions[3] == 3);
        }

        [TestMethod]
        public void Test_EnqueueAfter()
        {
            var queue = new Queue<int>([1, 2, 3]);
            var items = new int[] { 0 };
            QueueHelper.EnqueueAfter(queue, 0, items); // 0 = insert as first item
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3 }, queue.ToArray());
            QueueHelper.EnqueueAfter(queue, 4, items); // 4 = total count, insert as last item
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 0 }, queue.ToArray());
            QueueHelper.EnqueueAfter(queue, 2, items); // 2 = insert after 2nd item, so this becomes 3rd item 
            CollectionAssert.AreEqual(new[] { 0, 1, 0, 2, 3, 0 }, queue.ToArray());
        }
    }
}
