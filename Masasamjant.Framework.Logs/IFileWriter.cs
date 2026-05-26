namespace Masasamjant.Diagnostics
{
    
    /// <summary>
    /// Represents file writer.
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Appends lines to file specified by full file path.
        /// </summary>
        /// <param name="filePath">The full file path.</param>
        /// <param name="lines">The lines to append.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AppendAllLinesAsync(string filePath, IEnumerable<string> lines);
    }
}
