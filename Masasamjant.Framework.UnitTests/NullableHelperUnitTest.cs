namespace Masasamjant
{
    [TestClass]
    public class NullableHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_When_Empty_Then_FirstWithValue_Returns_Default()
        {
            var values = new int?[0];
            int? defaultValue = -1;
            int? result = NullableHelper.FirstWithValue(values, defaultValue);
            Assert.IsTrue(result == defaultValue);
        }

        [TestMethod]
        public void Test_When_No_Value_Then_FirstWithValue_Returns_Default()
        {
            var values = new int?[] { null, null, null };
            int? defaultValue = -1;
            int? result = NullableHelper.FirstWithValue(values, defaultValue);
            Assert.IsTrue(result == defaultValue);
        }

        [TestMethod]
        public void Test_When_Has_Values_Then_FirstWithValue_Returns_First()
        {
            var values = new int?[] { null, 2, 3 };
            int? defaultValue = -1;
            int? result = NullableHelper.FirstWithValue(values, defaultValue);
            Assert.IsTrue(result == 2);
        }

        [TestMethod]
        public void Test_When_Empty_Then_LastWithValue_Returns_Default()
        {
            var values = new int?[0];
            int? defaultValue = -1;
            int? result = NullableHelper.LastWithValue(values, defaultValue);
            Assert.IsTrue(result == defaultValue);
        }

        [TestMethod]
        public void Test_When_No_Value_Then_LastWithValue_Returns_Default()
        {
            var values = new int?[] { null, null, null };
            int? defaultValue = -1;
            int? result = NullableHelper.LastWithValue(values, defaultValue);
            Assert.IsTrue(result == defaultValue);
        }

        [TestMethod]
        public void Test_When_Has_Values_Then_LastWithValue_Returns_Last()
        {
            var values = new int?[] { null, 2, 3 };
            int? defaultValue = -1;
            int? result = NullableHelper.LastWithValue(values, defaultValue);
            Assert.IsTrue(result == 3);
        }

        [TestMethod]
        public void Test_When_Empty_Then_WithValue_Returns_Empty()
        {
            int?[] values = new int?[0];
            var result = NullableHelper.WithValue(values);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Test_When_No_Values_Then_WithValue_Returns_Empty()
        {
            int?[] values = new int?[] { null, null, null };
            var result = NullableHelper.WithValue(values);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Test_When_Has_Values_Then_WithValue_Returns_Values()
        {
            int?[] values = new int?[] { null, 2, null, 4 };
            var expected = new int[] { 2, 4 };
            var actual = NullableHelper.WithValue(values).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_When_Empty_Then_NoValue_Returns_True()
        {
            int?[] values = new int?[0];
            Assert.IsTrue(NullableHelper.NoValue(values));
        }

        [TestMethod]
        public void Test_When_No_Value_Then_NoValue_Returns_True()
        {
            int?[] values = new int?[] { null, null, null };
            Assert.IsTrue(NullableHelper.NoValue(values));
        }

        [TestMethod]
        public void Test_When_Has_Value_Then_NoValue_Returns_False()
        {
            int?[] values = new int?[] { null, null, 1 };
            Assert.IsFalse(NullableHelper.NoValue(values));
        }
    }
}
