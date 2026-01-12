using Masasamjant.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant
{
    [TestClass]
    public class DateTimeOffsetHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetTomorrow()
        {
            DateTimeOffset expected = DateTime.Today.AddDays(1);
            var actual = DateTimeOffsetHelper.GetTomorrow();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetYesterday()
        {
            DateTimeOffset expected = DateTime.Today.AddDays(-1);
            var actual = DateTimeOffsetHelper.GetYesterday();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsToday()
        {
            Assert.IsFalse(DateTimeOffsetHelper.IsToday(DateTime.Today.AddMilliseconds(1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsToday(DateTime.Today.AddMilliseconds(-1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsToday(DateTime.Today));
        }

        [TestMethod]
        public void Test_IsTodaysDate()
        {
            Assert.IsTrue(DateTimeOffsetHelper.IsTodaysDate(DateTime.Today.AddMilliseconds(1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsTodaysDate(DateTime.Today.AddMilliseconds(-1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsTodaysDate(DateTime.Today));
        }

        [TestMethod]
        public void Test_IsTomorrow()
        {
            Assert.IsTrue(DateTimeOffsetHelper.IsTomorrow(DateTimeHelper.GetTomorrow()));
            Assert.IsTrue(DateTimeOffsetHelper.IsTomorrow(DateTime.Today.AddDays(1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsTomorrow(DateTime.Today.AddDays(1).AddMilliseconds(1)));
        }

        [TestMethod]
        public void Test_IsTomorrowsDate()
        {
            Assert.IsTrue(DateTimeOffsetHelper.IsTomorrowsDate(DateTimeHelper.GetTomorrow()));
            Assert.IsTrue(DateTimeOffsetHelper.IsTomorrowsDate(DateTime.Today.AddDays(1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsTomorrowsDate(DateTime.Today.AddDays(1).AddMilliseconds(1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsTomorrowsDate(DateTime.Today.AddDays(1).AddMilliseconds(-1)));
        }

        [TestMethod]
        public void Test_IsYesterday()
        {
            Assert.IsTrue(DateTimeOffsetHelper.IsYesterday(DateTimeHelper.GetYesterday()));
            Assert.IsTrue(DateTimeOffsetHelper.IsYesterday(DateTime.Today.AddDays(-1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsYesterday(DateTime.Today.AddDays(-1).AddMilliseconds(1)));
        }

        [TestMethod]
        public void Test_IsYesterdaysDate()
        {
            Assert.IsTrue(DateTimeOffsetHelper.IsYesterdaysDate(DateTimeHelper.GetYesterday()));
            Assert.IsTrue(DateTimeOffsetHelper.IsYesterdaysDate(DateTime.Today.AddDays(-1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsYesterdaysDate(DateTime.Today.AddDays(-1).AddMilliseconds(1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsYesterdaysDate(DateTime.Today));
        }

        [TestMethod]
        public void Test_IsFuture()
        {
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddHours(-1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddHours(-1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(6), TimeSpan.FromMinutes(5)));

            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddHours(-1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddHours(-1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(6), TimeSpan.FromMinutes(5)));

            var localConfiguration = new DateTimeConfiguration(DateTimeKind.Local);
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddHours(-1), localConfiguration));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(1), localConfiguration));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(1), localConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(4), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.Now.AddMinutes(6), localConfiguration, TimeSpan.FromMinutes(5)));

            var utcConfiguration = new DateTimeConfiguration(DateTimeKind.Utc);
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddHours(-1), utcConfiguration));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(1), utcConfiguration));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(1), utcConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(4), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsFuture(DateTime.UtcNow.AddMinutes(6), utcConfiguration, TimeSpan.FromMinutes(5)));
        }

        [TestMethod]
        public void Test_IsPast()
        {
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddHours(-1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddHours(-1), TimeSpan.Zero));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(-4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(-6), TimeSpan.FromMinutes(5)));

            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddHours(-1)));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddHours(-1), TimeSpan.Zero));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(1)));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(-4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(-6), TimeSpan.FromMinutes(5)));

            var localConfiguration = new DateTimeConfiguration(DateTimeKind.Local);
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddHours(-1), localConfiguration));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.Zero));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(1), localConfiguration));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(1), localConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(-4), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.Now.AddMinutes(-6), localConfiguration, TimeSpan.FromMinutes(5)));

            var utcConfiguration = new DateTimeConfiguration(DateTimeKind.Utc);
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddHours(-1), utcConfiguration));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.Zero));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(1), utcConfiguration));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(1), utcConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(-4), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeOffsetHelper.IsPast(DateTime.UtcNow.AddMinutes(-6), utcConfiguration, TimeSpan.FromMinutes(5)));
        }

        [TestMethod]
        public void Test_IsNear()
        {
            var datetime = DateTimeOffset.Now;
            var compare = datetime;
            Assert.IsTrue(DateTimeOffsetHelper.IsNear(datetime, compare, TimeSpan.Zero));
            Assert.IsFalse(DateTimeOffsetHelper.IsNear(datetime, compare.AddSeconds(1), TimeSpan.Zero));
            Assert.IsTrue(DateTimeOffsetHelper.IsNear(datetime, compare, TimeSpan.FromSeconds(2)));
            Assert.IsTrue(DateTimeOffsetHelper.IsNear(datetime, compare.AddSeconds(1), TimeSpan.FromSeconds(2)));
            Assert.IsFalse(DateTimeOffsetHelper.IsNear(datetime, compare.AddSeconds(3), TimeSpan.FromSeconds(2)));
            Assert.ThrowsException<ArgumentException>(() => DateTimeOffsetHelper.IsNear(datetime, DateTimeOffset.UtcNow, TimeSpan.Zero));
        }

        [TestMethod]
        public void Test_IsUniveralTime()
        {
            Assert.IsTrue(DateTimeOffsetHelper.IsUniveralTime(DateTimeOffset.UtcNow));
            Assert.IsFalse(DateTimeOffsetHelper.IsUniveralTime(DateTimeOffset.Now));
        }

        [TestMethod]
        public void Test_IsLocalTime()
        {
            Assert.IsFalse(DateTimeOffsetHelper.IsLocalTime(DateTimeOffset.UtcNow));
            Assert.IsTrue(DateTimeOffsetHelper.IsLocalTime(DateTimeOffset.Now));
        }

        [TestMethod]
        public void Test_ToUniversalOrLocalTime()
        {
            DateTimeOffset datetime = DateTimeOffset.UtcNow;
            DateTimeKind kind;
            DateTimeOffset result = DateTimeOffsetHelper.ToUniversalOrLocalTime(datetime, out kind);
            Assert.AreEqual(datetime, result);
            Assert.AreEqual(DateTimeKind.Utc, kind);

            DateTimeOffset local = DateTimeOffset.Now;
            datetime = new DateTimeOffset(datetime.Ticks, local.Offset.Add(TimeSpan.FromHours(2)));
            result = DateTimeOffsetHelper.ToUniversalOrLocalTime(datetime, out kind);
            Assert.AreEqual(datetime.ToLocalTime(), result);
            Assert.AreEqual(local.Offset, result.Offset);
            Assert.AreEqual(DateTimeKind.Local, kind);

            result = DateTimeOffsetHelper.ToUniversalOrLocalTime(local, out kind);
            Assert.AreEqual(local, result);
            Assert.AreEqual(DateTimeKind.Local, kind);
        }

        [TestMethod]
        public void Test_AreClose()
        {
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero), DateTimeComponent.Minute, 5));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 30, 40, 40, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 12, 30, 40, 45, TimeSpan.Zero), DateTimeComponent.Microsecond, 5));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 30, 40, 40, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 12, 30, 40, 46, TimeSpan.Zero), DateTimeComponent.Microsecond, 5));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 30, 40, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 12, 30, 45, TimeSpan.Zero), DateTimeComponent.Millisecond, 5));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 30, 40, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 12, 30, 46, TimeSpan.Zero), DateTimeComponent.Millisecond, 5));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 30, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 12, 35, 0, TimeSpan.Zero), DateTimeComponent.Second, 5));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 30, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 12, 36, 0, TimeSpan.Zero), DateTimeComponent.Second, 5));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 17, 0, TimeSpan.Zero), DateTimeComponent.Minute, 5));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 12, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 12, 18, 0, TimeSpan.Zero), DateTimeComponent.Minute, 5));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 17, 0, 0, TimeSpan.Zero), DateTimeComponent.Hour, 5));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 18, 0, 0, TimeSpan.Zero), DateTimeComponent.Hour, 5));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 6, 0, 0, 0, TimeSpan.Zero), DateTimeComponent.Day, 5));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 7, 0, 0, 0, TimeSpan.Zero), DateTimeComponent.Day, 5));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 9, 1, 0, 0, 0, TimeSpan.Zero), DateTimeComponent.Month, 2));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 10, 1, 0, 0, 0, TimeSpan.Zero), DateTimeComponent.Month, 2));
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2022, 7, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero), DateTimeComponent.Year, 2));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(2021, 7, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero), DateTimeComponent.Year, 2));
            var ticks = DateTime.Today.Ticks;
            Assert.IsTrue(DateTimeOffsetHelper.AreClose(new DateTimeOffset(ticks, TimeSpan.Zero), new DateTimeOffset(ticks + 5, TimeSpan.Zero), DateTimeComponent.Ticks, 5));
            Assert.IsFalse(DateTimeOffsetHelper.AreClose(new DateTimeOffset(ticks, TimeSpan.Zero), new DateTimeOffset(ticks + 6, TimeSpan.Zero), DateTimeComponent.Ticks, 5));
        }

        [TestMethod]
        public void Test_AreClose_Not_Same_Offset()
        {
            var first = new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero);
            var second = new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero.Add(TimeSpan.FromHours(2)));
            Assert.ThrowsException<ArgumentException>(() => DateTimeOffsetHelper.AreClose(first, second, DateTimeComponent.Minute, 5));
        }

        [TestMethod]
        public void Test_AreClose_Negative_Offset()
        {
            var first = new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero);
            var second = new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DateTimeOffsetHelper.AreClose(first, second, DateTimeComponent.Minute, -5));
        }

        [TestMethod]
        public void Test_AreClose_Undefined_Component()
        {
            var first = new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero);
            var second = new DateTimeOffset(2024, 7, 1, 12, 12, 32, 43, TimeSpan.Zero);
            Assert.ThrowsException<ArgumentException>(() => DateTimeOffsetHelper.AreClose(first, second, (DateTimeComponent)99, 5));
        }

        [TestMethod]
        public void Test_Noon()
        {
            var expected = new DateTimeOffset(2024, 7, 1, 12, 0, 0, 0, TimeSpan.Zero);
            var actual = DateTimeOffsetHelper.Noon(new DateTimeOffset(2024, 7, 1, 8, 42, 23, 24, TimeSpan.Zero));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Midnight()
        {
            var expected = new DateTimeOffset(2024, 7, 1, 23, 59, 59, 999, TimeSpan.Zero);
            var actual = DateTimeOffsetHelper.Midnight(new DateTimeOffset(2024, 7, 1, 8, 42, 23, 24, TimeSpan.Zero));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Shift_Undefined_Component()
        {
            DateTimeOffsetHelper.Shift(DateTimeOffset.Now, (DateTimeComponent)99, 6);
        }

        [TestMethod]
        public void Test_Shift()
        {
            var datetime = new DateTimeOffset(2024, 7, 1, 12, 0, 0, 0, TimeSpan.Zero);
            Assert.AreEqual(datetime, DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Minute, 0));
            Assert.AreEqual(datetime.AddYears(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Year, 1));
            Assert.AreEqual(datetime.AddYears(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Year, -1));
            Assert.AreEqual(datetime.AddMonths(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Month, 1));
            Assert.AreEqual(datetime.AddMonths(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Month, -1));
            Assert.AreEqual(datetime.AddDays(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Day, 1));
            Assert.AreEqual(datetime.AddDays(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Day, -1));
            Assert.AreEqual(datetime.AddHours(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Hour, 1));
            Assert.AreEqual(datetime.AddHours(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Hour, -1));
            Assert.AreEqual(datetime.AddMinutes(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Minute, 1));
            Assert.AreEqual(datetime.AddMinutes(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Minute, -1));
            Assert.AreEqual(datetime.AddSeconds(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Second, 1));
            Assert.AreEqual(datetime.AddSeconds(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Second, -1));
            Assert.AreEqual(datetime.AddMilliseconds(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Millisecond, 1));
            Assert.AreEqual(datetime.AddMilliseconds(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Millisecond, -1));
            Assert.AreEqual(datetime.AddMicroseconds(1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Microsecond, 1));
            Assert.AreEqual(datetime.AddMicroseconds(-1), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Microsecond, -1));
            Assert.AreEqual(datetime.AddTicks(10), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Ticks, 10));
            Assert.AreEqual(datetime.AddTicks(-10), DateTimeOffsetHelper.Shift(datetime, DateTimeComponent.Ticks, -10));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GetComponent_Undefined_Component()
        {
            DateTimeOffsetHelper.GetComponent(DateTimeOffset.Now, (DateTimeComponent)99);
        }

        [TestMethod]
        public void Test_GetComponent()
        {
            var datetime = DateTimeOffset.Now;
            Assert.AreEqual(datetime.Year, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Year));
            Assert.AreEqual(datetime.Month, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Month));
            Assert.AreEqual(datetime.Day, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Day));
            Assert.AreEqual(datetime.Hour, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Hour));
            Assert.AreEqual(datetime.Minute, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Minute));
            Assert.AreEqual(datetime.Second, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Second));
            Assert.AreEqual(datetime.Millisecond, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Millisecond));
            Assert.AreEqual(datetime.Microsecond, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Microsecond));
            Assert.AreEqual(datetime.Ticks, DateTimeOffsetHelper.GetComponent(datetime, DateTimeComponent.Ticks));
        }

        [TestMethod]
        public void Test_GetPreviousWeekDay()
        {
            var refDate = new DateTimeOffset(new DateTime(2024, 7, 9)); // Tuesday
            var expected = new DateTimeOffset(new DateTime(2024, 7, 2));
            var actual = DateTimeOffsetHelper.GetPreviousWeekDay(refDate, DayOfWeek.Tuesday);
            Assert.AreEqual(expected, actual);
            Assert.ThrowsException<ArgumentException>(() => DateTimeOffsetHelper.GetPreviousWeekDay(refDate, (DayOfWeek)999));
        }

        [TestMethod]
        public void Test_GetNextWeekDay()
        {
            var refDate = new DateTimeOffset(new DateTime(2024, 7, 9)); // Tuesday
            var expected = new DateTimeOffset(new DateTime(2024, 7, 16));
            var actual = DateTimeOffsetHelper.GetNextWeekDay(refDate, DayOfWeek.Tuesday);
            Assert.AreEqual(expected, actual);
            Assert.ThrowsException<ArgumentException>(() => DateTimeOffsetHelper.GetNextWeekDay(refDate, (DayOfWeek)999));
        }
    }
}
