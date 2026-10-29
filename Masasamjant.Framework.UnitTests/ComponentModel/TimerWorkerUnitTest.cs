using System.Text;

namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class TimerWorkerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_TimerWorker_StartAndStop()
        {
            StringBuilder builder = new StringBuilder();

            TimerWorker worker = new TestTimerWorker(TimeSpan.FromMilliseconds(20), builder);
            Assert.IsFalse(worker.Started);
            worker.Start();
            Assert.IsTrue(worker.Started);
            Thread.Sleep(60);
            worker.Stop();
            Assert.IsFalse(worker.Started);
            worker.Dispose();

            string output = builder.ToString();
            Assert.IsTrue(output.Contains("Run executed."));
            Assert.IsTrue(output.Contains("Stopped execution due to cancellation."));
        }

        [TestMethod]
        public void Test_TimerWorker_StartAndDispose()
        {
            StringBuilder builder = new StringBuilder();

            TimerWorker worker = new TestTimerWorker(TimeSpan.FromMilliseconds(20), builder);
            Assert.IsFalse(worker.Started);
            worker.Start();
            Assert.IsTrue(worker.Started);
            Thread.Sleep(80);
            worker.Dispose();
            Assert.IsFalse(worker.Started);

            Assert.ThrowsException<ObjectDisposedException>(() => worker.Start());

            string output = builder.ToString();
            Assert.IsTrue(output.Contains("Run executed."));
            Assert.IsTrue(output.Contains("Stopped execution due to cancellation."));
        }

        private class TestTimerWorker : TimerWorker
        {
            private readonly StringBuilder builder;

            public TestTimerWorker(TimeSpan executionInterval, StringBuilder builder) 
                : base(executionInterval)
            {
                this.builder = builder;
            }
            
            protected override void Run(CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (builder.Length == 0)
                        builder.AppendLine("Run executed.");
                }

                builder.AppendLine("Stopped execution due to cancellation.");
            }
        }
    }
}
