using Masasamjant.Stubs;
using System.Text;

namespace Masasamjant.Text
{
    [TestClass]
    public class ThreadStringBuilderCacheUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_SetBuilder()
        {
            var provider = new ThreadStubProvider();
            var ta = new ThreadStub(1);
            var tb = new ThreadStub(2);
            provider.SetCurrentThread(ta);

            var cache = new ThreadStringBuilderCache(provider);
            var a = new StringBuilder("a");
            var b = new StringBuilder("b");

            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());
            cache.SetBuilder(a);
            var c = cache.GetBuilder();
            Assert.AreSame(a, c);

            provider.SetCurrentThread(tb);
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());
            cache.SetBuilder(b);
            c = cache.GetBuilder();
            Assert.AreSame(b, c);
        }

        [TestMethod]
        public void Test_Clear()
        {
            var provider = new ThreadStubProvider();
            var ta = new ThreadStub(1);
            var tb = new ThreadStub(2);
            provider.SetCurrentThread(ta);

            var cache = new ThreadStringBuilderCache(provider);
            var a = new StringBuilder("a");
            var b = new StringBuilder("b");

            bool ca = false;
            bool cb = false;
            cache.Clearing += (s, e) => 
            {
                if (ReferenceEquals(e.Builder, a))
                    ca = true;
                else if (ReferenceEquals(e.Builder, b))
                    cb = true;
            };

            cache.Clear();
            Assert.IsFalse(ca);
            Assert.IsFalse(cb);

            cache.SetBuilder(a);
            provider.SetCurrentThread(tb);

            cache.Clear();
            Assert.IsFalse(ca);
            Assert.IsFalse(cb);

            cache.SetBuilder(b);
            provider.SetCurrentThread(ta);
            cache.Clear();
            Assert.IsTrue(ca);
            Assert.IsFalse(cb);
            Assert.AreEqual(string.Empty, a.ToString());
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());

            provider.SetCurrentThread(tb);
            cache.Clear();
            Assert.IsTrue(cb);
            Assert.AreEqual(string.Empty, b.ToString());
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());
        }

        [TestMethod]
        public void Test_GetBuilder()
        {
            var provider = new ThreadStubProvider();
            var ta = new ThreadStub(1);
            var tb = new ThreadStub(2);
            provider.SetCurrentThread(ta);

            var cache = new ThreadStringBuilderCache(provider);
            var a = new StringBuilder("a");
            var b = new StringBuilder("b");
            bool ca = false;
            bool cb = false;
            cache.Clearing += (s, e) =>
            {
                if (ReferenceEquals(e.Builder, a))
                    ca = true;
                else if (ReferenceEquals(e.Builder, b))
                    cb = true;
            };

            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());
            cache.SetBuilder(a);
            var c = cache.GetBuilder();
            Assert.AreSame(a, c);
            Assert.IsFalse(ca);
            Assert.AreEqual("a", c.ToString());

            provider.SetCurrentThread(tb);
            Assert.ThrowsException<InvalidOperationException>(() => cache.GetBuilder());
            cache.SetBuilder(b);
            c = cache.GetBuilder();
            Assert.AreSame(b, c);
            Assert.IsFalse(cb);
            Assert.AreEqual("b", c.ToString());

            cache.IsPreviousContentCleared = true;
            
            provider.SetCurrentThread(ta);
            c = cache.GetBuilder();
            Assert.AreSame(a, c);
            Assert.IsTrue(ca);
            Assert.AreEqual(string.Empty, c.ToString());

            provider.SetCurrentThread(tb);
            c = cache.GetBuilder();
            Assert.AreSame(b, c);
            Assert.IsTrue(cb);
            Assert.AreEqual(string.Empty, c.ToString());

        }
    }
}
