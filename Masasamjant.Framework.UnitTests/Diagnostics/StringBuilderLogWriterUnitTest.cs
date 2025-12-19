using System.Globalization;
using System.Text;

namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class StringBuilderLogWriterUnitTest : UnitTest
    {
        private const string ErrorMessage = "Error message";
        private const string InformationMessage = "Information message";
        private const string WarningMessage = "Warning message";

        [TestMethod]
        public async Task Test_Log_Write_Without_Time()
        {
            var builder = new StringBuilder();
            var writer = new StringBuilderLogWriter(builder);
            writer.AppendTime = false;
            await writer.WriteErrorAsync(ErrorMessage, GetType());
            await writer.WriteInformationAsync(InformationMessage, GetType());
            await writer.WriteWarningAsync(WarningMessage, GetType());
            var content = builder.ToString();
            var lines = (await content.LinesAsync()).ToArray();
            Assert.AreEqual(3, lines.Length);
            var typeName = GetType().FullName ?? GetType().Name;
            AssertLine(lines[0], [TextLogWriter.ErrorCategory, ErrorMessage, typeName]);
            AssertLine(lines[1], [TextLogWriter.InformationCategory, InformationMessage, typeName]);
            AssertLine(lines[2], [TextLogWriter.WarningCategory, WarningMessage, typeName]);
        }

        [TestMethod]
        public async Task Test_Log_Write_With_Time()
        {
            var localTime = DateTime.Now;
            var builder = new StringBuilder();
            var writer = new TestStringBuilderLogWriter(builder, localTime);
            writer.AppendTime = true;
            await writer.WriteErrorAsync(ErrorMessage, GetType());
            await writer.WriteInformationAsync(InformationMessage, GetType());
            await writer.WriteWarningAsync(WarningMessage, GetType());
            var content = builder.ToString();
            var lines = (await content.LinesAsync()).ToArray();
            Assert.AreEqual(3, lines.Length);
            var typeName = GetType().FullName ?? GetType().Name;
            var timeString = localTime.ToString(CultureInfo.InvariantCulture);
            AssertLine(lines[0], [TextLogWriter.ErrorCategory, ErrorMessage, typeName]);
            AssertLine(lines[1], [TextLogWriter.InformationCategory, InformationMessage, typeName, timeString]);
            AssertLine(lines[2], [TextLogWriter.WarningCategory, WarningMessage, typeName, timeString]);
        }

        private class TestStringBuilderLogWriter : StringBuilderLogWriter
        {
            private readonly DateTime localTime;

            public TestStringBuilderLogWriter(StringBuilder builder, DateTime localTime)
                : base(builder) 
            {
                this.localTime = localTime;
            }

            protected override DateTime GetLocalDateTime()
            {
                return localTime;
            }
        }
    }
}
