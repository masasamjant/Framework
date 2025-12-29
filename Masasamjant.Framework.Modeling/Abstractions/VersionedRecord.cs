namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents <see cref="Record{TIdentifier}"/> that impelements <see cref="ISupportVersion"/> interface.
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public abstract class VersionedRecord<TIdentifier> : Record<TIdentifier>, ISupportVersion
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Gets the version bytes or empty array.
        /// </summary>
        public byte[] Version { get; protected set; } = [];

        /// <summary>
        /// Gets <see cref="Version"/> as string.
        /// </summary>
        /// <returns>A <see cref="Version"/> as string.</returns>
        public virtual string GetVersionString()
        {
            byte[] version = Version;

            if (version.Length == 0)
                return string.Empty;

            return Convert.ToBase64String(version).ToUpperInvariant();
        }
    }
}
