using System.Globalization;

namespace Masasamjant
{
    [TestClass]
    public class DateRangeUnitTest : UnitTest
    {
        private static readonly DateTime today = DateTime.Today;

        [TestMethod]
        public void Test_Constructors()
        {
            var range = new DateRange();
            Assert.IsTrue(range.IsEmpty);

            var range1 = new DateRange(today);
            Assert.IsFalse(range1.IsEmpty);
            Assert.AreEqual(1, range1.Days);
            Assert.AreEqual(today, range1.Begin);
            Assert.AreEqual(today, range1.End);
            
            var begin = today;
            var end = begin;
            var range2 = new DateRange(begin, end);
            Assert.IsFalse(range2.IsEmpty);
            Assert.AreEqual(1, range2.Days);
            Assert.AreEqual(begin, range2.Begin);
            Assert.AreEqual(end, range2.End);
            
            end = begin.AddDays(1);
            var range3 = new DateRange(begin, end);
            Assert.IsFalse(range3.IsEmpty);
            Assert.AreEqual(2, range3.Days);
            Assert.AreEqual(begin, range3.Begin);
            Assert.AreEqual(end, range3.End);

            DateTime now = DateTime.Now;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DateRange(now, now.AddDays(-2)));
        }

        [TestMethod]
        public void Test_BeginsAfter()
        {
            var range = new DateRange();
            Assert.IsFalse(range.BeginsAfter(today));
            Assert.IsFalse(range.BeginsAfter(new DateRange(today)));
            range = new DateRange(today);
            Assert.IsFalse(range.BeginsAfter(today));
            Assert.IsFalse(range.BeginsAfter(today.AddDays(1)));
            Assert.IsTrue(range.BeginsAfter(today.AddDays(-1)));
            Assert.IsFalse(range.BeginsAfter(new DateRange()));
            Assert.IsFalse(range.BeginsAfter(new DateRange(today)));
            Assert.IsFalse(range.BeginsAfter(new DateRange(today.AddDays(1))));
            Assert.IsTrue(range.BeginsAfter(new DateRange(today.AddDays(-1))));
        }

        [TestMethod]
        public void Test_BeginsBefore()
        {
            var range = new DateRange();
            Assert.IsFalse(range.BeginsBefore(today));
            Assert.IsFalse(range.BeginsBefore(new DateRange(today)));
            range = new DateRange(today);
            Assert.IsFalse(range.BeginsBefore(today));
            Assert.IsTrue(range.BeginsBefore(today.AddDays(1)));
            Assert.IsFalse(range.BeginsBefore(today.AddDays(-1)));
            Assert.IsFalse(range.BeginsBefore(new DateRange()));
            Assert.IsFalse(range.BeginsBefore(new DateRange(today)));
            Assert.IsTrue(range.BeginsBefore(new DateRange(today.AddDays(1))));
            Assert.IsFalse(range.BeginsBefore(new DateRange(today.AddDays(-1))));
        }

        [TestMethod]
        public void Test_Equals()
        {
            Assert.AreEqual(new DateRange(), new DateRange());
            Assert.AreEqual(new DateRange(today), new DateRange(today));
            Assert.AreEqual(new DateRange(today, today.AddDays(2)), new DateRange(today, today.AddDays(2)));
            Assert.AreNotEqual(new DateRange(today.AddDays(-1), today.AddDays(2)), new DateRange(today, today.AddDays(2)));
            Assert.AreNotEqual(new DateRange(today, today.AddDays(2)), new DateRange(today, today.AddDays(3)));
            Assert.IsFalse(new DateRange().Equals(null));
            Assert.IsFalse(new DateRange().Equals(DateTime.Now));
        }

        [TestMethod]
        public void Test_Combine()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            var result = range.Combine(other);
            Assert.AreEqual(result, other);
            range = new DateRange(today);
            other = new DateRange();
            result = range.Combine(other);
            Assert.AreEqual(result, range);
            other = new DateRange(today);
            result = range.Combine(other);
            Assert.AreEqual(result, range);
            other = new DateRange(today, today.AddDays(3));
            result = range.Combine(other);
            Assert.AreEqual(result, other);
            range = new DateRange(today.AddDays(-3), today);
            other = new DateRange(today, today.AddDays(3));
            result = range.Combine(other);
            Assert.AreEqual(result, new DateRange(today.AddDays(-3), today.AddDays(3)));
            range = new DateRange(today.AddDays(-20), today);
            var ranges = new[] 
            {
                new DateRange(today.AddDays(-10), today.AddDays(10)),
                new DateRange(today, today.AddDays(20))
            };
            result = range.Combine(ranges);
            Assert.AreEqual(result, new DateRange(today.AddDays(-20), today.AddDays(20)));
            ranges = [];
            result = range.Combine(ranges);
            Assert.AreEqual(result, range);
        }

        [TestMethod]
        public void Test_Contains()
        {
            var range = new DateRange();
            Assert.IsFalse(range.Contains(today));
            range = new DateRange(today);
            Assert.IsFalse(range.Contains(today.AddDays(-1)));
            Assert.IsFalse(range.Contains(today.AddDays(1)));
            Assert.IsTrue(range.Contains(today));
            range = new DateRange(today.AddDays(-1), today.AddDays(1));
            Assert.IsFalse(range.Contains(today.AddDays(-2)));
            Assert.IsFalse(range.Contains(today.AddDays(2)));
            Assert.IsTrue(range.Contains(today.AddDays(-1)));
            Assert.IsTrue(range.Contains(today.AddDays(1)));
            Assert.IsTrue(range.Contains(today));
        }

        [TestMethod]
        public void Test_FromDays()
        {
            var range = DateRange.FromDays(today, 0);
            Assert.AreEqual(new DateRange(today), range);
            range = DateRange.FromDays(today, -1);
            Assert.AreEqual(new DateRange(today.AddDays(-1), today), range);
            range = DateRange.FromDays(today, 1);
            Assert.AreEqual(new DateRange(today, today.AddDays(1)), range);
            range = DateRange.FromDays(2);
            Assert.AreEqual(new DateRange(today, today.AddDays(2)), range);
        }

        [TestMethod]
        public void Test_GetDates()
        {
            var range = new DateRange();
            var dates = range.GetDates();
            Assert.IsFalse(dates.Any());
            range = new DateRange(today);
            dates = range.GetDates();
            Assert.IsTrue(dates.Count() == 1 && dates.First().Equals(today));
            
            var expected = new[]
            {
                today.AddDays(-1),
                today,
                today.AddDays(1)
            };
            range = new DateRange(today.AddDays(-1), today.AddDays(1));
            var actual = range.GetDates().ToArray();
            CollectionAssert.AreEqual(expected, actual);

            var begin = new DateTime(2025, 6, 30); // monday
            var end = new DateTime(2025, 7, 6); // sunday
            range = new DateRange(begin, end);
            dates = range.GetDates(DayOfWeek.Tuesday);
            Assert.IsTrue(dates.Count() == 1 && dates.First().Equals(new DateTime(2025, 7, 1)));
            dates = range.GetDates(DayOfWeek.Tuesday, null, null);
            Assert.IsTrue(dates.Count() == 1 && dates.First().Equals(new DateTime(2025, 7, 1)));

            expected = new[]
            {
                new DateTime(2025, 7, 1),
                new DateTime(2025, 7, 2)
            };
            actual = range.GetDates([DayOfWeek.Tuesday, DayOfWeek.Wednesday]).ToArray();
            CollectionAssert.AreEqual(expected, actual);

            dates = range.GetDates(null, null, month: 6);
            Assert.IsTrue(dates.Count() == 1 && dates.First().Equals(new DateTime(2025, 6, 30)));

            expected = range.GetDates().ToArray();
            actual = range.GetDates(null, year: 2025, null).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetTotalDays()
        {
            var begin = new DateTime(2025, 6, 30); // monday
            var end = new DateTime(2025, 7, 6); // sunday
            var range = new DateRange(begin, end);
            Assert.AreEqual(5, range.GetTotalDays(false));
            Assert.AreEqual(7, range.GetTotalDays(true));
            Assert.AreEqual(7, range.GetTotalDays(null));
            Assert.AreEqual(1, range.GetTotalDays([DayOfWeek.Tuesday]));
            Assert.AreEqual(1, range.GetTotalDays(DayOfWeek.Tuesday));
        }

        [TestMethod]
        public void Test_EndsAfter()
        {
            var range = new DateRange();
            Assert.IsFalse(range.EndsAfter(today));
            Assert.IsFalse(range.EndsAfter(new DateRange()));
            Assert.IsFalse(range.EndsAfter(new DateRange(today)));
            range = new DateRange(today);
            Assert.IsTrue(range.EndsAfter(today.AddDays(-1)));
            Assert.IsFalse(range.EndsAfter(today));
            Assert.IsFalse(range.EndsAfter(today.AddDays(1)));
            Assert.IsTrue(range.EndsAfter(new DateRange(today.AddDays(-1))));
            Assert.IsFalse(range.EndsAfter(new DateRange(today)));
            Assert.IsFalse(range.EndsAfter(new DateRange(today.AddDays(1))));
        }

        [TestMethod]
        public void Test_EndsBefore()
        {
            var range = new DateRange();
            Assert.IsFalse(range.EndsBefore(today));
            Assert.IsFalse(range.EndsBefore(new DateRange()));
            Assert.IsFalse(range.EndsBefore(new DateRange(today)));
            range = new DateRange(today);
            Assert.IsFalse(range.EndsBefore(today.AddDays(-1)));
            Assert.IsFalse(range.EndsBefore(today));
            Assert.IsTrue(range.EndsBefore(today.AddDays(1)));
            Assert.IsFalse(range.EndsBefore(new DateRange(today.AddDays(-1))));
            Assert.IsFalse(range.EndsBefore(new DateRange(today)));
            Assert.IsTrue(range.EndsBefore(new DateRange(today.AddDays(1))));
        }

        [TestMethod]
        public void Test_Includes()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            Assert.IsFalse(range.Includes(other));

            range = new DateRange(today);
            other = new DateRange();
            Assert.IsFalse(range.Includes(other));

            range = new DateRange(today);
            other = new DateRange(today);
            Assert.IsTrue(range.Includes(other));

            range = new DateRange(today.AddDays(-2), today.AddDays(2));
            other = new DateRange(today.AddDays(-1), today.AddDays(1));
            Assert.IsTrue(range.Includes(other));
            Assert.IsFalse(other.Includes(range));
        }

        [TestMethod]
        public void Test_IsAdjacentTo()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            Assert.IsFalse(range.IsAdjacentTo(other));

            range = new DateRange(today);
            other = new DateRange();
            Assert.IsFalse(range.IsAdjacentTo(other));

            range = new DateRange(today.AddDays(-2), today.AddDays(-1));
            other = new DateRange(today, today.AddDays(1));
            Assert.IsTrue(range.IsAdjacentTo(other));
            Assert.IsTrue(other.IsAdjacentTo(range));

            other = new DateRange(today.AddDays(-4), today.AddDays(-3));
            Assert.IsTrue(range.IsAdjacentTo(other));
            Assert.IsTrue(other.IsAdjacentTo(range));

            range = new DateRange(today.AddDays(-3), today.AddDays(-2));
            other = new DateRange(today, today.AddDays(1));
            Assert.IsFalse(range.IsAdjacentTo(other));
            Assert.IsFalse(other.IsAdjacentTo(range));
        }

        [TestMethod]
        public void Test_IsAfter()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            Assert.IsFalse(range.IsAfter(other));

            range = new DateRange(today);
            other = new DateRange();
            Assert.IsFalse(range.IsAfter(other));

            range = new DateRange(today);
            Assert.IsFalse(range.IsAfter(new DateRange(today)));

            range = new DateRange(today.AddDays(1));
            Assert.IsTrue(range.IsAfter(new DateRange(today)));

            range = new DateRange(today.AddDays(-1));
            Assert.IsFalse(range.IsAfter(new DateRange(today)));

            range = new DateRange();
            Assert.IsFalse(range.IsAfter(today));

            range = new DateRange(today);
            Assert.IsFalse(range.IsAfter(today));

            range = new DateRange(today.AddDays(-1));
            Assert.IsFalse(range.IsAfter(today));

            range = new DateRange(today.AddDays(1));
            Assert.IsTrue(range.IsAfter(today));
        }

        [TestMethod]
        public void Test_IsImmediatelyAfter()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            Assert.IsFalse(range.IsImmediatelyAfter(other));

            range = new DateRange(today);
            other = new DateRange();
            Assert.IsFalse(range.IsImmediatelyAfter(other));

            range = new DateRange(today);
            other = new DateRange(today);
            Assert.IsFalse(range.IsImmediatelyAfter(other));

            other = new DateRange(today.AddDays(-1));
            Assert.IsTrue(range.IsImmediatelyAfter(other));

            other = new DateRange(today.AddDays(1));
            Assert.IsFalse(range.IsImmediatelyAfter(other));

            range = new DateRange();
            Assert.IsFalse(range.IsImmediatelyAfter(today));

            range = new DateRange(today);
            Assert.IsFalse(range.IsImmediatelyAfter(today));
            Assert.IsTrue(range.IsImmediatelyAfter(today.AddDays(-1)));
            Assert.IsFalse(range.IsImmediatelyAfter(today.AddDays(1)));
        }

        [TestMethod]
        public void Test_IsBefore()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            Assert.IsFalse(range.IsBefore(other));

            range = new DateRange(today);
            other = new DateRange();
            Assert.IsFalse(range.IsBefore(other));

            range = new DateRange(today);
            Assert.IsFalse(range.IsBefore(new DateRange(today)));

            range = new DateRange(today.AddDays(1));
            Assert.IsFalse(range.IsBefore(new DateRange(today)));

            range = new DateRange(today.AddDays(-1));
            Assert.IsTrue(range.IsBefore(new DateRange(today)));

            range = new DateRange();
            Assert.IsFalse(range.IsBefore(today));

            range = new DateRange(today);
            Assert.IsFalse(range.IsBefore(today));

            range = new DateRange(today.AddDays(-1));
            Assert.IsTrue(range.IsBefore(today));

            range = new DateRange(today.AddDays(1));
            Assert.IsFalse(range.IsBefore(today));
        }

        [TestMethod]
        public void Test_IsImmediatelyBefore()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            Assert.IsFalse(range.IsImmediatelyBefore(other));

            range = new DateRange(today);
            other = new DateRange();
            Assert.IsFalse(range.IsImmediatelyBefore(other));

            range = new DateRange(today);           
            other = new DateRange(today);
            Assert.IsFalse(range.IsImmediatelyBefore(other));
            
            other = new DateRange(today.AddDays(-1));
            Assert.IsFalse(range.IsImmediatelyBefore(other));
            
            other = new DateRange(today.AddDays(1));
            Assert.IsTrue(range.IsImmediatelyBefore(other));
            
            range = new DateRange();
            Assert.IsFalse(range.IsImmediatelyBefore(today));

            range = new DateRange(today);
            Assert.IsFalse(range.IsImmediatelyBefore(today));
            Assert.IsFalse(range.IsImmediatelyBefore(today.AddDays(-1)));
            Assert.IsTrue(range.IsImmediatelyBefore(today.AddDays(1)));
        }

        [TestMethod]
        public void Test_GetDatesBetween()
        {
            var first = new DateRange(today.AddDays(-1));
            var second = new DateRange(today.AddDays(1));
            var expected = new[] { today };
            var actual = DateRange.GetDatesBetween(first, second).ToArray();
            CollectionAssert.AreEqual(expected, actual);

            first = new DateRange();
            actual = DateRange.GetDatesBetween(first, second).ToArray();
            Assert.IsTrue(actual.Length == 0);

            first = new DateRange(today);
            second = new DateRange(today);
            actual = DateRange.GetDatesBetween(first, second).ToArray();
            Assert.IsTrue(actual.Length == 0);

            first = new DateRange(today.AddDays(1));
            second = new DateRange(today.AddDays(-1));
            actual = DateRange.GetDatesBetween(first, second).ToArray();
            Assert.IsTrue(actual.Length == 0);
        }

        [TestMethod]
        public void Test_GetRangeBetween()
        {
            var first = new DateRange(today.AddDays(-1));
            var second = new DateRange(today.AddDays(1));
            var range = DateRange.GetRangeBetween(first, second);
            Assert.AreEqual(new DateRange(today), range);

            first = new DateRange();
            range = DateRange.GetRangeBetween(first, second);
            Assert.IsTrue(range.IsEmpty);

            first = new DateRange(today);
            second = new DateRange(today);
            range = DateRange.GetRangeBetween(first, second);
            Assert.IsTrue(range.IsEmpty);

            first = new DateRange(today.AddDays(1));
            second = new DateRange(today.AddDays(-1));
            range = DateRange.GetRangeBetween(first, second);
            Assert.IsTrue(range.IsEmpty);
        }

        [TestMethod]
        public void Test_Overlap()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            var overlap = range.Overlap(other);
            Assert.IsTrue(overlap.IsEmpty);

            range = new DateRange(today);
            other = new DateRange();
            overlap = range.Overlap(other);
            Assert.IsTrue(overlap.IsEmpty);

            other = new DateRange(today);
            overlap = range.Overlap(other);
            Assert.AreEqual(range, overlap);

            range = new DateRange(today.AddDays(-1));
            other = new DateRange(today.AddDays(1));
            overlap = range.Overlap(other);
            Assert.IsTrue(overlap.IsEmpty);

            range = new DateRange(today.AddDays(-12), today.AddDays(-6));
            other = new DateRange(today.AddDays(-10), today.AddDays(-2));
            overlap = range.Overlap(other);
            Assert.AreEqual(new DateRange(today.AddDays(-10), today.AddDays(-6)), overlap);

            range = new DateRange(today.AddDays(-12), today.AddDays(12));
            var ranges = new[]
            {
                new DateRange(today.AddDays(-8), today.AddDays(8)),
                new DateRange(today.AddDays(-5), today.AddDays(5)),
                new DateRange(today.AddDays(-2), today.AddDays(2))
            };
            overlap = range.Overlap(ranges);
            Assert.AreEqual(new DateRange(today.AddDays(-2), today.AddDays(2)), overlap);
        }

        [TestMethod]
        public void Test_Overlaps_Is_False_When_Empty()
        {
            var range = new DateRange();
            var other = new DateRange(today);
            Assert.IsFalse(range.Overlaps(other));

            range = new DateRange(today);
            other = new DateRange();
            Assert.IsFalse(range.Overlaps(other));
        }

        [TestMethod]
        public void Test_Overlaps_When_Same_Ranges()
        {
            var range = new DateRange(today);
            var other = new DateRange(today);
            Assert.IsTrue(range.Overlaps(other));

            range = new DateRange(today, today.AddDays(2));
            other = new DateRange(today, today.AddDays(2));
            Assert.IsTrue(range.Overlaps(other));
        }

        [TestMethod]
        public void Test_Overlaps()
        {
            var range = new DateRange(today.AddDays(-5), today);
            var other = new DateRange(today.AddDays(-2), today.AddDays(2));
            Assert.IsTrue(range.Overlaps(other));

            range = new DateRange(today.AddDays(-5), today);
            other = new DateRange(today.AddDays(1), today.AddDays(2));
            Assert.IsFalse(range.Overlaps(other));

            range = new DateRange(today.AddDays(3), today.AddDays(4));
            other = new DateRange(today.AddDays(1), today.AddDays(2));
            Assert.IsFalse(range.Overlaps(other));
        }

        [TestMethod]
        public void Test_Shift()
        {
            var range = new DateRange(today);

            var result = range.Shift(DateRangePart.Begin, -1, 0, 0);
            Assert.AreEqual(new DateRange(today.AddYears(-1), today), result);          
            result = range.Shift(DateRangePart.Begin, 0, -1, 0);
            Assert.AreEqual(new DateRange(today.AddMonths(-1), today), result);        
            result = range.Shift(DateRangePart.Begin, 0, 0, -1);
            Assert.AreEqual(new DateRange(today.AddDays(-1), today), result);

            result = range.Shift(DateRangePart.End, 1, 0, 0);
            Assert.AreEqual(new DateRange(today, today.AddYears(1)), result);
            result = range.Shift(DateRangePart.End, 0, 1, 0);
            Assert.AreEqual(new DateRange(today, today.AddMonths(1)), result);
            result = range.Shift(DateRangePart.End, 0, 0, 1);
            Assert.AreEqual(new DateRange(today, today.AddDays(1)), result);

            result = range.Shift(DateRangePart.Both, 1, 0, 0);
            Assert.AreEqual(new DateRange(today.AddYears(1), today.AddYears(1)), result);
            result = range.Shift(DateRangePart.Both, 0, 1, 0);
            Assert.AreEqual(new DateRange(today.AddMonths(1), today.AddMonths(1)), result);
            result = range.Shift(DateRangePart.Both, 0, 0, 1);
            Assert.AreEqual(new DateRange(today.AddDays(1), today.AddDays(1)), result);

            Assert.ThrowsException<ArgumentException>(() => range.Shift((DateRangePart)999, 1, 1, 1));
        }

        [TestMethod]
        public void Test_Split()
        {
            int days = 2;
            var range = new DateRange();
            var result = range.Split(days);
            Assert.IsFalse(result.Any());
            
            range = new DateRange(today);
            result = range.Split(days);
            Assert.IsTrue(result.Count() == 1 && result.First().Equals(range));

            var begin = new DateTime(2025, 6, 30); // monday
            var end = new DateTime(2025, 7, 6); // sunday
            range = new DateRange(begin, end);
            var expected = new[] 
            {
                new DateRange(new DateTime(2025, 6, 30), new DateTime(2025, 7, 1)),
                new DateRange(new DateTime(2025, 7, 2), new DateTime(2025, 7, 3)),
                new DateRange(new DateTime(2025, 7, 4), new DateTime(2025, 7, 5)),
                new DateRange(new DateTime(2025, 7, 6))
            };
            var actual = range.Split(days).ToArray();
            CollectionAssert.AreEqual(expected, actual);

            actual = range.Split(-1).ToArray();
            CollectionAssert.AreEqual(new DateRange[] { range }, actual);

            actual = range.Split(0).ToArray();
            CollectionAssert.AreEqual(new DateRange[] { range }, actual);

            actual = range.Split(1).ToArray();
            CollectionAssert.AreEqual(new DateRange[] { range }, actual);
        }

        [TestMethod]
        public void Test_SplitByMonth()
        {
            var range = new DateRange(new DateTime(2025, 1, 1), new DateTime(2025, 3, 31));
            var expected = new[]
            {
                new DateRange(new DateTime(2025, 1, 1), new DateTime(2025, 1, 31)),
                new DateRange(new DateTime(2025, 2, 1), new DateTime(2025, 2, 28)),
                new DateRange(new DateTime(2025, 3, 1), new DateTime(2025, 3, 31))
            };
            var actual = range.SplitByMonth().ToArray();
            CollectionAssert.AreEqual(expected, actual);

            range = new DateRange(new DateTime(2026, 1, 8));
            expected = new[]
            {
                new DateRange(new DateTime(2026, 1, 8))
            };
            actual = range.SplitByMonth().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_SplitByYear() 
        {
            var range = new DateRange(new DateTime(2023, 1, 1), new DateTime(2024, 12, 31));
            var expected = new[] 
            {
                new DateRange(new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)),
                new DateRange(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31))
            };
            var actual = range.SplitByYear().ToArray();
            CollectionAssert.AreEqual(expected, actual);

            range = new DateRange(new DateTime(2026, 1, 8));
            expected = new[]
            {
                new DateRange(new DateTime(2026, 1, 8))
            };
            actual = range.SplitByYear().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var range = new DateRange(today);
            var other = new DateRange(today);
            Assert.IsTrue(range.Equals(other) && range.GetHashCode() == other.GetHashCode());

            range = new DateRange();
            Assert.AreEqual(0, range.GetHashCode());
        }

        [TestMethod]
        public void Test_Clone()
        {
            var range = new DateRange(today);
            var clone = range.Clone();
            Assert.AreEqual(range, clone);

            ICloneable cloneable = new DateRange(today);
            Assert.AreEqual(clone, cloneable.Clone());
        }

        [TestMethod]
        public void Test_ToString()
        {
            var range = new DateRange();
            Assert.AreEqual(string.Empty, range.ToString());

            range = new DateRange(today);
            string expected = today.ToShortDateString() + " " + today.ToShortDateString();
            string actual = range.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetWeek()
        {
            var date = new DateTime(2025, 7, 3); // thursday
            var expected = new DateRange(new DateTime(2025, 6, 30), new DateTime(2025, 7, 6));
            var actual = DateRange.GetWeek(date, DayOfWeek.Monday);
            Assert.AreEqual(expected, actual);

            expected = new DateRange(new DateTime(2025, 6, 29), new DateTime(2025, 7, 5));
            actual = DateRange.GetWeek(date, DayOfWeek.Sunday);
            Assert.AreEqual(expected, actual);

            date = new DateTime(2025, 6, 30);
            expected = new DateRange(new DateTime(2025, 6, 30), new DateTime(2025, 7, 6));
            actual = DateRange.GetWeek(date, DayOfWeek.Monday);
            Assert.AreEqual(expected, actual);

            Assert.ThrowsException<ArgumentException>(() => DateRange.GetWeek(date, DayOfWeek.Tuesday));
            Assert.ThrowsException<ArgumentException>(() => DateRange.GetWeek(date, DayOfWeek.Wednesday));
            Assert.ThrowsException<ArgumentException>(() => DateRange.GetWeek(date, DayOfWeek.Thursday));
            Assert.ThrowsException<ArgumentException>(() => DateRange.GetWeek(date, DayOfWeek.Friday));
            Assert.ThrowsException<ArgumentException>(() => DateRange.GetWeek(date, DayOfWeek.Saturday));

            date = new DateTime(2026, 1, 8);
            expected = new DateRange(new DateTime(2026, 1, 5), new DateTime(2026, 1, 11));
            actual = DateRange.GetWeek(date);
            Assert.AreEqual(expected, actual);

            date = new DateTime(2026, 1, 8);
            expected = new DateRange(new DateTime(2026, 1, 5), new DateTime(2026, 1, 11));
            actual = DateRange.GetWeek(date, CultureInfo.GetCultureInfo("fi-FI"));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetMonth()
        {
            var date = new DateTime(2025, 7, 3); // thursday
            var expected = new DateRange(new DateTime(2025, 7, 1), new DateTime(2025, 7, 31));
            var actual = DateRange.GetMonth(date);
            Assert.AreEqual(expected, actual);

            actual = DateRange.GetMonth(date, CultureInfo.GetCultureInfo("fi-FI"));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetRandomDate()
        {
            var random = RandomHelper.CreateRandom();
            var range = new DateRange(new DateTime(2025, 7, 1), new DateTime(2025, 7, 31));
            var date = range.GetRandomDate(random);
            Assert.IsTrue(date.HasValue && range.Contains(date.Value));
            
            var dt = DateRange.GetRandomDate(random, today, today);
            Assert.AreEqual(today, dt);
            dt = DateRange.GetRandomDate(random, new DateTime(2025, 7, 31), new DateTime(2025, 7, 1));
            Assert.IsTrue(range.Contains(dt));

            range = new DateRange(today);
            date = range.GetRandomDate(random);
            Assert.IsTrue(date.HasValue && today.Equals(date.Value));
            
            range = new DateRange();
            date = range.GetRandomDate(random);
            Assert.IsFalse(date.HasValue);
        }

        [TestMethod]
        public void Test_Operators()
        {
            var left = new DateRange(today);
            var right = new DateRange(today);
            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);

            right = new DateRange(today.AddDays(1));
            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
        }
    }
}

