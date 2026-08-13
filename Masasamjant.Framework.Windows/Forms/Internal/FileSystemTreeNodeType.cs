namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Defines the type of a <see cref="FileSystemTreeNode"/>.
    /// </summary>
    internal enum FileSystemTreeNodeType : int
    {
        /// <summary>
        /// Note representing a computer.
        /// </summary>
        Computer = 0,

        /// <summary>
        /// Node associated with a drive.
        /// </summary>
        Drive = 1,

        /// <summary>
        /// Node associated with a directory.
        /// </summary>
        Directory = 2,

        /// <summary>
        /// Node associated with a file.
        /// </summary>
        File = 3
    }
}
