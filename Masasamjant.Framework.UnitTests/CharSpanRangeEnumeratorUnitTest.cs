namespace Masasamjant
{
    [TestClass]
    public class CharSpanRangeEnumeratorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Enumerate_EmptySpan()
        {
            var span = ReadOnlySpan<char>.Empty;
            var enumerator = new CharSpanRangeEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var expected = new System.Range(0, 0);
            var actual = enumerator.Current;
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_Enumerate_SpanNoSeparators()
        {
            var span = "abcdef".AsSpan();
            var enumerator = new CharSpanRangeEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var expected = new System.Range(0, 6);
            var actual = enumerator.Current;
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_Enumerate_SpanWithSeparators()
        {
            var span = "abc,def,ghi".AsSpan();
            var enumerator = new CharSpanRangeEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var expected1 = new System.Range(0, 3);
            var actual1 = enumerator.Current;
            Assert.AreEqual(expected1, actual1);
            Assert.IsTrue(enumerator.MoveNext());
            var expected2 = new System.Range(4, 7);
            var actual2 = enumerator.Current;
            Assert.AreEqual(expected2, actual2);
            Assert.IsTrue(enumerator.MoveNext());
            var expected3 = new System.Range(8, 11);
            var actual3 = enumerator.Current;
            Assert.AreEqual(expected3, actual3);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Test_Enumerate_SpanWithConsecutiveSeparators()
        {
            var span = "abc,,def".AsSpan();
            var enumerator = new CharSpanRangeEnumerator(span, ',');
            Assert.IsTrue(enumerator.MoveNext());
            var expected1 = new System.Range(0, 3);
            var actual1 = enumerator.Current;
            Assert.AreEqual(expected1, actual1);
            Assert.IsTrue(enumerator.MoveNext());
            var expected2 = new System.Range(4, 4); // Empty segment
            var actual2 = enumerator.Current;
            Assert.AreEqual(expected2, actual2);
            Assert.IsTrue(enumerator.MoveNext());
            var expected3 = new System.Range(5, 8);
            var actual3 = enumerator.Current;
            Assert.AreEqual(expected3, actual3);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
