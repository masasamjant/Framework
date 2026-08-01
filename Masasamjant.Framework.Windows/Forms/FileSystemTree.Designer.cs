namespace Masasamjant.Windows.Forms
{
    partial class FileSystemTree
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            imageListIcons = new ImageList(components);
            treeViewFiles = new TreeView();
            SuspendLayout();
            // 
            // imageListIcons
            // 
            imageListIcons.ColorDepth = ColorDepth.Depth32Bit;
            imageListIcons.ImageSize = new Size(16, 16);
            imageListIcons.TransparentColor = Color.Transparent;
            // 
            // treeViewFiles
            // 
            treeViewFiles.Dock = DockStyle.Fill;
            treeViewFiles.Location = new Point(0, 0);
            treeViewFiles.Name = "treeViewFiles";
            treeViewFiles.ShowNodeToolTips = true;
            treeViewFiles.Size = new Size(150, 266);
            treeViewFiles.TabIndex = 0;
            treeViewFiles.AfterCollapse += OnTreeViewFilesAfterCollapse;
            treeViewFiles.BeforeExpand += OnTreeViewFilesBeforeExpand;
            treeViewFiles.NodeMouseClick += OnTreeViewFilesNodeMouseClick;
            // 
            // FileSystemTree
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(treeViewFiles);
            Name = "FileSystemTree";
            Size = new Size(150, 266);
            Load += OnFileSystemTreeLoad;
            VisibleChanged += OnFileSystemTreeVisibleChanged;
            Paint += OnFileSystemTreePaint;
            ResumeLayout(false);
        }

        #endregion

        private ImageList imageListIcons;
        private TreeView treeViewFiles;
    }
}
