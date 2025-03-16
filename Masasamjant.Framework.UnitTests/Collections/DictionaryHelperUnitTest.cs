using Masasamjant.Collections.Adapters;
using System.Collections.ObjectModel;

namespace Masasamjant.Collections
{
    [TestClass]
    public class DictionaryHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CreateReadOnly()
        {
            var a = DictionaryHelper.CreateReadOnly<int, int>();
            Assert.IsTrue(a.Count == 0);
            var source = new Dictionary<int, int>();
            a = DictionaryHelper.CreateReadOnly(source);
            Assert.IsTrue(a.Count == 0);
            source.Add(1, 1);
            source.Add(2, 2);
            a = DictionaryHelper.CreateReadOnly(source);
            Assert.IsTrue(a.Count == 2 && a[1] == 1 && a[2] == 2);
        }

        [TestMethod]
        public void Test_AsInterface()
        {
            var source = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>());
            var dictionary = DictionaryHelper.AsDictionary(source);
            Assert.IsTrue(dictionary.IsReadOnly);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GetOrAdd_Throws_When_Dictionary_Read_Only()
        {
            var dictionary = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            Func<int, int> valueProvider = k => k + 1;
            DictionaryHelper.GetOrAdd(dictionary, 1, valueProvider);
        }

        [TestMethod]
        public void Test_GetOrAdd()
        {
            var dictionary = new Dictionary<int, int>() { { 1, 1 } };
            Func<int, int> valueProvider = k => k + 1;
            Assert.AreEqual(1, DictionaryHelper.GetOrAdd(dictionary, 1, valueProvider));
            Assert.AreEqual(3, DictionaryHelper.GetOrAdd(dictionary, 2, valueProvider));
            Assert.IsTrue(dictionary[2] == 3);
        }

        [TestMethod]
        public void Test_AreEqual()
        {
            // Same reference > equal
            var left = new Dictionary<int, int>() { { 1, 1 } };
            var right = left;
            Assert.IsTrue(DictionaryHelper.AreEqual(left, right));

            // Both empty > equal
            left = new Dictionary<int, int>();
            right = new Dictionary<int, int>();
            Assert.IsTrue(DictionaryHelper.AreEqual(left, right));

            // Different counts > not equal
            left = new Dictionary<int, int>() { { 1, 1 } };
            right = new Dictionary<int, int>();
            Assert.IsFalse(DictionaryHelper.AreEqual(left, right));

            // Different values > not equal
            left = new Dictionary<int, int>() { { 1, 1 } };
            right = new Dictionary<int, int>() { { 1, 2 } };
            Assert.IsFalse(DictionaryHelper.AreEqual(left, right));

            // Same keys/values > equal
            left = new Dictionary<int, int>() { { 1, 1 } };
            right = new Dictionary<int, int>() { { 1, 1 } };
            Assert.IsTrue(DictionaryHelper.AreEqual(left, right));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Combine_Throws_When_Duplicate_Behavior_Not_Defined()
        {
            var dictionaries = new[]
            {
                new Dictionary<int, int>(),
                new Dictionary<int, int>()
            };

            DuplicateBehavior duplicateBehavior = (DuplicateBehavior)999;
            DictionaryHelper.Combine(dictionaries, duplicateBehavior);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Test_Combine_Throws_When_Duplicate_Behavior_Is_Insert()
        {
            var dictionaries = new[]
            {
                new Dictionary<int, int>(),
                new Dictionary<int, int>()
            };

            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Insert;
            DictionaryHelper.Combine(dictionaries, duplicateBehavior);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Combine_Throws_When_Max_Item_Count_Is_Less_Than_One()
        {
            var dictionaries = new[]
            {
                new Dictionary<int, int>(),
                new Dictionary<int, int>()
            };

            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore;
            int maxItemCount = 0;
            DictionaryHelper.Combine(dictionaries, duplicateBehavior, maxItemCount);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException<int>))]
        public void Test_Combine_When_Duplicate_Behavior_Is_Error()
        {
            var dictionaries = new[]
{
                new Dictionary<int, int>()
                {
                    {1, 1},
                    {2, 2}
                },
                new Dictionary<int, int>()
                {
                    {1, 2},
                    {3, 3}
                }
            };

            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Error;
            int maxItemCount = 10;
            DictionaryHelper.Combine(dictionaries, duplicateBehavior, maxItemCount);
        }

        [TestMethod]
        public void Test_Combine_When_Duplicate_Behavior_Is_Ignore()
        {
            var dictionaries = new[]
{
                new Dictionary<int, int>()
                {
                    {1, 1},
                    {2, 2}
                },
                new Dictionary<int, int>()
                {
                    {1, 2},
                    {3, 3}
                }
            };

            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore;
            int maxItemCount = 10;
            var result = DictionaryHelper.Combine(dictionaries, duplicateBehavior, maxItemCount);
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result[1] == 1);
            Assert.IsTrue(result[2] == 2);
            Assert.IsTrue(result[3] == 3);
        }

        [TestMethod]
        public void Test_Combine_When_Duplicate_Behavior_Is_Replace()
        {
            var dictionaries = new[]
{
                new Dictionary<int, int>()
                {
                    {1, 1},
                    {2, 2}
                },
                new Dictionary<int, int>()
                {
                    {1, 2},
                    {3, 3}
                }
            };

            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Replace;
            int maxItemCount = 10;
            var result = DictionaryHelper.Combine(dictionaries, duplicateBehavior, maxItemCount);
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result[1] == 2);
            Assert.IsTrue(result[2] == 2);
            Assert.IsTrue(result[3] == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Combine_When_Max_Item_Count_Is_Exceeded()
        {
            var dictionaries = new[]
{
                new Dictionary<int, int>()
                {
                    {1, 1},
                    {2, 2}
                },
                new Dictionary<int, int>()
                {
                    {1, 2},
                    {3, 3}
                }
            };

            DuplicateBehavior duplicateBehavior = DuplicateBehavior.Replace;
            int maxItemCount = 2;
            DictionaryHelper.Combine(dictionaries, duplicateBehavior, maxItemCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Merge_Throws_When_Destination_Is_ReadOnly()
        {
            var sources = new[]
            {
                new Dictionary<int, int>()
                {
                },
                new Dictionary<int, int>()
                {
                }
            };
            var destination = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            DictionaryHelper.Merge(destination, sources);
        }

        [TestMethod]
        public void Test_Merge()
        {
            var destination = new Dictionary<int, int>()
            {
                {1, 1}, // Not in sources > removed
                {2, 2}  // In sources > updated
            };

            var other = destination;

            var sources = new[]
            {
                other, // Ignored
                new Dictionary<int, int>()
                {
                    {2, 4}
                },
                new Dictionary<int, int>()
                {
                    {3, 6} // Not in destination > added
                }
            };

            DictionaryHelper.Merge(destination, sources);
            Assert.IsFalse(destination.ContainsKey(1));
            Assert.IsTrue(destination[2] == 4);
            Assert.IsTrue(destination[3] == 6);
            Assert.IsTrue(destination.Count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RemoveRange_Throws_When_Dictionary_Is_ReadOnly()
        {
            var keys = new int[] { 1, 2 };
            var dictionary = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            DictionaryHelper.RemoveRange(dictionary, keys);
        }

        [TestMethod]
        public void Test_RemoveRange()
        {
            var keys = new int[] { 1, 2 };
            var dictionary = new Dictionary<int, int>()
            {
                {1, 1},
                {2, 2}
            };
            DictionaryHelper.RemoveRange(dictionary, keys);
            Assert.IsTrue(dictionary.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_TryGetAndRemove_Throws_When_Dictionary_Is_ReadOnly()
        {
            var dictionary = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            DictionaryHelper.TryGetAndRemove(dictionary, 1, out var result);
        }

        [TestMethod]
        public void Test_TryGetAndRemove()
        {
            var dictionary = new Dictionary<int, int>()
            {
                {1, 1}
            };

            Assert.IsFalse(DictionaryHelper.TryGetAndRemove(dictionary, 2, out var result));
            Assert.IsTrue(DictionaryHelper.TryGetAndRemove(dictionary, 1, out result));
            Assert.IsTrue(result == 1);
            Assert.IsFalse(dictionary.ContainsKey(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Keep_KeyValue_Throws_When_Dictionary_Is_ReadOnly()
        {
            var keepPredicate = new Func<KeyValuePair<int, int>, bool>(kv => kv.Key > 10 && kv.Value > 10);
            var dictionary = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            DictionaryHelper.Keep(dictionary, keepPredicate);
        }

        [TestMethod]
        public void Test_Keep_KeyValue()
        {
            var keepPredicate = new Func<KeyValuePair<int, int>, bool>(kv => kv.Key > 10 && kv.Value > 10);
            var dictionary = new Dictionary<int, int>()
            {
                {1, 10},  // Remove
                {11, 11}, // Keep,
                {2, 20},  // Remove
                {22, 22}, // Keep
            };

            DictionaryHelper.Keep(dictionary, keepPredicate);
            Assert.IsTrue(dictionary.Count == 2);
            Assert.IsTrue(dictionary[11] == 11);
            Assert.IsTrue(dictionary[22] == 22);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Keep_Key_Throws_When_Dictionary_Is_ReadOnly()
        {
            var keepPredicate = new Func<int, bool>(k => k < 10);
            var dictionary = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            DictionaryHelper.Keep(dictionary, keepPredicate);
        }

        [TestMethod]
        public void Test_Keep_Key()
        {
            var keepPredicate = new Func<int, bool>(k => k < 10);
            var dictionary = new Dictionary<int, int>()
            {
                {1, 10},  // Keep
                {11, 11}, // Remove,
                {2, 20},  // Keep
                {22, 22}, // Remove
            };

            DictionaryHelper.Keep(dictionary, keepPredicate);
            Assert.IsTrue(dictionary.Count == 2);
            Assert.IsTrue(dictionary[1] == 10);
            Assert.IsTrue(dictionary[2] == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Remove_KeyValue_Throws_When_Dictionary_Is_ReadOnly()
        {
            var removePredicate = new Func<KeyValuePair<int, int>, bool>(kv => kv.Key > 10 && kv.Value > 10);
            var dictionary = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            DictionaryHelper.Remove(dictionary, removePredicate);
        }

        [TestMethod]
        public void Test_Remove_KeyValue()
        {
            var removePredicate = new Func<KeyValuePair<int, int>, bool>(kv => kv.Key > 10 && kv.Value > 10);
            var dictionary = new Dictionary<int, int>()
            {
                {1, 10},  // Keep
                {11, 11}, // Remove,
                {2, 20},  // Keep
                {22, 22}, // Remove
            };

            DictionaryHelper.Remove(dictionary, removePredicate);
            Assert.IsTrue(dictionary.Count == 2);
            Assert.IsTrue(dictionary[1] == 10);
            Assert.IsTrue(dictionary[2] == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Remove_Key_Throws_When_Dictionary_Is_ReadOnly()
        {
            var removePredicate = new Func<int, bool>(k => k < 10);
            var dictionary = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            DictionaryHelper.Remove(dictionary, removePredicate);
        }

        [TestMethod]
        public void Test_Remove_Key()
        {
            var removePredicate = new Func<int, bool>(k => k < 10);
            var dictionary = new Dictionary<int, int>()
            {
                {1, 10},  // Remove
                {11, 11}, // Keep,
                {2, 20},  // Remove
                {22, 22}, // Keep
            };

            DictionaryHelper.Remove(dictionary, removePredicate);
            Assert.IsTrue(dictionary.Count == 2);
            Assert.IsTrue(dictionary[11] == 11);
            Assert.IsTrue(dictionary[22] == 22);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FromTuples_Throws_When_Duplicate_Behavior_Undefined()
        {
            var tuples = new List<Tuple<int, int>>();
            var duplicateBehavior = (DuplicateBehavior)999;
            DictionaryHelper.FromTuples(tuples, duplicateBehavior);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Test_FromTuples_When_Duplicate_Behavior_Is_Insert()
        {
            var tuples = new List<Tuple<int, int>>();
            var duplicateBehavior = DuplicateBehavior.Insert;
            DictionaryHelper.FromTuples(tuples, duplicateBehavior);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException<int>))]
        public void Test_FromTuples_When_Duplicate_Behavior_Is_Error()
        {
            var tuples = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 2)
            };
            var duplicateBehavior = DuplicateBehavior.Error;
            DictionaryHelper.FromTuples(tuples, duplicateBehavior);
        }

        [TestMethod]
        public void Test_FromTuples_When_Duplicate_Behavior_Is_Ignore()
        {
            var tuples = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 2)
            };
            var duplicateBehavior = DuplicateBehavior.Ignore;
            var result = DictionaryHelper.FromTuples(tuples, duplicateBehavior);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[1] == 1);
        }

        [TestMethod]
        public void Test_FromTuples_When_Duplicate_Behavior_Is_Replace()
        {
            var tuples = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 2)
            };
            var duplicateBehavior = DuplicateBehavior.Replace;
            var result = DictionaryHelper.FromTuples(tuples, duplicateBehavior);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[1] == 2);
        }

        [TestMethod]
        public void Test_ToTuples()
        {
            var dictionary = new Dictionary<int, int>()
            {
                {1, 1}, {2, 2}, {3, 3}
            };

            var expected = new[]
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(3, 3)
            };

            var actual = DictionaryHelper.ToTuples(dictionary).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ContainsKeys()
        {
            var dictionary = new Dictionary<int, int>();
            var keys = new int[] { 1, 2, 3 };
            Assert.IsFalse(DictionaryHelper.ContainsKeys(dictionary, keys));
            dictionary.Add(1, 1);
            keys = new int[0];
            Assert.IsFalse(DictionaryHelper.ContainsKeys(dictionary, keys));
            dictionary.Add(2, 2);
            keys = new int[] { 1, 2, 3 };
            Assert.IsFalse(DictionaryHelper.ContainsKeys(dictionary, keys));
            dictionary.Add(3, 3);
            dictionary.Add(4, 4);
            Assert.IsTrue(DictionaryHelper.ContainsKeys(dictionary, keys));
        }
    }
}
