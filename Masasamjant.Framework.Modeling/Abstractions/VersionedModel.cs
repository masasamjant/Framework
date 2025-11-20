namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents <see cref="Model"/> that impelements <see cref="ISupportVersion"/> interface.
    /// </summary>
    public abstract class VersionedModel : Model, ISupportVersion
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

    /// <summary>
    /// Represents <see cref="Model{TIdentifier}"/> that implements <see cref="ISupportVersion"/> interface.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class VersionedModel<TIdentifier> : Model<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
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
