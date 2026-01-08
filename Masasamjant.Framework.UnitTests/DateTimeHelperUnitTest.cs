using Masasamjant.Configuration;
using System.Globalization;
using System.Xml.XPath;

namespace Masasamjant
{
    [TestClass]
    public class DateTimeHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constants()
        {
            Assert.AreEqual(7, DateTimeHelper.DaysInWeek);
            Assert.AreEqual(1, DateTimeHelper.FirstQuarter);
            Assert.AreEqual(2, DateTimeHelper.SecondQuarter);
            Assert.AreEqual(3, DateTimeHelper.ThirdQuarter);
            Assert.AreEqual(4, DateTimeHelper.FourthQuarter);
        }

        [TestMethod]
        public void Test_GetTomorrow()
        {
            var expected = DateTime.Today.AddDays(1);
            var actual = DateTimeHelper.GetTomorrow();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetYesterday()
        {
            var expected = DateTime.Today.AddDays(-1);
            var actual = DateTimeHelper.GetYesterday();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsToday()
        {
            Assert.IsFalse(DateTimeHelper.IsToday(DateTime.Today.AddMilliseconds(1)));
            Assert.IsFalse(DateTimeHelper.IsToday(DateTime.Today.AddMilliseconds(-1)));
            Assert.IsTrue(DateTimeHelper.IsToday(DateTime.Today));
        }

        [TestMethod]
        public void Test_IsTodaysDate()
        {
            Assert.IsTrue(DateTimeHelper.IsTodaysDate(DateTime.Today.AddMilliseconds(1)));
            Assert.IsFalse(DateTimeHelper.IsTodaysDate(DateTime.Today.AddMilliseconds(-1)));
            Assert.IsTrue(DateTimeHelper.IsTodaysDate(DateTime.Today));
        }

        [TestMethod]
        public void Test_IsTomorrow()
        {
            Assert.IsTrue(DateTimeHelper.IsTomorrow(DateTimeHelper.GetTomorrow()));
            Assert.IsTrue(DateTimeHelper.IsTomorrow(DateTime.Today.AddDays(1)));
            Assert.IsFalse(DateTimeHelper.IsTomorrow(DateTime.Today.AddDays(1).AddMilliseconds(1)));
        }

        [TestMethod]
        public void Test_IsTomorrowsDate()
        {
            Assert.IsTrue(DateTimeHelper.IsTomorrowsDate(DateTimeHelper.GetTomorrow()));
            Assert.IsTrue(DateTimeHelper.IsTomorrowsDate(DateTime.Today.AddDays(1)));
            Assert.IsTrue(DateTimeHelper.IsTomorrowsDate(DateTime.Today.AddDays(1).AddMilliseconds(1)));
            Assert.IsFalse(DateTimeHelper.IsTomorrowsDate(DateTime.Today.AddDays(1).AddMilliseconds(-1)));
        }

        [TestMethod]
        public void Test_IsYesterday()
        {
            Assert.IsTrue(DateTimeHelper.IsYesterday(DateTimeHelper.GetYesterday()));
            Assert.IsTrue(DateTimeHelper.IsYesterday(DateTime.Today.AddDays(-1)));
            Assert.IsFalse(DateTimeHelper.IsYesterday(DateTime.Today.AddDays(-1).AddMilliseconds(1)));
        }

        [TestMethod]
        public void Test_IsYesterdaysDate()
        {
            Assert.IsTrue(DateTimeHelper.IsYesterdaysDate(DateTimeHelper.GetYesterday()));
            Assert.IsTrue(DateTimeHelper.IsYesterdaysDate(DateTime.Today.AddDays(-1)));
            Assert.IsTrue(DateTimeHelper.IsYesterdaysDate(DateTime.Today.AddDays(-1).AddMilliseconds(1)));
            Assert.IsFalse(DateTimeHelper.IsYesterdaysDate(DateTime.Today));
        }

        [TestMethod]
        public void Test_IsNear()
        {
            var datetime = DateTime.Now;
            var compare = datetime;
            Assert.IsTrue(DateTimeHelper.IsNear(datetime, compare, TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsNear(datetime, compare.AddSeconds(1), TimeSpan.Zero));
            Assert.IsTrue(DateTimeHelper.IsNear(datetime, compare, TimeSpan.FromSeconds(2)));
            Assert.IsTrue(DateTimeHelper.IsNear(datetime, compare.AddSeconds(1), TimeSpan.FromSeconds(2)));
            Assert.IsFalse(DateTimeHelper.IsNear(datetime, compare.AddSeconds(3), TimeSpan.FromSeconds(2)));
        }

        [TestMethod]
        public void Test_IsFuture()
        {
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddHours(-1)));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddHours(-1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(1)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(6), TimeSpan.FromMinutes(5)));

            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddHours(-1)));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddHours(-1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(1)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(6), TimeSpan.FromMinutes(5)));

            var localConfiguration = new DateTimeConfiguration(DateTimeKind.Local);
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddHours(-1), localConfiguration));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(1), localConfiguration));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(1), localConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(4), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.Now.AddMinutes(6), localConfiguration, TimeSpan.FromMinutes(5)));

            var utcConfiguration = new DateTimeConfiguration(DateTimeKind.Utc);
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddHours(-1), utcConfiguration));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(1), utcConfiguration));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(1), utcConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(4), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsFuture(DateTime.UtcNow.AddMinutes(6), utcConfiguration, TimeSpan.FromMinutes(5)));
        }

        [TestMethod]
        public void Test_IsPast()
        {
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddHours(-1)));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddHours(-1), TimeSpan.Zero));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(1)));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(-4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(-6), TimeSpan.FromMinutes(5)));

            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddHours(-1)));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddHours(-1), TimeSpan.Zero));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddHours(-1), TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(1)));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(1), TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(-4), TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(-6), TimeSpan.FromMinutes(5)));

            var localConfiguration = new DateTimeConfiguration(DateTimeKind.Local);
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddHours(-1), localConfiguration));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.Zero));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddHours(-1), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(1), localConfiguration));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(1), localConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(-4), localConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.Now.AddMinutes(-6), localConfiguration, TimeSpan.FromMinutes(5)));

            var utcConfiguration = new DateTimeConfiguration(DateTimeKind.Utc);
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddHours(-1), utcConfiguration));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.Zero));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddHours(-1), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(1), utcConfiguration));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(1), utcConfiguration, TimeSpan.Zero));
            Assert.IsFalse(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(-4), utcConfiguration, TimeSpan.FromMinutes(5)));
            Assert.IsTrue(DateTimeHelper.IsPast(DateTime.UtcNow.AddMinutes(-6), utcConfiguration, TimeSpan.FromMinutes(5)));
        }

        [TestMethod]
        public void Test_DaysLeftInThisMonth()
        {
            int a = DateTimeHelper.DaysLeftInThisMonth();
            int b = DateTimeHelper.DaysLeftInMonth(DateTime.Today);
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void Test_DaysLeftInMonth()
        {
            var culture = CultureInfo.GetCultureInfo("fi-FI");
            int day = 1;
            int daysInMonth = 31;

            while (day <= daysInMonth)
            {
                Assert.AreEqual(daysInMonth - day, DateTimeHelper.DaysLeftInMonth(new DateTime(2024, 12, day), culture));
                day++;
            }

            var currentCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = culture;

            day = 1;
            while (day <= daysInMonth)
            {
                Assert.AreEqual(daysInMonth - day, DateTimeHelper.DaysLeftInMonth(new DateTime(2024, 12, day)));
                day++;
            }

            CultureInfo.CurrentCulture = currentCulture;
        }

        [TestMethod]
        public void Test_DaysLeftInThisYear()
        {
            int a = DateTimeHelper.DaysLeftInThisYear();
            int b = DateTimeHelper.DaysLeftInYear(DateTime.Today);
            Assert.AreEqual(a, b);
        }


        [TestMethod]
        public void Test_DaysLeftInYear()
        {
            var culture = CultureInfo.GetCultureInfo("fi-FI");
            int day = 1;
            int daysInMonth = 31;

            while (day <= daysInMonth)
            {
                Assert.AreEqual(daysInMonth - day, DateTimeHelper.DaysLeftInYear(new DateTime(2024, 12, day), culture));
                day++;
            }

            var currentCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = culture;

            day = 1;
            while (day <= daysInMonth)
            {
                Assert.AreEqual(daysInMonth - day, DateTimeHelper.DaysLeftInYear(new DateTime(2024, 12, day)));
                day++;
            }

            CultureInfo.CurrentCulture = currentCulture;
        }

        [TestMethod]
        public void Test_DaysLeftInThisWeek()
        {
            int a = DateTimeHelper.DaysLeftInThisWeek();
            int b = DateTimeHelper.DaysLeftInWeek(DateTime.Today);
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void Test_DaysLeftInWeek_When_Monday_Is_First_Day_Of_Week()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var culture = CultureInfo.GetCultureInfo("fi-FI");
            CultureInfo.CurrentCulture = culture;

            // 1st of july of 2024 was monday
            int day = 1;

            while (day <= 7)
            {
                Assert.AreEqual(7 - day, DateTimeHelper.DaysLeftInWeek(new DateTime(2024, 7, day)));
                day++;
            }

            CultureInfo.CurrentCulture = currentCulture;
        }

        [TestMethod]
        public void Test_DaysLeftInWeek_When_Sunday_Is_First_Day_Of_Week()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var culture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;

            // 1st of june of 2025 was sunday
            int day = 1;

            while (day <= 7)
            {
                Assert.AreEqual(7 - day, DateTimeHelper.DaysLeftInWeek(new DateTime(2025, 6, day)));
                day++;
            }

            CultureInfo.CurrentCulture = currentCulture;
        }

        [TestMethod]
        public void Test_AreEqual()
        {
            var x = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            var y = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Utc);
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Date, true));
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Date, false));
            x = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Date, true));
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Date, false));
            x = new DateTime(2021, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Date, true));
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Date, false));

            x = new DateTime(2000, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Utc);
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Time, true));
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Time, false));
            x = new DateTime(2000, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Time, true));
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Time, false));
            x = new DateTime(2000, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 36, DateTimeKind.Local);
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Time, true));
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.Time, false));

            x = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Utc);
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, true));
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, false));

            x = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, true));
            Assert.IsTrue(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, false));

            x = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 36, DateTimeKind.Local);
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, true));
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, false));

            x = new DateTime(2020, 3, 2, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, true));
            Assert.IsFalse(DateTimeHelper.AreEqual(x, y, DateTimeComparison.DateTime, false));
        }

        [TestMethod]
        public void Test_AreClose_Negative_Offset()
        {
            var first = new DateTime(2024, 7, 1, 12, 12, 32, 43);
            var second = new DateTime(2024, 7, 1, 12, 12, 32, 43);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DateTimeHelper.AreClose(first, second, DateTimeComponent.Minute, -5));
        }

        [TestMethod]
        public void Test_AreClose_Undefined_Component()
        {
            var first = new DateTime(2024, 7, 1, 12, 12, 32, 43);
            var second = new DateTime(2024, 7, 1, 12, 12, 32, 43);
            Assert.ThrowsException<ArgumentException>(() => DateTimeHelper.AreClose(first, second, (DateTimeComponent)99, 5));
        }

        [TestMethod]
        public void Test_AreClose()
        {
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 32, 43), new DateTime(2024, 7, 1, 12, 12, 32, 43), DateTimeComponent.Minute, 5));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 30, 40, 40), new DateTime(2024, 7, 1, 12, 12, 30, 40, 45), DateTimeComponent.Microsecond, 5));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 30, 40, 40), new DateTime(2024, 7, 1, 12, 12, 30, 40, 46), DateTimeComponent.Microsecond, 5));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 30, 40), new DateTime(2024, 7, 1, 12, 12, 30, 45), DateTimeComponent.Millisecond, 5));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 30, 40), new DateTime(2024, 7, 1, 12, 12, 30, 46), DateTimeComponent.Millisecond, 5));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 30), new DateTime(2024, 7, 1, 12, 12, 35), DateTimeComponent.Second, 5));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 30), new DateTime(2024, 7, 1, 12, 12, 36), DateTimeComponent.Second, 5));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 0), new DateTime(2024, 7, 1, 12, 17, 0), DateTimeComponent.Minute, 5));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 12, 0), new DateTime(2024, 7, 1, 12, 18, 0), DateTimeComponent.Minute, 5));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 0, 0), new DateTime(2024, 7, 1, 17, 0, 0), DateTimeComponent.Hour, 5));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2024, 7, 1, 12, 0, 0), new DateTime(2024, 7, 1, 18, 0, 0), DateTimeComponent.Hour, 5));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1), new DateTime(2024, 7, 6), DateTimeComponent.Day, 5));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2024, 7, 1), new DateTime(2024, 7, 7), DateTimeComponent.Day, 5));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2024, 7, 1), new DateTime(2024, 9, 1), DateTimeComponent.Month, 2));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2024, 7, 1), new DateTime(2024, 10, 1), DateTimeComponent.Month, 2));
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(2022, 7, 1), new DateTime(2024, 7, 1), DateTimeComponent.Year, 2));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(2021, 7, 1), new DateTime(2024, 7, 1), DateTimeComponent.Year, 2));
            var ticks = DateTime.Today.Ticks;
            Assert.IsTrue(DateTimeHelper.AreClose(new DateTime(ticks), new DateTime(ticks + 5), DateTimeComponent.Ticks, 5));
            Assert.IsFalse(DateTimeHelper.AreClose(new DateTime(ticks), new DateTime(ticks + 6), DateTimeComponent.Ticks, 5));
        }

        [TestMethod]
        public void Test_TotalMonths()
        {
            int months = DateTimeHelper.TotalMonths(new DateTime(1900, 1, 1));
            Assert.IsTrue(DateTimeHelper.TotalMonths(new DateTime(1901, 1, 1)) == (months + 12));
            Assert.IsTrue(DateTimeHelper.TotalMonths(new DateTime(2000, 1, 1)) == (months + (12 * 100)));
            Assert.IsTrue((DateTimeHelper.TotalMonths(new DateTime(2000, 1, 1)) - months) == (12 * 100));
        }

        [TestMethod]
        public void Test_Noon()
        {
            var expected = new DateTime(2024, 7, 1, 12, 0, 0, 0, DateTimeKind.Local);
            var actual = DateTimeHelper.Noon(new DateTime(2024, 7, 1, 8, 42, 23, 24, DateTimeKind.Local));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Midnight()
        {
            var expected = new DateTime(2024, 7, 1, 23, 59, 59, 999, DateTimeKind.Local);
            var actual = DateTimeHelper.Midnight(new DateTime(2024, 7, 1, 8, 42, 23, 24, DateTimeKind.Local));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Shift_Undefined_Component()
        {
            DateTimeHelper.Shift(DateTime.Now, (DateTimeComponent)99, 6);
        }

        [TestMethod]
        public void Test_Shift()
        {
            var datetime = new DateTime(2024, 7, 1, 12, 0, 0, 0);
            Assert.AreEqual(datetime, DateTimeHelper.Shift(datetime, DateTimeComponent.Minute, 0));
            Assert.AreEqual(datetime.AddYears(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Year, 1));
            Assert.AreEqual(datetime.AddYears(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Year, -1));
            Assert.AreEqual(datetime.AddMonths(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Month, 1));
            Assert.AreEqual(datetime.AddMonths(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Month, -1));
            Assert.AreEqual(datetime.AddDays(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Day, 1));
            Assert.AreEqual(datetime.AddDays(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Day, -1));
            Assert.AreEqual(datetime.AddHours(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Hour, 1));
            Assert.AreEqual(datetime.AddHours(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Hour, -1));
            Assert.AreEqual(datetime.AddMinutes(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Minute, 1));
            Assert.AreEqual(datetime.AddMinutes(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Minute, -1));
            Assert.AreEqual(datetime.AddSeconds(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Second, 1));
            Assert.AreEqual(datetime.AddSeconds(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Second, -1));
            Assert.AreEqual(datetime.AddMilliseconds(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Millisecond, 1));
            Assert.AreEqual(datetime.AddMilliseconds(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Millisecond, -1));
            Assert.AreEqual(datetime.AddMicroseconds(1), DateTimeHelper.Shift(datetime, DateTimeComponent.Microsecond, 1));
            Assert.AreEqual(datetime.AddMicroseconds(-1), DateTimeHelper.Shift(datetime, DateTimeComponent.Microsecond, -1));
            Assert.AreEqual(datetime.AddTicks(10), DateTimeHelper.Shift(datetime, DateTimeComponent.Ticks, 10));
            Assert.AreEqual(datetime.AddTicks(-10), DateTimeHelper.Shift(datetime, DateTimeComponent.Ticks, -10));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GetComponent_Undefined_Component()
        {
            DateTimeHelper.GetComponent(DateTime.Now, (DateTimeComponent)99);
        }

        [TestMethod]
        public void Test_GetComponent()
        {
            var datetime = DateTime.Now;
            Assert.AreEqual(datetime.Year, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Year));
            Assert.AreEqual(datetime.Month, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Month));
            Assert.AreEqual(datetime.Day, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Day));
            Assert.AreEqual(datetime.Hour, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Hour));
            Assert.AreEqual(datetime.Minute, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Minute));
            Assert.AreEqual(datetime.Second, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Second));
            Assert.AreEqual(datetime.Millisecond, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Millisecond));
            Assert.AreEqual(datetime.Microsecond, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Microsecond));
            Assert.AreEqual(datetime.Ticks, DateTimeHelper.GetComponent(datetime, DateTimeComponent.Ticks));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_IsWeekDay_Undefined_DayOfWeek()
        {
            DateTimeHelper.IsWeekDay((DayOfWeek)999);
        }

        [TestMethod]
        public void Test_IsWeekDay()
        {
            Assert.IsTrue(DateTimeHelper.IsWeekDay(DayOfWeek.Monday));
            Assert.IsTrue(DateTimeHelper.IsWeekDay(DayOfWeek.Tuesday));
            Assert.IsTrue(DateTimeHelper.IsWeekDay(DayOfWeek.Wednesday));
            Assert.IsTrue(DateTimeHelper.IsWeekDay(DayOfWeek.Thursday));
            Assert.IsTrue(DateTimeHelper.IsWeekDay(DayOfWeek.Friday));
            Assert.IsFalse(DateTimeHelper.IsWeekDay(DayOfWeek.Saturday));
            Assert.IsFalse(DateTimeHelper.IsWeekDay(DayOfWeek.Sunday));
            Assert.IsTrue(DateTimeHelper.IsWeekDay(new DateTime(2026, 1, 8)));
            Assert.IsFalse(DateTimeHelper.IsWeekDay(new DateTime(2026, 1, 10)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_IsWeekend_Undefined_DayOfWeek()
        {
            DateTimeHelper.IsWeekend((DayOfWeek)999);
        }

        [TestMethod]
        public void Test_IsWeekend()
        {
            Assert.IsFalse(DateTimeHelper.IsWeekend(DayOfWeek.Monday));
            Assert.IsFalse(DateTimeHelper.IsWeekend(DayOfWeek.Tuesday));
            Assert.IsFalse(DateTimeHelper.IsWeekend(DayOfWeek.Wednesday));
            Assert.IsFalse(DateTimeHelper.IsWeekend(DayOfWeek.Thursday));
            Assert.IsFalse(DateTimeHelper.IsWeekend(DayOfWeek.Friday));
            Assert.IsTrue(DateTimeHelper.IsWeekend(DayOfWeek.Saturday));
            Assert.IsTrue(DateTimeHelper.IsWeekend(DayOfWeek.Sunday));
            Assert.IsFalse(DateTimeHelper.IsWeekend(new DateTime(2026, 1, 8)));
            Assert.IsTrue(DateTimeHelper.IsWeekend(new DateTime(2026, 1, 10)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FirstDayOfWeek_Invalid_WeekBegin()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            DateTimeHelper.FirstDayOfWeek(refDate, DayOfWeek.Wednesday);
        }

        [TestMethod]
        public void Test_FirstDayOfWeek_Week_Begin_On_Monday()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var monday = new DateTime(2024, 7, 8);
            var result = DateTimeHelper.FirstDayOfWeek(refDate, DayOfWeek.Monday);
            Assert.AreEqual(monday, result);

            refDate = new DateTime(2024, 7, 8);
            result = DateTimeHelper.FirstDayOfWeek(refDate, DayOfWeek.Monday);
            Assert.AreEqual(monday, result);
        }

        [TestMethod]
        public void Test_FirstDayOfWeek_Week_Begin_On_Sunday()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var sunday = new DateTime(2024, 7, 7);
            var result = DateTimeHelper.FirstDayOfWeek(refDate, DayOfWeek.Sunday);
            Assert.AreEqual(sunday, result);
        }

        [TestMethod]
        public void Test_FirstDayOfWeek_With_Culture()
        {
            var culture = new CultureInfo("fi-FI");
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var monday = new DateTime(2024, 7, 8);
            var result = DateTimeHelper.FirstDayOfWeek(refDate, culture);
            Assert.AreEqual(monday, result);
        }

        [TestMethod]
        public void Test_FirstDayOfThisWeek()
        {
            var a = DateTimeHelper.FirstDayOfThisWeek(DayOfWeek.Monday);
            var b = DateTimeHelper.FirstDayOfWeek(DateTime.Today, DayOfWeek.Monday);
            Assert.AreEqual(a, b);

            var culture = CultureInfo.GetCultureInfo("fi-FI");
            a = DateTimeHelper.FirstDayOfThisWeek(culture);
            b = DateTimeHelper.FirstDayOfWeek(DateTime.Today, culture);
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void Test_LastDayOfThisWeek()
        {
            var a = DateTimeHelper.LastDayOfThisWeek(DayOfWeek.Monday);
            var b = DateTimeHelper.LastDayOfWeek(DateTime.Today, DayOfWeek.Monday);
            Assert.AreEqual(a, b);

            var culture = CultureInfo.GetCultureInfo("fi-FI");
            a = DateTimeHelper.LastDayOfThisWeek(culture);
            b = DateTimeHelper.LastDayOfWeek(DateTime.Today, culture);
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void Test_LastDayOfWeek_Week_Begin_On_Monday()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var sunday = new DateTime(2024, 7, 14);
            var result = DateTimeHelper.LastDayOfWeek(refDate, DayOfWeek.Monday);
            Assert.AreEqual(sunday, result);
        }

        [TestMethod]
        public void Test_LastDayOfWeek_Week_Begin_On_Sunday()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var saturday = new DateTime(2024, 7, 13);
            var result = DateTimeHelper.LastDayOfWeek(refDate, DayOfWeek.Sunday);
            Assert.AreEqual(saturday, result);
        }

        [TestMethod]
        public void Test_LastDayOfWeek_With_Culture()
        {
            var culture = new CultureInfo("fi-FI");
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var sunday = new DateTime(2024, 7, 14);
            var result = DateTimeHelper.LastDayOfWeek(refDate, culture);
            Assert.AreEqual(sunday, result);
        }

        [TestMethod]
        public void Test_FirstDayOfMonth()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var expected = new DateTime(2024, 7, 1);
            var actual = DateTimeHelper.FirstDayOfMonth(refDate);
            Assert.AreEqual(expected, actual);

            refDate = DateTime.MinValue;
            expected = DateTime.MinValue;
            actual = DateTimeHelper.FirstDayOfMonth(refDate);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_LastDayOfMonth()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var expected = new DateTime(2024, 7, 31);
            var actual = DateTimeHelper.LastDayOfMonth(refDate);
            Assert.AreEqual(expected, actual);

            refDate = DateTime.MaxValue;
            expected = DateTime.MaxValue;
            actual = DateTimeHelper.LastDayOfMonth(refDate);
            Assert.AreEqual(expected, actual);

            refDate = DateTime.MaxValue.Date;
            expected = DateTime.MaxValue;
            actual = DateTimeHelper.LastDayOfMonth(refDate);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetPreviousWeekDay()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var expected = new DateTime(2024, 7, 2);
            var actual = DateTimeHelper.GetPreviousWeekDay(refDate, DayOfWeek.Tuesday);
            Assert.AreEqual(expected, actual);
            Assert.ThrowsException<ArgumentException>(() => DateTimeHelper.GetPreviousWeekDay(refDate, (DayOfWeek)999));
        }

        [TestMethod]
        public void Test_GetNextWeekDay()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday
            var expected = new DateTime(2024, 7, 16);
            var actual = DateTimeHelper.GetNextWeekDay(refDate, DayOfWeek.Tuesday);
            Assert.AreEqual(expected, actual);
            Assert.ThrowsException<ArgumentException>(() => DateTimeHelper.GetNextWeekDay(refDate, (DayOfWeek)999));
        }

        [TestMethod]
        public void Test_Quarter()
        {
            var datetime = new DateTime(2024, 1, 1);
            Assert.AreEqual(DateTimeHelper.FirstQuarter, DateTimeHelper.Quarter(datetime));
            Assert.AreEqual(DateTimeHelper.FirstQuarter, DateTimeHelper.Quarter(datetime.AddMonths(1)));
            Assert.AreEqual(DateTimeHelper.FirstQuarter, DateTimeHelper.Quarter(datetime.AddMonths(2)));
            Assert.AreEqual(DateTimeHelper.SecondQuarter, DateTimeHelper.Quarter(datetime.AddMonths(3)));
            Assert.AreEqual(DateTimeHelper.SecondQuarter, DateTimeHelper.Quarter(datetime.AddMonths(4)));
            Assert.AreEqual(DateTimeHelper.SecondQuarter, DateTimeHelper.Quarter(datetime.AddMonths(5)));
            Assert.AreEqual(DateTimeHelper.ThirdQuarter, DateTimeHelper.Quarter(datetime.AddMonths(6)));
            Assert.AreEqual(DateTimeHelper.ThirdQuarter, DateTimeHelper.Quarter(datetime.AddMonths(7)));
            Assert.AreEqual(DateTimeHelper.ThirdQuarter, DateTimeHelper.Quarter(datetime.AddMonths(8)));
            Assert.AreEqual(DateTimeHelper.FourthQuarter, DateTimeHelper.Quarter(datetime.AddMonths(9)));
            Assert.AreEqual(DateTimeHelper.FourthQuarter, DateTimeHelper.Quarter(datetime.AddMonths(10)));
            Assert.AreEqual(DateTimeHelper.FourthQuarter, DateTimeHelper.Quarter(datetime.AddMonths(11)));
        }

        [TestMethod]
        public void Test_AddWeeks()
        {
            var refDate = new DateTime(2024, 7, 9); // Tuesday, vko 28
            Assert.AreEqual(new DateTime(2024, 7, 9), DateTimeHelper.AddWeeks(refDate, 0));
            Assert.AreEqual(new DateTime(2024, 7, 2), DateTimeHelper.AddWeeks(refDate, -1));
            Assert.AreEqual(new DateTime(2024, 4, 16), DateTimeHelper.AddWeeks(refDate, -12));
            Assert.AreEqual(new DateTime(2024, 7, 16), DateTimeHelper.AddWeeks(refDate, 1));
            Assert.AreEqual(new DateTime(2024, 10, 1), DateTimeHelper.AddWeeks(refDate, 12));
        }

        [TestMethod]
        public void Test_SetTime()
        {
            var datetime = new DateTime(2024, 7, 9, 12, 30, 0, 0);
            Assert.AreEqual(datetime, DateTimeHelper.SetTime(datetime));
            Assert.AreEqual(new DateTime(2024, 7, 9, 13, 32, 28, 3), DateTimeHelper.SetTime(datetime, 13, 32, 28, 3));
            Assert.AreEqual(new DateTime(2024, 7, 9, 13, 32, 28, 0), DateTimeHelper.SetTime(datetime, 13, 32, 28));
            Assert.AreEqual(new DateTime(2024, 7, 9, 13, 32, 0, 0), DateTimeHelper.SetTime(datetime, 13, 32));
            Assert.AreEqual(new DateTime(2024, 7, 9, 13, 30, 0, 0), DateTimeHelper.SetTime(datetime, 13));
        }

        [TestMethod]
        public void Test_OnlyDate()
        {
            var datetime = new DateTime(2024, 7, 9, 12, 30, 23, 14, 5);
            var expected = new DateOnly(2024, 7, 9);
            var actual = DateTimeHelper.OnlyDate(datetime);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_OnlyTime()
        {
            var datetime = new DateTime(2024, 7, 9, 12, 30, 23, 14);
            Assert.AreEqual(new TimeOnly(12, 30, 23, 14), DateTimeHelper.OnlyTime(datetime));
        }

        [TestMethod]
        public void Test_Nearest()
        {
            var value = new DateTime(2024, 7, 9);
            var others = new List<DateTime>();
            Assert.IsNull(DateTimeHelper.Nearest(value, others));

            others.Add(new DateTime(2024, 8, 9));
            others.Add(new DateTime(2024, 8, 1));
            others.Add(new DateTime(2024, 7, 26));
            var expected = new DateTime(2024, 7, 26);
            var actual = DateTimeHelper.Nearest(value, others);
            Assert.AreEqual(expected, actual);

            others.Clear();
            others.Add(value);
            expected = value;
            actual = DateTimeHelper.Nearest(value, others);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Nearest_Group()
        {
            var value = new DateTime(2024, 7, 9);
            var others = new List<DateTime>
            {
                new DateTime(2024, 7, 12),
                new DateTime(2024, 7, 16),
                new DateTime(2024, 7, 18),
                new DateTime(2024, 7, 26),
                new DateTime(2024, 7, 30)
            };
            var expected = new DateTime[]
            {
                new DateTime(2024, 7, 12),
                new DateTime(2024, 7, 16),
                new DateTime(2024, 7, 18)
            }.ToArray();
            var actual = DateTimeHelper.Nearest(value, others, TimeSpan.FromDays(12)).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Farest()
        {
            var value = new DateTime(2024, 7, 9);
            var others = new List<DateTime>();
            Assert.IsNull(DateTimeHelper.Farest(value, others));
            others.Add(new DateTime(2024, 7, 26));
            others.Add(new DateTime(2024, 8, 1));
            others.Add(new DateTime(2024, 8, 9));
            var expected = new DateTime(2024, 8, 9);
            var actual = DateTimeHelper.Farest(value, others);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Farest_Group()
        {
            var value = new DateTime(2024, 7, 9);
            var others = new List<DateTime>
            {
                new DateTime(2024, 7, 12),
                new DateTime(2024, 7, 16),
                new DateTime(2024, 7, 18),
                new DateTime(2024, 7, 26),
                new DateTime(2024, 7, 30)
            };
            var expected = new DateTime[]
            {
                new DateTime(2024, 7, 26),
                new DateTime(2024, 7, 30)
            }.ToArray();
            var actual = DateTimeHelper.Farest(value, others, TimeSpan.FromDays(12)).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
