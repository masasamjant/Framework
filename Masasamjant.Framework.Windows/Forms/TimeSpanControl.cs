using System.ComponentModel;

namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Control for editing a <see cref="TimeSpan"/> value.
    /// </summary>
    [DefaultEvent(nameof(ValueChanged))]
    [DefaultProperty(nameof(Value))]
    [DefaultBindingProperty(nameof(Value))]
    public partial class TimeSpanControl : UserControl
    {
        private bool supressValueChanged = false;

        public TimeSpanControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Notifies when value of the control has changed.
        /// </summary>
        [Description("Notifies when value of the control has changed.")]
        public event EventHandler? ValueChanged;

        /// <summary>
        /// Gets or sets the value of the control.
        /// </summary>
        [Description("Gets or sets the value of the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan Value
        {
            get { return GetTimeSpan(); }
            set { SetTimeSpan(value); }
        }

        /// <summary>
        /// Gets or sets the text of the label for days.
        /// </summary>
        [Description("Gets or sets the text of the label for days.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string DaysLabelText
        {
            get { return labelDays.Text; }
            set { labelDays.Text = value; }
        }

        /// <summary>
        /// Gets or sets the text of the label for hours.
        /// </summary>
        [Description("Gets or sets the text of the label for hours.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string HoursLabelText
        {
            get { return labelHours.Text; }
            set { labelHours.Text = value; }
        }

        /// <summary>
        /// Gets or sets the text of the label for minutes.
        /// </summary>
        [Description("Gets or sets the text of the label for minutes.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string MinutesLabelText
        {
            get { return labelMinutes.Text; }
            set { labelMinutes.Text = value; }
        }

        /// <summary>
        /// Gets or sets the text of the label for seconds.
        /// </summary>
        [Description("Gets or sets the text of the label for seconds.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string SecondsLabelText
        {
            get { return labelSeconds.Text; }
            set { labelSeconds.Text = value; }
        }

        /// <summary>
        /// Gets or sets the text of the label for milliseconds.
        /// </summary>
        [Description("Gets or sets the text of the label for milliseconds.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string MillisecondsLabelText
        {
            get { return labelMilliseconds.Text; }
            set { labelMilliseconds.Text = value; }
        }

        private void OnNumberValueChanged(object sender, EventArgs e)
        {
            if (supressValueChanged)
                return;

            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private TimeSpan GetTimeSpan()
        {
            int days = Convert.ToInt32(numDays.Value);
            int hours = Convert.ToInt32(numHours.Value);
            int minutes = Convert.ToInt32(numMinutes.Value);
            int seconds = Convert.ToInt32(numSeconds.Value);
            int milliseconds = Convert.ToInt32(numMilliseconds.Value);
            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }

        private void SetTimeSpan(TimeSpan value) 
        {
            try
            {
                supressValueChanged = true;
                numDays.Value = value.Days;
                numHours.Value = value.Hours;
                numMinutes.Value = value.Minutes;
                numSeconds.Value = value.Seconds;
                numMilliseconds.Value = value.Milliseconds;
                supressValueChanged = false;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                supressValueChanged = false;
            }
        }
    }
}
