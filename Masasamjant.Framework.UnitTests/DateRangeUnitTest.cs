using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant
{
    [TestClass]
    public class DateRangeUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructors()
        {
            var range = new DateRange();
            Assert.IsTrue(range.IsEmpty);

            var today = DateTime.Today;
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
        }
    }
}

