using System.Text;

namespace Masasamjant.Text
{
    /// <summary>
    /// Provides a cache for a single <see cref="StringBuilder"/> instance, allowing controlled clearing of its content
    /// and notification when the content is cleared.
    /// </summary>
    /// <remarks>Use <see cref="StringBuilderCache"/> to reuse a <see cref="StringBuilder"/> instance across
    /// multiple operations, reducing allocations. The <see cref="IsPreviousContentCleared"/> property determines
    /// whether the cached <see cref="StringBuilder"/> is cleared before each retrieval via <see cref="GetBuilder"/>. If
    /// clearing occurs, the <see cref="Clearing"/> event is raised prior to the content being cleared. This class is
    /// not thread-safe; external synchronization is required if accessed concurrently.</remarks>
    public sealed class StringBuilderCache : IStringBuilderCache
    {
        private StringBuilder? currentBuilder;

        /// <summary>
        /// Notifies when cached <see cref="StringBuilder"/> is cleared. 
        /// Only occurs if <see cref="IsPreviousContentCleared"/> is <c>true</c>.
        /// </summary>
        public event EventHandler<StringBuilderClearingEventArgs>? Clearing;

        /// <summary>
        /// Initializes new instance of the <see cref="StringBuilderCache"/> class without cached <see cref="StringBuilder"/>.
        /// </summary>
        public StringBuilderCache()
        {
            IsPreviousContentCleared = false;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="StringBuilderCache"/> class with cached <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> instance to cache.</param>
        public StringBuilderCache(StringBuilder builder)
            : this()
        {
            SetBuilder(builder);
        }

        /// <summary>
        /// Gets or sets whether or not previous content in cached <see cref="StringBuilder"/> is 
        /// cleared when <see cref="GetBuilder"/> is invoked. If <c>true</c> then <see cref="Clearing"/> event occur before
        /// cached <see cref="StringBuilder"/> is actually cleared. Default value is <c>false</c>.
        /// </summary>
        public bool IsPreviousContentCleared { get; set; }

        /// <summary>
        /// Gets the cached <see cref="StringBuilder"/> instance. If <see cref="IsPreviousContentCleared"/> is <c>true</c>,
        /// then previous content is cleared before returning the instance.
        /// </summary>
        /// <returns>A cached <see cref="StringBuilder"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If cached instance has not been set.</exception>
        public StringBuilder GetBuilder()
        {
            if (currentBuilder == null)
                throw new InvalidOperationException("The cached string builder not set.");

            if (IsPreviousContentCleared && currentBuilder.Length > 0)
            {
                OnClearing(new StringBuilderClearingEventArgs(currentBuilder));
                currentBuilder.Clear();
            }

            return currentBuilder;
        }

        /// <summary>
        /// Sets cached <see cref="StringBuilder"/> instance. Invoking this method overrides any previously set <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> instance to cache.</param>
        public void SetBuilder(StringBuilder builder)
        {
            currentBuilder = builder;
        }

        /// <summary>
        /// Removes all content from the current builder and releases the cached instance.
        /// </summary>
        /// <remarks>If the builder contains any content, the clearing operation triggers the clearing
        /// event before resetting. After calling this method, the cached builder is cleared and cached instance is set to null and cannot be used until
        /// reinitialized.</remarks>
        public void Clear()
        {
            if (currentBuilder == null)
                return;

            if (currentBuilder.Length > 0)
            {
                OnClearing(new StringBuilderClearingEventArgs(currentBuilder));
                currentBuilder.Clear();
            }

            currentBuilder = null;
        }

        private void OnClearing(StringBuilderClearingEventArgs e)
        {
            Clearing?.Invoke(this, e);
        }
    }
}
