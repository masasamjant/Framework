namespace Masasamjant
{
    [TestClass]
    public class TimeSpanHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsZero()
        {
            Assert.IsFalse(TimeSpanHelper.IsZero(TimeSpan.FromTicks(-1)));
            Assert.IsTrue(TimeSpanHelper.IsZero(TimeSpan.Zero));
            Assert.IsTrue(TimeSpanHelper.IsZero(TimeSpan.FromTicks(0)));
            Assert.IsFalse(TimeSpanHelper.IsZero(TimeSpan.FromTicks(1)));
        }

        [TestMethod]
        public void Test_IsNegative()
        {
            Assert.IsTrue(TimeSpanHelper.IsNegative(TimeSpan.FromTicks(-1)));
            Assert.IsFalse(TimeSpanHelper.IsNegative(TimeSpan.FromTicks(0)));
            Assert.IsFalse(TimeSpanHelper.IsNegative(TimeSpan.FromTicks(1)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_FromMonths_When_Negative_Months()
        {
            TimeSpanHelper.FromMonths(-1, DateTime.Today);
        }

        [TestMethod]
        public void Test_FromMonths()
        {
            var span = TimeSpanHelper.FromMonths(0, DateTime.Today);
            Assert.IsTrue(TimeSpanHelper.IsZero(span));
            var refDate = new DateTime(2024, 1, 1);
            span = TimeSpanHelper.FromMonths(1, refDate);
            Assert.IsTrue(span.Days == 31);
            span = TimeSpanHelper.FromMonths(2, refDate);
            Assert.IsTrue(span.Days == 60);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_FromYears_When_Negative_Months()
        {
            TimeSpanHelper.FromYears(-1, DateTime.Today);
        }

        [TestMethod]
        public void Test_FromYears()
        {
            var span = TimeSpanHelper.FromYears(0, DateTime.Today);
            Assert.IsTrue(TimeSpanHelper.IsZero(span));
            var refDate = new DateTime(2023, 1, 1);
            span = TimeSpanHelper.FromYears(1, refDate);
            Assert.IsTrue(span.Days == 365);
            span = TimeSpanHelper.FromYears(2, refDate);
            Assert.IsTrue(span.Days == 731);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_CreateTimeSpan_When_Undefined_Time_Component()
        {
            TimeComponent component = (TimeComponent)999;
            double value = 0;
            TimeSpanHelper.CreateTimeSpan(component, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_CreateTimeSpan_When_Time_Component_Value_Is_Negative()
        {
            TimeComponent component = TimeComponent.Hour;
            double value = -1;
            TimeSpanHelper.CreateTimeSpan(component, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_CreateTimeSpan_When_Undefined_Date_Time_Component()
        {
            DateTimeComponent component = (DateTimeComponent)999;
            double value = 0;
            TimeSpanHelper.CreateTimeSpan(component, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_CreateTimeSpan_When_Date_Time_Component_Value_Is_Negative()
        {
            DateTimeComponent component = DateTimeComponent.Hour;
            double value = -1;
            TimeSpanHelper.CreateTimeSpan(component, value);
        }

        [TestMethod]
        public void Test_CreateTimeSpan_With_Time_Component()
        {
            Assert.AreEqual(TimeSpan.FromTicks(0), TimeSpanHelper.CreateTimeSpan(TimeComponent.Hour, 0D));
            Assert.AreEqual(Timeout.InfiniteTimeSpan, TimeSpanHelper.CreateTimeSpan(TimeComponent.Hour, double.NaN));
            Assert.AreEqual(Timeout.InfiniteTimeSpan, TimeSpanHelper.CreateTimeSpan(TimeComponent.Hour, double.PositiveInfinity));
            Assert.AreEqual(Timeout.InfiniteTimeSpan, TimeSpanHelper.CreateTimeSpan(TimeComponent.Hour, double.NegativeInfinity));
            Assert.AreEqual(TimeSpan.FromHours(15), TimeSpanHelper.CreateTimeSpan(TimeComponent.Hour, 15));
            Assert.AreEqual(TimeSpan.FromMinutes(15), TimeSpanHelper.CreateTimeSpan(TimeComponent.Minute, 15));
            Assert.AreEqual(TimeSpan.FromSeconds(15), TimeSpanHelper.CreateTimeSpan(TimeComponent.Second, 15));
            Assert.AreEqual(TimeSpan.FromMilliseconds(150), TimeSpanHelper.CreateTimeSpan(TimeComponent.Millisecond, 150));
            Assert.AreEqual(TimeSpan.FromMicroseconds(1500), TimeSpanHelper.CreateTimeSpan(TimeComponent.Microsecond, 1500));
        }

        [TestMethod]
        public void Test_CreateTimeSpan_With_Date_Time_Component()
        {
            Assert.AreEqual(TimeSpan.FromTicks(0), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Hour, 0D));
            Assert.AreEqual(Timeout.InfiniteTimeSpan, TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Hour, double.NaN));
            Assert.AreEqual(Timeout.InfiniteTimeSpan, TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Hour, double.PositiveInfinity));
            Assert.AreEqual(Timeout.InfiniteTimeSpan, TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Hour, double.NegativeInfinity));
            Assert.AreEqual(TimeSpanHelper.FromYears(15, DateTime.Today), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Year, 15));
            Assert.AreEqual(TimeSpanHelper.FromMonths(15, DateTime.Today), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Month, 15));
            Assert.AreEqual(TimeSpan.FromDays(15), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Day, 15));
            Assert.AreEqual(TimeSpan.FromHours(15), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Hour, 15));
            Assert.AreEqual(TimeSpan.FromMinutes(15), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Minute, 15));
            Assert.AreEqual(TimeSpan.FromSeconds(15), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Second, 15));
            Assert.AreEqual(TimeSpan.FromMilliseconds(150), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Millisecond, 150));
            Assert.AreEqual(TimeSpan.FromMicroseconds(1500), TimeSpanHelper.CreateTimeSpan(DateTimeComponent.Microsecond, 1500));
        }
    }
}
