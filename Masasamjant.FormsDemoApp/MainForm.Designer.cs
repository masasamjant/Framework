namespace Masasamjant.FormsDemoApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            panelDemoControl = new Panel();
            groupBox2 = new GroupBox();
            radioEnumComboBox = new RadioButton();
            radioEnumSelector = new RadioButton();
            radioTimeControl = new RadioButton();
            radioFileSystemTreeDemo = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1395, 569);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(panelDemoControl);
            groupBox3.Location = new Point(280, 19);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1112, 547);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "File Tree Demo";
            // 
            // panelDemoControl
            // 
            panelDemoControl.Dock = DockStyle.Fill;
            panelDemoControl.Location = new Point(3, 19);
            panelDemoControl.Name = "panelDemoControl";
            panelDemoControl.Size = new Size(1106, 525);
            panelDemoControl.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioEnumComboBox);
            groupBox2.Controls.Add(radioEnumSelector);
            groupBox2.Controls.Add(radioTimeControl);
            groupBox2.Controls.Add(radioFileSystemTreeDemo);
            groupBox2.Dock = DockStyle.Left;
            groupBox2.Location = new Point(3, 19);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(261, 547);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Demos";
            // 
            // radioEnumComboBox
            // 
            radioEnumComboBox.AutoSize = true;
            radioEnumComboBox.Location = new Point(40, 176);
            radioEnumComboBox.Name = "radioEnumComboBox";
            radioEnumComboBox.Size = new Size(153, 19);
            radioEnumComboBox.TabIndex = 3;
            radioEnumComboBox.TabStop = true;
            radioEnumComboBox.Text = "Enum ComboBox Demo";
            radioEnumComboBox.UseVisualStyleBackColor = true;
            // 
            // radioEnumSelector
            // 
            radioEnumSelector.AutoSize = true;
            radioEnumSelector.Location = new Point(40, 135);
            radioEnumSelector.Name = "radioEnumSelector";
            radioEnumSelector.Size = new Size(136, 19);
            radioEnumSelector.TabIndex = 2;
            radioEnumSelector.TabStop = true;
            radioEnumSelector.Text = "Enum Selector Demo";
            radioEnumSelector.UseVisualStyleBackColor = true;
            // 
            // radioTimeControl
            // 
            radioTimeControl.AutoSize = true;
            radioTimeControl.Location = new Point(40, 96);
            radioTimeControl.Name = "radioTimeControl";
            radioTimeControl.Size = new Size(130, 19);
            radioTimeControl.TabIndex = 1;
            radioTimeControl.TabStop = true;
            radioTimeControl.Text = "Time Control Demo";
            radioTimeControl.UseVisualStyleBackColor = true;
            // 
            // radioFileSystemTreeDemo
            // 
            radioFileSystemTreeDemo.AutoSize = true;
            radioFileSystemTreeDemo.Checked = true;
            radioFileSystemTreeDemo.Location = new Point(40, 55);
            radioFileSystemTreeDemo.Name = "radioFileSystemTreeDemo";
            radioFileSystemTreeDemo.Size = new Size(103, 19);
            radioFileSystemTreeDemo.TabIndex = 0;
            radioFileSystemTreeDemo.TabStop = true;
            radioFileSystemTreeDemo.Text = "File Tree Demo";
            radioFileSystemTreeDemo.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1395, 569);
            Controls.Add(groupBox1);
            Name = "MainForm";
            Text = "Demo App";
            Load += OnMainFormLoad;
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton radioEnumComboBox;
        private RadioButton radioEnumSelector;
        private RadioButton radioTimeControl;
        private RadioButton radioFileSystemTreeDemo;
        private GroupBox groupBox3;
        private Panel panelDemoControl;
    }
}
