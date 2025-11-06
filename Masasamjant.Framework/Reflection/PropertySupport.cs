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
        Setter = 8,

        /// <summary>
        /// Property should have a public getter.
        /// </summary>
        [Obsolete("Use Public | Getter combination instead. Will be removed in version 1.8.0.")]
        PublicGetter = 16,

        /// <summary>
        /// Property should have a public setter.
        /// </summary>
        [Obsolete("Use Public | Setter combination instead. Will be removed in version 1.8.0.")]
        PublicSetter = 32,

        /// <summary>
        /// Property should have a non-public getter.
        /// </summary>
        [Obsolete("Use NonPublic | Getter combination instead. Will be removed in version 1.8.0.")]
        NonPublicGetter = 64,

        /// <summary>
        /// Property should have a non-public setter.
        /// </summary>
        [Obsolete("Use NonPublic | Getter combination instead. Will be removed in version 1.8.0.")]
        NonPublicSetter = 128
    }
}
