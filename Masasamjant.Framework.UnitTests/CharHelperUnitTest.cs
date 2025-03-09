﻿namespace Masasamjant
{
    [TestClass]
    public class CharHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CommonSeparators()
        {
            char[] expected = [',', ';', '|', ':', '+', '-'];
            char[] actual = CharHelper.CommonSeparators;
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsFalse(ReferenceEquals(CharHelper.CommonSeparators, CharHelper.CommonSeparators));
        }

        [TestMethod]
        public void Test_TryGetSeparator()
        {
            var values = new List<string>()
            {
                "A98", "A48", "A34"
            };
            var separators = new List<char>();
            char? separator;

            // No separators
            Assert.IsFalse(CharHelper.TryGetSeparator(values, separators, out separator));
            Assert.IsFalse(separator.HasValue);

            // No suitable separator
            separators.Add('A');
            Assert.IsFalse(CharHelper.TryGetSeparator(values, separators, out separator));
            Assert.IsFalse(separator.HasValue);

            // Suitable separator
            separators.Add('|');
            Assert.IsTrue(CharHelper.TryGetSeparator(values, separators, out separator));
            Assert.IsTrue(separator.HasValue);
            Assert.AreEqual('|', separator.Value);

            // No values first separator is suitable.
            values.Clear();
            Assert.IsTrue(CharHelper.TryGetSeparator(values, separators, out separator));
            Assert.IsTrue(separator.HasValue);
            Assert.AreEqual('A', separator.Value);
        }
    }
}
