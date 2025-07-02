namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods for <see cref="IDisposable"/> interface.
    /// </summary>
    public static class DisposableHelper
    {
        /// <summary>
        /// Safely disposes <see cref="IDisposable"/> by swallowing possible exceptions.
        /// </summary>
        /// <param name="disposable">The <see cref="IDisposable"/> to dispose.</param>
        public static void SafeDispose(this IDisposable disposable)
        {
            SafeDispose(disposable, out _);
        }

        /// <summary>
        /// Safely disposes <see cref="IDisposable"/> by swallowing possible exceptions.
        /// </summary>
        /// <param name="disposable">The <see cref="IDisposable"/> to dispose.</param>
        /// <param name="exception">The occurred exception, if returns <c>false</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if <paramref name="disposable"/> was disposed without exception; <c>false</c> otherwise.</returns>
        public static bool SafeDispose(this IDisposable disposable, out Exception? exception)
        {
            exception = null;
            
            try
            {
                disposable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }
    }
}
