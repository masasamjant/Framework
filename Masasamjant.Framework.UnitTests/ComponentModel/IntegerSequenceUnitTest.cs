namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class IntegerSequenceUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Create()
        {
            var sequence = IntegerSequence.Create(3);
            Assert.AreEqual("000", sequence.Value);
            Assert.IsFalse(sequence.IsFull);
            Assert.IsFalse(sequence.IsEmpty);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => IntegerSequence.Create(0));

            sequence = IntegerSequence.Create(1, 3);
            Assert.AreEqual("001", sequence.Value);
            Assert.IsFalse(sequence.IsFull);
            Assert.IsFalse(sequence.IsEmpty);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => IntegerSequence.Create(1, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => IntegerSequence.Create(-1, 3));

            sequence = IntegerSequence.Create(999, 3);
            Assert.AreEqual("999", sequence.Value);
            Assert.IsTrue(sequence.IsFull);
            Assert.IsFalse(sequence.IsEmpty);

            sequence = IntegerSequence.Create(1L, 3);
            Assert.AreEqual("001", sequence.Value);
            Assert.IsFalse(sequence.IsFull);
            Assert.IsFalse(sequence.IsEmpty);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => IntegerSequence.Create(1L, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => IntegerSequence.Create(-1L, 3));

            sequence = IntegerSequence.Create(999L, 3);
            Assert.AreEqual("999", sequence.Value);
            Assert.IsTrue(sequence.IsFull);
            Assert.IsFalse(sequence.IsEmpty);

            sequence = IntegerSequence.Create("123456");
            Assert.AreEqual("123456", sequence.Value);
            Assert.IsFalse(sequence.IsFull);
            Assert.IsFalse(sequence.IsEmpty);
            sequence = IntegerSequence.Create(string.Empty);
            Assert.IsTrue(sequence.IsEmpty);
            Assert.ThrowsException<ArgumentException>(() => IntegerSequence.Create("1A"));
        }

        [TestMethod]
        public void Test_Next()
        {
            var sequence = IntegerSequence.Create(999, 3);
            Assert.ThrowsException<InvalidOperationException>(() => sequence.Next());
            sequence = IntegerSequence.Create(1, 3);
            sequence = sequence.Next();
            Assert.AreEqual("002", sequence.Value);
            sequence = sequence.Next();
            Assert.AreEqual("003", sequence.Value);
        }

        [TestMethod]
        public void Test_Equals()
        {
            Assert.IsTrue(IntegerSequence.Empty.Equals(IntegerSequence.Create(string.Empty)));
            Assert.IsFalse(IntegerSequence.Empty.Equals(IntegerSequence.Create("123")));
            Assert.IsTrue(IntegerSequence.Create(123, 3).Equals(IntegerSequence.Create("123")));
            Assert.IsFalse(IntegerSequence.Empty.Equals(DateTime.Now));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            Assert.AreEqual(IntegerSequence.Create(123, 3).GetHashCode(), IntegerSequence.Create("123").GetHashCode());
        }
    }
}
