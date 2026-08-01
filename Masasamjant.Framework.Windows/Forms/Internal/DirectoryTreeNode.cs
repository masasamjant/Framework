namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Represents base class for any <see cref="FileSystemTreeNode"/> that can contain directory nodes.
    /// </summary>
    internal abstract class DirectoryTreeNode : FileSystemTreeNode
    {
        public DirectoryTreeNode(FileSystemTreeNodeType nodeType)
            : this(nodeType, string.Empty)
        { }

        public DirectoryTreeNode(FileSystemTreeNodeType nodeType, string text)
            : base(nodeType, text)
        { }

        public IEnumerable<DirectoryNode> DirectoryNodes
        {
            get { return Nodes.OfType<DirectoryNode>(); }
        }

        public DirectoryNode Add(DirectoryInfo directory)
        {
            var node = new DirectoryNode(directory);
            Nodes.Add(node);
            return node;
        }
    }
}
