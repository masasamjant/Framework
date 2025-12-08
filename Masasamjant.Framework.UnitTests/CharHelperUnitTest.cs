using Masasamjant.Collections;

namespace Masasamjant
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
        public void Test_AsciiDigits()
        {
            char[] expected = "0123456789".ToCharArray();
            char[] actual = CharHelper.AsciiDigits;
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsFalse(ReferenceEquals(CharHelper.AsciiDigits, CharHelper.AsciiDigits));
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

        [TestMethod]
        public void Test_GetSeparator()
        {
            var values = new List<string>()
            {
                "A98", "A48", "A34"
            };
            var separators = new List<char>();
            var separator = CharHelper.GetSeparator(values, separators);
            Assert.IsFalse(separator.HasValue);
            separators.Add('A');
            separators.Add('3');
            separator = CharHelper.GetSeparator(values, separators);
            Assert.IsFalse(separator.HasValue);
            separators.Add('|');
            separator = CharHelper.GetSeparator(values, separators);
            Assert.AreEqual('|', separator);
        }

        [TestMethod]
        public void Test_IsNumberOrLetter()
        {
            string a = "abcdefghijklmnopqrstuvwxyzäöå";
            string b = a.ToUpperInvariant();
            string c = "0123456789";
            string d = " ";
            string f = "!@£#$¤%&{}()[]=+?*¨^~";
            a.ForEach(x => Assert.IsTrue(CharHelper.IsNumberOrLetter(x)));
            b.ForEach(x => Assert.IsTrue(CharHelper.IsNumberOrLetter(x)));
            c.ForEach(x => Assert.IsTrue(CharHelper.IsNumberOrLetter(x)));
            d.ForEach(x => Assert.IsFalse(CharHelper.IsNumberOrLetter(x)));
            f.ForEach(x => Assert.IsFalse(CharHelper.IsNumberOrLetter(x)));
        }
    }
}
