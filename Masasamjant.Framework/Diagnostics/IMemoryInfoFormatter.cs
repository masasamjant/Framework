namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents formatter that formats count of bytes into string presentation.
    /// </summary>
    public interface IMemoryInfoFormatter
    {
        /// <summary>
        /// Formats count of bytes into string presentation.
        /// </summary>
        /// <param name="bytes">A count of bytes.</param>
        /// <returns>A string format of <paramref name="bytes"/>.</returns>
        string FormatByteCount(long bytes);
    }
}
