using Masasamjant.Threading;
using System.Collections.Concurrent;
using System.Text;

namespace Masasamjant.Text
{
    /// <summary>
    /// Provides a thread-specific cache for <see cref="StringBuilder"/> instances, enabling efficient reuse and
    /// management of <see cref="StringBuilder"/> objects within each thread.
    /// </summary>
    /// <remarks>This class maintains a separate <see cref="StringBuilder"/> for each thread, allowing threads
    /// to retrieve and store their own cached instance. It supports clearing cached content and notifies subscribers
    /// when a <see cref="StringBuilder"/> is about to be cleared. Use this class to reduce allocations and improve
    /// performance in scenarios where frequent string manipulations occur on a per-thread basis. This class is
    /// thread-safe and intended for use in multi-threaded environments.</remarks>
    public sealed class ThreadStringBuilderCache : IStringBuilderCache
    {
        private readonly ConcurrentDictionary<int, StringBuilder> builders = new();
        private readonly IThreadProvider threadProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadStringBuilderCache"/> class.
        /// </summary>
        /// <param name="threadProvider">The thread provider to use for obtaining the current thread.</param>
        public ThreadStringBuilderCache(IThreadProvider threadProvider)
        {
            IsPreviousContentCleared = false;
            this.threadProvider = threadProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadStringBuilderCache"/> class.
        /// </summary>
        public ThreadStringBuilderCache() 
            : this(new ThreadProvider())
        { }

        /// <summary>
        /// Gets or sets whether or not previous content in cached <see cref="StringBuilder"/> is 
        /// cleared when <see cref="GetBuilder"/> is invoked. If <c>true</c> then <see cref="Clearing"/> event occur before
        /// cached <see cref="StringBuilder"/> is actually cleared. Default value is <c>false</c>.
        /// </summary>
        public bool IsPreviousContentCleared { get; set; }

        /// <summary>
        /// Notifies when cached <see cref="StringBuilder"/> is cleared. 
        /// Only occurs if <see cref="IsPreviousContentCleared"/> is <c>true</c>.
        /// </summary>
        public event EventHandler<StringBuilderClearingEventArgs>? Clearing;

        /// <summary>
        /// Removes all content from the builder associated with current thread and releases the cached instance.
        /// </summary>
        /// <remarks>If the builder contains any content, the clearing operation triggers the clearing
        /// event before resetting. After calling this method, the cached builder is cleared and cached instance for thread is set to null and cannot be used until
        /// reinitialized.</remarks>
        public void Clear()
        {
            if (builders.TryRemove(GetCurrentManagedThreadId(), out var builder) && builder.Length > 0)
            {
                OnClearing(new StringBuilderClearingEventArgs(builder));
                builder.Clear();
            }
        }

        /// <summary>
        /// Gets the cached <see cref="StringBuilder"/> instance. If <see cref="IsPreviousContentCleared"/> is <c>true</c>,
        /// then previous content is cleared before returning the instance.
        /// </summary>
        /// <returns>A cached <see cref="StringBuilder"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If cached instance has not been set.</exception>
        public StringBuilder GetBuilder()
        {
            if (builders.TryGetValue(GetCurrentManagedThreadId(), out var builder))
            {
                if (IsPreviousContentCleared && builder.Length > 0)
                {
                    OnClearing(new StringBuilderClearingEventArgs(builder));
                    builder.Clear();
                }
                return builder;
            }
            else
                throw new InvalidOperationException("The cached string builder not set for current thread.");
        }

        /// <summary>
        /// Sets cached <see cref="StringBuilder"/> instance. Invoking this method overrides any previously set <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> instance to cache.</param>
        public void SetBuilder(StringBuilder builder)
        {
            builders.AddOrUpdate(GetCurrentManagedThreadId(), builder, (k, v) => builder);
        }

        private void OnClearing(StringBuilderClearingEventArgs e)
        {
            Clearing?.Invoke(this, e);
        }

        private int GetCurrentManagedThreadId()
        {
            var thread = threadProvider.GetCurrentThread();
            return thread.ManagedThreadId;
        }
    }
}
