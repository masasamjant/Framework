namespace Masasamjant.Linq
{
    [TestClass]
    public class PredicateBuilderConditionCollectionUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var collection = new PredicateBuilderConditionCollection();
            Assert.IsTrue(collection.Count == 0);
            var condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            collection = new PredicateBuilderConditionCollection([condition]);
            Assert.IsTrue(collection.Count == 1);
            Assert.IsTrue(collection.Contains(condition));
        }

        [TestMethod]
        public void Test_Add()
        {
            var collection = new PredicateBuilderConditionCollection();
            var condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            collection.Add(condition);
            Assert.IsTrue(collection.Count == 1);
            Assert.IsTrue(collection.Contains(condition));
            collection.Add(condition);
            Assert.IsTrue(collection.Count == 1);
        }

        [TestMethod]
        public void Test_Contains()
        {
            var collection = new PredicateBuilderConditionCollection();
            var condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            Assert.IsFalse(collection.Contains(condition));
            collection.Add(condition);
            Assert.IsTrue(collection.Contains(condition));
        }

        [TestMethod]
        public void Test_Remove()
        {
            var collection = new PredicateBuilderConditionCollection();
            var condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            Assert.IsFalse(collection.Remove(condition));
            collection.Add(condition);
            Assert.IsTrue(collection.Remove(condition));
            Assert.IsTrue(collection.Count == 0);
        }
    }
}
