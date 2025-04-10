namespace Masasamjant.Text
{
    [TestClass]
    public class KeyValueConcatStringUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var cs = new KeyValueConcatString();
            Assert.IsTrue(cs.IsEmpty);
            Assert.IsTrue(cs.Values.Count == 0);
            Assert.IsTrue(cs.ItemSeparator == ConcatString.NoSeparator);
            Assert.IsTrue(cs.KeyValueSeparator == ConcatString.NoSeparator);
            Assert.ThrowsException<ArgumentException>(() => 
            { 
                cs = new KeyValueConcatString(ConcatString.NoSeparator, '|', new Dictionary<string, string>());
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                cs = new KeyValueConcatString('|', ConcatString.NoSeparator, new Dictionary<string, string>());
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                cs = new KeyValueConcatString('|', '|', new Dictionary<string, string>());
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                cs = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key|", "value" } });
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                cs = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "value|" } });
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                cs = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key+", "value" } });
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                cs = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "value+" } });
            });

            cs = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "value" } });
            Assert.IsFalse(cs.IsEmpty);
            Assert.AreEqual('+', cs.ItemSeparator);
            Assert.AreEqual('|', cs.KeyValueSeparator);
        }

        [TestMethod]
        public void Test_Concat()
        {
            var cs = new KeyValueConcatString();
            Assert.AreEqual(string.Empty, cs.Concat());
            var values = new Dictionary<string, string>();
            cs = new KeyValueConcatString('+', '|', values);
            Assert.AreEqual("+|", cs.Concat());
            values.Add("1", "1");
            values.Add("2", "2");
            Assert.AreEqual("+|1|1+2|2", cs.Concat());
        }

        [TestMethod]
        public void Test_Equals()
        {
            var a = new KeyValueConcatString();
            Assert.IsFalse(a.Equals(null));
            var b = new KeyValueConcatString();
            Assert.IsTrue(a.Equals(b));
            a = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "value" } });
            Assert.IsFalse(a.Equals(b));
            b = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "ke", "value" } });
            Assert.IsFalse(a.Equals(b));
            b = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "valu" } });
            Assert.IsFalse(a.Equals(b));
            b = new KeyValueConcatString('|', '+', new Dictionary<string, string> { { "key", "value" } });
            Assert.IsFalse(a.Equals(b));
            b = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "value" } });
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var a = new KeyValueConcatString();
            var b = new KeyValueConcatString();
            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            a = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "value" } });
            b = new KeyValueConcatString('+', '|', new Dictionary<string, string> { { "key", "value" } });
            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Test_ParseFrom()
        {
            var cs = new KeyValueConcatString();
            cs.ParseFrom(string.Empty);
            Assert.AreEqual(new KeyValueConcatString(), cs);
            Assert.ThrowsException<FormatException>(() => cs.ParseFrom("a"));
            cs.ParseFrom("+|");
            Assert.AreEqual('+', cs.ItemSeparator);
            Assert.AreEqual('|', cs.KeyValueSeparator);
            Assert.AreEqual(0, cs.Values.Count);
            cs.ParseFrom("+|1|1+2|2");
            Assert.AreEqual('+', cs.ItemSeparator);
            Assert.AreEqual('|', cs.KeyValueSeparator);
            Assert.IsTrue(cs.Values["1"] == "1");
            Assert.IsTrue(cs.Values["2"] == "2");
        }
    }
}
