using System.Collections.Concurrent;

namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents component that performs work and caches the result for future use.
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public sealed class WorkCache<TInput, TOutput>
    {
        private readonly ConcurrentDictionary<object, TOutput> cache;
        private readonly Func<TInput, TOutput> worker;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkCache{TInput, TOutput}"/> class with the specified worker function.
        /// </summary>
        /// <param name="worker">The worker function to perform the work.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="worker"/> is <c>null</c>.</exception>
        public WorkCache(Func<TInput, TOutput> worker)
        {
            this.worker = worker ?? throw new ArgumentNullException(nameof(worker));
            this.cache = new ConcurrentDictionary<object, TOutput>();
        }

        /// <summary>
        /// Performs the work with the specified input and returns the cached result if available. 
        /// If the result is not cached, it will be computed using the worker function and stored in the cache for future use.
        /// </summary>
        /// <param name="input">The input for the work.</param>
        /// <param name="key">An optional key to identify the cached result. If <c>null</c>, the input itself will be used as the key.</param>
        /// <returns>The result of the work.</returns>
        public TOutput Perform(TInput input, object? key = null)
        {
            ArgumentNullException.ThrowIfNull(input);

            object itemKey = key != null ? key : input;

            if (cache.TryGetValue(itemKey, out TOutput? value))
            {
                return value;
            }
            else
            {
                value = worker(input);
                cache.AddOrUpdate(itemKey, value, (currentKey, currentValue) => value);
                return value;
            }
        }

        /// <summary>
        /// Clears the cache, removing all cached results.
        /// </summary>
        public void ClearCache()
        {
            cache.Clear();
        }
    }
}
