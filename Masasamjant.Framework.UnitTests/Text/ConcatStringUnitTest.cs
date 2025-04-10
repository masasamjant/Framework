namespace Masasamjant.Text
{
    [TestClass]
    public class ConcatStringUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Empty_ConcatString()
        {
            var a = new ConcatString();
            Assert.IsTrue(a.IsEmpty);
            Assert.AreEqual(string.Empty, a.Concat());
            Assert.AreEqual(ConcatString.NoSeparator, a.Separator);
            Assert.IsFalse(a.Values.Any());
            var b = new ConcatString();
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_NoSeparator_Is_Not_Allowed()
        {
            new ConcatString(ConcatString.NoSeparator, Enumerable.Empty<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Separator_Not_Allowed_In_Value()
        {
            char separator = '|';
            var values = new[] { "ahaa", "a|haa" };
            new ConcatString(separator, values);
        }

        [TestMethod]
        public void Test_ConcatString_Without_Values()
        {
            char separator = '|';
            var a = new ConcatString(separator, Enumerable.Empty<string>());
            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(separator, a.Separator);
            Assert.IsFalse(a.Values.Any());
            Assert.AreEqual("|", a.Concat());
            var b = new ConcatString(separator, Enumerable.Empty<string>());
            Assert.AreEqual(a, b);
            var c = new ConcatString(separator, new[] { "a", "b", "c" });
            Assert.AreNotEqual(a, c);
        }

        [TestMethod]
        public void Test_ConcatString_With_Values()
        {
            char separator = '|';
            var a = new ConcatString(separator, new[] { "1", "2", "3" });
            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(separator, a.Separator);
            CollectionAssert.AreEqual(new[] { "1", "2", "3" }, a.Values.ToArray());
            Assert.AreEqual("|1|2|3", a.Concat());
            var b = new ConcatString(separator, new[] { "1", "2", "3" });
            Assert.AreEqual(a, b);
            var c = new ConcatString(separator, new[] { "a", "b", "c" });
            Assert.AreNotEqual(a, c);
            var d = new ConcatString('+', new[] { "1", "2", "3" });
        }

        [TestMethod]
        public void Test_ParseFrom()
        {
            var cs = new ConcatString();
            cs.ParseFrom(string.Empty);
            Assert.AreEqual(cs, new ConcatString());
            cs.ParseFrom("+");
            Assert.AreEqual('+', cs.Separator);
            Assert.IsFalse(cs.Values.Any());
            cs.ParseFrom("+1+12+123");
            Assert.AreEqual('+', cs.Separator);
            CollectionAssert.AreEqual(new[] { "1", "12", "123" }, cs.Values.ToArray());
        }
    }
}
