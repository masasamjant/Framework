namespace Masasamjant.Linq
{
    [TestClass]
    public class SortDescriptorUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_When_Sort_Order_Undefined()
        {
            new SortDescriptor("Foo", (SortOrder)999);
        }

        [TestMethod]
        public void Test_Constructor()
        {
            var descriptor = new SortDescriptor("Foo", SortOrder.Descending);
            Assert.AreEqual("Foo", descriptor.PropertyName);
            Assert.AreEqual(SortOrder.Descending, descriptor.SortOrder);
        }

        [TestMethod]
        public void Test_Not_Equal()
        {
            var descriptor = new SortDescriptor("Foo", SortOrder.Descending);
            var other = new SortDescriptor("Bar", SortOrder.Descending);
            Assert.IsFalse(descriptor.Equals(other));
            other = new SortDescriptor("Foo", SortOrder.Ascending);
            Assert.IsFalse(descriptor.Equals(other));
        }

        [TestMethod]
        public void Test_Equal()
        {
            var descriptor = new SortDescriptor("Foo", SortOrder.Descending);
            var other = new SortDescriptor("Foo", SortOrder.Descending);
            Assert.IsTrue(descriptor.Equals(other));
            Assert.AreEqual(descriptor.GetHashCode(), other.GetHashCode());
        }

        [TestMethod]
        public void Test_ToString()
        {
            var descriptor = new SortDescriptor();
            Assert.AreEqual("", descriptor.ToString());
            descriptor = new SortDescriptor("Foo", SortOrder.None);
            Assert.AreEqual("Foo", descriptor.ToString());
            descriptor = new SortDescriptor("Foo", SortOrder.Ascending);
            Assert.AreEqual("Foo ASC", descriptor.ToString());
            descriptor = new SortDescriptor("Foo", SortOrder.Descending);
            Assert.AreEqual("Foo DESC", descriptor.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Create_When_Property_Not_Exist()
        {
            SortDescriptor.Create(typeof(SortDescriptor), "Foo", SortOrder.Descending);
        }

        [TestMethod]
        public void Test_Create()
        {
            var expected = new SortDescriptor("PropertyName", SortOrder.Descending);
            var actual = SortDescriptor.Create(typeof(SortDescriptor), "PropertyName", SortOrder.Descending);
            Assert.AreEqual(expected, actual);
        }
    }
}
