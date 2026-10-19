namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class FrameworkTimerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor_WithDefaultInterval()
        {
            var timer = new FrameworkTimer();
            Assert.IsNotNull(timer);
            Assert.AreEqual(100, timer.Interval);
        }

        [TestMethod]
        public void Test_Constructor_WithCustomInterval()
        {
            var timer = new FrameworkTimer(TimeSpan.FromMilliseconds(500));
            Assert.IsNotNull(timer);
            Assert.AreEqual(500, timer.Interval);
        }

        [TestMethod]
        public void Test_Constructor_WithInvalidInterval()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FrameworkTimer(TimeSpan.FromMilliseconds(0)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FrameworkTimer(TimeSpan.FromMilliseconds(-100)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FrameworkTimer(TimeSpan.FromMilliseconds(int.MaxValue + 1.0)));
        }

        [TestMethod]
        public void Test_Elapsed_Event_Raised()
        {
            var timer = new FrameworkTimer(TimeSpan.FromMilliseconds(100));
            bool eventRaised = false;
            timer.Elapsed += (sender, e) => { eventRaised = true; };
            timer.Start();
            Thread.Sleep(200);
            timer.Stop();
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void Test_RaiseElapsed_Method()
        {
            var timer = new FrameworkTimer();
            bool eventRaised = false;
            timer.Elapsed += (sender, e) => { eventRaised = true; };
            timer.RaiseElapsed();
            Assert.IsTrue(eventRaised);
            timer.AllowRaiseElapsed = false;
            eventRaised = false;
            timer.RaiseElapsed();
            Assert.IsFalse(eventRaised);
        }

        [TestMethod]
        public void Test_Setting_Invalid_Interval()
        {
            var timer = new FrameworkTimer();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => timer.Interval = 0);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => timer.Interval = -100);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => timer.Interval = int.MaxValue + 1.0);
        }

        [TestMethod]
        public void Test_Dispose_Method()
        {
            var timer = new FrameworkTimer();
            timer.Dispose();
            Assert.ThrowsException<ObjectDisposedException>(() => timer.Start());
            Assert.ThrowsException<ObjectDisposedException>(() => timer.Stop());
            Assert.ThrowsException<ObjectDisposedException>(() => timer.Interval = 100);
            Assert.ThrowsException<ObjectDisposedException>(() => timer.Enabled = true);
            Assert.ThrowsException<ObjectDisposedException>(() => timer.RaiseElapsed());
            Assert.ThrowsException<ObjectDisposedException>(() => timer.AutoReset = false);
        }
    }
}
