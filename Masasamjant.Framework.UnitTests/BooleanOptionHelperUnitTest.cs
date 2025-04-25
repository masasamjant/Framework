namespace Masasamjant
{
    [TestClass]
    public class BooleanOptionHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsTrue()
        {
            Assert.IsTrue(BooleanOptionHelper.IsTrue(BooleanOption.True));
            Assert.IsFalse(BooleanOptionHelper.IsTrue(BooleanOption.False));
            Assert.IsFalse(BooleanOptionHelper.IsTrue(BooleanOption.Default));
        }

        [TestMethod]
        public void Test_IsFalse()
        {
            Assert.IsFalse(BooleanOptionHelper.IsFalse(BooleanOption.True));
            Assert.IsTrue(BooleanOptionHelper.IsFalse(BooleanOption.False));
            Assert.IsFalse(BooleanOptionHelper.IsFalse(BooleanOption.Default));
        }

        [TestMethod]
        public void Test_IsDefault()
        {
            Assert.IsFalse(BooleanOptionHelper.IsDefault(BooleanOption.True));
            Assert.IsFalse(BooleanOptionHelper.IsDefault(BooleanOption.False));
            Assert.IsTrue(BooleanOptionHelper.IsDefault(BooleanOption.Default));
        }

        [TestMethod]
        public void Test_FromBoolean()
        {
            Assert.AreEqual(BooleanOption.Default, BooleanOptionHelper.FromBoolean(null));
            Assert.AreEqual(BooleanOption.False, BooleanOptionHelper.FromBoolean(new bool?(false)));
            Assert.AreEqual(BooleanOption.True, BooleanOptionHelper.FromBoolean(new bool?(true)));
            Assert.AreEqual(BooleanOption.True, BooleanOptionHelper.FromBoolean(true));
            Assert.AreEqual(BooleanOption.False, BooleanOptionHelper.FromBoolean(false));
        }

        [TestMethod]
        public void Test_ToBoolean()
        {
            Assert.AreEqual(new bool?(), BooleanOptionHelper.ToBoolean(BooleanOption.Default));
            Assert.AreEqual(new bool?(false), BooleanOptionHelper.ToBoolean(BooleanOption.False));
            Assert.AreEqual(new bool?(true), BooleanOptionHelper.ToBoolean(BooleanOption.True));
            Assert.AreEqual(true, BooleanOptionHelper.ToBoolean(BooleanOption.Default, true));
            Assert.AreEqual(false, BooleanOptionHelper.ToBoolean(BooleanOption.Default, false));
            Assert.AreEqual(false, BooleanOptionHelper.ToBoolean(BooleanOption.False, true));
            Assert.AreEqual(true, BooleanOptionHelper.ToBoolean(BooleanOption.True, false));
        }

        [TestMethod]
        public void Test_TrueIfDefault()
        {
            Assert.AreEqual(true, BooleanOptionHelper.TrueIfDefault(BooleanOption.Default));
            Assert.AreEqual(true, BooleanOptionHelper.TrueIfDefault(BooleanOption.True));
            Assert.AreEqual(false, BooleanOptionHelper.TrueIfDefault(BooleanOption.False));
        }

        [TestMethod]
        public void Test_FalseIfDefault()
        {
            Assert.AreEqual(false, BooleanOptionHelper.FalseIfDefault(BooleanOption.Default));
            Assert.AreEqual(true, BooleanOptionHelper.FalseIfDefault(BooleanOption.True));
            Assert.AreEqual(false, BooleanOptionHelper.FalseIfDefault(BooleanOption.False));
        }

        [TestMethod]
        public void Test_And()
        {
            Assert.AreEqual(true, BooleanOptionHelper.And(BooleanOption.True, BooleanOption.True));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.True, BooleanOption.False));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.True, BooleanOption.Default));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.False, BooleanOption.True));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.False, BooleanOption.False));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.False, BooleanOption.Default));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.True));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.False));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.Default));

            Assert.AreEqual(true, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.Default, true));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.Default, false));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.True, false));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.False, false));
            Assert.AreEqual(true, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.True, true));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.Default, BooleanOption.False, true));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.False, BooleanOption.Default, false));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.True, BooleanOption.Default, false));
            Assert.AreEqual(false, BooleanOptionHelper.And(BooleanOption.False, BooleanOption.Default, true));
            Assert.AreEqual(true, BooleanOptionHelper.And(BooleanOption.True, BooleanOption.Default, true));
        }

        [TestMethod]
        public void Test_Or()
        {
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.True, BooleanOption.True));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.True, BooleanOption.False));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.True, BooleanOption.Default));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.False, BooleanOption.True));
            Assert.AreEqual(false, BooleanOptionHelper.Or(BooleanOption.False, BooleanOption.False));
            Assert.AreEqual(false, BooleanOptionHelper.Or(BooleanOption.False, BooleanOption.Default));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.True));
            Assert.AreEqual(false, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.False));
            Assert.AreEqual(false, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.Default));

            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.Default, true));
            Assert.AreEqual(false, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.Default, false));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.True, false));
            Assert.AreEqual(false, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.False, false));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.True, true));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.Default, BooleanOption.False, true));
            Assert.AreEqual(false, BooleanOptionHelper.Or(BooleanOption.False, BooleanOption.Default, false));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.True, BooleanOption.Default, false));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.False, BooleanOption.Default, true));
            Assert.AreEqual(true, BooleanOptionHelper.Or(BooleanOption.True, BooleanOption.Default, true));
        }

        [TestMethod]
        public void Test_Opposite()
        {
            Assert.AreEqual(BooleanOption.False, BooleanOptionHelper.Opposite(BooleanOption.True));
            Assert.AreEqual(BooleanOption.True, BooleanOptionHelper.Opposite(BooleanOption.False));
            Assert.AreEqual(BooleanOption.Default, BooleanOptionHelper.Opposite(BooleanOption.Default));
            Assert.AreEqual(BooleanOption.False, BooleanOptionHelper.Opposite(BooleanOption.Default, true));
            Assert.AreEqual(BooleanOption.True, BooleanOptionHelper.Opposite(BooleanOption.Default, false));
        }

        [TestMethod]
        public void Test_All()
        {
            Assert.IsTrue(BooleanOptionHelper.All([BooleanOption.True, BooleanOption.True, BooleanOption.True], BooleanOption.True));
            Assert.IsFalse(BooleanOptionHelper.All([BooleanOption.True, BooleanOption.True, BooleanOption.Default], BooleanOption.True));
        }

        [TestMethod]
        public void Test_Any()
        {
            Assert.IsTrue(BooleanOptionHelper.Any([BooleanOption.True, BooleanOption.True, BooleanOption.Default], BooleanOption.True));
            Assert.IsFalse(BooleanOptionHelper.Any([BooleanOption.True, BooleanOption.True, BooleanOption.Default], BooleanOption.False));
        }
    }
}
