using System.Text;

namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class MemoryInfoMonitorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Create()
        {
            var monitor = MemoryInfoMonitor.Create();
            Assert.IsFalse(monitor.AllowMemoryCollection);
            monitor = MemoryInfoMonitor.Create(true);
            Assert.IsTrue(monitor.AllowMemoryCollection);
            monitor.AllowMemoryCollection = false;
            Assert.IsFalse(monitor.AllowMemoryCollection);
        }

        [TestMethod]
        public void Test_GetCurrent()
        {
            var monitor = MemoryInfoMonitor.Create(true);
            var current = monitor.GetCurrent();
            Assert.IsNotNull(current);
            Assert.IsTrue(current.TotalBytes > 0);
            string s = new string('A', 100000);
            var other = monitor.GetCurrent();
            Assert.AreNotEqual(current, other);
        }

        [TestMethod]
        public void Test_StartMonitoring()
        {
            var interval = TimeSpan.FromMilliseconds(100);
            var monitor = MemoryInfoMonitor.Create(true);
            monitor.Dispose();
            Assert.ThrowsException<ObjectDisposedException>(() => monitor.StartMonitoring(interval));
            monitor = MemoryInfoMonitor.Create(true);
            Assert.IsFalse(monitor.IsMonitoring);
            monitor.StartMonitoring(interval);
            Assert.IsTrue(monitor.IsMonitoring);
            Assert.ThrowsException<InvalidOperationException>(() => monitor.StartMonitoring(interval));
            monitor.Dispose();
        }

        [TestMethod]
        public void Test_StopMonitoring() 
        {
            var interval = TimeSpan.FromMilliseconds(100);
            var monitor = MemoryInfoMonitor.Create(true);
            monitor.Dispose();
            Assert.ThrowsException<ObjectDisposedException>(() => monitor.StopMonitoring());
            monitor = MemoryInfoMonitor.Create(true);
            Assert.IsFalse(monitor.IsMonitoring);
            monitor.StartMonitoring(interval);
            Assert.IsTrue(monitor.IsMonitoring);
            monitor.StopMonitoring();
            Assert.IsFalse(monitor.IsMonitoring);
            monitor.Dispose();
        }

        [TestMethod]
        public void Test_Dispose()
        {
            var interval = TimeSpan.FromMilliseconds(100);
            var monitor = MemoryInfoMonitor.Create(true);
            Assert.IsFalse(monitor.IsMonitoring);
            monitor.StartMonitoring(interval);
            Assert.IsTrue(monitor.IsMonitoring);
            monitor.Dispose();
            Assert.IsFalse(monitor.IsMonitoring);
        }

        [TestMethod]
        public void Test_Monitoring()
        {
            var sb = new StringBuilder();
            var interval = TimeSpan.FromMilliseconds(100);
            var monitor = MemoryInfoMonitor.Create(true);
            monitor.StartMonitoring(interval);
            var list = new List<MemoryInfo>();
            monitor.MonitoringMemoryRead += (s, e) => list.Add(e.Memory);
            int n = 0;
            while (n < 12)
            {
                sb.Append(new string('A', 100000));
                n++;
                Thread.Sleep(40);
            }
            var monitored = monitor.GetMonitoredMemory().ToList();
            monitor.StopMonitoring();
            monitor.Dispose();

            foreach (var item in monitored)
                Assert.IsTrue(list.Contains(item));
        }
    }
}
