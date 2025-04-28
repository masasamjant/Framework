namespace Masasamjant
{
    [TestClass]
    public class DoubleHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constants()
        { 
            Assert.AreEqual(0.0, DoubleHelper.Zero);
        }

        [TestMethod]
        public void Test_ReplaceNaN()
        {
            Assert.AreEqual(0.0, DoubleHelper.ReplaceNaN(double.NaN, 0.0));
            Assert.AreEqual(1.0, DoubleHelper.ReplaceNaN(1.0, 0.0));
            Assert.ThrowsException<ArgumentException>(() => DoubleHelper.ReplaceNaN(1.0, double.NaN));
        }

        [TestMethod]
        public void Test_ReplaceInfinity()
        {
            Assert.AreEqual(0.0, DoubleHelper.ReplaceInfinity(double.PositiveInfinity, 0.0));
            Assert.AreEqual(0.0, DoubleHelper.ReplaceInfinity(double.NegativeInfinity, 0.0));
            Assert.AreEqual(1.0, DoubleHelper.ReplaceInfinity(1.0, 0.0));
            Assert.ThrowsException<ArgumentException>(() => DoubleHelper.ReplaceInfinity(1.0, double.PositiveInfinity));
            Assert.ThrowsException<ArgumentException>(() => DoubleHelper.ReplaceInfinity(1.0, double.NegativeInfinity));
        }
    }
}
