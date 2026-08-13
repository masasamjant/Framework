namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Represents a drive node in the <see cref="FileSystemTree"/>.
    /// </summary>
    internal sealed class DriveNode : DirectoryTreeNode
    {
        public DriveNode(DriveInfo drive)
            : base(FileSystemTreeNodeType.Drive, drive.Name)
        {
            Drive = drive ?? throw new ArgumentNullException(nameof(drive));
            SetDriveImage();
        }

        public DriveInfo Drive { get; }

        private void SetDriveImage()
        {
            switch (Drive.DriveType)
            {
                case DriveType.Network:
                    ImageKey = ImageKeys.NetworkDrive;
                    SelectedImageKey = ImageKeys.NetworkDrive;
                    break;
                case DriveType.CDRom or DriveType.Removable:
                    ImageKey = ImageKeys.DiscDrive;
                    SelectedImageKey = ImageKeys.DiscDrive;
                    break;
                case DriveType.Fixed:
                    ImageKey = ImageKeys.Drive;
                    SelectedImageKey = ImageKeys.Drive;
                    break;
                default:
                    ImageKey = ImageKeys.DiscDriveEmpty;
                    SelectedImageKey = ImageKeys.DiscDriveEmpty;
                    break;
            }
        }
    }
}
