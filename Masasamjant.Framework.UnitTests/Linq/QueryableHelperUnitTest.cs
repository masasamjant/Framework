namespace Masasamjant.Linq
{
    [TestClass]
    public class QueryableHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Page()
        {
            var baseQuery = CreateInt32List(100).AsQueryable();

            // Zero values generate empty page.
            var page = new PageInfo(0, 0);
            var result = QueryableHelper.Page(baseQuery, page, false);
            Assert.IsTrue(page.TotalCount < 0);
            Assert.IsFalse(result.Any());

            // Get first page no total count.
            page = new PageInfo(0, 5);
            result = QueryableHelper.Page(baseQuery, page, false);
            Assert.IsTrue(page.TotalCount < 0);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 4 }, result.ToArray());

            // Get next page no total count.
            page = page.Next();
            result = QueryableHelper.Page(baseQuery, page, false);
            Assert.IsTrue(page.TotalCount < 0);
            CollectionAssert.AreEqual(new int[] { 5, 6, 7, 8, 9 }, result.ToArray());

            // Calculate total count.
            page = new PageInfo(0, 20);
            result = QueryableHelper.Page(baseQuery, page, true);
            Assert.IsTrue(page.TotalCount == 100);
            Assert.IsTrue(result.Count() == 20);
        }

        [TestMethod]
        public void Test_Sort()
        {
            var persons = GetPersons();

            var descriptors = new List<SortDescriptor>()
            {
                new SortDescriptor("LastName", SortOrder.Ascending),
                new SortDescriptor("Age", SortOrder.Descending)
            };

            var expected = persons.OrderBy(x => x.LastName).ThenByDescending(x => x.Age).First();
            var actual = QueryableHelper.Sort(persons, descriptors).First();
            Assert.AreEqual(expected, actual);

            descriptors.Clear();
            descriptors.AddRange(new[]
            {
                new SortDescriptor("LastName", SortOrder.Descending),
                new SortDescriptor("Age", SortOrder.Ascending)
            });

            expected = persons.OrderByDescending(x => x.LastName).ThenBy(x => x.Age).Last();
            actual = QueryableHelper.Sort(persons, descriptors).Last();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Sort_Property_Not_Exists()
        {
            var persons = GetPersons();
            var descriptors = new List<SortDescriptor>()
            {
                new SortDescriptor("Gender", SortOrder.Descending)
            };
            QueryableHelper.Sort(persons, descriptors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Sort_Property_Name_Empty()
        {
            var persons = GetPersons();
            var descriptors = new List<SortDescriptor>()
            {
                new SortDescriptor()
            };
            QueryableHelper.Sort(persons, descriptors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_OrderBy_Empty_Name()
        {
            var persons = GetPersons();
            QueryableHelper.OrderBy(persons, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_ThenBy_Empty_Name()
        {
            var persons = GetPersons().OrderBy(x => x.Age);
            QueryableHelper.ThenBy(persons, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_OrderByDescending_Empty_Name()
        {
            var persons = GetPersons();
            QueryableHelper.OrderByDescending(persons, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_ThenByDescending_Empty_Name()
        {
            var persons = GetPersons().OrderBy(x => x.Age);
            QueryableHelper.ThenByDescending(persons, "");
        }

        [TestMethod]
        public void Test_OrderBy_ThenBy()
        {
            var persons = GetPersons();
            var expected = persons.OrderBy(x => x.LastName).ThenBy(x => x.Age).First();
            var actual = QueryableHelper.OrderBy(persons, "LastName").ThenBy("Age").First();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_OrderByDescending_ThenByDescending()
        {
            var persons = GetPersons();
            var expected = persons.OrderByDescending(x => x.LastName).ThenByDescending(x => x.Age).Last();
            var actual = QueryableHelper.OrderByDescending(persons, "LastName").ThenByDescending("Age").Last();
            Assert.AreEqual(expected, actual);
        }

        private static IQueryable<Person> GetPersons()
        {
            return new List<Person>()
            {
                new Person("Annie", "Smith", 43),
                new Person("Mark", "Williams", 37),
                new Person("Henry", "Smith", 75),
                new Person("Mary", "Blue", 12),
                new Person("Jane", "Williams", 33),
                new Person("Cory", "Williams", 5),
                new Person("James", "Blue", 16)
            }.AsQueryable();
        }
    }
}
