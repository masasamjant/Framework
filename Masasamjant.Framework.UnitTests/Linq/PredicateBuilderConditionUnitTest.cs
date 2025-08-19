namespace Masasamjant.Linq
{
    [TestClass]
    public class PredicateBuilderConditionUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new PredicateBuilderCondition(string.Empty, typeof(string), "foo", EqualityOperator.Contains, LogicalOperator.And));
            Assert.ThrowsException<ArgumentNullException>(() => new PredicateBuilderCondition("  ", typeof(string), "foo", EqualityOperator.Contains, LogicalOperator.And));
            Assert.ThrowsException<ArgumentException>(() => new PredicateBuilderCondition("Name", typeof(string), 1, EqualityOperator.Contains, LogicalOperator.And));
            var condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", (EqualityOperator)999, LogicalOperator.And);
            Assert.AreEqual(EqualityOperator.Equal, condition.EqualityOperator);
            condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.Contains, (LogicalOperator)999);
            Assert.AreEqual(LogicalOperator.None, condition.LogicalOperator);
            condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            Assert.AreEqual("Name", condition.MemberName);
            Assert.AreEqual(typeof(string), condition.MemberType);
            Assert.AreEqual("Foo", condition.MemberValue);
            Assert.AreEqual(EqualityOperator.StartsWith, condition.EqualityOperator);
            Assert.AreEqual(LogicalOperator.Or, condition.LogicalOperator);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            Assert.IsFalse(condition.Equals(null));
            Assert.IsTrue(condition.Equals(condition));
            Assert.IsTrue(condition.Equals(new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or)));
            Assert.IsFalse(condition.Equals(new PredicateBuilderCondition("FirstName", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or)));
            Assert.IsFalse(condition.Equals(new PredicateBuilderCondition("Name", typeof(string), "Boo", EqualityOperator.StartsWith, LogicalOperator.Or)));
            Assert.IsFalse(condition.Equals(new PredicateBuilderCondition("Name", typeof(int), 1, EqualityOperator.StartsWith, LogicalOperator.Or)));
            Assert.IsFalse(condition.Equals(new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.EndsWith, LogicalOperator.Or)));
            Assert.IsFalse(condition.Equals(new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.And)));
            Assert.IsFalse(condition.Equals(DateTime.Now));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var condition = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            var other = new PredicateBuilderCondition("Name", typeof(string), "Foo", EqualityOperator.StartsWith, LogicalOperator.Or);
            Assert.IsTrue(condition.GetHashCode() == other.GetHashCode());
        }
    }
}
