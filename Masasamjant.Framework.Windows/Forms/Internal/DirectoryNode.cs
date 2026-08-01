namespace Masasamjant.Windows.Forms
{
    internal sealed class DirectoryNode : DirectoryTreeNode
    {
        public DirectoryNode(DirectoryInfo directory)
            : base(FileSystemTreeNodeType.Directory, directory.Name)
        {
            Directory = directory;
            ToolTipText = directory.FullName;
            ImageKey = ImageKeys.Folder;
            SelectedImageKey = ImageKeys.Folder;
        }

        public DirectoryInfo Directory { get; }

        public IEnumerable<FileNode> FileNodes
        {
            get { return Nodes.OfType<FileNode>(); }
        }

        public FileNode Add(FileInfo file)
        {
            var node = new FileNode(file);
            Nodes.Add(node);
            return node;
        }
    }
}
