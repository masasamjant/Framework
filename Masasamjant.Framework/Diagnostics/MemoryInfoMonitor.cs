namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents monitor to get <see cref="MemoryInfo"/> or monitor memory usage in specified intervals.
    /// </summary>
    public sealed class MemoryInfoMonitor : IDisposable
    {
        private readonly long startBytes;
        private readonly List<MemoryInfo> list;
        private readonly Lock listLock;
        private int size;
        private System.Timers.Timer? timer;
        private bool disposed;

        private MemoryInfoMonitor(long startBytes, bool allowMemoryCollection)
        {
            AllowMemoryCollection = allowMemoryCollection;
            this.startBytes = startBytes;
            this.listLock = new Lock();
            this.list = new List<MemoryInfo>();
            this.size = 10;
            this.disposed = false;
        }

        ~MemoryInfoMonitor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Notifies when memory read if <see cref="IsMonitoring"/> is enabled.
        /// </summary>
        public event EventHandler<MemoryInfoEventArgs>? MonitoringMemoryRead;

        /// <summary>
        /// Gets or sets whether or not memory collection is allowed when reading current memory.
        /// </summary>
        /// <remarks>When <c>true</c> then waits for system to collect memory. So use <c>false</c> if must not wait for memory collection.</remarks>
        public bool AllowMemoryCollection { get; set; }

        /// <summary>
        /// Gets whether or not monitoring memory has been enabled.
        /// </summary>
        public bool IsMonitoring { get; private set; }

        /// <summary>
        /// Creates new instance of the <see cref="MemoryInfoMonitor"/> class.
        /// </summary>
        /// <param name="allowMemoryCollection"><c>true</c> if memory collection is allowed; <c>false</c> otherwise.</param>
        /// <returns>A <see cref="MemoryInfoMonitor"/> instance.</returns>
        public static MemoryInfoMonitor Create(bool allowMemoryCollection = false)
        {
            var startBytes = GC.GetTotalMemory(allowMemoryCollection);
            return new MemoryInfoMonitor(startBytes, allowMemoryCollection);
        }

        /// <summary>
        /// Gets the <see cref="MemoryInfo"/> of the current state.
        /// </summary>
        /// <returns>A <see cref="MemoryInfo"/> of the current state.</returns>
        public MemoryInfo GetCurrent()
        {
            var endBytes = GC.GetTotalMemory(AllowMemoryCollection);
            return new MemoryInfo(startBytes, endBytes);
        }

        /// <summary>
        /// Gets the monitored memory if <see cref="IsMonitoring"/> is enabled.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MemoryInfo> GetMonitoredMemory()
        {
            if (IsMonitoring)
            {
                List<MemoryInfo> current;

                lock (listLock)
                {
                    current = list.ToList();
                }

                foreach (var item in current)
                    yield return item;
            }
            else
                yield break;
        }

        /// <summary>
        /// Start monitoring memory, if <see cref="IsMonitoring"/> is not enabled.
        /// </summary>
        /// <param name="interval">The interval how often memory is monitored.</param>
        /// <param name="size">The size how many monitored <see cref="MemoryInfo"/> values are kept.</param>
        /// <exception cref="InvalidOperationException">If <see cref="IsMonitoring"/> is already <c>true</c>.</exception>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public void StartMonitoring(TimeSpan interval, int size = 10)
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().Name);

            if (IsMonitoring)
                throw new InvalidOperationException("Monitoring is already started. Stop monitoring first and then re-start.");

            IsMonitoring = true;
            this.size = Math.Max(1, size);
            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Elapsed += OnTimerElapsed;
            timer.Interval = interval.TotalMilliseconds;
            timer.Start();
        }

        /// <summary>
        /// Stops the monitoring memory.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public void StopMonitoring()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().Name);

            StopMonitoringImpl();
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;
            
            disposed = true;
            StopMonitoringImpl();
        }

        private void StopMonitoringImpl()
        {
            if (IsMonitoring)
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer.Elapsed -= OnTimerElapsed;
                    timer.Dispose();
                    timer = null;
                }

                lock (listLock)
                {
                    list.Clear();
                }

                IsMonitoring = false;
            }
        }

        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsMonitoring || disposed)
                return;

            var current = GetCurrent();

            // Raise memory read event.
            MonitoringMemoryRead?.Invoke(this, new MemoryInfoEventArgs(current));

            // Add memory to monitored memory list. Make room if size reached.
            lock (listLock) 
            {
                int lastIndex = list.Count - 1;
                while (list.Count >= size)
                    list.RemoveAt(lastIndex--);
                list.Add(current);
            }
        }
    }
}
