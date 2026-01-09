using System.Text;

namespace Masasamjant
{
    [TestClass]
    public class StringHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Remove()
        {
            char[] except = ['A', 'B', 'C'];
            Assert.AreEqual(string.Empty, StringHelper.Remove(null, except));
            Assert.AreEqual(string.Empty, StringHelper.Remove("", except));
            Assert.AreEqual("Foo", StringHelper.Remove("Foo", new char[0]));
            Assert.AreEqual("ABC", StringHelper.Remove("AaBbCc", except));
        }

        [TestMethod]
        public void Test_Replace()
        {
            var value = "ABC";
            var map = new CharacterMap();
            Assert.AreEqual("ABC", StringHelper.Replace(value, map));
            value = "";
            map.Add('X', '#');
            Assert.AreEqual("", StringHelper.Replace(value, map));
            value = "ABC";
            Assert.AreEqual("ABC", StringHelper.Replace(value, map));
            map.Add('A', 'H');
            map.Add('B', 'E');
            Assert.AreEqual("HEC", StringHelper.Replace(value, map));
        }

        [TestMethod]
        public void Test_Left()
        {
            Assert.AreEqual(string.Empty, StringHelper.Left(null, 3));
            Assert.AreEqual(string.Empty, StringHelper.Left("", 3));
            Assert.AreEqual(string.Empty, StringHelper.Left("ABDC", 0));
            Assert.AreEqual(string.Empty, StringHelper.Left("ABDC", -1));
            Assert.AreEqual("ABD", StringHelper.Left("ABDC", 3));
            Assert.AreEqual("ABCD", StringHelper.Left("ABCD", 8));
        }

        [TestMethod]
        public void Test_Right()
        {
            Assert.AreEqual(string.Empty, StringHelper.Right(null, 3));
            Assert.AreEqual(string.Empty, StringHelper.Right("", 3));
            Assert.AreEqual(string.Empty, StringHelper.Right("ABDC", 0));
            Assert.AreEqual(string.Empty, StringHelper.Right("ABDC", -1));
            Assert.AreEqual("BDC", StringHelper.Right("ABDC", 3));
            Assert.AreEqual("ABCD", StringHelper.Right("ABCD", 8));
        }

        [TestMethod]
        public void Test_LeftCount()
        {
            Assert.AreEqual(0, StringHelper.LeftCount(null, 'A'));
            Assert.AreEqual(0, StringHelper.LeftCount("", 'A'));
            Assert.AreEqual(0, StringHelper.LeftCount("BAAB", 'A'));
            Assert.AreEqual(2, StringHelper.LeftCount("AAB", 'A'));
        }

        [TestMethod]
        public void Test_RightCount()
        {
            Assert.AreEqual(0, StringHelper.RightCount(null, 'A'));
            Assert.AreEqual(0, StringHelper.RightCount("", 'A'));
            Assert.AreEqual(0, StringHelper.RightCount("BAAB", 'A'));
            Assert.AreEqual(2, StringHelper.RightCount("BAA", 'A'));
        }

        [TestMethod]
        public void Test_Count()
        {
            Assert.AreEqual(0, StringHelper.Count(null, 'X'));
            Assert.AreEqual(0, StringHelper.Count("", 'X'));
            Assert.AreEqual(0, StringHelper.Count("837", 'X'));
            Assert.AreEqual(1, StringHelper.Count("12X2", 'X'));
            Assert.AreEqual(2, StringHelper.Count("12X849X3", 'X'));
            Assert.AreEqual(3, StringHelper.Count("12X8X9X3", 'X'));
            Assert.AreEqual(4, StringHelper.Count("1XX8X9X3", 'X'));
        }

        [TestMethod]
        public void Test_AfterFirst()
        {
            Assert.AreEqual("", StringHelper.AfterFirst(null, '-'));
            Assert.AreEqual("", StringHelper.AfterFirst("", '-'));
            Assert.AreEqual("", StringHelper.AfterFirst("123456789", '-'));
            Assert.AreEqual("456-789", StringHelper.AfterFirst("123-456-789", '-'));
        }

        [TestMethod]
        public void Test_AfterLast()
        {
            Assert.AreEqual("", StringHelper.AfterLast(null, '-'));
            Assert.AreEqual("", StringHelper.AfterLast("", '-'));
            Assert.AreEqual("", StringHelper.AfterLast("123456789", '-'));
            Assert.AreEqual("789", StringHelper.AfterLast("123-456-789", '-'));
        }

        [TestMethod]
        public void Test_BeforeFirst()
        {
            Assert.AreEqual("", StringHelper.BeforeFirst(null, '-'));
            Assert.AreEqual("", StringHelper.BeforeFirst("", '-'));
            Assert.AreEqual("", StringHelper.BeforeFirst("123456789", '-'));
            Assert.AreEqual("123", StringHelper.BeforeFirst("123-456-789", '-'));
        }

        [TestMethod]
        public void Test_BeforeLast()
        {
            Assert.AreEqual("", StringHelper.BeforeLast(null, '-'));
            Assert.AreEqual("", StringHelper.BeforeLast("", '-'));
            Assert.AreEqual("", StringHelper.BeforeLast("123456789", '-'));
            Assert.AreEqual("123-456", StringHelper.BeforeLast("123-456-789", '-'));
        }

        [TestMethod]
        public void Test_Between()
        {
            Assert.AreEqual("", StringHelper.Between(null, '-', '+'));
            Assert.AreEqual("", StringHelper.Between("", '-', '+'));
            Assert.AreEqual("", StringHelper.Between("1234", '-', '+'));
            Assert.AreEqual("", StringHelper.Between("12+34-56", '-', '+'));
            Assert.AreEqual("", StringHelper.Between("123-+45", '-', '+'));
            Assert.AreEqual("", StringHelper.Between("+45", '-', '+'));
            Assert.AreEqual("", StringHelper.Between("123-", '-', '+'));
            Assert.AreEqual("123", StringHelper.Between("123+45", '-', '+'));
            Assert.AreEqual("45", StringHelper.Between("123-45", '-', '+'));
            Assert.AreEqual("23", StringHelper.Between("1-23+45", '-', '+'));
            Assert.AreEqual("49", StringHelper.Between("394-49-4920", '-'));
            Assert.AreEqual("", StringHelper.Between("123++45", '+'));
        }

        [TestMethod]
        public void Test_Trim()
        {
            Assert.AreEqual("", StringHelper.Trim(null, StringTrimOptions.Start, '+'));
            Assert.AreEqual("", StringHelper.Trim("", StringTrimOptions.Start, '+'));
            Assert.AreEqual("123", StringHelper.Trim("123", StringTrimOptions.Start, '+'));
            Assert.AreEqual("+123", StringHelper.Trim("+123", StringTrimOptions.None, '+'));
            Assert.AreEqual("123+", StringHelper.Trim("+123+", StringTrimOptions.Start, '+'));
            Assert.AreEqual("+123", StringHelper.Trim("+123+", StringTrimOptions.End, '+'));
            Assert.AreEqual("123", StringHelper.Trim("+123+", StringTrimOptions.Start | StringTrimOptions.End, '+'));
            Assert.AreEqual("123", StringHelper.Trim("123", StringTrimOptions.Start, new char[0]));
        }

        [TestMethod]
        public void Test_TrimUntilStartWith()
        {
            var starts = new char[] { 'A' };
            Assert.AreEqual("", StringHelper.TrimUntilStartsWith(null, starts));
            Assert.AreEqual("", StringHelper.TrimUntilStartsWith("", starts));
            Assert.AreEqual("", StringHelper.TrimUntilStartsWith("123", starts));
            Assert.AreEqual("ABC", StringHelper.TrimUntilStartsWith("123+ABC", starts));
            Assert.ThrowsException<ArgumentException>(() => StringHelper.TrimUntilStartsWith("Test", new char[0]));
            Assert.AreEqual("ABC", StringHelper.TrimUntilStartsWith("123+ABC", 'A'));
        }

        [TestMethod]
        public void Test_TrimUntilEndsWith()
        {
            var ends = new char[] { 'A' };
            Assert.AreEqual("", StringHelper.TrimUntilEndsWith(null, ends));
            Assert.AreEqual("", StringHelper.TrimUntilEndsWith("", ends));
            Assert.AreEqual("", StringHelper.TrimUntilEndsWith("123", ends));
            Assert.AreEqual("123+A", StringHelper.TrimUntilEndsWith("123+ABC", ends));
            Assert.AreEqual("123+A", StringHelper.TrimUntilEndsWith("123+ABC", 'A'));
            Assert.ThrowsException<ArgumentException>(() => StringHelper.TrimUntilEndsWith("Test", new char[0]));
        }

        [TestMethod]
        public void Test_GetInvalidCharacters()
        {
            Assert.IsTrue(StringHelper.GetInvalidCharacters("", new[] { '!', '%' }).Count == 0);
            Assert.IsTrue(StringHelper.GetInvalidCharacters("!%", new char[0]).Count == 0);
            Assert.IsTrue(StringHelper.GetInvalidCharacters("123", new[] { '!', '%' }).Count == 0);
            var result = StringHelper.GetInvalidCharacters("123%%56!98", new[] { '!', '%' });
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.ContainsKey(3) && result.ContainsKey(4) && result.ContainsKey(7));
            Assert.IsTrue(result[3] == '%');
            Assert.IsTrue(result[4] == '%');
            Assert.IsTrue(result[7] == '!');
        }

        [TestMethod]
        public void Test_ToStringArray()
        {
            Assert.AreEqual(0, StringHelper.ToStringArray(null).Length);
            Assert.AreEqual(0, StringHelper.ToStringArray("").Length);
            CollectionAssert.AreEqual(new[] { "1", "2", "3" }, StringHelper.ToStringArray("123"));
        }

        [TestMethod]
        public void Test_HasWhiteSpace()
        {
            Assert.IsFalse(StringHelper.HasWhiteSpace(null));
            Assert.IsFalse(StringHelper.HasWhiteSpace(""));
            Assert.IsFalse(StringHelper.HasWhiteSpace("123"));
            Assert.IsTrue(StringHelper.HasWhiteSpace(" 123"));
            Assert.IsTrue(StringHelper.HasWhiteSpace("123 "));
            Assert.IsTrue(StringHelper.HasWhiteSpace(" 123 "));
        }

        [TestMethod]
        public void Test_HasLeadingWhiteSpace()
        {
            Assert.IsFalse(StringHelper.HasLeadingWhiteSpace(null));
            Assert.IsFalse(StringHelper.HasLeadingWhiteSpace(""));
            Assert.IsFalse(StringHelper.HasLeadingWhiteSpace("123"));
            Assert.IsTrue(StringHelper.HasLeadingWhiteSpace(" 123"));
            Assert.IsFalse(StringHelper.HasLeadingWhiteSpace("123 "));
            Assert.IsTrue(StringHelper.HasLeadingWhiteSpace(" 123 "));
        }

        [TestMethod]
        public void Test_HasTrailingWhiteSpace()
        {
            Assert.IsFalse(StringHelper.HasTrailingWhiteSpace(null));
            Assert.IsFalse(StringHelper.HasTrailingWhiteSpace(""));
            Assert.IsFalse(StringHelper.HasTrailingWhiteSpace("123"));
            Assert.IsFalse(StringHelper.HasTrailingWhiteSpace(" 123"));
            Assert.IsTrue(StringHelper.HasTrailingWhiteSpace("123 "));
            Assert.IsTrue(StringHelper.HasTrailingWhiteSpace(" 123 "));
        }

        [TestMethod]
        public void Test_GetByteArray()
        {
            string value = "";
            byte[] actual = StringHelper.GetByteArray(value);
            Assert.IsTrue(actual.Length == 0);
            actual = StringHelper.GetByteArray(value, Encoding.UTF8);
            Assert.IsTrue(actual.Length == 0);
            value = "Testing";
            byte[] expected = Encoding.Unicode.GetBytes(value);
            actual = StringHelper.GetByteArray(value);
            CollectionAssert.AreEqual(expected, actual);
            expected = Encoding.UTF8.GetBytes(value);
            actual = StringHelper.GetByteArray(value, Encoding.UTF8);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsTrueString()
        {
            Assert.IsFalse(StringHelper.IsTrueString(null));
            Assert.IsFalse(StringHelper.IsTrueString(string.Empty));
            Assert.IsFalse(StringHelper.IsTrueString("  "));
            Assert.IsFalse(StringHelper.IsTrueString("123"));
            Assert.IsFalse(StringHelper.IsTrueString("false"));
            Assert.IsFalse(StringHelper.IsTrueString("tru4"));
            Assert.IsTrue(StringHelper.IsTrueString("True"));
            Assert.IsTrue(StringHelper.IsTrueString("true"));
            Assert.IsTrue(StringHelper.IsTrueString("TRUE"));
            Assert.IsTrue(StringHelper.IsTrueString("TruE"));
        }

        [TestMethod]
        public void Test_IsFalseString()
        {
            Assert.IsFalse(StringHelper.IsFalseString(null));
            Assert.IsFalse(StringHelper.IsFalseString(string.Empty));
            Assert.IsFalse(StringHelper.IsFalseString("  "));
            Assert.IsFalse(StringHelper.IsFalseString("123"));
            Assert.IsFalse(StringHelper.IsFalseString("true"));
            Assert.IsTrue(StringHelper.IsFalseString("False"));
            Assert.IsTrue(StringHelper.IsFalseString("false"));
            Assert.IsTrue(StringHelper.IsFalseString("FALSE"));
            Assert.IsTrue(StringHelper.IsFalseString("FalsE"));
        }

        [TestMethod]
        public void Test_EnsureStartsWith()
        {
            Assert.AreEqual(".Home", StringHelper.EnsureStartsWith(".Home", '.'));
            Assert.AreEqual(".Home", StringHelper.EnsureStartsWith("Home", '.'));
            Assert.AreEqual("...Home", StringHelper.EnsureStartsWith("...Home", "..."));
            Assert.AreEqual("...Home", StringHelper.EnsureStartsWith("Home", "..."));
            Assert.AreEqual("...", StringHelper.EnsureStartsWith("...", "..."));
            Assert.AreEqual("...", StringHelper.EnsureStartsWith("", "..."));
        }

        [TestMethod]
        public void Test_EnsureEndsWith()
        {
            Assert.AreEqual("Home.", StringHelper.EnsureEndsWith("Home.", '.'));
            Assert.AreEqual("Home.", StringHelper.EnsureEndsWith("Home", '.'));
            Assert.AreEqual("Home...", StringHelper.EnsureEndsWith("Home...", "..."));
            Assert.AreEqual("Home...", StringHelper.EnsureEndsWith("Home", "..."));
            Assert.AreEqual("...", StringHelper.EnsureEndsWith("...", "..."));
            Assert.AreEqual("...", StringHelper.EnsureEndsWith("", "..."));
        }

        [TestMethod]
        public void Test_ContainsOnlyLettersAndNumbers()
        {
            string a = "abcdefghijklmnopqrstuvwxyzäöå";
            string b = a.ToUpperInvariant();
            string c = "0123456789";
            string d = " ";
            string f = "!@£#$¤%&{}()[]=+?*¨^~";
            Assert.IsFalse(StringHelper.ContainsOnlyLettersAndNumbers(""));
            Assert.IsTrue(StringHelper.ContainsOnlyLettersAndNumbers(a));
            Assert.IsTrue(StringHelper.ContainsOnlyLettersAndNumbers(b));
            Assert.IsTrue(StringHelper.ContainsOnlyLettersAndNumbers(c));
            Assert.IsFalse(StringHelper.ContainsOnlyLettersAndNumbers(d));
            Assert.IsFalse(StringHelper.ContainsOnlyLettersAndNumbers(f));
        }

        [TestMethod]
        public void Test_StartsWithAny()
        {
            Assert.ThrowsException<ArgumentException>(() => StringHelper.StartsWithAny("", null, (StringComparison)999));
            Assert.IsFalse(StringHelper.StartsWithAny("Foo123", null));
            Assert.IsFalse(StringHelper.StartsWithAny(null, ["1", "2"]));
            Assert.IsFalse(StringHelper.StartsWithAny("Foo123", Enumerable.Empty<string>()));
            Assert.IsFalse(StringHelper.StartsWithAny("Foo123", ["1", "2"]));
            Assert.IsTrue(StringHelper.StartsWithAny("Foo123", ["1", "Fo"]));
            Assert.IsTrue(StringHelper.StartsWithAny("Foo123", ["1", "", "Fo"]));
        }

        [TestMethod]
        public void Test_EndsWithAny()
        {
            Assert.ThrowsException<ArgumentException>(() => StringHelper.EndsWithAny("", null, (StringComparison)999));
            Assert.IsFalse(StringHelper.EndsWithAny("Foo123", null));
            Assert.IsFalse(StringHelper.EndsWithAny(null, ["1", "2"]));
            Assert.IsFalse(StringHelper.EndsWithAny("Foo123", Enumerable.Empty<string>()));
            Assert.IsFalse(StringHelper.EndsWithAny("Foo123", ["1", "Fo"]));
            Assert.IsTrue(StringHelper.EndsWithAny("Foo123", ["1", "23"]));
            Assert.IsTrue(StringHelper.EndsWithAny("Foo123", ["1", "", "23"]));
        }

        [TestMethod]
        public void Test_Truncate()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => StringHelper.Truncate("Foobar", -1));
            Assert.ThrowsException<ArgumentException>(() => StringHelper.Truncate("Foobar", 2, "..."));
            Assert.AreEqual(string.Empty, StringHelper.Truncate("", 3));
            Assert.AreEqual(string.Empty, StringHelper.Truncate("Foobar", 0));
            Assert.AreEqual("Foo", StringHelper.Truncate("Foobar", 3));
            Assert.AreEqual("F..", StringHelper.Truncate("Foobar", 3, ".."));
            Assert.AreEqual("Foobar", StringHelper.Truncate("Foobar", 8));
        }

        [TestMethod]
        public void Test_Lines()
        {
            var lines = StringHelper.Lines(null);
            Assert.IsFalse(lines.Any());
            lines = StringHelper.Lines("");
            Assert.IsFalse(lines.Any());
            lines = StringHelper.Lines("Line1" + Environment.NewLine + "Line2");
            Assert.IsTrue(lines.Count() == 2);
            Assert.AreEqual("Line1", lines.First());
            Assert.AreEqual("Line2", lines.Last());
        }

        [TestMethod]
        public async Task Test_LinesAsync()
        {
            var lines = await StringHelper.LinesAsync(null);
            Assert.IsFalse(lines.Any());
            lines = await StringHelper.LinesAsync("");
            Assert.IsFalse(lines.Any());
            lines = await StringHelper.LinesAsync("Line1" + Environment.NewLine + "Line2");
            Assert.IsTrue(lines.Count() == 2);
            Assert.AreEqual("Line1", lines.First());
            Assert.AreEqual("Line2", lines.Last());
        }
    }
}
