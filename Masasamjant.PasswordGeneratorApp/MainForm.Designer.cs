namespace Masasamjant.PasswordGeneratorApp
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

            presenter.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textPassword = new TextBox();
            buttonEdit = new LinkLabel();
            buttonProperties = new LinkLabel();
            buttonNext = new Button();
            SuspendLayout();
            // 
            // textPassword
            // 
            textPassword.Enabled = false;
            textPassword.Location = new Point(33, 29);
            textPassword.Name = "textPassword";
            textPassword.ReadOnly = true;
            textPassword.Size = new Size(445, 23);
            textPassword.TabIndex = 0;
            // 
            // buttonEdit
            // 
            buttonEdit.ActiveLinkColor = Color.Blue;
            buttonEdit.AutoSize = true;
            buttonEdit.DisabledLinkColor = Color.DarkGray;
            buttonEdit.Location = new Point(495, 32);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(27, 15);
            buttonEdit.TabIndex = 1;
            buttonEdit.TabStop = true;
            buttonEdit.Text = "Edit";
            buttonEdit.VisitedLinkColor = Color.Blue;
            buttonEdit.LinkClicked += OnEditLinkClicked;
            // 
            // buttonProperties
            // 
            buttonProperties.ActiveLinkColor = Color.Blue;
            buttonProperties.AutoSize = true;
            buttonProperties.DisabledLinkColor = Color.DarkGray;
            buttonProperties.Location = new Point(33, 90);
            buttonProperties.Name = "buttonProperties";
            buttonProperties.Size = new Size(60, 15);
            buttonProperties.TabIndex = 2;
            buttonProperties.TabStop = true;
            buttonProperties.Text = "Properties";
            buttonProperties.VisitedLinkColor = Color.Blue;
            buttonProperties.LinkClicked += OnPropertiesLinkClicked;
            // 
            // buttonNext
            // 
            buttonNext.Location = new Point(403, 86);
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new Size(75, 23);
            buttonNext.TabIndex = 3;
            buttonNext.Text = "&Next";
            buttonNext.UseVisualStyleBackColor = true;
            buttonNext.Click += OnNextClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 145);
            Controls.Add(buttonNext);
            Controls.Add(buttonProperties);
            Controls.Add(buttonEdit);
            Controls.Add(textPassword);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Password Generator";
            FormClosing += OnFormClosing;
            FormClosed += OnFormClosed;
            Load += OnLoad;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textPassword;
        private LinkLabel buttonEdit;
        private LinkLabel buttonProperties;
        private Button buttonNext;
    }
}
