namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class DisposableComponentUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Dispose_ShouldCallOnDisposing()
        {
            var disposable = new TestDisposableComponent();
            Assert.IsFalse(disposable.OnDisposingCalled);
            disposable.Dispose();
            Assert.IsTrue(disposable.OnDisposingCalled);
            Assert.IsTrue(disposable.IsDisposed);
        }

        private class TestDisposableComponent : DisposableComponent
        {
            public bool OnDisposingCalled { get; private set; } = false;
            
            protected override void OnDisposing()
            {
                OnDisposingCalled = true;
            }
        }
    }
}
