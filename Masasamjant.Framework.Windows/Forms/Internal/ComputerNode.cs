namespace Masasamjant.Windows.Forms
{
    internal sealed class ComputerNode : FileSystemTreeNode
    {
        public ComputerNode()
            : this("My Computer")
        { }

        public ComputerNode(string text)
            : base(FileSystemTreeNodeType.Computer, text)
        {
            ImageKey = ImageKeys.Computer;
            SelectedImageKey = ImageKeys.Computer;
        }

        public DriveNode Add(DriveInfo drive)
        {
            var node = new DriveNode(drive);
            Nodes.Add(node);
            return node;
        }
    }
}
