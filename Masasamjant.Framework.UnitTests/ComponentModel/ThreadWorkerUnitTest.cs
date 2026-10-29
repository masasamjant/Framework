using System.Diagnostics;
using System.Text;

namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class ThreadWorkerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ThreadWorker_StartAndStop()
        {
            StringBuilder builder = new StringBuilder();

            ThreadWorker worker = new TestThreadWorker("TestThreadWorker", builder);
            Assert.IsFalse(worker.Started);
            worker.Start();
            Thread.Sleep(10);
            Assert.IsTrue(worker.Started);
            worker.Stop();
            Assert.IsFalse(worker.Started);
            worker.Dispose();

            string output = builder.ToString();
            Assert.IsTrue(output.Contains("Working at"));
            Assert.IsTrue(output.Contains("Stopped after"));
        }

        [TestMethod]
        public void Test_ThreadWorker_StartAndDispose()
        {
            StringBuilder builder = new StringBuilder();

            ThreadWorker worker = new TestThreadWorker("TestThreadWorker", builder);
            Assert.IsFalse(worker.Started);
            worker.Start();
            Thread.Sleep(10);
            Assert.IsTrue(worker.Started);
            worker.Dispose();
            Assert.IsFalse(worker.Started);

            Assert.ThrowsException<ObjectDisposedException>(() => worker.Start());

            string output = builder.ToString();
            Assert.IsTrue(output.Contains("Working at"));
            Assert.IsTrue(output.Contains("Stopped after"));
        }

        private class TestThreadWorker : ThreadWorker
        {
            private readonly StringBuilder builder;

            public TestThreadWorker(string? threadName, StringBuilder builder) 
                : base(threadName)
            {
                this.builder = builder;
            }

            protected override void Run(CancellationToken cancellationToken)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (builder.Length == 0)
                        builder.AppendLine("Working at " + DateTime.Now.ToString("HH:mm:ss.fff"));
                }

                stopwatch.Stop();
                builder.AppendLine("Stopped after " + stopwatch.ElapsedMilliseconds + " ms");
            }
        }
    }
}
