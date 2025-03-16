namespace Masasamjant.Configuration
{
    /// <summary>
    /// Represents configuration of <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
    /// </summary>
    public sealed class DateTimeConfiguration : IDateTimeConfiguration
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DateTimeConfiguration"/> class.
        /// </summary>
        /// <param name="kind">The <see cref="DateTimeKind"/> to use.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="kind"/> is not defined in <see cref="DateTimeKind"/>.</exception>
        public DateTimeConfiguration(DateTimeKind kind)
        {
            if (!Enum.IsDefined(kind))
                throw new ArgumentException("The value is not defined.", nameof(kind));

            Kind = kind;
        }

        /// <summary>
        /// Gets what <see cref="DateTimeKind"/> configuration is using.
        /// </summary>
        public DateTimeKind Kind { get; }

        /// <summary>
        /// Gets the current <see cref="DateTime"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTime.Now"/> if <see cref="Kind"/> is not <see cref="DateTimeKind.Utc"/>.
        /// -or-
        /// A <see cref="DateTime.UtcNow"/> otherwise.
        /// </returns>
        public DateTime GetDateTimeNow()
        {
            if (Kind == DateTimeKind.Utc)
                return DateTime.UtcNow;

            return DateTime.Now;
        }

        /// <summary>
        /// Gets the current <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTimeOffset.Now"/> if <see cref="Kind"/> is not <see cref="DateTimeKind.Utc"/>.
        /// -or-
        /// A <see cref="DateTimeOffset.UtcNow"/> otherwise.
        /// </returns>
        public DateTimeOffset GetDateTimeOffsetNow()
        {
            if (Kind == DateTimeKind.Utc)
                return DateTimeOffset.UtcNow;

            return DateTimeOffset.Now;
        }
    }
}
