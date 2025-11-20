namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that supports version.
    /// </summary>
    public interface ISupportVersion
    {
        /// <summary>
        /// Gets the version bytes or empty array.
        /// </summary>
        byte[] Version { get; }

        /// <summary>
        /// Gets <see cref="Version"/> as string.
        /// </summary>
        /// <returns>A <see cref="Version"/> as string.</returns>
        string GetVersionString();
    }
}
