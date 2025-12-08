namespace Masasamjant.Applications
{
    /// <summary>
    /// Represents application item that has a name.
    /// </summary>
    public interface ISupportName
    {
        /// <summary>
        /// Gets the name or empty string.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Change <see cref="Name"/> to the value of <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The new name.</param>
        /// <exception cref="InvalidNameException">If value of <paramref name="name"/> is not valid name.</exception>
        /// <exception cref="NotSupportedException">If changing <see cref="Name"/> is not supported.</exception>
        void Rename(string name);
    }
}
