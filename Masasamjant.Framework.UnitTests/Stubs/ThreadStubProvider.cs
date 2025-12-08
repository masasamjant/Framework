using Masasamjant.Threading;

namespace Masasamjant.Stubs
{
    internal class ThreadStubProvider : IThreadProvider
    {
        private ThreadStub thread = new ThreadStub(0);

        public ThreadStub GetCurrentThread()
        {
            return thread;
        }

        public void SetCurrentThread(ThreadStub thread)
        {
            this.thread = thread;
        }   

        IThread IThreadProvider.GetCurrentThread()
        {
            return this.GetCurrentThread();
        }
    }
}
