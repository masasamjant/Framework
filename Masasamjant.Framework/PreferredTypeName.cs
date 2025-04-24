namespace Masasamjant
{
    /// <summary>
    /// Defines what name of type is preferred to use.
    /// </summary>
    public enum PreferredTypeName : int
    {
        /// <summary>
        /// Use assembly qualified name, if available.
        /// </summary>
        AssemblyQualifiedName = 0,

        /// <summary>
        /// Use full name, if available.
        /// </summary>
        FullName = 1,

        /// <summary>
        /// Use name, if available.
        /// </summary>
        Name = 2
    }
}
