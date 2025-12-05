using System.Text;

namespace Masasamjant.Text
{
    [TestClass]
    public class StringBuilderCacheUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Default_Constructor()
        {
            var cache = new StringBuilderCache();
            Assert.IsFalse(cache.IsPreviousContentCleared);
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());
        }

        [TestMethod]
        public void Test_Constructor_With_Builder()
        {
            var sb = new StringBuilder("Test");
            var cache = new StringBuilderCache(sb);
            Assert.IsFalse(cache.IsPreviousContentCleared);
            var retrievedBuilder = cache.GetBuilder();
            Assert.AreSame(sb, retrievedBuilder);
        }

        [TestMethod]
        public void Test_IsPreviousContentCleared_Property()
        {
            var sb = new StringBuilder("Test");
            var cache = new StringBuilderCache(sb);
            Assert.IsFalse(cache.IsPreviousContentCleared);
            cache.IsPreviousContentCleared = true;
            Assert.IsTrue(cache.IsPreviousContentCleared);
        }

        [TestMethod]
        public void Test_SetBuilder()
        {
            var initial = new StringBuilder("Initial");
            var cache = new StringBuilderCache();
            cache.SetBuilder(initial);
            var builder = cache.GetBuilder();
            Assert.AreSame(initial, builder);
        }

        [TestMethod]
        public void Test_Clear()
        {
            var builder = new StringBuilder();
            var cache = new StringBuilderCache(builder);
            bool cleared = false;
            cache.Clearing += (s, e) => { cleared = true; };

            cache.Clear();
            Assert.IsFalse(cleared);
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());

            builder = new StringBuilder("Content");
            cache.SetBuilder(builder);
            cache.Clear();
            Assert.IsTrue(cleared);
            Assert.AreEqual(string.Empty, builder.ToString());
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());
        }

        [TestMethod]
        public void GetBuilder()
        {
            var cache = new StringBuilderCache();
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());

            var initial = new StringBuilder();
            cache.SetBuilder(initial);
            var builder = cache.GetBuilder();
            builder.Append("Foo");
            Assert.AreSame(initial, builder);
            builder = cache.GetBuilder();
            builder.Append(" Bar");
            Assert.AreSame(initial, builder);
            builder = cache.GetBuilder();
            Assert.AreEqual("Foo Bar", builder.ToString());

            bool cleared = false;
            cache.Clearing += (s, e) => cleared = true;
            cache.IsPreviousContentCleared = true;
            initial = new StringBuilder("Content");
            cache.SetBuilder(initial);
            builder = cache.GetBuilder();
            Assert.AreSame(initial, builder);
            Assert.IsTrue(cleared);
            Assert.AreEqual(string.Empty, builder.ToString());

            builder.Append("Foo");
            builder.Append(" Bar");
            Assert.AreEqual("Foo Bar", builder.ToString());
            var other = cache.GetBuilder();
            Assert.AreSame(builder, other);
            Assert.AreEqual(string.Empty, builder.ToString());
        }
    }
}
