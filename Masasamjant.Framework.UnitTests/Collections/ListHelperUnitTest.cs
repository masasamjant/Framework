namespace Masasamjant.Collections
{
    [TestClass]
    public class ListHelperUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Merge_When_Destination_Read_Only()
        {
            var destination = new List<int>().AsReadOnly();
            var sources = Enumerable.Empty<IList<int>>();
            ListHelper.Merge(destination, sources);
        }

        [TestMethod]
        public void Test_Merge()
        {
            var destination = new List<int>() { 1, 4, 6, 7, 9 };
            var sources = new List<IList<int>>()
            {
                destination,
                new List<int>() { 1, 2, 3, 4 },
                new List<int>() { 4, 5, 6 },
                new List<int>() { 6, 7, 8 }
            };
            ListHelper.Merge(destination, sources);
            int[] expected = { 1, 4, 6, 7, 2, 3, 5, 8 };
            CollectionAssert.AreEqual(expected, destination);
        }

        [TestMethod]
        public void Test_SplitByIndex()
        {
            var list = new List<int>();

            // List is empty.
            var result = ListHelper.SplitByIndex(list, 1);
            Assert.AreEqual(0, result.Count());
            
            list.Add(1);
            
            // List contains single item.
            result = ListHelper.SplitByIndex(list, 0);
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(ReferenceEquals(result.First(), list));
            Assert.IsTrue(result.Last().Count == 0);
            
            list.Add(2);

            // Split index is under range.
            result = ListHelper.SplitByIndex(list, -1);
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(ReferenceEquals(result.First(), list));
            Assert.IsTrue(result.Last().Count == 0);

            // Split index is over range.
            result = ListHelper.SplitByIndex(list, 6);
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(ReferenceEquals(result.First(), list));
            Assert.IsTrue(result.Last().Count == 0);

            list.Add(3);

            // First  list contains items to index and second list contains items after index.
            result = ListHelper.SplitByIndex(list, 1);
            Assert.IsTrue(result.Count() == 2);
            CollectionAssert.AreEqual(new[] { 1, 2 }, result.First().ToArray());
            CollectionAssert.AreEqual(new[] { 3 }, result.Last().ToArray());
        }

        [TestMethod]
        public void Test_IterateForward()
        {
            var list = new List<int>() { 1, 2, 3 };
            var expected = new Iteration<int>[]
            {
                new Iteration<int>(1, 0),
                new Iteration<int>(2, 1),
                new Iteration<int>(3, 2)
            };
            var actual = ListHelper.IterateForward(list).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IterateBackward()
        {
            var list = new List<int>() { 1, 2, 3 };
            var expected = new Iteration<int>[]
            {
                new Iteration<int>(3, 2),
                new Iteration<int>(2, 1),
                new Iteration<int>(1, 0)
            };
            var actual = ListHelper.IterateBackward(list).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Keep_When_List_Is_Read_Only()
        {
            var list = new List<int>().AsReadOnly();
            Func<int, bool> keepPredicate = item => item > 5;
            ListHelper.Keep(list, keepPredicate);
        }

        [TestMethod]
        public void Test_Keep()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Func<int, bool> keepPredicate = item => item > 5;
            var expected = new List<int>() { 6, 7, 8, 9, 10 };
            ListHelper.Keep(list, keepPredicate);
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Remove_When_List_Is_Read_Only()
        {
            var list = new List<int>().AsReadOnly();
            Func<int, bool> removePredicate = item => item > 5;
            ListHelper.Remove(list, removePredicate);
        }

        [TestMethod]
        public void Test_Remove()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Func<int, bool> removePredicate = item => item > 5;
            var expected = new List<int>() { 1, 2, 3, 4, 5 };
            ListHelper.Remove(list, removePredicate);
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void Test_RemoveOrphants_Read_Only_Destination()
        {
            var destination = new List<int>().AsReadOnly();
            Assert.ThrowsException<ArgumentException>(() => ListHelper.RemoveOrphants(destination, new List<int>()));
            Assert.ThrowsException<ArgumentException>(() => ListHelper.RemoveOrphants(destination, new List<List<int>>()));
        }

        [TestMethod]
        public void Test_RemoveOrphants()
        {
            var destination = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var compare = new List<int>() { 1, 3, 5, 7, 9 };
            ListHelper.RemoveOrphants(destination, compare);
            CollectionAssert.AreEqual(compare, destination);

            var lists = new List<List<int>>()
            {
                new List<int>() { 1 },
                new List<int>() { 5 },
                new List<int>() { 9 },
            };

            ListHelper.RemoveOrphants(destination, lists);

            var expected = new Collection<int>() { 1, 5, 9 };

            CollectionAssert.AreEqual(expected, destination);

            ListHelper.RemoveOrphants(destination, new List<int>());

            Assert.IsTrue(destination.Count == 0);
        }

        [TestMethod]
        public void Test_AddDuplicate()
        {
            Assert.ThrowsException<ArgumentException>(() => ListHelper.AddDuplicate(new List<int>().AsReadOnly(), 1, DuplicateBehavior.Ignore));
            Assert.ThrowsException<ArgumentException>(() => ListHelper.AddDuplicate(new List<int>(), 1, (DuplicateBehavior)999));
            var list = new List<int>();
            
            ListHelper.AddDuplicate(list, 1, DuplicateBehavior.Error);
            Assert.IsTrue(list.Contains(1));
            
            Assert.ThrowsException<DuplicateItemException>(() => ListHelper.AddDuplicate(list, 1, DuplicateBehavior.Error));
            
            ListHelper.AddDuplicate(list, 1, DuplicateBehavior.Ignore);
            Assert.IsTrue(list.Count == 1);
            Assert.IsTrue(list.Contains(1));

            ListHelper.AddDuplicate(list, 1, DuplicateBehavior.Replace);
            Assert.IsTrue(list.Count == 1);
            Assert.IsTrue(list.Contains(1));

            ListHelper.AddDuplicate(list, 1, DuplicateBehavior.Insert);
            Assert.IsTrue(list.Count == 2);
            Assert.IsTrue(list.Contains(1));
        }
    }
}
