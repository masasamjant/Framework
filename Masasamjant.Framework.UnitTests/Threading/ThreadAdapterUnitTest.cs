namespace Masasamjant.Threading
{
    [TestClass]
    public class ThreadAdapterUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor_Throws_When_Null_Thread()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ThreadAdapter(null!));
        }

        [TestMethod]
        public void Test_ThreadAdapter_Properties()
        {
            var thread = Thread.CurrentThread;
            var adapter = new ThreadAdapter(thread);
            Assert.AreEqual(thread.ManagedThreadId, adapter.ManagedThreadId);
            Assert.AreEqual(thread.CurrentCulture, adapter.CurrentCulture);
            Assert.AreEqual(thread.CurrentUICulture, adapter.CurrentUICulture);
        }
    }
}
