namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Represents abstract node of <see cref="FileSystemTree"/>.
    /// </summary>
    internal abstract class FileSystemTreeNode : TreeNode
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="FileSystemTreeNode"/> class.
        /// </summary>
        protected FileSystemTreeNode(FileSystemTreeNodeType nodeType)
            : this(nodeType, string.Empty)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="FileSystemTreeNode"/> class with the specified text.
        /// </summary>
        /// <param name="text">The text for the node.</param>
        protected FileSystemTreeNode(FileSystemTreeNodeType nodeType, string text)
            : base(text)
        {
            NodeType = nodeType;
        }

        /// <summary>
        /// Gets the type of the node.
        /// </summary>
        public FileSystemTreeNodeType NodeType { get; }

        /// <summary>
        /// Gets the full name of the node.
        /// </summary>
        public virtual string FullName
        {
            get { return Text; }
        }
    }
}
