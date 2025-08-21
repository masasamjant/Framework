namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents unit of work.
    /// </summary>
    public interface IWork
    {
        /// <summary>
        /// Saves the work.
        /// </summary>
        /// <returns>A task.</returns>
        /// <exception cref="InvalidOperationException">If saving work fails.</exception>
        Task SaveAsync();
    }
}
