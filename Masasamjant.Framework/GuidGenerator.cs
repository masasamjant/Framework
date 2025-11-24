using Masasamjant.ComponentModel;

namespace Masasamjant
{
    /// <summary>
    /// Represents a generator of new <see cref="Guid"/> values.
    /// </summary>
    public sealed class GuidGenerator : ValueGenerator<Guid>
    {
        /// <summary>
        /// Generates a new globally unique identifier (GUID).
        /// </summary>
        /// <returns>A <see cref="Guid"/> value representing a newly generated unique identifier.</returns>
        public override Guid GenerateValue()
        {
            return Guid.NewGuid();
        }
    }
}
