using System.Diagnostics;

namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class StopwatchScopeUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_StopwatchScope()
        {
            var stopwatch = new Stopwatch();

            using (var scope = StopwatchScope.Create(stopwatch))
            {
                Assert.IsTrue(stopwatch.IsRunning);
                Thread.Sleep(150);
            }

            Assert.IsFalse(stopwatch.IsRunning);
            Assert.IsTrue(stopwatch.ElapsedMilliseconds > 100);
        }
    }
}
