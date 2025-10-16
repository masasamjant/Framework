using System.Diagnostics;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents scope of stopwatch that will stop tha stopwatch when disposed if still running.
    /// </summary>
    public sealed class StopwatchScope : IDisposable
    {
        /// <summary>
        /// Creates new <see cref="StopwatchScope"/> instance for specified <see cref="Stopwatch"/>.
        /// A <paramref name="stopwatch"/> is started if not already running.
        /// </summary>
        /// <param name="stopwatch">The stopwatch to scope.</param>
        /// <returns>A <see cref="StopwatchScope"/>.</returns>
        public static StopwatchScope Create(Stopwatch stopwatch)
        {
            var scope = new StopwatchScope(stopwatch);
            if (!stopwatch.IsRunning)
                stopwatch.Start();
            return scope;
        }

        /// <summary>
        /// Disposes current instance and stops the scoped stopwatch if still running.
        /// </summary>
        public void Dispose()
        {
            if (stopwatch.IsRunning)
                stopwatch.Stop();

            GC.SuppressFinalize(this);
        }

        private readonly Stopwatch stopwatch;

        private StopwatchScope(Stopwatch stopwatch)
        {
            this.stopwatch = stopwatch;
        }

        ~StopwatchScope()
        {
            Dispose();
        }
    }
}
