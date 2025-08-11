namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class IntegerSequenceHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsZero()
        {
            Assert.IsFalse(IntegerSequenceHelper.IsZero(IntegerSequence.Empty));
            Assert.IsTrue(IntegerSequenceHelper.IsZero(IntegerSequence.Create(4)));
            Assert.IsTrue(IntegerSequenceHelper.IsZero(IntegerSequence.Create("000")));
            Assert.IsFalse(IntegerSequenceHelper.IsZero(IntegerSequence.Create("001")));
            Assert.IsTrue(IntegerSequenceHelper.IsZero(IntegerSequence.Create(0, 3)));
            Assert.IsFalse(IntegerSequenceHelper.IsZero(IntegerSequence.Create(1, 3)));
        }

        [TestMethod]
        public void Test_TryGetInt32()
        {
            int? result;
            Assert.IsFalse(IntegerSequenceHelper.TryGetInt32(IntegerSequence.Empty, out result));
            Assert.IsTrue(IntegerSequenceHelper.TryGetInt32(IntegerSequence.Create(3), out result) && result == 0);
            Assert.IsTrue(IntegerSequenceHelper.TryGetInt32(IntegerSequence.Create(int.MaxValue, int.MaxValue.ToString().Length), out result) && result == int.MaxValue);
            Assert.IsFalse(IntegerSequenceHelper.TryGetInt32(IntegerSequence.Create(long.MaxValue, long.MaxValue.ToString().Length), out result));
        }

        [TestMethod]
        public void Test_TryGetInt64()
        {
            long? result;
            Assert.IsFalse(IntegerSequenceHelper.TryGetInt64(IntegerSequence.Empty, out result));
            Assert.IsTrue(IntegerSequenceHelper.TryGetInt64(IntegerSequence.Create(3), out result) && result == 0);
            Assert.IsTrue(IntegerSequenceHelper.TryGetInt64(IntegerSequence.Create(long.MaxValue, long.MaxValue.ToString().Length), out result) && result == long.MaxValue);
            var sequence = IntegerSequence.Create(long.MaxValue, long.MaxValue.ToString().Length).Next();
            Assert.IsFalse(IntegerSequenceHelper.TryGetInt64(sequence, out result));
        }

        [TestMethod]
        public void Test_TryGetDouble()
        {
            double? result;
            Assert.IsFalse(IntegerSequenceHelper.TryGetDouble(IntegerSequence.Empty, out result));
            Assert.IsTrue(IntegerSequenceHelper.TryGetDouble(IntegerSequence.Create(3), out result) && result == 0);
            Assert.IsTrue(IntegerSequenceHelper.TryGetDouble(IntegerSequence.Create(long.MaxValue, long.MaxValue.ToString().Length), out result) && result == long.MaxValue);
            var sequence = IntegerSequence.Create(long.MaxValue, long.MaxValue.ToString().Length).Next();
            Assert.IsTrue(IntegerSequenceHelper.TryGetDouble(sequence, out result) && result == (double)long.MaxValue + 1);
        }
    }
}
