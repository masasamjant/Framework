using Microsoft.Extensions.Logging;
using System.Text;

namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class LogWriterUnitTest : UnitTest
    {
        private const string ErrorMessage = "Error message";
        private const string WarningMessage = "Warning message";
        private const string InformationMessage = "Information message";

        [TestMethod]
        public async Task Test_LogWriter()
        {
            var builder = new StringBuilder();
            var logger = new TestLogger(builder);
            var factory = new TestLoggerFactory(logger);
            var writer = new LogWriter(factory);
            await writer.WriteErrorAsync(ErrorMessage, GetType());
            await writer.WriteWarningAsync(WarningMessage, GetType());
            await writer.WriteInformationAsync(InformationMessage, GetType());
            var content = builder.ToString();
            var lines = (await content.LinesAsync()).ToArray();
            Assert.AreEqual(3, lines.Length);
            AssertLine(lines[0], [LogLevel.Error.ToString(), ErrorMessage]);
            AssertLine(lines[1], [LogLevel.Warning.ToString(), WarningMessage]);
            AssertLine(lines[2], [LogLevel.Information.ToString(), InformationMessage]);
        }

        private class TestLogger : ILogger
        {
            private StringBuilder builder;

            public TestLogger(StringBuilder builder)
            {
                this.builder = builder;
            }

            public IDisposable? BeginScope<TState>(TState state) where TState : notnull
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                throw new NotImplementedException();
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {
                var message = logLevel.ToString() + " " + formatter(state, exception);
                builder.AppendLine(message);
            }
        }

        private class TestLoggerFactory : ILoggerFactory
        {
            private TestLogger logger;

            public TestLoggerFactory(TestLogger logger)
            {
                this.logger = logger;
            }

            public void AddProvider(ILoggerProvider provider)
            {
                throw new NotImplementedException();
            }

            public ILogger CreateLogger(string categoryName)
            {
                return logger;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}
