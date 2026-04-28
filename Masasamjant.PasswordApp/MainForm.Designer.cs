namespace Masasamjant.PasswordApp
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
            mainFormTabs = new TabControl();
            generateTab = new TabPage();
            validateTab = new TabPage();
            propertiesTab = new TabPage();
            buttonSaveProperties = new Button();
            buttonCancelProperties = new Button();
            groupComplexity = new GroupBox();
            panelSpecialCount = new Panel();
            numericSpecialCount = new NumericUpDown();
            labelSpecialCount = new Label();
            checkSpecials = new CheckBox();
            checkNumbers = new CheckBox();
            checkUpperCaseLetters = new CheckBox();
            checkLowerCaseLetters = new CheckBox();
            numericMaximumLength = new NumericUpDown();
            labelMaximumLength = new Label();
            numericMinimumLength = new NumericUpDown();
            labelMinimumLength = new Label();
            textBoxChacacters = new TextBox();
            labelCharacters = new Label();
            mainFormTabs.SuspendLayout();
            propertiesTab.SuspendLayout();
            groupComplexity.SuspendLayout();
            panelSpecialCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericSpecialCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMaximumLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMinimumLength).BeginInit();
            SuspendLayout();
            // 
            // mainFormTabs
            // 
            mainFormTabs.Controls.Add(generateTab);
            mainFormTabs.Controls.Add(validateTab);
            mainFormTabs.Controls.Add(propertiesTab);
            mainFormTabs.Dock = DockStyle.Fill;
            mainFormTabs.Location = new Point(0, 0);
            mainFormTabs.Name = "mainFormTabs";
            mainFormTabs.SelectedIndex = 0;
            mainFormTabs.Size = new Size(618, 417);
            mainFormTabs.TabIndex = 0;
            mainFormTabs.Selected += OnMainFormTabsSelected;
            // 
            // generateTab
            // 
            generateTab.BorderStyle = BorderStyle.FixedSingle;
            generateTab.Location = new Point(4, 24);
            generateTab.Name = "generateTab";
            generateTab.Padding = new Padding(3);
            generateTab.Size = new Size(610, 422);
            generateTab.TabIndex = 0;
            generateTab.Text = "Generate";
            generateTab.UseVisualStyleBackColor = true;
            // 
            // validateTab
            // 
            validateTab.BorderStyle = BorderStyle.FixedSingle;
            validateTab.Location = new Point(4, 24);
            validateTab.Name = "validateTab";
            validateTab.Padding = new Padding(3);
            validateTab.Size = new Size(610, 422);
            validateTab.TabIndex = 1;
            validateTab.Text = "Validate";
            validateTab.UseVisualStyleBackColor = true;
            // 
            // propertiesTab
            // 
            propertiesTab.BorderStyle = BorderStyle.FixedSingle;
            propertiesTab.Controls.Add(buttonSaveProperties);
            propertiesTab.Controls.Add(buttonCancelProperties);
            propertiesTab.Controls.Add(groupComplexity);
            propertiesTab.Controls.Add(numericMaximumLength);
            propertiesTab.Controls.Add(labelMaximumLength);
            propertiesTab.Controls.Add(numericMinimumLength);
            propertiesTab.Controls.Add(labelMinimumLength);
            propertiesTab.Controls.Add(textBoxChacacters);
            propertiesTab.Controls.Add(labelCharacters);
            propertiesTab.Location = new Point(4, 24);
            propertiesTab.Name = "propertiesTab";
            propertiesTab.Padding = new Padding(3);
            propertiesTab.Size = new Size(610, 389);
            propertiesTab.TabIndex = 2;
            propertiesTab.Text = "Properties";
            propertiesTab.UseVisualStyleBackColor = true;
            // 
            // buttonSaveProperties
            // 
            buttonSaveProperties.Location = new Point(408, 339);
            buttonSaveProperties.Name = "buttonSaveProperties";
            buttonSaveProperties.Size = new Size(75, 23);
            buttonSaveProperties.TabIndex = 8;
            buttonSaveProperties.Text = "&Save";
            buttonSaveProperties.UseVisualStyleBackColor = true;
            buttonSaveProperties.Click += OnButtonSavePropertiesClick;
            // 
            // buttonCancelProperties
            // 
            buttonCancelProperties.Location = new Point(503, 339);
            buttonCancelProperties.Name = "buttonCancelProperties";
            buttonCancelProperties.Size = new Size(75, 23);
            buttonCancelProperties.TabIndex = 7;
            buttonCancelProperties.Text = "&Cancel";
            buttonCancelProperties.UseVisualStyleBackColor = true;
            buttonCancelProperties.Click += OnButtonCancelPropertiesClick;
            // 
            // groupComplexity
            // 
            groupComplexity.Controls.Add(panelSpecialCount);
            groupComplexity.Controls.Add(checkSpecials);
            groupComplexity.Controls.Add(checkNumbers);
            groupComplexity.Controls.Add(checkUpperCaseLetters);
            groupComplexity.Controls.Add(checkLowerCaseLetters);
            groupComplexity.Location = new Point(24, 211);
            groupComplexity.Name = "groupComplexity";
            groupComplexity.Size = new Size(554, 100);
            groupComplexity.TabIndex = 6;
            groupComplexity.TabStop = false;
            groupComplexity.Text = "Complexity";
            // 
            // panelSpecialCount
            // 
            panelSpecialCount.Controls.Add(numericSpecialCount);
            panelSpecialCount.Controls.Add(labelSpecialCount);
            panelSpecialCount.Location = new Point(274, 49);
            panelSpecialCount.Name = "panelSpecialCount";
            panelSpecialCount.Size = new Size(226, 45);
            panelSpecialCount.TabIndex = 4;
            // 
            // numericSpecialCount
            // 
            numericSpecialCount.Location = new Point(65, 12);
            numericSpecialCount.Name = "numericSpecialCount";
            numericSpecialCount.Size = new Size(120, 23);
            numericSpecialCount.TabIndex = 1;
            numericSpecialCount.ValueChanged += OnNumericSpecialCountValueChanged;
            // 
            // labelSpecialCount
            // 
            labelSpecialCount.AutoSize = true;
            labelSpecialCount.Location = new Point(16, 14);
            labelSpecialCount.Name = "labelSpecialCount";
            labelSpecialCount.Size = new Size(43, 15);
            labelSpecialCount.TabIndex = 0;
            labelSpecialCount.Text = "Count:";
            // 
            // checkSpecials
            // 
            checkSpecials.AutoSize = true;
            checkSpecials.Checked = true;
            checkSpecials.CheckState = CheckState.Checked;
            checkSpecials.Location = new Point(171, 62);
            checkSpecials.Name = "checkSpecials";
            checkSpecials.Size = new Size(68, 19);
            checkSpecials.TabIndex = 3;
            checkSpecials.Text = "Specials";
            checkSpecials.UseVisualStyleBackColor = true;
            checkSpecials.CheckedChanged += OnCheckSpecialsCheckedChanged;
            // 
            // checkNumbers
            // 
            checkNumbers.AutoSize = true;
            checkNumbers.Checked = true;
            checkNumbers.CheckState = CheckState.Checked;
            checkNumbers.Location = new Point(171, 32);
            checkNumbers.Name = "checkNumbers";
            checkNumbers.Size = new Size(75, 19);
            checkNumbers.TabIndex = 2;
            checkNumbers.Text = "Numbers";
            checkNumbers.UseVisualStyleBackColor = true;
            checkNumbers.CheckedChanged += OnCheckNumbersCheckedChanged;
            // 
            // checkUpperCaseLetters
            // 
            checkUpperCaseLetters.AutoSize = true;
            checkUpperCaseLetters.Checked = true;
            checkUpperCaseLetters.CheckState = CheckState.Checked;
            checkUpperCaseLetters.Location = new Point(18, 62);
            checkUpperCaseLetters.Name = "checkUpperCaseLetters";
            checkUpperCaseLetters.Size = new Size(119, 19);
            checkUpperCaseLetters.TabIndex = 1;
            checkUpperCaseLetters.Text = "Upper case letters";
            checkUpperCaseLetters.UseVisualStyleBackColor = true;
            checkUpperCaseLetters.CheckedChanged += OnCheckUpperCaseLettersCheckedChanged;
            // 
            // checkLowerCaseLetters
            // 
            checkLowerCaseLetters.AutoSize = true;
            checkLowerCaseLetters.Checked = true;
            checkLowerCaseLetters.CheckState = CheckState.Checked;
            checkLowerCaseLetters.Location = new Point(18, 32);
            checkLowerCaseLetters.Name = "checkLowerCaseLetters";
            checkLowerCaseLetters.Size = new Size(119, 19);
            checkLowerCaseLetters.TabIndex = 0;
            checkLowerCaseLetters.Text = "Lower case letters";
            checkLowerCaseLetters.UseVisualStyleBackColor = true;
            checkLowerCaseLetters.CheckedChanged += OnCheckLowerCaseLettersCheckedChanged;
            // 
            // numericMaximumLength
            // 
            numericMaximumLength.Location = new Point(152, 169);
            numericMaximumLength.Name = "numericMaximumLength";
            numericMaximumLength.Size = new Size(200, 23);
            numericMaximumLength.TabIndex = 5;
            numericMaximumLength.ValueChanged += OnNumericMaximumLengthValueChanged;
            // 
            // labelMaximumLength
            // 
            labelMaximumLength.AutoSize = true;
            labelMaximumLength.Location = new Point(24, 171);
            labelMaximumLength.Name = "labelMaximumLength";
            labelMaximumLength.Size = new Size(101, 15);
            labelMaximumLength.TabIndex = 4;
            labelMaximumLength.Text = "Maximum length:";
            // 
            // numericMinimumLength
            // 
            numericMinimumLength.Location = new Point(152, 133);
            numericMinimumLength.Name = "numericMinimumLength";
            numericMinimumLength.Size = new Size(200, 23);
            numericMinimumLength.TabIndex = 3;
            numericMinimumLength.ValueChanged += OnNumericMinimumLengthValueChanged;
            // 
            // labelMinimumLength
            // 
            labelMinimumLength.AutoSize = true;
            labelMinimumLength.Location = new Point(24, 135);
            labelMinimumLength.Name = "labelMinimumLength";
            labelMinimumLength.Size = new Size(100, 15);
            labelMinimumLength.TabIndex = 2;
            labelMinimumLength.Text = "Minimum length:";
            // 
            // textBoxChacacters
            // 
            textBoxChacacters.Location = new Point(24, 37);
            textBoxChacacters.Multiline = true;
            textBoxChacacters.Name = "textBoxChacacters";
            textBoxChacacters.ReadOnly = true;
            textBoxChacacters.Size = new Size(554, 69);
            textBoxChacacters.TabIndex = 1;
            // 
            // labelCharacters
            // 
            labelCharacters.AutoSize = true;
            labelCharacters.Location = new Point(24, 19);
            labelCharacters.Name = "labelCharacters";
            labelCharacters.Size = new Size(66, 15);
            labelCharacters.TabIndex = 0;
            labelCharacters.Text = "Characters:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 417);
            Controls.Add(mainFormTabs);
            Name = "MainForm";
            Text = "Passwords";
            Load += OnMainFormLoad;
            mainFormTabs.ResumeLayout(false);
            propertiesTab.ResumeLayout(false);
            propertiesTab.PerformLayout();
            groupComplexity.ResumeLayout(false);
            groupComplexity.PerformLayout();
            panelSpecialCount.ResumeLayout(false);
            panelSpecialCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericSpecialCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMaximumLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMinimumLength).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl mainFormTabs;
        private TabPage generateTab;
        private TabPage validateTab;
        private TabPage propertiesTab;
        private TextBox textBoxChacacters;
        private Label labelCharacters;
        private NumericUpDown numericMinimumLength;
        private Label labelMinimumLength;
        private NumericUpDown numericMaximumLength;
        private Label labelMaximumLength;
        private GroupBox groupComplexity;
        private CheckBox checkNumbers;
        private CheckBox checkUpperCaseLetters;
        private CheckBox checkLowerCaseLetters;
        private CheckBox checkSpecials;
        private Panel panelSpecialCount;
        private NumericUpDown numericSpecialCount;
        private Label labelSpecialCount;
        private Button buttonSaveProperties;
        private Button buttonCancelProperties;
    }
}
