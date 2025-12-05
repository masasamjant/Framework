using System.ComponentModel.DataAnnotations;

namespace Masasamjant.Collections
{
    [TestClass]
    public class StackHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ForEachPop()
        {
            var s = new Stack<int>([1, 2, 3]);
            var items = new List<int>();
            var action = new Action<int>(items.Add);
            StackHelper.ForEachPop(s, action);
            Assert.IsTrue(s.Count == 0);
            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, items);
        }

        [TestMethod]
        public void Test_PushRange()
        {
            var s = new Stack<int>();
            var items = new[] { 1, 2, 3 };
            StackHelper.PushRange(s, items);
            Assert.IsTrue(s.Count == 3);
            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, s.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_PopRange_When_Count_Is_Negative()
        {
            var s = new Stack<int>();
            StackHelper.PopRange(s, -1);
        }

        [TestMethod]
        public void Test_PopRange()
        {
            var s = new Stack<int>([1, 2, 3, 4, 5]);

            // Count is 0, dequeue nothing.
            var items = StackHelper.PopRange(s, 0);
            Assert.IsFalse(items.Any());
            Assert.IsTrue(s.Count == 5);

            // No count, dequeue all.
            items = StackHelper.PopRange(s);
            CollectionAssert.AreEqual(new[] { 5, 4, 3, 2, 1 }, items.ToArray());
            Assert.IsTrue(s.Count == 0);

            // Dequeue specified count
            s = new Stack<int>([1, 2, 3, 4, 5]);
            items = StackHelper.PopRange(s, 3);
            CollectionAssert.AreEqual(new[] { 5, 4, 3 }, items.ToArray());
            CollectionAssert.AreEqual(new[] { 2, 1 }, s.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Queue_Split_When_Size_Less_Than_One()
        {
            var s = new Stack<int>();
            StackHelper.Split(s, 0);
        }

        [TestMethod]
        public void Test_Split()
        {
            var stack = new Stack<int>();
            var splitStack = StackHelper.Split(stack, 1);
            Assert.IsFalse(splitStack.Any());

            // If size is more than stack size, then creates new stack to hold all.
            stack.PushRange(new int[] { 1, 2, 3 });
            splitStack = StackHelper.Split(stack, 4);
            Assert.IsTrue(splitStack.Count() == 1);
            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, splitStack.First().ToArray());
            Assert.IsTrue(stack.Count == 0);

            // If size is less than stack count, then split to several stacks.
            stack.PushRange(new int[] { 1, 2, 3 });
            splitStack = StackHelper.Split(stack, 1);
            Assert.IsTrue(splitStack.Count() == 3);
            int n = 3;
            foreach (var x in splitStack)
            {
                CollectionAssert.AreEqual(new[] { n }, x.ToArray());
                n--;
            }
        }

        [TestMethod]
        public void Test_PopUntil()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Predicate<int> stopPredicate = item => item == 3;

            var expected = new int[] { 5, 4 };
            var actual = StackHelper.PopUntil(stack, stopPredicate).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            expected = new int[] { 3, 2 };
            actual = StackHelper.PopUntil(stack, 1).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_PushMatching()
        {
            var stack = new Stack<int>();
            var items = new List<int>() { 1, 2, 3, 4, 5 };
            Func<int, bool> pushPredicate = item => item >= 3;
            StackHelper.PushMatches(stack, items, pushPredicate);
            var expected = new int[] { 5, 4, 3 };
            var actual = stack.ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
