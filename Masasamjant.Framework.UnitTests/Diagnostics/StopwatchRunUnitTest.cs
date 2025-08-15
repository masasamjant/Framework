using System.Text;

namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class StopwatchRunUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var run = new StopwatchRun(StopwatchRunBehavior.CalculateTotalTime, StopwatchRunUnit.Milliseconds, 1024, false);
            Assert.AreEqual(StopwatchRunBehavior.CalculateTotalTime, run.Behavior);
            Assert.AreEqual(StopwatchRunUnit.Milliseconds, run.Unit);
            Assert.AreEqual(true, run.CanRecord);
            Assert.IsFalse(run.IsInitialized);

            run = new StopwatchRun(StopwatchRunBehavior.CalculateWriteTime, StopwatchRunUnit.Ticks);
            Assert.AreEqual(StopwatchRunBehavior.CalculateWriteTime, run.Behavior);
            Assert.AreEqual(StopwatchRunUnit.Ticks, run.Unit);
            Assert.AreEqual(true, run.CanRecord);
            Assert.IsFalse(run.IsInitialized);

            run = new StopwatchRun((StopwatchRunBehavior)999, (StopwatchRunUnit)999, 1024, false);
            Assert.AreEqual(StopwatchRunBehavior.CalculateTotalTime, run.Behavior);
            Assert.AreEqual(StopwatchRunUnit.Milliseconds, run.Unit);
            Assert.AreEqual(true, run.CanRecord);
            Assert.IsFalse(run.IsInitialized);
        }

        [TestMethod]
        public void Test_Initialize()
        {
            var run = new StopwatchRun(StopwatchRunBehavior.CalculateTotalTime, StopwatchRunUnit.Milliseconds);
            Assert.IsFalse(run.IsInitialized);
            run.Initialize();
            Assert.IsTrue(run.IsInitialized);
        }

        [TestMethod]
        public void Test_Recording()
        {
            var sb = new StringBuilder();
            var run = new StopwatchRun(StopwatchRunBehavior.CalculateTotalTime, StopwatchRunUnit.Milliseconds);
            Assert.ThrowsException<InvalidOperationException>(() => run.Record("Test"));
            run.Initialize();
            run.Record("[START]");
            run.Record("[RECORD1]");
            run.Record("[RECORD2]");
            run.Write(sb, "[FINAL]");
            Assert.IsFalse(run.IsInitialized);
            var s = sb.ToString();
            Assert.IsTrue(s.Contains("[START]"));
            Assert.IsTrue(s.Contains("[RECORD1]"));
            Assert.IsTrue(s.Contains("[RECORD2]"));
            Assert.IsTrue(s.Contains("[FINAL]"));
            var records = run.Records.ToList();
            Assert.AreEqual(4, records.Count);
            Assert.IsTrue(records[0].Message.Contains("[START]"));
            Assert.IsTrue(records[1].Message.Contains("[RECORD1]"));
            Assert.IsTrue(records[2].Message.Contains("[RECORD2]"));
            Assert.IsTrue(records[3].Message.Contains("[FINAL]"));
        }
    }
}
