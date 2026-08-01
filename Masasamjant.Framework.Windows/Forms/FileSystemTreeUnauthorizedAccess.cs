namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Defines how the <see cref="FileSystemTree"/> control should handle unauthorized access exceptions when enumerating the file system.
    /// </summary>
    public enum FileSystemTreeUnauthorizedAccess : int
    {
        /// <summary>
        /// Hide nodes that cause unauthorized access exceptions.
        /// </summary>
        Hide = 0,

        /// <summary>
        /// Show nodes that cause unauthorized access exceptions.
        /// </summary>
        Show = 1
    }
}
