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
        /// Property should have a public getter.
        /// </summary>
        PublicGetter = 1,

        /// <summary>
        /// Property should have a public setter.
        /// </summary>
        PublicSetter = 2,

        /// <summary>
        /// Property should have a non-public getter.
        /// </summary>
        NonPublicGetter = 4,

        /// <summary>
        /// Property should have a non-public setter.
        /// </summary>
        NonPublicSetter = 8
    }
}
