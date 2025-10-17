namespace Masasamjant
{
    [TestClass]
    public class DisposableContextUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_DisposableContext()
        {
            var context = new DisposableContext();
            var disposable = context.Add(new TestDisposable());
            Assert.IsFalse(disposable.IsDispose);
            context.Dispose();
            Assert.IsTrue(disposable.IsDispose);
            Assert.ThrowsException<ObjectDisposedException>(() => context.Add(disposable));
        }

        private class TestDisposable : IDisposable
        {
            public bool IsDispose { get; private set; } = false;

            public void Dispose()
            {
                IsDispose = true;
            }
        }
    }
}
