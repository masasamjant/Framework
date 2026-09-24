namespace Masasamjant.Applications
{
    [TestClass]
    public class ArgumentsReaderUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Read()
        {
            var args = new string[] { "-name", "value", "--flag", "-count", "5", "-skip" };
            ArgumentReadCallback readCallback = (name, value) =>
            {
                return name != "skip";
            };

            var reader = ArgumentsReader.Read(args, readCallback);

            Assert.IsTrue(reader.Contains("name"));
            Assert.AreEqual("value", reader.GetValue("name"));

            Assert.IsTrue(reader.Contains("flag"));
            Assert.AreEqual(string.Empty, reader.GetValue("flag"));

            Assert.IsTrue(reader.Contains("count"));
            Assert.AreEqual("5", reader.GetValue("count"));

            Assert.IsFalse(reader.Contains("nonexistent"));
            Assert.AreEqual(string.Empty, reader.GetValue("nonexistent"));

            Assert.IsFalse(reader.Contains("skip"));
        }
    }
}
