using Masasamjant.Threading;
using System.Globalization;

namespace Masasamjant.Stubs
{
    internal class ThreadStub : IThread
    {
        public ThreadStub(int managedThreadId, CultureInfo? currentCulture = null, CultureInfo? currentUICulture = null)
        {
            ManagedThreadId = managedThreadId;
            CurrentCulture = currentCulture ?? CultureInfo.CurrentCulture;
            CurrentUICulture = currentUICulture ?? CultureInfo.CurrentUICulture;
        }

        public int ManagedThreadId { get; }

        public CultureInfo CurrentCulture { get; } 

        public CultureInfo CurrentUICulture { get; }
    }
}
