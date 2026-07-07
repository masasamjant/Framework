namespace Masasamjant.Windows.Forms
{
    partial class EnumSelectorControl
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
            groupControls = new GroupBox();
            SuspendLayout();
            // 
            // groupControls
            // 
            groupControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupControls.Dock = DockStyle.Fill;
            groupControls.Location = new Point(0, 0);
            groupControls.Name = "groupControls";
            groupControls.Size = new Size(150, 150);
            groupControls.TabIndex = 0;
            groupControls.TabStop = false;
            // 
            // EnumSelectorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupControls);
            Name = "EnumSelectorControl";
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupControls;
    }
}
