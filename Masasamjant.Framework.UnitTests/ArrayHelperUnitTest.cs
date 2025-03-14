﻿namespace Masasamjant
{
    [TestClass]
    public class ArrayHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IterateForward_With_Empty_Array()
        {
            var array = new int[0];
            var result = ArrayHelper.IterateForward(array);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Test_IterateForward_With_Array()
        {
            var array = new int[] { 1, 2, 3 };
            var expected = new Iteration<int>[]
            {
                new Iteration<int>(1, 0),
                new Iteration<int>(2, 1),
                new Iteration<int>(3, 2)
            };
            var actual = ArrayHelper.IterateForward(array).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IterateBackward_With_Empty_Array()
        {
            var array = new int[0];
            var result = ArrayHelper.IterateBackward(array);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Test_IterateBackward_With_Array()
        {
            var array = new int[] { 1, 2, 3 };
            var expected = new Iteration<int>[]
            {
                new Iteration<int>(3, 2),
                new Iteration<int>(2, 1),
                new Iteration<int>(1, 0)
            };
            var actual = ArrayHelper.IterateBackward(array).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Combine_With_Null_Arrays()
        {
            int[]? array1 = null;
            int[]? array2 = null;
            var result = ArrayHelper.Combine(array1, array2);
            Assert.IsTrue(result.Length == 0);
        }

        [TestMethod]
        public void Test_Combine_With_First_Null_Array()
        {
            int[]? array1 = null;
            int[]? array2 = new int[] { 4, 5, 6 };
            var expected = new int[] { 4, 5, 6 };
            var actual = ArrayHelper.Combine(array1, array2);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Combine_With_Second_Null_Array()
        {
            int[]? array1 = new int[] { 1, 2, 3 };
            int[]? array2 = null;
            var expected = new int[] { 1, 2, 3 };
            var actual = ArrayHelper.Combine(array1, array2);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Combine_With_Both_Arrays()
        {
            int[]? array1 = new int[] { 1, 2, 3 };
            int[]? array2 = new int[] { 4, 5, 6 };
            var expected = new int[] { 1, 2, 3, 4, 5, 6 };
            var actual = ArrayHelper.Combine(array1, array2);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
