namespace Masasamjant
{
    [TestClass]
    public class TimingHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsNow()
        {
            DateTime startDateTime = DateTime.Now.AddHours(-2);
            DateTime endDateTime = DateTime.Now.AddHours(2);
            var timing = new Timing(startDateTime, endDateTime);
            Assert.IsTrue(TimingHelper.IsNow(timing));
            startDateTime = DateTime.Now.AddHours(1);
            endDateTime = DateTime.Now.AddHours(2);
            timing = new Timing(startDateTime, endDateTime);
            Assert.IsFalse(TimingHelper.IsNow(timing));
        }

        [TestMethod]
        public void Test_IsFuture()
        {
            DateTime startDateTime = DateTime.Now.AddHours(-2);
            DateTime endDateTime = DateTime.Now.AddHours(2);
            var timing = new Timing(startDateTime, endDateTime);
            Assert.IsFalse(TimingHelper.IsFuture(timing));
            startDateTime = DateTime.Now.AddHours(1);
            endDateTime = DateTime.Now.AddHours(2);
            timing = new Timing(startDateTime, endDateTime);
            Assert.IsTrue(TimingHelper.IsFuture(timing));
        }

        [TestMethod]
        public void Test_IsOver()
        {
            DateTime startDateTime = DateTime.Now.AddHours(-2);
            DateTime endDateTime = DateTime.Now.AddHours(2);
            var timing = new Timing(startDateTime, endDateTime);
            Assert.IsFalse(TimingHelper.IsOver(timing));
            startDateTime = DateTime.Now.AddHours(1);
            endDateTime = DateTime.Now.AddHours(2);
            timing = new Timing(startDateTime, endDateTime);
            Assert.IsFalse(TimingHelper.IsOver(timing));
            startDateTime = DateTime.Now.AddHours(-2);
            endDateTime = DateTime.Now.AddHours(-2);
            timing = new Timing(startDateTime, endDateTime);
            Assert.IsTrue(TimingHelper.IsOver(timing));
        }
    }
}
