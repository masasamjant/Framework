namespace Masasamjant.Windows.Forms
{
    internal sealed class FileNode : FileSystemTreeNode
    {
        private static readonly Dictionary<string, string> imageKeyLookup;

        public FileNode(FileInfo file)
            : base(FileSystemTreeNodeType.File, file.Name)
        {
            File = file;
            SetImageKeys();
            ToolTipText = file.FullName;
        }

        static FileNode()
        {
            imageKeyLookup = new Dictionary<string, string>()
            {
                { "jpg;jpeg;gif;tif;tiff;png;bmp", ImageKeys.FileImage },
                { "mp3;wma", ImageKeys.FileMusic },
                { "doc;docx", ImageKeys.FileWord },
                { "xls;xlsx", ImageKeys.FileExcel },
                { "zip;rar", ImageKeys.FileArchive },
                { "ppt;pptx", ImageKeys.FilePowerpoint },
                { "ini;config;bat;cmd", ImageKeys.FileSettings },
                { "txt;xml;log", ImageKeys.FileText }
            };
        }

        public FileInfo File { get; }

        private void SetImageKeys()
        {
            var key = GetImageKey(File.Extension);
            ImageKey = key;
            SelectedImageKey = key;
        }

        private static string GetImageKey(string extension)
        {
            extension = extension.Replace(".", "").ToLowerInvariant();

            foreach (var key in imageKeyLookup.Keys)
            {
                var extensions = key.Split(';');
                if (extensions.Contains(extension))
                    return imageKeyLookup[key];
            }

            return ImageKeys.File;
        }
    }
}
