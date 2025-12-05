namespace Masasamjant.Applications
{
    /// <summary>
    /// Represents activation key generator.
    /// </summary>
    public interface IActivationKeyGenerator
    {
        /// <summary>
        /// Gets the activation key properties.
        /// </summary>
        IActivationKeyProperties KeyProperties { get; }

        /// <summary>
        /// Creates an activation key without prefix.
        /// </summary>
        /// <param name="previousSeed">The seed of the previous activation key that was created. Can be <c>null</c>, if first activation key is generated.</param>
        /// <returns>A activation key.</returns>
        ActivationKey CreateActivationKey(ActivationKeySeed? previousSeed);

        /// <summary>
        /// Creates an activation key with specified prefix.
        /// </summary>
        /// <param name="prefix">The key prefix.</param>
        /// <param name="previousSeed">The seed of the previous activation key that was created. Can be <c>null</c>, if first activation key is generated.</param>
        /// <returns>A activation key.</returns>
        ActivationKey CreateActivationKey(string prefix, ActivationKeySeed? previousSeed);
    }
}
