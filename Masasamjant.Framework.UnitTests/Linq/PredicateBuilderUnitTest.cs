namespace Masasamjant.Linq
{
    [TestClass]
    public class PredicateBuilderUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var condition = new PredicateBuilderCondition("FirstName", typeof(string), "Fr", EqualityOperator.StartsWith, LogicalOperator.Or);
            var builder = new PredicateBuilder<Person>();
            Assert.IsTrue(builder.Conditions.Count == 0);
            builder = new PredicateBuilder<Person>([condition]);
            Assert.IsTrue(builder.Conditions.Contains(condition));
        }

        [TestMethod]
        public void Test_Build()
        {
            var persons = new List<Person>()
            {
                new Person("Fred", "Jackson", 12),
                new Person("Frank", "Miller", 44),
                new Person("Jane", "Dole", 72),
                new Person("Frances", "Green", 25)
            };

            var builder = new PredicateBuilder<Person>();
            Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            builder.Conditions.Add(new PredicateBuilderCondition("FirstName", typeof(string), "Fr", EqualityOperator.StartsWith, LogicalOperator.And));
            var expression = builder.Build();
            var expected = persons.Where(x => x.FirstName.StartsWith("Fr")).ToList();
            var actual = persons.AsQueryable().Where(expression).ToList();

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("LastName", typeof(string), "n", EqualityOperator.EndsWith, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.LastName.EndsWith("n")).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("LastName", typeof(string), "e", EqualityOperator.Contains, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.LastName.Contains("e")).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("FirstName", typeof(string), "Fred", EqualityOperator.Equal, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.FirstName == "Fred").ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 1);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("FirstName", typeof(string), "Fred", EqualityOperator.NotEqual, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.FirstName != "Fred").ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 3);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("Age", typeof(int), 44, EqualityOperator.GreaterThan, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.Age > 44).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 1);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("Age", typeof(int), 44, EqualityOperator.GreaterThanOrEqual, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.Age >= 44).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 2);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("Age", typeof(int), 25, EqualityOperator.LessThan, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.Age < 25).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 1);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("Age", typeof(int), 25, EqualityOperator.LessThanOrEqual, LogicalOperator.And));
            expression = builder.Build();
            expected = persons.Where(x => x.Age <= 25).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 2);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("FirstName", typeof(string), "Fr", EqualityOperator.StartsWith, LogicalOperator.And));
            builder.Conditions.Add(new PredicateBuilderCondition("LastName", typeof(string), "son", EqualityOperator.EndsWith, LogicalOperator.None));
            expression = builder.Build();
            expected = persons.Where(x => x.FirstName.StartsWith("Fr") && x.LastName.EndsWith("son")).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 1);

            builder.Conditions.Clear();
            builder.Conditions.Add(new PredicateBuilderCondition("FirstName", typeof(string), "Fred", EqualityOperator.Equal, LogicalOperator.Or));
            builder.Conditions.Add(new PredicateBuilderCondition("Age", typeof(int), 70, EqualityOperator.GreaterThanOrEqual, LogicalOperator.None));
            expression = builder.Build();
            expected = persons.Where(x => x.FirstName == "Fred" || x.Age >= 70).ToList();
            actual = persons.AsQueryable().Where(expression).ToList();
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count == 2);
        }
    }
}
