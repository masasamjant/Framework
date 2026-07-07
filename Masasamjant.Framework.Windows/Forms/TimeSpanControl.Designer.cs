namespace Masasamjant.Windows.Forms
{
    partial class TimeSpanControl
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
            numDays = new NumericUpDown();
            labelDays = new Label();
            numHours = new NumericUpDown();
            labelHours = new Label();
            numMinutes = new NumericUpDown();
            labelMinutes = new Label();
            numSeconds = new NumericUpDown();
            labelSeconds = new Label();
            numMilliseconds = new NumericUpDown();
            labelMilliseconds = new Label();
            ((System.ComponentModel.ISupportInitialize)numDays).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSeconds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMilliseconds).BeginInit();
            SuspendLayout();
            // 
            // numDays
            // 
            numDays.Location = new Point(10, 13);
            numDays.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numDays.Name = "numDays";
            numDays.Size = new Size(120, 23);
            numDays.TabIndex = 0;
            numDays.ValueChanged += OnNumberValueChanged;
            // 
            // labelDays
            // 
            labelDays.AutoSize = true;
            labelDays.Location = new Point(142, 15);
            labelDays.Name = "labelDays";
            labelDays.Size = new Size(31, 15);
            labelDays.TabIndex = 1;
            labelDays.Text = "days";
            // 
            // numHours
            // 
            numHours.Location = new Point(10, 42);
            numHours.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numHours.Name = "numHours";
            numHours.Size = new Size(120, 23);
            numHours.TabIndex = 2;
            numHours.ValueChanged += OnNumberValueChanged;
            // 
            // labelHours
            // 
            labelHours.AutoSize = true;
            labelHours.Location = new Point(142, 44);
            labelHours.Name = "labelHours";
            labelHours.Size = new Size(37, 15);
            labelHours.TabIndex = 3;
            labelHours.Text = "hours";
            // 
            // numMinutes
            // 
            numMinutes.Location = new Point(10, 71);
            numMinutes.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numMinutes.Name = "numMinutes";
            numMinutes.Size = new Size(120, 23);
            numMinutes.TabIndex = 4;
            numMinutes.ValueChanged += OnNumberValueChanged;
            // 
            // labelMinutes
            // 
            labelMinutes.AutoSize = true;
            labelMinutes.Location = new Point(142, 73);
            labelMinutes.Name = "labelMinutes";
            labelMinutes.Size = new Size(50, 15);
            labelMinutes.TabIndex = 5;
            labelMinutes.Text = "minutes";
            // 
            // numSeconds
            // 
            numSeconds.Location = new Point(10, 100);
            numSeconds.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numSeconds.Name = "numSeconds";
            numSeconds.Size = new Size(120, 23);
            numSeconds.TabIndex = 6;
            numSeconds.ValueChanged += OnNumberValueChanged;
            // 
            // labelSeconds
            // 
            labelSeconds.AutoSize = true;
            labelSeconds.Location = new Point(142, 102);
            labelSeconds.Name = "labelSeconds";
            labelSeconds.Size = new Size(50, 15);
            labelSeconds.TabIndex = 7;
            labelSeconds.Text = "seconds";
            // 
            // numMilliseconds
            // 
            numMilliseconds.Location = new Point(10, 129);
            numMilliseconds.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numMilliseconds.Name = "numMilliseconds";
            numMilliseconds.Size = new Size(120, 23);
            numMilliseconds.TabIndex = 8;
            numMilliseconds.ValueChanged += OnNumberValueChanged;
            // 
            // labelMilliseconds
            // 
            labelMilliseconds.AutoSize = true;
            labelMilliseconds.Location = new Point(142, 131);
            labelMilliseconds.Name = "labelMilliseconds";
            labelMilliseconds.Size = new Size(73, 15);
            labelMilliseconds.TabIndex = 9;
            labelMilliseconds.Text = "milliseconds";
            // 
            // TimeSpanControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelMilliseconds);
            Controls.Add(numMilliseconds);
            Controls.Add(labelSeconds);
            Controls.Add(numSeconds);
            Controls.Add(labelMinutes);
            Controls.Add(numMinutes);
            Controls.Add(labelHours);
            Controls.Add(numHours);
            Controls.Add(labelDays);
            Controls.Add(numDays);
            Name = "TimeSpanControl";
            Size = new Size(236, 163);
            ((System.ComponentModel.ISupportInitialize)numDays).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSeconds).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMilliseconds).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDays;
        private NumericUpDown numHours;
        private Label labelHours;
        private NumericUpDown numMinutes;
        private Label labelMinutes;
        private NumericUpDown numSeconds;
        private Label labelSeconds;
        private NumericUpDown numMilliseconds;
        private Label labelMilliseconds;
        private NumericUpDown numDays;
    }
}
