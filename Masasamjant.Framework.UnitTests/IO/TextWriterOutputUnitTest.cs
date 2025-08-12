using System.Text;

namespace Masasamjant.IO
{
    [TestClass]
    public class TextWriterOutputUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Write()
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var writer = new StringWriter(sb1);
            var output = new TextWriterOutput(writer);
            output.Write("1");
            sb2.Append("1");
            output.Write("2");
            sb2.Append("2");
            Assert.AreEqual(sb2.ToString(), sb1.ToString());
        }

        [TestMethod]
        public void Test_WriteLine()
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var writer = new StringWriter(sb1);
            var output = new TextWriterOutput(writer);
            output.WriteLine("1");
            sb2.AppendLine("1");
            output.WriteLine("2");
            sb2.AppendLine("2");
            Assert.AreEqual(sb2.ToString(), sb1.ToString());
        }

        [TestMethod]
        public async Task Test_WriteAsync()
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var writer = new StringWriter(sb1);
            var output = new TextWriterOutput(writer);
            await output.WriteAsync("1");
            sb2.Append("1");
            await output.WriteAsync("2");
            sb2.Append("2");
            Assert.AreEqual(sb2.ToString(), sb1.ToString());
        }

        [TestMethod]
        public async Task Test_WriteLineAsync()
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var writer = new StringWriter(sb1);
            var output = new TextWriterOutput(writer);
            await output.WriteLineAsync("1");
            sb2.AppendLine("1");
            await output.WriteLineAsync("2");
            sb2.AppendLine("2");
            Assert.AreEqual(sb2.ToString(), sb1.ToString());
        }
    }
}
