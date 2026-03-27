namespace Masasamjant.IO
{
    /// <summary>
    /// Defines units of file size.
    /// </summary>
    public enum FileSizeUnit : int
    {
        /// <summary>
        /// Bytes
        /// </summary>
        Bytes = 0,

        /// <summary>
        /// Kilobytes (1 KB = 1024 bytes)
        /// </summary>
        Kilobytes = 1,

        /// <summary>
        /// Megabytes (1 MB = 1024 KB)
        /// </summary>
        Megabytes = 2,

        /// <summary>
        /// Gigabytes (1 GB = 1024 MB)
        /// </summary>
        Gigabytes = 3
    }
}
