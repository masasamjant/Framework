namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents default formatter that formats count of bytes into string presentation.
    /// </summary>
    public sealed class DefaultMemoryInfoFormatter : IMemoryInfoFormatter
    {
        /// <summary>
        /// Formats count of bytes into string presentation.
        /// </summary>
        /// <param name="bytes">A count of bytes.</param>
        /// <returns>A string format of <paramref name="bytes"/>.</returns>
        public string FormatByteCount(long bytes)
        {
            if (bytes <= 0)
                return "0 B";

            const double div = 1024.0;

            if (bytes < 1000)
                return bytes.ToString() + " B";
            else if (bytes < 1000000)
                return Math.Round(bytes / div, 2).ToString() + " KB";
            else
                return Math.Round(bytes / div / div, 2).ToString() + " MB";
        }
    }
}
