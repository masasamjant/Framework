namespace Masasamjant.Reflection
{
    /// <summary>
    /// Defines what operations property should support.
    /// </summary>
    [Flags]
    public enum PropertySupport : int
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Property can be public.
        /// </summary>
        Public = 1,

        /// <summary>
        /// Property can be non-public.
        /// </summary>
        NonPublic = 2,

        /// <summary>
        /// Property must have getter.
        /// </summary>
        Getter = 4,

        /// <summary>
        /// Property must have setter.
        /// </summary>
        Setter = 8
    }
}
