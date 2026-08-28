namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents a model that supports validity period.
    /// </summary>
    public interface ISupportValidityPeriod
    {
        /// <summary>
        /// Gets the start date and time of validity period. 
        /// If not set, then is valid until <see cref="ValidTo"/>.
        /// </summary>
        /// <remarks>
        /// This is inclusive so that if <see cref="ValidFrom"/> is set to 2023-01-01 12:23:22.378, 
        /// then the model is valid at 2023-01-01 12:23:22.378 and onwards.
        /// </remarks>
        DateTimeOffset? ValidFrom { get; }

        /// <summary>
        /// Gets the end date and time of validity period.
        /// If not set, then is valid since <see cref="ValidFrom"/>.
        /// </summary>
        /// <remarks>
        /// This is inclusive so that if <see cref="ValidTo"/> is set to 2023-01-01 12:23:22.378, 
        /// then the model is valid at 2023-01-01 12:23:22.378, but not afterwards.
        /// </remarks>
        DateTimeOffset? ValidTo { get; }
    }
}
