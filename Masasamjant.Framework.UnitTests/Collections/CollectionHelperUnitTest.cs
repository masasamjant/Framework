using System.Collections;

namespace Masasamjant.Collections
{
    [TestClass]
    public class CollectionHelperUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Merge_When_Destination_Read_Only()
        {
            var destination = new Collection<int>();
            destination.SetReadOnly();
            var sources = Enumerable.Empty<IList<int>>();
            CollectionHelper.Merge(destination, sources);
        }

        [TestMethod]
        public void Test_Merge()
        {
            var destination = new Collection<int>() { 1, 4, 6, 7, 9 };
            var sources = new List<Collection<int>>()
            {
                destination,
                new Collection<int>() { 1, 2, 3, 4 },
                new Collection<int>() { 4, 5, 6 },
                new Collection<int>() { 6, 7, 8 }
            };
            CollectionHelper.Merge(destination, sources);
            int[] expected = { 1, 4, 6, 7, 2, 3, 5, 8 };
            CollectionAssert.AreEqual(expected, destination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Combine_When_Duplicate_Behavior_Undefined()
        {
            var collections = new List<List<int>>();
            DuplicateBehavior duplicateBehavior = (DuplicateBehavior)999;
            CollectionHelper.Combine(collections, duplicateBehavior);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Combine_When_Max_Item_Count_Less_Than_One()
        {
            var collections = new List<List<int>>();
            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Insert;
            int maxItemCount = 0;
            CollectionHelper.Combine(collections, duplicateBehavior, maxItemCount);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateItemException))]
        public void Test_Combine_With_Error_Duplicate_Behavior()
        {
            var collections = new List<List<int>>()
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 3, 4, 5 }
            };
            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Error;
            CollectionHelper.Combine(collections, duplicateBehavior);
        }

        [TestMethod]
        public void Test_Combine_With_Insert_Duplicate_Behavior()
        {
            var collections = new List<List<int>>()
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 3, 4, 5 }
            };
            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Insert;
            var expected = new int[] { 1, 2, 3, 3, 4, 5 };
            var actual = CollectionHelper.Combine(collections, duplicateBehavior).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Combine_With_Ignore_Duplicate_Behavior()
        {
            var collections = new List<List<int>>()
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 3, 4, 5 }
            };
            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore;
            var expected = new int[] { 1, 2, 3, 4, 5 };
            var actual = CollectionHelper.Combine(collections, duplicateBehavior).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Combine_With_Max_Items()
        {
            var collections = new List<List<int>>()
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 3, 4, 5 }
            };
            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore;
            int maxItemCount = 3;
            CollectionHelper.Combine(collections, duplicateBehavior, maxItemCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_AddRange_When_Collection_Is_Read_Only()
        {
            var collection = new Collection<int>();
            collection.SetReadOnly();
            var items = new List<int>() { 1, 2, 3 };
            CollectionHelper.AddRange(collection, items);
        }

        [TestMethod]
        public void Test_AddRange()
        {
            var collection = new Collection<int>();
            var items = new List<int>() { 1, 2, 3 };
            CollectionHelper.AddRange(collection, items);
            CollectionAssert.AreEqual(items, collection);
        }

        [TestMethod]
        public void Test_AddMissing_Read_Only()
        {
            var collection = new Collection<int>();
            collection.SetReadOnly();
            Assert.ThrowsException<ArgumentException>(() => CollectionHelper.AddMissing(collection, new Collection<int>()));
            Assert.ThrowsException<ArgumentException>(() => CollectionHelper.AddMissing(collection, new Collection<Collection<int>>()));
        }

        [TestMethod]
        public void Test_AddMissing()
        {
            var collection = new Collection<int>() { 1, 2 };
            var items = new List<int>() { 1, 2, 3 };
            CollectionHelper.AddMissing(collection, items);
            CollectionAssert.AreEqual(items, collection);
            
            var collections = new Collection<Collection<int>>()
            {
                new Collection<int>() { 4 },
                new Collection<int>() { 5 }
            };

            CollectionHelper.AddMissing(collection, collections);

            CollectionAssert.AreEqual(new Collection<int>() { 1, 2, 3, 4, 5 }, collection);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Fill_When_Collection_Is_Read_Only()
        {
            var collection = new Collection<IEnumerable>();
            collection.SetReadOnly();
            var items = new List<List<int>>();
            CollectionHelper.Fill(collection, items);
        }

        [TestMethod]
        public void Test_Fill()
        {
            var collection = new Collection<IEnumerable>();
            var list = new List<int>() { 1, 2, 3 };
            var items = new List<List<int>>()
            {
                list
            };
            CollectionHelper.Fill(collection, items);
            Assert.IsTrue(collection.Contains(list));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RemoveRange_When_Collection_Is_Read_Only()
        {
            var collection = new Collection<int>();
            collection.SetReadOnly();
            var items = new List<int>() { 1, 2, 3 };
            CollectionHelper.RemoveRange(collection, items);
        }

        [TestMethod]
        public void Test_RemoveRange()
        {
            var collection = new Collection<int>() { 1, 4, 2, 3 };
            var items = new List<int>() { 1, 2, 3 };
            CollectionHelper.RemoveRange(collection, items);
            CollectionAssert.AreEqual(new int[] { 4 }, collection);
        }

        [TestMethod]
        public void Test_ContainsRange()
        {
            var collection = new Collection<int>();
            var items = new Collection<int>();
            Assert.IsTrue(CollectionHelper.ContainsRange(collection, items));
            items.Add(1);
            Assert.IsFalse(CollectionHelper.ContainsRange(collection, items));
            collection.Add(1);
            collection.Add(2);
            items.Add(3);
            Assert.IsFalse(CollectionHelper.ContainsRange(collection, items));
            collection.Add(3);
            Assert.IsTrue(CollectionHelper.ContainsRange(collection, items));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_SplitByCount_When_Split_Count_Is_Negative()
        {
            var collection = new Collection<int>();
            var splitCount = -1;
            CollectionHelper.SplitByCount(collection, splitCount);
        }

        [TestMethod]
        public void Test_SplitByCount()
        {
            var collection = new Collection<int>() { 1, 2, 3 };

            // Split count is zero > no split.
            var splitCount = 0;
            var result = CollectionHelper.SplitByCount(collection, splitCount);
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(ReferenceEquals(collection, result.First()));

            // Split count is greater than collection count.
            splitCount = 6;
            result = CollectionHelper.SplitByCount(collection, splitCount);
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(ReferenceEquals(collection, result.First()));

            collection.AddRange(new int[] { 4, 5, 6, 7 });
            splitCount = 2;
            result = CollectionHelper.SplitByCount(collection, splitCount);
            Assert.AreEqual(4, result.Count());
            var array = result.ToArray();
            CollectionAssert.AreEqual(new[] { 1, 2 }, array[0].ToArray());
            CollectionAssert.AreEqual(new[] { 3, 4 }, array[1].ToArray());
            CollectionAssert.AreEqual(new[] { 5, 6 }, array[2].ToArray());
            CollectionAssert.AreEqual(new[] { 7 }, array[3].ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Keep_When_List_Is_Read_Only()
        {
            var collection = new Collection<int>();
            collection.SetReadOnly();
            Func<int, bool> keepPredicate = item => item > 5;
            CollectionHelper.Keep(collection, keepPredicate);
        }

        [TestMethod]
        public void Test_Keep()
        {
            var collection = new Collection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Func<int, bool> keepPredicate = item => item > 5;
            var expected = new List<int>() { 6, 7, 8, 9, 10 };
            CollectionHelper.Keep(collection, keepPredicate);
            CollectionAssert.AreEqual(expected, collection);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Remove_When_List_Is_Read_Only()
        {
            var collection = new Collection<int>();
            collection.SetReadOnly();
            Func<int, bool> removePredicate = item => item > 5;
            CollectionHelper.Remove(collection, removePredicate);
        }

        [TestMethod]
        public void Test_Remove()
        {
            var collection = new Collection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Func<int, bool> removePredicate = item => item > 5;
            var expected = new List<int>() { 1, 2, 3, 4, 5 };
            CollectionHelper.Remove(collection, removePredicate);
            CollectionAssert.AreEqual(expected, collection);
        }

        [TestMethod]
        public void Test_RemoveOrphants_Read_Only_Destination()
        {
            var destination = new Collection<int>();
            destination.SetReadOnly();
            Assert.ThrowsException<ArgumentException>(() => CollectionHelper.RemoveOrphants(destination, new Collection<int>()));
            Assert.ThrowsException<ArgumentException>(() => CollectionHelper.RemoveOrphants(destination, new Collection<Collection<int>>()));
        }

        [TestMethod]
        public void Test_RemoveOrphants()
        {
            var destination = new Collection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var compare = new Collection<int>() { 1, 3, 5, 7, 9 };
            CollectionHelper.RemoveOrphants(destination, compare);
            CollectionAssert.AreEqual(compare, destination);

            var collections = new Collection<Collection<int>>()
            {
                new Collection<int>() { 1 },
                new Collection<int>() { 5 },
                new Collection<int>() { 9 },
            };

            CollectionHelper.RemoveOrphants(destination, collections);

            var expected = new Collection<int>() { 1, 5, 9 };

            CollectionAssert.AreEqual(expected, destination);

            CollectionHelper.RemoveOrphants(destination, new Collection<int>());

            Assert.IsTrue(destination.Count == 0);
        }
    }
}
