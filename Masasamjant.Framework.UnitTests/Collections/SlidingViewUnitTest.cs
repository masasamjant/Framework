namespace Masasamjant.Collections
{
    [TestClass]
    public class SlidingViewUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentException>(() => new SlidingView<int>(new SlidingView<int>(new List<int>(), 1), 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SlidingView<int>(new List<int>(), -1));
            var view = new SlidingView<int>(new List<int>(), 4);
            Assert.IsNotNull(view);
            Assert.IsTrue(view.Size == 4);
        }

        [TestMethod]
        public void Test_Slide()
        {
            var source = Range.GetRange(1, 10);
            var size = 5;
            var view = new SlidingView<int>(source, size);
            Assert.IsTrue(view.Slide());
            Assert.IsTrue(view.Slide());
            Assert.IsFalse(view.Slide());
        }

        [TestMethod]
        public void Test_Reset()
        {
            var source = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 0
            };
            var size = 3;
            var view = new SlidingView<int>(source, size);
            view.Slide();
            int[] expected = new int[] { 1, 2, 3 };
            int[] actual = view.Items.ToArray();
            CollectionAssert.AreEqual(expected, actual);
            view.Slide();
            expected = new int[] { 4, 5, 6 };
            actual = view.Items.ToArray();
            CollectionAssert.AreEqual(expected, actual);
            view.Reset();
            view.Slide();
            expected = new int[] { 1, 2, 3 };
            actual = view.Items.ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Items()
        {
            var source = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 0
            };
            var size = 3;
            var view = new SlidingView<int>(source, size);
            
            Assert.IsTrue(view.Slide());
            int[] expected = new int[] { 1, 2, 3 };
            int[] actual = view.Items.ToArray();
            CollectionAssert.AreEqual(expected, actual);
            
            Assert.IsTrue(view.Slide());
            expected = new int[] { 4, 5, 6 };
            actual = view.Items.ToArray();
            CollectionAssert.AreEqual(expected, actual);
            
            Assert.IsTrue(view.Slide());
            expected = new int[] { 7, 8, 9 };
            actual = view.Items.ToArray();
            CollectionAssert.AreEqual(expected, actual);

            Assert.IsTrue(view.Slide());
            expected = new int[] { 0 };
            actual = view.Items.ToArray();
            CollectionAssert.AreEqual(expected, actual);

            Assert.IsFalse(view.Slide());
        }

        [TestMethod]
        public void Test_GetEnumerator()
        {
            var source = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 0
            };
            var size = 3;
            var view = new SlidingView<int>(source, size);
            var result = new List<int>();
            var enumerator = view.GetEnumerator();
            while (enumerator.MoveNext())
                result.Add(enumerator.Current);
            CollectionAssert.AreEqual(source, result);
        }
    }
}
