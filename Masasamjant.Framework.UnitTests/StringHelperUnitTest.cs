namespace Masasamjant
{
    [TestClass]
    public class StringHelperUnitTest : UnitTest
    {
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
        }

        [TestMethod]
        public void Test_Right()
        {
            Assert.AreEqual(string.Empty, StringHelper.Right(null, 3));
            Assert.AreEqual(string.Empty, StringHelper.Right("", 3));
            Assert.AreEqual(string.Empty, StringHelper.Right("ABDC", 0));
            Assert.AreEqual(string.Empty, StringHelper.Right("ABDC", -1));
            Assert.AreEqual("BDC", StringHelper.Right("ABDC", 3));
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
        [ExpectedException(typeof(ArgumentException))]
        public void Test_TrimUntilStartsWith_Require_One_Start()
        {
            StringHelper.TrimUntilStartsWith("Test", new char[0]);
        }

        [TestMethod]
        public void Test_TrimUntilStartWith()
        {
            var starts = new char[] { 'A' };
            Assert.AreEqual("", StringHelper.TrimUntilStartsWith(null, starts));
            Assert.AreEqual("", StringHelper.TrimUntilStartsWith("", starts));
            Assert.AreEqual("", StringHelper.TrimUntilStartsWith("123", starts));
            Assert.AreEqual("ABC", StringHelper.TrimUntilStartsWith("123+ABC", starts));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_TrimUntilEndsWith_Require_One_End()
        {
            StringHelper.TrimUntilEndsWith("Test", new char[0]);
        }

        [TestMethod]
        public void Test_TrimUntilEndsWith()
        {
            var ends = new char[] { 'A' };
            Assert.AreEqual("", StringHelper.TrimUntilEndsWith(null, ends));
            Assert.AreEqual("", StringHelper.TrimUntilEndsWith("", ends));
            Assert.AreEqual("", StringHelper.TrimUntilEndsWith("123", ends));
            Assert.AreEqual("123+A", StringHelper.TrimUntilEndsWith("123+ABC", ends));
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
    }
}
