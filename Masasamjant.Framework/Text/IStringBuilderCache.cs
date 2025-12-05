using System.Text;

namespace Masasamjant.Text
{
    /// <summary>
    /// Defines a contract for managing a cached <see cref="StringBuilder"/> instance, providing mechanisms to retrieve,
    /// set, and clear the cached builder, as well as to observe clearing events.
    /// </summary>
    /// <remarks>Implementations of this interface enable efficient reuse of <see cref="StringBuilder"/>
    /// instances to reduce memory allocations in scenarios with frequent string manipulations. The <see
    /// cref="Clearing"/> event allows subscribers to respond when the cached builder is about to be cleared. Thread
    /// safety and cache lifetime are determined by the specific implementation.</remarks>
    public interface IStringBuilderCache
    {
        /// <summary>
        /// Notifies when cached <see cref="StringBuilder"/> is cleared. 
        /// Only occurs if <see cref="IsPreviousContentCleared"/> is <c>true</c>.
        /// </summary>
        event EventHandler<StringBuilderClearingEventArgs>? Clearing;

        /// <summary>
        /// Gets or sets whether or not previous content in cached <see cref="StringBuilder"/> is 
        /// cleared when <see cref="GetBuilder"/> is invoked. If <c>true</c> then <see cref="Clearing"/> event occur before
        /// cached <see cref="StringBuilder"/> is actually cleared. Default value is <c>false</c>.
        /// </summary>
        bool IsPreviousContentCleared { get; set; }

        /// <summary>
        /// Gets the cached <see cref="StringBuilder"/> instance. If <see cref="IsPreviousContentCleared"/> is <c>true</c>,
        /// then previous content is cleared before returning the instance.
        /// </summary>
        /// <returns>A cached <see cref="StringBuilder"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If cached instance has not been set.</exception>
        StringBuilder GetBuilder();

        /// <summary>
        /// Sets cached <see cref="StringBuilder"/> instance. Invoking this method overrides any previously set <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> instance to cache.</param>
        void SetBuilder(StringBuilder builder);

        /// <summary>
        /// Removes all content from the current builder and resets its state.
        /// </summary>
        /// <remarks>If the builder contains any content, the clearing operation triggers the clearing
        /// event before resetting. After calling this method, the cached builder instance is set to null and cannot be used until
        /// reinitialized.</remarks>
        void Clear();
    }
}
