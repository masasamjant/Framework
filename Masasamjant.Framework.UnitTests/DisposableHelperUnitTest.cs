namespace Masasamjant
{
    [TestClass]
    public class DisposableHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_SafeDispose()
        {
            var disposable = new TestDisposable();
            Assert.IsFalse(disposable.IsDisposed);
            var result = DisposableHelper.SafeDispose(disposable, out var exception);
            Assert.IsTrue(result);
            Assert.IsNull(exception);
            Assert.IsTrue(disposable.IsDisposed);
            result = DisposableHelper.SafeDispose(disposable, out exception);
            Assert.IsFalse(result);
            Assert.IsInstanceOfType<ObjectDisposedException>(exception);
        }
        private class TestDisposable : IDisposable
        {
            public bool IsDisposed { get; private set; }
            
            public void Dispose()
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                IsDisposed = true;
            }
        }
    }
}
