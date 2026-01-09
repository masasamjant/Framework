namespace Masasamjant
{
    [TestClass]
    public class StringFilterUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_When_Filter_Type_Is_Undefined()
        {
            StringFilterType filterType = (StringFilterType)999;
            new StringFilter("Foo", filterType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_When_Comparison_Is_Undefined()
        {
            StringComparison comparison = (StringComparison)999;
            new StringFilter("Foo", StringFilterType.Match, comparison);
        }

        [TestMethod]
        public void Test_Constructor()
        {
            var filter = new StringFilter("Foo", StringFilterType.EndsWith, StringComparison.InvariantCultureIgnoreCase);
            Assert.AreEqual("Foo", filter.FilterValue);
            Assert.AreEqual(StringFilterType.EndsWith, filter.FilterType);
            Assert.AreEqual(StringComparison.InvariantCultureIgnoreCase, filter.Comparison);

            filter = new StringFilter("Foo", StringFilterType.StartsWith);
            Assert.AreEqual("Foo", filter.FilterValue);
            Assert.AreEqual(StringFilterType.StartsWith, filter.FilterType);
            Assert.AreEqual(StringComparison.CurrentCulture, filter.Comparison);

            filter = new StringFilter();
            Assert.AreEqual(string.Empty, filter.FilterValue);
            Assert.AreEqual(StringFilterType.Match, filter.FilterType);
            Assert.AreEqual(StringComparison.CurrentCulture, filter.Comparison);
        }

        [TestMethod]
        public void Test_Matches()
        {
            StringComparison comparison = StringComparison.Ordinal;
            Assert.IsTrue(new StringFilter("Foo", StringFilterType.Match, comparison).Matches("Foo"));
            Assert.IsFalse(new StringFilter("Foo", StringFilterType.Match, comparison).Matches("Bar"));
            Assert.IsTrue(new StringFilter("Foo", StringFilterType.Contains, comparison).Matches("FooBar"));
            Assert.IsTrue(new StringFilter("Bar", StringFilterType.Contains, comparison).Matches("FooBar"));
            Assert.IsFalse(new StringFilter("Dum", StringFilterType.Contains, comparison).Matches("FooBar"));
            Assert.IsTrue(new StringFilter("Foo", StringFilterType.StartsWith, comparison).Matches("FooBar"));
            Assert.IsTrue(new StringFilter("Bar", StringFilterType.EndsWith, comparison).Matches("FooBar"));
            Assert.IsFalse(new StringFilter("Bar", StringFilterType.StartsWith, comparison).Matches("FooBar"));
            Assert.IsFalse(new StringFilter("Foo", StringFilterType.EndsWith, comparison).Matches("FooBar"));
            comparison = StringComparison.OrdinalIgnoreCase;
            Assert.IsTrue(new StringFilter("Foo", StringFilterType.Match, comparison).Matches("foo"));
            Assert.IsFalse(new StringFilter("Foo", StringFilterType.Match, comparison).Matches("bar"));
            Assert.IsTrue(new StringFilter("Foo", StringFilterType.Contains, comparison).Matches("foobar"));
            Assert.IsTrue(new StringFilter("Bar", StringFilterType.Contains, comparison).Matches("foobar"));
            Assert.IsFalse(new StringFilter("Dum", StringFilterType.Contains, comparison).Matches("foobar"));
            Assert.IsTrue(new StringFilter("Foo", StringFilterType.StartsWith, comparison).Matches("foobar"));
            Assert.IsTrue(new StringFilter("Bar", StringFilterType.EndsWith, comparison).Matches("foobar"));
            Assert.IsFalse(new StringFilter("Bar", StringFilterType.StartsWith, comparison).Matches("foobar"));
            Assert.IsFalse(new StringFilter("Foo", StringFilterType.EndsWith, comparison).Matches("foobar"));
        }

        [TestMethod]
        public void Test_Apply()
        {
            var filter = new StringFilter("123", StringFilterType.Contains, StringComparison.Ordinal);
            var values = new List<string>() 
            {
                "Mickey 123",
                "Mickey 456",
                "123456",
                "Donald",
            };
            var expected = new[] { "Mickey 123", "123456" };
            var actual = filter.Apply(values).ToArray();
            CollectionAssert.AreEqual(expected, actual);
            filter = new StringFilter("123", StringFilterType.EndsWith, StringComparison.Ordinal);
            expected = new[] { "Mickey 123" };
            actual = filter.Apply(values).ToArray();
            CollectionAssert.AreEqual(expected, actual);
            filter = new StringFilter("123", StringFilterType.StartsWith, StringComparison.Ordinal);
            expected = new[] { "123456" };
            actual = filter.Apply(values).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
