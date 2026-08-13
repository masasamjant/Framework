namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Represents a hook that can be set to monitor input events. 
    /// The Scope property indicates whether the hook is local to the current thread or global across the entire system.
    /// </summary>
    public interface IHook
    {
        /// <summary>
        /// Gets the scope of the hook, 
        /// indicating whether it is local to the current thread or global across the entire system.
        /// </summary>
        HookScope Scope { get; }
    }
}
