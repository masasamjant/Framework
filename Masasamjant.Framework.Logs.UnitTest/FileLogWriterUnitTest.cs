using Masasamjant.Diagnostics.EventLogs;
using System.Text;

namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class FileLogWriterUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor_When_Settings_Null_Then_ThrowsArgumentNullException()
        {
            FileLogWriterSettings settings = null!;
            ILogMessageFormatter formatter = new DefaultLogMessageFormatter();
            IEventLogFactory eventLogFactory = new DefaultEventLogFactory();
            IFileWriter writer = new TestFileWriter(new StringBuilder());
            Assert.ThrowsExactly<ArgumentNullException>(() => new FileLogWriter(settings, formatter, eventLogFactory, writer));
        }

        [TestMethod]
        public void Test_Constructor_When_Formatter_Null_Then_ThrowsArgumentNullException()
        {
            FileLogWriterSettings settings = new SingleFileLogWriterSettings("C:\\Logs\\log.txt", 10, 500);
            ILogMessageFormatter formatter = null!;
            IEventLogFactory eventLogFactory = new DefaultEventLogFactory();
            IFileWriter writer = new TestFileWriter(new StringBuilder());
            Assert.ThrowsExactly<ArgumentNullException>(() => new FileLogWriter(settings, formatter, eventLogFactory, writer));
        }

        [TestMethod]
        public void Test_Constructor_When_EventLogFactory_Null_Then_ThrowsArgumentNullException()
        {
            FileLogWriterSettings settings = new SingleFileLogWriterSettings("C:\\Logs\\log.txt", 10, 500);
            ILogMessageFormatter formatter = new DefaultLogMessageFormatter();
            IEventLogFactory eventLogFactory = null!;
            IFileWriter writer = new TestFileWriter(new StringBuilder());
            Assert.ThrowsExactly<ArgumentNullException>(() => new FileLogWriter(settings, formatter, eventLogFactory, writer));
        }

        [TestMethod]
        public void Test_Constructor_When_FileWriter_Null_Then_ThrowsArgumentNullException()
        {
            FileLogWriterSettings settings = new SingleFileLogWriterSettings("C:\\Logs\\log.txt", 10, 500);
            ILogMessageFormatter formatter = new DefaultLogMessageFormatter();
            IEventLogFactory eventLogFactory = new DefaultEventLogFactory();
            IFileWriter writer = null!;
            Assert.ThrowsExactly<ArgumentNullException>(() => new FileLogWriter(settings, formatter, eventLogFactory, writer));
        }

        [TestMethod]
        public async Task Test_When_Flushed_Then_Writes_To_File()
        {
            var builder = new StringBuilder();
            FileLogWriterSettings settings = new SingleFileLogWriterSettings("C:\\Logs\\log.txt", 10, 500);
            ILogMessageFormatter formatter = new DefaultLogMessageFormatter();
            IEventLogFactory eventLogFactory = new DefaultEventLogFactory();
            IFileWriter writer = new TestFileWriter(builder);
            using (FileLogWriter logWriter = new FileLogWriter(settings, formatter, eventLogFactory, writer))
            {
                await logWriter.WriteInformationAsync("Test message", typeof(FileLogWriterUnitTest));
                await logWriter.FlushAsync();
            }
            string logContent = builder.ToString();
            Assert.Contains("Test message", logContent);
        }

        [TestMethod]
        public async Task Test_When_Batch_Size_Reached_Then_Writes_To_File()
        {
            var builder = new StringBuilder();
            FileLogWriterSettings settings = new SingleFileLogWriterSettings("C:\\Logs\\log.txt", 3, 5000);
            ILogMessageFormatter formatter = new DefaultLogMessageFormatter();
            IEventLogFactory eventLogFactory = new DefaultEventLogFactory();
            IFileWriter writer = new TestFileWriter(builder);
            using (FileLogWriter logWriter = new FileLogWriter(settings, formatter, eventLogFactory, writer))
            {
                await logWriter.WriteInformationAsync("Test message 1", typeof(FileLogWriterUnitTest));
                await logWriter.WriteInformationAsync("Test message 2", typeof(FileLogWriterUnitTest));
                await logWriter.WriteInformationAsync("Test message 3", typeof(FileLogWriterUnitTest)); // This should trigger a flush due to batch size being reached.
            }

            string logContent = builder.ToString();
            Assert.Contains("Test message 1", logContent);
            Assert.Contains("Test message 2", logContent);
            Assert.Contains("Test message 3", logContent);
        }

        [TestMethod]
        public async Task Test_When_Interval_Reached_Then_Writes_To_File()
        {
            var builder = new StringBuilder();
            FileLogWriterSettings settings = new SingleFileLogWriterSettings("C:\\Logs\\log.txt", 20, 150);
            ILogMessageFormatter formatter = new DefaultLogMessageFormatter();
            IEventLogFactory eventLogFactory = new DefaultEventLogFactory();
            IFileWriter writer = new TestFileWriter(builder);
            using (FileLogWriter logWriter = new FileLogWriter(settings, formatter, eventLogFactory, writer))
            {
                await logWriter.WriteInformationAsync("Test message 1", typeof(FileLogWriterUnitTest));
                await logWriter.WriteInformationAsync("Test message 2", typeof(FileLogWriterUnitTest));
                await logWriter.WriteInformationAsync("Test message 3", typeof(FileLogWriterUnitTest));
                await Task.Delay(200);
            }

            string logContent = builder.ToString();
            Assert.Contains("Test message 1", logContent);
            Assert.Contains("Test message 2", logContent);
            Assert.Contains("Test message 3", logContent);
        }

        private class TestFileWriter : IFileWriter
        {
            private readonly StringBuilder builder;

            public TestFileWriter(StringBuilder builder)
            {
                this.builder = builder;
            }

            public Task AppendAllLinesAsync(string filePath, IEnumerable<string> lines)
            {
                foreach (var line in lines)
                    builder.AppendLine(line);

                return Task.CompletedTask;
            }
        }
    }
}
