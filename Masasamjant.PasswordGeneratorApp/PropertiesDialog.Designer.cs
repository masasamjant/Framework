namespace Masasamjant.PasswordGeneratorApp
{
    partial class PropertiesDialog
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

            presenter.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelCharacters = new Label();
            textCharacters = new TextBox();
            labelMinLength = new Label();
            numMinLength = new NumericUpDown();
            labelMaxLength = new Label();
            numMaxLength = new NumericUpDown();
            groupComplexity = new GroupBox();
            panelSpecialCount = new Panel();
            numSpecialCount = new NumericUpDown();
            labelSpecialCount = new Label();
            checkSpecials = new CheckBox();
            checkNumbers = new CheckBox();
            checkUpperCaseLetters = new CheckBox();
            checkLowerCaseLetters = new CheckBox();
            buttonCancel = new Button();
            buttonSave = new Button();
            ((System.ComponentModel.ISupportInitialize)numMinLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxLength).BeginInit();
            groupComplexity.SuspendLayout();
            panelSpecialCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSpecialCount).BeginInit();
            SuspendLayout();
            // 
            // labelCharacters
            // 
            labelCharacters.AutoSize = true;
            labelCharacters.Location = new Point(28, 34);
            labelCharacters.Name = "labelCharacters";
            labelCharacters.Size = new Size(66, 15);
            labelCharacters.TabIndex = 0;
            labelCharacters.Text = "Characters:";
            // 
            // textCharacters
            // 
            textCharacters.Location = new Point(28, 64);
            textCharacters.Multiline = true;
            textCharacters.Name = "textCharacters";
            textCharacters.ReadOnly = true;
            textCharacters.Size = new Size(352, 93);
            textCharacters.TabIndex = 1;
            // 
            // labelMinLength
            // 
            labelMinLength.AutoSize = true;
            labelMinLength.Location = new Point(28, 178);
            labelMinLength.Name = "labelMinLength";
            labelMinLength.Size = new Size(68, 15);
            labelMinLength.TabIndex = 2;
            labelMinLength.Text = "Min length:";
            // 
            // numMinLength
            // 
            numMinLength.Location = new Point(114, 176);
            numMinLength.Name = "numMinLength";
            numMinLength.Size = new Size(266, 23);
            numMinLength.TabIndex = 3;
            numMinLength.ValueChanged += OnMinLengthValueChanged;
            // 
            // labelMaxLength
            // 
            labelMaxLength.AutoSize = true;
            labelMaxLength.Location = new Point(28, 212);
            labelMaxLength.Name = "labelMaxLength";
            labelMaxLength.Size = new Size(69, 15);
            labelMaxLength.TabIndex = 4;
            labelMaxLength.Text = "Max length:";
            // 
            // numMaxLength
            // 
            numMaxLength.Location = new Point(114, 210);
            numMaxLength.Name = "numMaxLength";
            numMaxLength.Size = new Size(266, 23);
            numMaxLength.TabIndex = 5;
            numMaxLength.ValueChanged += OnMaxLengthValueChanged;
            // 
            // groupComplexity
            // 
            groupComplexity.Controls.Add(panelSpecialCount);
            groupComplexity.Controls.Add(checkSpecials);
            groupComplexity.Controls.Add(checkNumbers);
            groupComplexity.Controls.Add(checkUpperCaseLetters);
            groupComplexity.Controls.Add(checkLowerCaseLetters);
            groupComplexity.Location = new Point(28, 248);
            groupComplexity.Name = "groupComplexity";
            groupComplexity.Size = new Size(352, 156);
            groupComplexity.TabIndex = 6;
            groupComplexity.TabStop = false;
            groupComplexity.Text = "Complexity";
            // 
            // panelSpecialCount
            // 
            panelSpecialCount.Controls.Add(numSpecialCount);
            panelSpecialCount.Controls.Add(labelSpecialCount);
            panelSpecialCount.Location = new Point(187, 59);
            panelSpecialCount.Name = "panelSpecialCount";
            panelSpecialCount.Size = new Size(149, 73);
            panelSpecialCount.TabIndex = 4;
            // 
            // numSpecialCount
            // 
            numSpecialCount.Location = new Point(16, 30);
            numSpecialCount.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numSpecialCount.Name = "numSpecialCount";
            numSpecialCount.Size = new Size(120, 23);
            numSpecialCount.TabIndex = 1;
            numSpecialCount.ValueChanged += OnSpecialCountValueChanged;
            // 
            // labelSpecialCount
            // 
            labelSpecialCount.AutoSize = true;
            labelSpecialCount.Location = new Point(16, 12);
            labelSpecialCount.Name = "labelSpecialCount";
            labelSpecialCount.Size = new Size(40, 15);
            labelSpecialCount.TabIndex = 0;
            labelSpecialCount.Text = "Count";
            // 
            // checkSpecials
            // 
            checkSpecials.AutoSize = true;
            checkSpecials.Location = new Point(187, 34);
            checkSpecials.Name = "checkSpecials";
            checkSpecials.Size = new Size(120, 19);
            checkSpecials.TabIndex = 3;
            checkSpecials.Text = "Special characters";
            checkSpecials.UseVisualStyleBackColor = true;
            checkSpecials.CheckedChanged += OnSpecialsCheckedChanged;
            // 
            // checkNumbers
            // 
            checkNumbers.AutoSize = true;
            checkNumbers.Location = new Point(21, 105);
            checkNumbers.Name = "checkNumbers";
            checkNumbers.Size = new Size(75, 19);
            checkNumbers.TabIndex = 2;
            checkNumbers.Text = "Numbers";
            checkNumbers.UseVisualStyleBackColor = true;
            checkNumbers.CheckedChanged += OnNumbersCheckedChanged;
            // 
            // checkUpperCaseLetters
            // 
            checkUpperCaseLetters.AutoSize = true;
            checkUpperCaseLetters.Location = new Point(21, 70);
            checkUpperCaseLetters.Name = "checkUpperCaseLetters";
            checkUpperCaseLetters.Size = new Size(119, 19);
            checkUpperCaseLetters.TabIndex = 1;
            checkUpperCaseLetters.Text = "Upper case letters";
            checkUpperCaseLetters.UseVisualStyleBackColor = true;
            checkUpperCaseLetters.CheckedChanged += OnUpperCaseLettersCheckedChanged;
            // 
            // checkLowerCaseLetters
            // 
            checkLowerCaseLetters.AutoSize = true;
            checkLowerCaseLetters.Location = new Point(21, 34);
            checkLowerCaseLetters.Name = "checkLowerCaseLetters";
            checkLowerCaseLetters.Size = new Size(119, 19);
            checkLowerCaseLetters.TabIndex = 0;
            checkLowerCaseLetters.Text = "Lower case letters";
            checkLowerCaseLetters.UseVisualStyleBackColor = true;
            checkLowerCaseLetters.CheckedChanged += OnLowerCaseLettersCheckedChanged;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(305, 415);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "&Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += OnCancelClick;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(215, 415);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 8;
            buttonSave.Text = "&Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += OnSaveClick;
            // 
            // PropertiesDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(408, 450);
            Controls.Add(buttonSave);
            Controls.Add(buttonCancel);
            Controls.Add(groupComplexity);
            Controls.Add(numMaxLength);
            Controls.Add(labelMaxLength);
            Controls.Add(numMinLength);
            Controls.Add(labelMinLength);
            Controls.Add(textCharacters);
            Controls.Add(labelCharacters);
            Name = "PropertiesDialog";
            Text = "Properties";
            FormClosing += OnFormClosing;
            FormClosed += OnFormClosed;
            Load += OnLoad;
            ((System.ComponentModel.ISupportInitialize)numMinLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxLength).EndInit();
            groupComplexity.ResumeLayout(false);
            groupComplexity.PerformLayout();
            panelSpecialCount.ResumeLayout(false);
            panelSpecialCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSpecialCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelCharacters;
        private TextBox textCharacters;
        private Label labelMinLength;
        private NumericUpDown numMinLength;
        private Label labelMaxLength;
        private NumericUpDown numMaxLength;
        private GroupBox groupComplexity;
        private Button buttonCancel;
        private Button buttonSave;
        private CheckBox checkLowerCaseLetters;
        private Panel panelSpecialCount;
        private NumericUpDown numSpecialCount;
        private Label labelSpecialCount;
        private CheckBox checkSpecials;
        private CheckBox checkNumbers;
        private CheckBox checkUpperCaseLetters;
    }
}