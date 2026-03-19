using System.Collections;

namespace Masasamjant
{
    [TestClass]
    public class DayOfWeekUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor_WhenDefault_ShouldCreateEmptyCollection()
        {
            var collection = new DayOfWeekCollection();
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Test_Constructor_WhenNull_ShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DayOfWeekCollection(null!));
        }

        [TestMethod]
        public void Test_Constructor_WhenUndefinedValue_ShouldThrowException()
        {
            Assert.ThrowsException<ArgumentException>(() => new DayOfWeekCollection(new[] { (DayOfWeek)100 }));
        }


        [TestMethod]
        public void Test_Constructor_WhenEmptyCollection_ShouldCreateEmptyCollection()
        {
            var collection = new DayOfWeekCollection(Array.Empty<DayOfWeek>());
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Test_Constructor_WhenCollection_ShouldCreateCollectionWithSpecifiedValues()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            Assert.AreEqual(3, collection.Count);
            Assert.IsTrue(collection.Contains(DayOfWeek.Monday));
            Assert.IsTrue(collection.Contains(DayOfWeek.Wednesday));
            Assert.IsTrue(collection.Contains(DayOfWeek.Friday));
        }

        [TestMethod]
        public void Test_Constructor_WhenCollectionWithDuplicates_ShouldCreateCollectionWithUniqueValues()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Tuesday });
            Assert.AreEqual(2, collection.Count);
            Assert.IsTrue(collection.Contains(DayOfWeek.Monday));
            Assert.IsTrue(collection.Contains(DayOfWeek.Tuesday));
        }

        [TestMethod]
        public void Test_IsReadOnly_WhenDefault_ShouldBeFalse()
        {
            var collection = new DayOfWeekCollection();
            Assert.IsFalse(collection.IsReadOnly);
        }

        [TestMethod]
        public void Test_SetReadOnly_WhenFalse_SetsTrue()
        {
            var collection = new DayOfWeekCollection();
            Assert.IsFalse(collection.IsReadOnly);
            collection.SetReadOnly();
            Assert.IsTrue(collection.IsReadOnly);
        }

        [TestMethod]
        public void Test_CreateAll_ShouldCreateCollectionWithAllDaysOfWeek()
        {
            var collection = DayOfWeekCollection.CreateAll();
            Assert.AreEqual(7, collection.Count);
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                Assert.IsTrue(collection.Contains(day));
            }
        }

        [TestMethod]
        public void Test_WhenReadOnly_ThenCannotModify()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            collection.SetReadOnly();
            Assert.ThrowsException<InvalidOperationException>(() => collection.Add(DayOfWeek.Tuesday));
            Assert.ThrowsException<InvalidOperationException>(() => collection.Remove(DayOfWeek.Monday));
            Assert.ThrowsException<InvalidOperationException>(() => collection.Clear());
        }

        [TestMethod]
        public void Test_Add_WhenUndefined_ThenThrowException()
        {
            var collection = new DayOfWeekCollection();
            Assert.ThrowsException<ArgumentException>(() => collection.Add((DayOfWeek)100));
        }

        [TestMethod]
        public void Test_Add_WhenValid_ThenAdded()
        {
            var collection = new DayOfWeekCollection();
            collection.Add(DayOfWeek.Monday);
            Assert.AreEqual(1, collection.Count);
            Assert.IsTrue(collection.Contains(DayOfWeek.Monday));
        }

        [TestMethod]
        public void Test_Add_WhenDuplicate_ThenNotAdded()
        {
            var collection = new DayOfWeekCollection();
            collection.Add(DayOfWeek.Monday);
            collection.Add(DayOfWeek.Monday);
            Assert.AreEqual(1, collection.Count);
            Assert.IsTrue(collection.Contains(DayOfWeek.Monday));
        }

        [TestMethod]
        public void Test_Clear_RemovesAll()
        {
            var collection = DayOfWeekCollection.CreateAll();
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Test_Contains()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            Assert.IsTrue(collection.Contains(DayOfWeek.Monday));
            Assert.IsFalse(collection.Contains(DayOfWeek.Tuesday));
            Assert.IsTrue(collection.Contains(DayOfWeek.Wednesday));
            Assert.IsFalse(collection.Contains(DayOfWeek.Thursday));
            Assert.IsTrue(collection.Contains(DayOfWeek.Friday));
            Assert.IsFalse(collection.Contains(DayOfWeek.Saturday));
            Assert.IsFalse(collection.Contains(DayOfWeek.Sunday));
        }

        [TestMethod]
        public void Test_CopyTo_WhenNullArray_ShouldThrowException()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday });
            Assert.ThrowsException<ArgumentNullException>(() => collection.CopyTo(null!, 0));
        }

        [TestMethod]
        public void Test_CopyTo_WhenNegativeIndex_ShouldThrowException()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday });
            var array = new DayOfWeek[2];
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => collection.CopyTo(array, -1));
        }

        [TestMethod]
        public void Test_CopyTo_WhenIndexGreaterThanArrayLength_ShouldThrowException()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday });
            var array = new DayOfWeek[2];
            Assert.ThrowsException<ArgumentException>(() => collection.CopyTo(array, 3));
        }

        [TestMethod]
        public void Test_CopyTo_WhenNoCount_ThenCopyAll()
        { 
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            var array = new DayOfWeek[6];
            collection.CopyTo(array, 0);
            Assert.AreEqual(DayOfWeek.Monday, array[0]);
            Assert.AreEqual(DayOfWeek.Wednesday, array[1]);
            Assert.AreEqual(DayOfWeek.Friday, array[2]);
            Assert.AreEqual(DayOfWeek.Sunday, array[3]);
            Assert.AreEqual(DayOfWeek.Sunday, array[4]);
            Assert.AreEqual(DayOfWeek.Sunday, array[5]);
        }

        [TestMethod]
        public void Test_CopyTo_WhenNegativeCount_ThenThrows()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            var array = new DayOfWeek[6];
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => collection.CopyTo(array, 0, -1));
        }

        [TestMethod]
        public void Test_CopyTo_WhenCount_ThenCopyCount()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            var array = new DayOfWeek[6];
            collection.CopyTo(array, 0, 2);
            Assert.AreEqual(DayOfWeek.Monday, array[0]);
            Assert.AreEqual(DayOfWeek.Wednesday, array[1]);
            Assert.AreEqual(DayOfWeek.Sunday, array[2]);
            Assert.AreEqual(DayOfWeek.Sunday, array[3]);
            Assert.AreEqual(DayOfWeek.Sunday, array[4]);
            Assert.AreEqual(DayOfWeek.Sunday, array[5]);
        }

        [TestMethod]
        public void Test_Generic_GetEnumerator()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            var enumerator = collection.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(DayOfWeek.Monday, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(DayOfWeek.Wednesday, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(DayOfWeek.Friday, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_GetEnumerator()
        {
            IEnumerable enumerable = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            IEnumerator enumerator = enumerable.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(DayOfWeek.Monday, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(DayOfWeek.Wednesday, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(DayOfWeek.Friday, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_Remove_WhenUndefined_ThenReturnsFalse()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday });
            Assert.IsFalse(collection.Remove((DayOfWeek)100));
        }

        [TestMethod]
        public void Test_Remove_WhenNotExists_ThenReturnsFalse()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday });
            Assert.IsFalse(collection.Remove(DayOfWeek.Friday));
        }

        [TestMethod]
        public void Test_Remove_WhenExists_ThenReturnsTrue()
        {
            var collection = new DayOfWeekCollection(new[] { DayOfWeek.Monday, DayOfWeek.Wednesday });
            Assert.IsTrue(collection.Remove(DayOfWeek.Monday));
            Assert.AreEqual(1, collection.Count);
            Assert.IsFalse(collection.Contains(DayOfWeek.Monday));
        }   
    }
}
