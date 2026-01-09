namespace Masasamjant
{
    [TestClass]
    public class TimingUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            DateOnly startDate = new DateOnly(2000, 1, 1);
            TimeOnly startTime = new TimeOnly(12, 0);
            DateOnly endDate = new DateOnly(2000, 1, 2);
            TimeOnly endTime = new TimeOnly(10, 0);
            var timing = new Timing(startDate, startTime, endDate, endTime);
            Assert.AreEqual(startDate, timing.StartDate);
            Assert.AreEqual(startTime, timing.StartTime);
            Assert.AreEqual(endDate, timing.EndDate);
            Assert.AreEqual(endTime, timing.EndTime);
            var startDateTime = new DateTime(startDate, startTime, DateTimeKind.Local);
            var endDateTime = new DateTime(endDate, endTime, DateTimeKind.Local);
            Assert.AreEqual(startDateTime, timing.StartDateTime);
            Assert.AreEqual(endDateTime, timing.EndDateTime);
            var duration = endDateTime.Subtract(startDateTime);
            Assert.AreEqual(duration, timing.Duration);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Timing(startDate, startTime, new DateOnly(1999, 12, 31), endTime));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Timing(startDate, startTime, endTime));
            endTime = new TimeOnly(16, 0);
            timing = new Timing(startDate, startTime, endTime);
            Assert.AreEqual(startDate, timing.StartDate);
            Assert.AreEqual(startTime, timing.StartTime);
            Assert.AreEqual(startDate, timing.EndDate);
            Assert.AreEqual(endTime, timing.EndTime);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var timing = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Assert.IsFalse(timing.Equals(DateTime.Now));
            
            var other = timing.Clone();
            Assert.IsTrue(timing.Equals(other));
            
            other = new Timing(timing.StartDateTime, timing.EndDateTime);
            Assert.IsTrue(timing.Equals(other));

            other = new Timing(timing.StartDateTime, timing.EndDateTime.AddHours(3));
            Assert.IsFalse(timing.Equals(other));

            other = new Timing(timing.StartDateTime.AddHours(1), timing.EndDateTime);
            Assert.IsFalse(timing.Equals(other));

            other = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Assert.IsTrue(timing.Equals(other));
            Assert.IsTrue(timing == other);
            Assert.IsFalse(timing != other);
            
            other = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(13, 0));
            Assert.IsFalse(timing.Equals(other));
            Assert.IsFalse(timing == other);
            Assert.IsTrue(timing != other);
        }

        [TestMethod]
        public void Test_GetHashCode() 
        {
            var timing = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            var other = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Assert.AreEqual(timing.GetHashCode(), other.GetHashCode());
        }

        [TestMethod]
        public void Test_Contains()
        {
            var timing = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Assert.IsTrue(timing.Contains(timing.StartDateTime));
            Assert.IsTrue(timing.Contains(timing.EndDateTime));
            Assert.IsTrue(timing.Contains(new DateTime(2000, 1, 1, 11, 22, 22, DateTimeKind.Local)));
            Assert.IsFalse(timing.Contains(new DateTime(2000, 1, 1, 9, 22, 22, DateTimeKind.Local)));
            Assert.IsFalse(timing.Contains(new DateTime(2000, 1, 1, 12, 22, 22, DateTimeKind.Local)));
            Assert.IsTrue(timing.Contains(new DateOnly(2000, 1, 1), new TimeOnly(11, 0)));
            Assert.IsFalse(timing.Contains(new DateOnly(2000, 1, 1), new TimeOnly(13, 0)));
        }

        [TestMethod]
        public void Test_ToString()
        {
            var startDate = new DateOnly(2000, 1, 1);
            var startTime = new TimeOnly(10, 0);
            var endTime = new TimeOnly(12, 0);
            var expected = $"{startDate}: {startTime} - {endTime}";
            var actual = new Timing(startDate, startTime, endTime).ToString();
            Assert.AreEqual(expected, actual);
            var endDate = new DateOnly(2000, 1, 2);
            expected = $"{startDate}: {startTime} - {endDate}: {endTime}";
            actual = new Timing(startDate, startTime, endDate, endTime).ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_FromHours()
        {
            var startDateTime = new DateTime(2000, 1, 1, 10, 0, 0);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Timing.FromHours(startDateTime, -1));
            var expected = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(10, 0));
            var actual = Timing.FromHours(startDateTime, 0);
            Assert.AreEqual(expected, actual);
            expected = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(11, 0));
            actual = Timing.FromHours(startDateTime, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_FromMinutes() 
        {
            var startDateTime = new DateTime(2000, 1, 1, 10, 0, 0);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Timing.FromMinutes(startDateTime, -1));
            var expected = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(10, 0));
            var actual = Timing.FromMinutes(startDateTime, 0);
            Assert.AreEqual(expected, actual);
            expected = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(10, 30));
            actual = Timing.FromMinutes(startDateTime, 30);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Extend()
        {
            var timing = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(11, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => timing.Extend(TimeSpan.FromHours(-1)));
            var actual = timing.Extend(TimeSpan.Zero);
            Assert.AreEqual(timing, actual);
            actual = timing.Extend(TimeSpan.FromHours(1));
            var expected = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Shorten()
        {
            var timing = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => timing.Shorten(TimeSpan.FromHours(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => timing.Shorten(TimeSpan.FromHours(6)));
            var actual = timing.Shorten(TimeSpan.Zero);
            Assert.AreEqual(timing, actual);
            var expected = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(11, 0));
            actual = timing.Shorten(TimeSpan.FromHours(1));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Clone()
        {
            ICloneable cloneable = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Timing timing = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(12, 0));
            Assert.AreEqual(timing, cloneable.Clone());
        }
    }
}
