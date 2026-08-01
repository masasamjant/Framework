using Masasamjant.Windows.Resources;
using System.ComponentModel;
using System.Diagnostics;

namespace Masasamjant.Windows.Forms
{
    public partial class FileSystemTree : UserControl
    {
        private FileSystemTreeUnauthorizedAccess unauthorizedAccess = FileSystemTreeUnauthorizedAccess.Hide;
        private int driveReadTimeout = 5;
        private string extensions = "*.*";
        private bool trackDriveState = false;
        private readonly List<string> inactiveDrives = new List<string>();
        private bool initialTreeCreated = false;
        private System.Windows.Forms.Timer? timer;
        private readonly Dictionary<string, FileSystemTreeNode> selectedNodes = new Dictionary<string, FileSystemTreeNode>();
        private string rootDrive = string.Empty;

        /// <summary>
        /// Initializes new instance of the <see cref="FileSystemTree"/> class.
        /// </summary>
        public FileSystemTree()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Notifies when drive has been activated.
        /// </summary>
        [Category("Behavior")]
        [Description("Notifies when drive has been activated.")]
        public event EventHandler<DriveInfoEventArgs>? DriveActivated;

        /// <summary>
        /// Notifies when drive has been deactivated.
        /// </summary>
        [Category("Behavior")]
        [Description("Notifies when drive has been deactivated.")]
        public event EventHandler<DriveInfoEventArgs>? DriveUnactivated;

        /// <summary>
        /// Notifies when drive has been selected.
        /// </summary>
        [Category("Behavior")]
        [Description("Notifies when drive has been selected.")]
        public event EventHandler<DriveInfoEventArgs>? DriveSelected;

        /// <summary>
        /// Notifies when directory has been selected by clicking or expanding.
        /// </summary>
        [Category("Behavior")]
        [Description("Notifies when directory has been selected by clicking or expanding.")]
        public event EventHandler<DirectoryInfoEventArgs>? DirectorySelected;

        /// <summary>
        /// Notifies when file has been selected by clicking or expanding.
        /// </summary>
        [Category("Behavior")]
        [Description("Notifies when file has been selected by clicking or expanding.")]
        public event EventHandler<FileInfoEventArgs>? FileSelected;

        /// <summary>
        /// Gets or sets if or not directories and files with hidden attribute should be displayed.
        /// </summary>
        [DefaultValue(false)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines whether hidden file system items are shown.")]
        public bool ShowHidden { get; set; } = false;

        /// <summary>
        /// Gets or sets if or not directories and files with system attribute should be displayed.
        /// </summary>
        [DefaultValue(false)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines whether system file items are shown.")]
        public bool ShowSystem { get; set; } = false;

        /// <summary>
        /// Gets or sets if or not network drives should be displayed.
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines whether network drives are shown.")]
        public bool ShowNetworkDrives { get; set; } = true;

        /// <summary>
        /// Gets or sets if or not removable drives should be displayed.
        /// This includes disc drives, USB drives, and other removable media.
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines whether removable drives are shown.")]
        public bool ShowRemovableDrives { get; set; } = true;

        /// <summary>
        /// Gets or sets if or not RAM drives should be displayed.
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines whether RAM drives are shown.")]
        public bool ShowRamDrives { get; set; } = true;

        /// <summary>
        /// Gets or sets if or not inactive drives should be displayed.
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines whether inactive drives are shown.")]
        public bool ShowInactiveDrives { get; set; } = true;

        /// <summary>
        /// Gets or sets timeout, in seconds, for attempting to wait for a drive to become ready.
        /// </summary>
        [DefaultValue(5)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines the timeout for reading drives in seconds.")]
        public int DriveReadTimeout
        {
            get { return driveReadTimeout; }
            set
            {
                if (value < 1 || value > 60)
                    throw new ArgumentOutOfRangeException(nameof(DriveReadTimeout), value, "Drive read timeout must be between 1 and 60 seconds.");

                driveReadTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the file extensions filter for displaying files. 
        /// Use a semicolon to separate multiple extensions. For example: "*.txt;*.jpg;*.png". Use "*.*" to display all files.
        /// </summary>
        [DefaultValue("*.*")]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines the file extensions to be displayed. Use a semicolon-separated list of extensions, e.g., '*.txt;*.jpg'.")]
        public string FileExtensions
        {
            get { return extensions; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    extensions = "*.*";
                else
                    extensions = value;
            }
        }

        /// <summary>
        /// Gets or sets the behavior when unauthorized access occurs while reading directories or files.
        /// </summary>
        [DefaultValue(typeof(FileSystemTreeUnauthorizedAccess), "Hide")]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines the behavior when unauthorized access occurs.")]
        public FileSystemTreeUnauthorizedAccess UnauthorizedAccess
        {
            get { return unauthorizedAccess; }
            set
            {
                if (!Enum.IsDefined(value))
                    throw new ArgumentException("Value is not defined.", nameof(UnauthorizedAccess));

                unauthorizedAccess = value;
            }
        }

        /// <summary>
        /// Gets whether or not directories with unauthorized access are displayed in the tree.
        /// </summary>
        [Browsable(false)]
        public bool HideUnauthorizedDirectories
        {
            get { return UnauthorizedAccess == FileSystemTreeUnauthorizedAccess.Hide; }
        }

        /// <summary>
        /// Gets or sets whether or not images are shown in tree.
        /// </summary>
        [DefaultValue(false)]
        [Category("Appearance")]
        [Browsable(true)]
        [Description("Determines whether images are shown in tree.")]
        public bool ShowImages { get; set; } = false;

        /// <summary>
        /// Gets or sets if or not tree is tracking drive state.
        /// </summary>
        [DefaultValue(false)]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines whether drive state tracking is enabled.")]
        public bool IsDriveStateTracked
        {
            get { return trackDriveState; }
            set
            {
                trackDriveState = value;
                if (trackDriveState)
                    EnableDriveStateCheck();
                else
                    DisableDriveStateCheck();
            }
        }

        /// <summary>
        /// Gets or sets the root drive to display in the tree. 
        /// If empty, then My Computer is root.
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        [Browsable(true)]
        [Description("Determines the root drive for the file system tree. If empty, then My Computer is root.")]
        public string RootDrive
        {
            get { return rootDrive; }
            set
            {
                if (value != rootDrive)
                {
                    rootDrive = Path.GetPathRoot(value) ?? string.Empty;
                    if (rootDrive.EndsWith(':'))
                        rootDrive += @"\";
                    CreateInitialTree();
                }
            }
        }

        /// <summary>
        /// Gets selected files.
        /// </summary>
        /// <returns>An enumerable collection of selected files.</returns>
        public IEnumerable<FileInfo> GetSelectedFiles()
        {
            foreach (FileSystemTreeNode node in selectedNodes.Values)
            {
                if (node.NodeType == FileSystemTreeNodeType.File)
                {
                    FileNode fileNode = (FileNode)node;
                    yield return fileNode.File;
                }
            }
        }

        /// <summary>
        /// Gets selected folders.
        /// </summary>
        /// <returns>An enumerable collection of selected folders.</returns>
        public IEnumerable<DirectoryInfo> GetSelectedDirectories()
        {
            foreach (FileSystemTreeNode node in selectedNodes.Values)
            {
                if (node.NodeType == FileSystemTreeNodeType.Directory)
                {
                    DirectoryNode directoryNode = (DirectoryNode)node;
                    yield return directoryNode.Directory;
                }
            }
        }

        public void Expand(string fullPath)
        {
            var rootNode = (FileSystemTreeNode)treeViewFiles.Nodes[0];

            if (rootNode.NodeType != FileSystemTreeNodeType.Computer && rootNode.NodeType != FileSystemTreeNodeType.Drive)
                return;

            var directoryNames = new List<string>(fullPath.Split(Path.DirectorySeparatorChar));
            var driveName = GetDriveName(directoryNames);

            if (rootNode.NodeType == FileSystemTreeNodeType.Computer)
            {
                rootNode.Expand();
                    
                foreach (DriveNode driveNode in rootNode.Nodes)
                    ExpandDriveNode(driveNode, driveName, directoryNames);
            }
            else
            { 
                DriveNode driveNode = (DriveNode)rootNode;

                if (driveNode.Name.Equals(driveName, StringComparison.InvariantCultureIgnoreCase))
                {
                    driveNode.Expand();
                    ExpandDriveNode(driveNode, driveName, directoryNames);
                }
            }
        }

        public void Collapse(string fullPath)
        {
            var rootNode = (FileSystemTreeNode)treeViewFiles.Nodes[0];

            if (rootNode.NodeType != FileSystemTreeNodeType.Computer && rootNode.NodeType != FileSystemTreeNodeType.Drive)
                return;

            var directoryNames = new List<string>(fullPath.Split(Path.DirectorySeparatorChar));
            var driveName = GetDriveName(directoryNames);

            if (rootNode.NodeType == FileSystemTreeNodeType.Computer)
            {
                foreach (DriveNode driveNode in rootNode.Nodes)
                    CollapseDriveNode(driveNode, driveName, directoryNames);

                rootNode.Collapse();
            }
            else
            {
                DriveNode driveNode = (DriveNode)rootNode;
                
                if (driveNode.Name.Equals(driveName, StringComparison.InvariantCultureIgnoreCase))
                {
                    CollapseDriveNode(driveNode, driveName, directoryNames);
                    driveNode.Collapse();
                }
            }
        }

        private static string GetDriveName(List<string> directoryNames)
            => directoryNames[0] + @"\";

        private static void ExpandDriveNode(DriveNode driveNode, string driveName, List<string> directoryNames)
        {
            if (driveNode.Drive.Name.Equals(driveName, StringComparison.InvariantCultureIgnoreCase))
            {
                driveNode.Expand();
                ExpandDirectoryNodes(driveNode.DirectoryNodes, directoryNames, 1);
            }
        }

        private static void ExpandDirectoryNodes(IEnumerable<DirectoryNode> directoryNodes, List<string> directoryNames, int index)
        {
            if (!directoryNodes.Any() || index > directoryNames.Count - 1)
                return;

            foreach (var directoryNode in directoryNodes)
            {
                if (directoryNames[index].Equals(directoryNode.Directory.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    directoryNode.Expand();
                    ExpandDirectoryNodes(directoryNode.DirectoryNodes, directoryNames, index + 1);
                }
            }
        }

        private static void CollapseDriveNode(DriveNode driveNode, string driveName, List<string> directoryNames)
        {
            if (driveNode.Drive.Name.Equals(driveName, StringComparison.InvariantCultureIgnoreCase))
            {
                CollapseDirectoryNodes(driveNode.DirectoryNodes, directoryNames, 1);
                driveNode.Collapse();
            }
        }

        private static void CollapseDirectoryNodes(IEnumerable<DirectoryNode> directoryNodes, List<string> directoryNames, int index)
        {
            if (!directoryNodes.Any() || index > directoryNames.Count - 1)
                return;

            foreach (var directoryNode in directoryNodes)
            {
                if (directoryNames[index].Equals(directoryNode.Directory.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    CollapseDirectoryNodes(directoryNode.DirectoryNodes, directoryNames, index + 1);
                    directoryNode.Collapse();
                }
            }
        }

        private void OnFileSystemTreeLoad(object sender, EventArgs e)
        {
            Disposed += OnFileSystemTreeDisposed;

            InitializeIconsImageList();

            if (IsDriveStateTracked)
                EnableDriveStateCheck();
        }

        private void OnFileSystemTreeDisposed(object? sender, EventArgs e)
        {
            Disposed -= OnFileSystemTreeDisposed;

            if (IsDriveStateTracked)
                DisableDriveStateCheck();
        }

        private void OnFileSystemTreePaint(object sender, PaintEventArgs e)
        {
            if (Visible && !initialTreeCreated)
            {
                CreateInitialTree();
                initialTreeCreated = true;
            }
        }

        private void OnFileSystemTreeVisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                treeViewFiles.Nodes.Clear();
                initialTreeCreated = false;
            }
        }

        private void OnTreeViewFilesAfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node is FileSystemTreeNode node)
                OnNodeCollapsed(node);
        }

        private void OnTreeViewFilesBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node is FileSystemTreeNode node)
                OnNodeExpanding(node);
        }

        private void OnTreeViewFilesNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Node is FileSystemTreeNode node)
            {
                if (ModifierKeys == Keys.Control)
                {
                    selectedNodes.TryAdd(node.FullName, node);
                }
                else
                {
                    ClearSelectedNodes();
                    selectedNodes.Add(node.FullName, node);
                }

                HighlightSelectedNodes();

                OnNodeClicked(node);
            }
        }

        private static void OnNodeCollapsed(FileSystemTreeNode node)
        {
            foreach (FileSystemTreeNode child in node.Nodes)
            {
                if (child.NodeType != FileSystemTreeNodeType.Computer && child.NodeType != FileSystemTreeNodeType.Drive)
                {
                    if (child.Nodes.Count > 0)
                        child.Nodes.Clear();
                }
            }
        }

        private void OnNodeExpanding(FileSystemTreeNode node)
        {
            switch (node.NodeType)
            {
                case FileSystemTreeNodeType.Drive:
                    CreateDirectoryTree((DriveNode)node);
                    break;
                case FileSystemTreeNodeType.Directory:
                    CreateDirectoryTree((DirectoryNode)node);
                    CreateFileTree((DirectoryNode)node);
                    break;
            }

            OnNodeSelected(node);
        }

        private void OnNodeSelected(FileSystemTreeNode node)
        {
            switch (node.NodeType)
            {
                case FileSystemTreeNodeType.Drive:
                    DriveInfo drive = ((DriveNode)node).Drive;
                    OnDriveSelected(drive);
                    break;
                case FileSystemTreeNodeType.Directory:
                    DirectoryInfo directory = ((DirectoryNode)node).Directory;
                    OnDirectorySelected(directory);
                    break;
                case FileSystemTreeNodeType.File:
                    FileInfo file = ((FileNode)node).File;
                    OnFileSelected(file);
                    break;
            }
        }

        private void OnNodeClicked(FileSystemTreeNode node)
        {
            if (node.NodeType == FileSystemTreeNodeType.Drive)
            {
                DriveNode driveNode = (DriveNode)node;
                
                if (!driveNode.Drive.IsReady)
                {
                    if (IsReady(driveNode.Drive))
                        CreateInitialDriveTree(driveNode);
                }
            }
            OnNodeSelected(node);
        }

        private void ClearSelectedNodes()
        {
            foreach (var node in selectedNodes.Values)
            {
                node.BackColor = Color.Empty;
                node.ForeColor = Color.Empty;
            }

            selectedNodes.Clear();
        }

        private void HighlightSelectedNodes()
        {
            foreach (var node in selectedNodes.Values)
            {
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
            }
        }

        private void EnableDriveStateCheck()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = Convert.ToInt32(TimeSpan.FromSeconds(3).TotalMilliseconds);
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void DisableDriveStateCheck()
        {
            if (timer == null)
                return;

            timer.Stop();
            timer.Tick -= OnTimerTick;
            timer.Dispose();
            timer = null;
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            foreach (FileSystemTreeNode node in treeViewFiles.Nodes[0].Nodes)
            {
                if (node.NodeType != FileSystemTreeNodeType.Drive)
                    continue;

                var driveNode = (DriveNode)node;

                if (driveNode.Drive.IsReady && inactiveDrives.Contains(driveNode.Drive.Name))
                { 
                    inactiveDrives.Remove(driveNode.Drive.Name);
                    CreateInitialDriveTree(driveNode);
                    OnDriveActivated(driveNode.Drive);
                }
                else if (!driveNode.Drive.IsReady && !inactiveDrives.Contains(driveNode.Drive.Name))
                {
                    inactiveDrives.Add(driveNode.Drive.Name);
                    if (node.Nodes.Count > 0)
                        node.Nodes.Clear();
                    OnDriveUnactivated(driveNode.Drive);
                }
            }
        }

        private void CreateInitialDriveTree(DriveNode driveNode)
        {
            if (!driveNode.Drive.IsReady)
                return;

            var directories = GetDirectories(driveNode.Drive);
            CreateDirectoryTree(driveNode, directories);
        }

        private static List<DirectoryInfo> GetDirectories(DriveInfo drive)
        {
            var directories = new List<DirectoryInfo>();
            var directoryNames = Directory.GetDirectories(drive.Name);

            foreach (var directoryName in directoryNames)
            {
                var directory = new DirectoryInfo(directoryName);
                if (directory.Exists)
                    directories.Add(directory);
            }

            return directories;
        }

        private void CreateDirectoryTree(DriveNode node)
        {
            if (!IsReady(node.Drive))
                return;

            var remove = new List<DirectoryNode>();

            foreach (DirectoryNode directoryNode in node.DirectoryNodes)
            {
                try
                {
                    var directories = directoryNode.Directory.GetDirectories();
                    CreateDirectoryTree(directoryNode, directories);
                }
                catch (UnauthorizedAccessException)
                {
                    if (HideUnauthorizedDirectories)
                        remove.Add(directoryNode);
                    continue;
                }
            }

            if (remove.Count > 0)
            {
                foreach (var directoryNode in remove)
                    node.Nodes.Remove(directoryNode);
            }
        }

        private void CreateDirectoryTree(DirectoryNode directoryNode)
        {
            var remove = new List<DirectoryNode>();

            foreach (DirectoryNode childNode in directoryNode.DirectoryNodes)
            {
                try
                {
                    var directories = childNode.Directory.GetDirectories();
                    CreateDirectoryTree(childNode, directories);
                    CreateFileTree(childNode);
                }
                catch (UnauthorizedAccessException)
                {
                    if (HideUnauthorizedDirectories)
                        remove.Add(childNode);

                    continue;
                }
            }
        }

        private void CreateDirectoryTree(DirectoryTreeNode parentNode, IEnumerable<DirectoryInfo> directories)
        {
            foreach (var directory in directories)
            {
                if (directory.Attributes.HasFlag(FileAttributes.Hidden) && !ShowHidden)
                    continue;
                if (directory.Attributes.HasFlag(FileAttributes.System) && !ShowSystem)
                    continue;
                parentNode.Add(directory);
            }
        }

        private void CreateFileTree(DirectoryNode directoryNode)
        {
            if (!directoryNode.FileNodes.Any())
            {
                var files = GetFiles(directoryNode.Directory);
                CreateFileTree(directoryNode, files);
            }
        }

        private void CreateFileTree(DirectoryNode parentNode, IEnumerable<FileInfo> files)
        {
            foreach (var file in files)
            {
                if (file.Attributes.HasFlag(FileAttributes.Hidden) && !ShowHidden)
                    continue;
                if (file.Attributes.HasFlag(FileAttributes.System) && !ShowSystem)
                    continue;
                parentNode.Add(file);
            }
        }

        private IEnumerable<FileInfo> GetFiles(DirectoryInfo directory)
        {
            if (FileExtensions == "*.*")
                return directory.GetFiles();

            var files = new List<FileInfo>();
            var extensions = FileExtensions.Split(';');

            if (extensions.Length == 0)
                return files;

            foreach (var extension in extensions)
            {
                if (string.IsNullOrWhiteSpace(extension))
                    continue;

                var actualExtension =  CheckExtension(extension);
                var fileInfos = directory.GetFiles(actualExtension);
                if (fileInfos.Length > 0)
                    files.AddRange(fileInfos);
            }

            return files;
        }

        private static string CheckExtension(string extension)
        {
            var tmp = extension;

            if (!tmp.StartsWith("*."))
            {
                if (!tmp.StartsWith('.'))
                    tmp = "." + tmp;

                if (!tmp.StartsWith('*'))
                    tmp = "*" + tmp;
            }
            else if (!tmp.StartsWith('*'))
                tmp = "*" + tmp;

            return tmp;
        }

        private void OnDriveActivated(DriveInfo drive)
        {
            DriveActivated?.Invoke(this, new DriveInfoEventArgs(drive));
        }

        private void OnDriveUnactivated(DriveInfo drive)
        {
            DriveUnactivated?.Invoke(this, new DriveInfoEventArgs(drive));
        }

        private void OnDriveSelected(DriveInfo drive)
        {
            DriveSelected?.Invoke(this, new DriveInfoEventArgs(drive));
        }

        private void OnDirectorySelected(DirectoryInfo directory)
        {
            DirectorySelected?.Invoke(this, new DirectoryInfoEventArgs(directory));
        }

        private void OnFileSelected(FileInfo file)
        {
            FileSelected?.Invoke(this, new FileInfoEventArgs(file));
        }

        private void CreateInitialTree()
        {
            treeViewFiles.Nodes.Clear();

            var rootNode = ResolveRootNode();
            var rootType = rootNode.NodeType;

            if (rootType == FileSystemTreeNodeType.Computer)
            {
                ComputerNode computerNode = (ComputerNode)rootNode;
                var drives = DriveInfo.GetDrives();

                foreach (var drive in drives)
                {
                    if (drive.DriveType == DriveType.Unknown)
                        continue;

                    if (!ShowInactiveDrives && !drive.IsReady)
                        continue;

                    if (drive.DriveType == DriveType.Network && !ShowNetworkDrives)
                        continue;
                    if (drive.DriveType == DriveType.Ram && !ShowRamDrives)
                        continue;
                    if ((drive.DriveType == DriveType.CDRom || drive.DriveType == DriveType.Removable) && !ShowRemovableDrives)
                        continue;

                    var driveNode = computerNode.Add(drive);

                    if (ShowInactiveDrives && !drive.IsReady)
                        inactiveDrives.Add(drive.Name);

                    CreateInitialDriveTree(driveNode);
                }
            }
            else
            {
                DriveNode driveNode = (DriveNode)rootNode;
                if (ShowInactiveDrives && !driveNode.Drive.IsReady)
                    inactiveDrives.Add(driveNode.Drive.Name);
                CreateInitialDriveTree(driveNode);
            }

            treeViewFiles.Nodes.Add(rootNode);
        }

        private FileSystemTreeNode ResolveRootNode()
        {
            if (string.IsNullOrWhiteSpace(RootDrive))
                return new ComputerNode();

            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (drive.Name == RootDrive)
                    return new DriveNode(drive);
            }

            return new ComputerNode();
        }

        private bool IsReady(DriveInfo drive)
        {
            if (drive.IsReady)
                return true;

            var message = $"The drive '{drive.Name}' is not ready. Please ensure the drive is connected and accessible.";
            var caption = $"{drive.Name} not ready";

            if (MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    long timeout = Convert.ToInt64(TimeSpan.FromSeconds(DriveReadTimeout).TotalMilliseconds);
                    var stopwatch = Stopwatch.StartNew();
                    var sleepTime = Convert.ToInt32(timeout / 10);
                    Cursor.Current = Cursors.WaitCursor;

                    while (true)
                    {
                        var elapsed = stopwatch.ElapsedMilliseconds;

                        if (elapsed >= timeout)
                        {
                            stopwatch.Stop();
                            break;
                        }

                        if (drive.IsReady)
                        {
                            stopwatch.Stop();
                            return true;
                        }

                        Thread.Sleep(sleepTime);
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Arrow;
                }

                message = $"The drive '{drive.Name}' is still not ready after waiting for {DriveReadTimeout} seconds.";

                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void InitializeIconsImageList()
        {
            imageListIcons.ColorDepth = ColorDepth.Depth32Bit;
            imageListIcons.Images.Add(ImageKeys.Computer, BitmapIconResource.Computer);
            imageListIcons.Images.Add(ImageKeys.DiscDrive, BitmapIconResource.DiscDrive);
            imageListIcons.Images.Add(ImageKeys.DiscDriveEmpty, BitmapIconResource.DiscDriveEmpty);
            imageListIcons.Images.Add(ImageKeys.Drive, BitmapIconResource.Drive);
            imageListIcons.Images.Add(ImageKeys.File, BitmapIconResource.File);
            imageListIcons.Images.Add(ImageKeys.FileArchive, BitmapIconResource.FileArchive);
            imageListIcons.Images.Add(ImageKeys.FileExcel, BitmapIconResource.FileExcel);
            imageListIcons.Images.Add(ImageKeys.FileImage, BitmapIconResource.FileImage);
            imageListIcons.Images.Add(ImageKeys.FileMusic, BitmapIconResource.FileMusic);
            imageListIcons.Images.Add(ImageKeys.FilePowerpoint, BitmapIconResource.FilePowerpoint);
            imageListIcons.Images.Add(ImageKeys.FileSettings, BitmapIconResource.FileSettings);
            imageListIcons.Images.Add(ImageKeys.FileText, BitmapIconResource.FileText);
            imageListIcons.Images.Add(ImageKeys.FileWord, BitmapIconResource.FileWord);
            imageListIcons.Images.Add(ImageKeys.Folder, BitmapIconResource.Folder);
            imageListIcons.Images.Add(ImageKeys.NetworkDrive, BitmapIconResource.NetworkDrive);
            treeViewFiles.ImageList = imageListIcons;
            treeViewFiles.ImageKey = ImageKeys.Computer;
        }
    }
}
