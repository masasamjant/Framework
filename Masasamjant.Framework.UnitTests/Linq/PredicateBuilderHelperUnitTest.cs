namespace Masasamjant.Linq
{
    [TestClass]
    public class PredicateBuilderHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_BuildAndCompile()
        {
            var persons = new List<Person>()
            {
                new Person("Fred", "Jackson", 12),
                new Person("Frank", "Miller", 44),
                new Person("Jane", "Dole", 72),
                new Person("Frances", "Green", 25)
            };

            var builder = new PredicateBuilder<Person>();
            builder.Conditions.Add(new PredicateBuilderCondition("FirstName", typeof(string), "Fr", EqualityOperator.StartsWith, LogicalOperator.And));
            var expected = persons.Where(x => x.FirstName.StartsWith("Fr")).ToList();
            var actual = persons.Where(PredicateBuilderHelper.BuildAndCompile(builder)).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
