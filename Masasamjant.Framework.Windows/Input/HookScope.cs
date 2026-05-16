namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Defines the scope of the hook, whether it is local to the current thread or global across the entire system.
    /// </summary>
    public enum HookScope : int
    {
        /// <summary>
        /// Local hook associated with the current thread. 
        /// It will only receive events from the thread that created the hook.
        /// </summary>
        Local = 0,

        /// <summary>
        /// Global hook associated with the entire system.
        /// It will receive events from all threads.
        /// </summary>
        Global = 1
    }
}
