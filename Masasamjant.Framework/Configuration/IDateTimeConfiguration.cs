namespace Masasamjant.Configuration
{
    /// <summary>
    /// Represents configuration of <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
    /// </summary>
    public interface IDateTimeConfiguration
    {
        /// <summary>
        /// Gets what <see cref="DateTimeKind"/> configuration is using.
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// Gets the current <see cref="DateTime"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTime.Now"/> if <see cref="Kind"/> is not <see cref="DateTimeKind.Utc"/>.
        /// -or-
        /// A <see cref="DateTime.UtcNow"/> otherwise.
        /// </returns>
        DateTime GetDateTimeNow();

        /// <summary>
        /// Gets the current <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTimeOffset.Now"/> if <see cref="Kind"/> is not <see cref="DateTimeKind.Utc"/>.
        /// -or-
        /// A <see cref="DateTimeOffset.UtcNow"/> otherwise.
        /// </returns>
        DateTimeOffset GetDateTimeOffsetNow();
    }
}
