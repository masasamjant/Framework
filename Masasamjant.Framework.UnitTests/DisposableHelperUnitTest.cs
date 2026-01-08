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

            disposable = new TestDisposable();
            Assert.IsFalse(disposable.IsDisposed);
            DisposableHelper.SafeDispose(disposable);
            Assert.IsTrue(disposable.IsDisposed);
        }

        [TestMethod]
        public void Test_CheckIsDisposed()
        {
            var disposable = new TestDisposable();
            DisposableHelper.CheckIsDisposed(disposable);
            disposable.Dispose();
            Assert.ThrowsException<ObjectDisposedException>(() => DisposableHelper.CheckIsDisposed(disposable));
        }

        private class TestDisposable : IDisposable, ISupportIsDisposed
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
