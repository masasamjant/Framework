namespace Masasamjant.FormsDemoApp.Controls
{
    partial class FileTreeDemo
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
            groupBoxProperties = new GroupBox();
            fileSystemTree = new Masasamjant.Windows.Forms.FileSystemTree();
            SuspendLayout();
            // 
            // groupBoxProperties
            // 
            groupBoxProperties.Location = new Point(30, 31);
            groupBoxProperties.Name = "groupBoxProperties";
            groupBoxProperties.Size = new Size(761, 100);
            groupBoxProperties.TabIndex = 0;
            groupBoxProperties.TabStop = false;
            groupBoxProperties.Text = "Properties";
            // 
            // fileSystemTree
            // 
            fileSystemTree.Location = new Point(30, 148);
            fileSystemTree.Name = "fileSystemTree";
            fileSystemTree.Size = new Size(315, 377);
            fileSystemTree.TabIndex = 1;
            // 
            // FileTreeDemo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(fileSystemTree);
            Controls.Add(groupBoxProperties);
            Name = "FileTreeDemo";
            Size = new Size(838, 673);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxProperties;
        private Windows.Forms.FileSystemTree fileSystemTree;
    }
}
