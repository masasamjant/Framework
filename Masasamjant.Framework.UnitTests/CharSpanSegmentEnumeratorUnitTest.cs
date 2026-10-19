namespace Masasamjant
{
    [TestClass]
    public class CharSpanSegmentEnumeratorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Enumerate_EmptySpan()
        {
            var span = ReadOnlySpan<char>.Empty;
            var enumerator = new CharSpanSegmentEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var actual = enumerator.Current;
            Assert.IsTrue(actual.Length == 0);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_Enumerate_SpanNoSeparators()
        {
            var span = "abcdef".AsSpan();
            var enumerator = new CharSpanSegmentEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var actual = enumerator.Current;
            Assert.AreEqual("abcdef", actual.ToString());
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_Enumerate_SpanWithSeparators()
        {
            var span = "abc,def,ghi".AsSpan();
            var enumerator = new CharSpanSegmentEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var actual1 = enumerator.Current;
            Assert.AreEqual("abc", actual1.ToString());
            Assert.IsTrue(enumerator.MoveNext());
            var actual2 = enumerator.Current;
            Assert.AreEqual("def", actual2.ToString());
            Assert.IsTrue(enumerator.MoveNext());
            var actual3 = enumerator.Current;
            Assert.AreEqual("ghi", actual3.ToString());
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_Enumerate_SpanWithConsecutiveSeparators()
        {
            var span = "abc,,def".AsSpan();
            var enumerator = new CharSpanSegmentEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var actual1 = enumerator.Current;
            Assert.AreEqual("abc", actual1.ToString());
            Assert.IsTrue(enumerator.MoveNext());
            var actual2 = enumerator.Current;
            Assert.AreEqual(string.Empty, actual2.ToString()); // Empty segment
            Assert.IsTrue(enumerator.MoveNext());
            var actual3 = enumerator.Current;
            Assert.AreEqual("def", actual3.ToString());
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
