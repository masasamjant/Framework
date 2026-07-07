using Masasamjant.Resources;
using System.ComponentModel;
using System.Reflection;

namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// User control to select an enum value or values.
    /// </summary>
    public partial class EnumSelectorControl : UserControl
    {
        private Type? enumType;
        private bool isFlagsEnum = false;
        private bool layoutChange = false;
        private ControlLayout layout = ControlLayout.Vertical;
        private List<object> selectedValues = new List<object>();
        private bool suppressCheckedChanged = false;
        private const int LeftRightMargin = 12;
        private const int TopBottomMargin = 12;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumSelectorControl"/> class.
        /// </summary>
        public EnumSelectorControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Notifies when selected values has changed.
        /// </summary>
        public event EventHandler? SelectedValuesChanged;

        /// <summary>
        /// Gets or sets the target enum type.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Type? EnumType
        {
            get { return enumType; }
            set
            {
                if (value != null && !value.IsEnum)
                    throw new ArgumentException("Type must be an enum.", nameof(EnumType));

                if (!Equals(enumType, value))
                {
                    enumType = value;

                    if (enumType == null)
                        isFlagsEnum = false;
                    else
                    {
                        isFlagsEnum = enumType.GetCustomAttribute<FlagsAttribute>() != null;
                        layoutChange = true;
                    }

                    Refresh();

                    layoutChange = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the enum values are displayed in a vertical or horizontal layout.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlLayout ControlLayout
        {
            get { return layout; }
            set
            {
                if (layout != value)
                {
                    layoutChange = true;
                    layout = value;
                    Refresh();
                    layoutChange = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets resource provider for enum texts.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IStringResourceProvider? EnumTextResourceProvider { get; set; }

        /// <summary>
        /// Gets selected enum values.
        /// </summary>
        public IReadOnlyCollection<object> SelectedValues
        {
            get { return selectedValues.ToList().AsReadOnly(); }
        }

        /// <summary>
        /// Gets a value indicating whether the enum is a flags enum.
        /// If returns <c>true</c>, then uses checkboxes to select multiple values, otherwise uses radio buttons to select a single value.
        /// </summary>
        public bool IsFlagsEnum
        {
            get { return isFlagsEnum; }
        }

        /// <summary>
        /// Sets selected value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value to select.</param>
        /// <exception cref="ArgumentException">If the provided enum type does not match the current enum type.</exception>
        public void SetSelectedValue<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            if (EnumType == null)
                EnumType = typeof(TEnum);
            else if (!EnumType.Equals(typeof(TEnum)))
                throw new ArgumentException($"The provided enum type '{typeof(TEnum).FullName}' does not match the current enum type '{EnumType.FullName}'.", nameof(value));

            if (IsFlagsEnum)
            {
                var values = Enum.GetValues<TEnum>();
                var currentValues = SelectedValues.ToList();
                var newValues = new List<object>();
                var oldValues = new List<object>();

                foreach (var flag in values)
                {
                    if (value.HasFlag(flag))
                        newValues.Add(flag);
                }

                foreach (var flag in currentValues)
                {
                    if (!newValues.Contains(flag))
                        oldValues.Add(flag);
                }

                bool selectedValuesChanged = false;

                foreach (var flag in oldValues)
                {
                    if (selectedValues.Remove(flag))
                        selectedValuesChanged = true;
                }

                foreach (var flag in newValues)
                {
                    if (!selectedValues.Contains(flag))
                    {
                        selectedValues.Add(flag);
                        selectedValuesChanged = true;
                    }
                }

                if (selectedValuesChanged && groupControls.Controls.Count > 0)
                {
                    suppressCheckedChanged = true;

                    foreach (Control control in groupControls.Controls)
                    {
                        if (control is CheckBox checkBox)
                            checkBox.Checked = selectedValues.Contains(checkBox.Tag!);
                    }

                    suppressCheckedChanged = false;

                    OnSelectedValuesChanged();
                }
            }
            else
            {
                if (selectedValues.Count == 1 && selectedValues.Contains(value))
                    return;

                selectedValues.Clear();
                selectedValues.Add(value);

                if (groupControls.Controls.Count > 0)
                {
                    suppressCheckedChanged = true;

                    foreach (Control control in groupControls.Controls)
                    {
                        if (control is RadioButton radioButton)
                            radioButton.Checked = selectedValues.Contains(radioButton.Tag!);
                    }

                    suppressCheckedChanged = false;

                    OnSelectedValuesChanged();
                }
            }
        }

        /// <summary>
        /// Forces the control to refresh its layout and controls.
        /// </summary>
        public override void Refresh()
        {
            if (EnumType == null || groupControls.Controls.Count > 0)
                ClearControls();

            if (layoutChange)
                BuildLayout();

            base.Refresh();
        }

        protected virtual void OnSelectedValuesChanged()
        {
            SelectedValuesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ClearControls() 
        {
            if (groupControls.Controls.Count == 0)
                return;

            foreach (Control control in groupControls.Controls)
            {
                if (control is RadioButton radioButton)
                    radioButton.CheckedChanged -= OnCheckedChanged;
                else if (control is CheckBox checkBox)
                    checkBox.CheckedChanged -= OnCheckedChanged;

                control.Dispose();
            }

            groupControls.Controls.Clear();
        }

        private void BuildLayout()
        {
            if (EnumType == null)
                return;

            var values = Enum.GetValues(EnumType);

            if (values.Length == 0)
                return;

            var list = new List<Tuple<string, object>>(values.Length);

            foreach (var value in values)
            {
                var text = GetEnumText(value);
                list.Add(new Tuple<string, object>(text, value));
            }

            if (ControlLayout == ControlLayout.Horizontal)
                BuildHorizontalLayout(list);
            else
                BuildVerticalLayout(list);
        }

        private void BuildHorizontalLayout(List<Tuple<string, object>> values)
        {
            var controls = CreateControls(values);

            int x = LeftRightMargin;
            int y = TopBottomMargin;
            int width = 0;
            int height = 0;

            foreach (Control control in controls)
            {
                control.Location = new Point(x, y);
                x += control.Width;
                if (height < control.Height)
                    height = control.Height;
                width += control.Width;
                groupControls.Controls.Add(control);
            }

            Width = width + (LeftRightMargin * 2);
            Height = height + (TopBottomMargin * 2);
        }

        private void BuildVerticalLayout(List<Tuple<string, object>> values) 
        {
            var controls = CreateControls(values);

            int x = LeftRightMargin;
            int y = TopBottomMargin;
            int height = 0;
            int width = 0;

            foreach (Control control in controls)
            {
                control.Location = new Point(x, y);
                if (width < control.Width)
                    width = control.Width;
                y += control.Height;
                height += control.Height;
                groupControls.Controls.Add(control);
            }

            Height = height + (TopBottomMargin * 2);
            Width = width + (LeftRightMargin * 2);
        }

        private List<Control> CreateControls(List<Tuple<string, object>> values)
        {
            bool first = true;
            var controls = new List<Control>(values.Count);

            foreach (var value in values)
            {
                var control = CreateControl(value.Item1, value.Item2, first);
                controls.Add(control);
                first = false;
            }

            return controls;
        }

        private Control CreateControl(string text, object value, bool first)
        {
            Control control;

            if (IsFlagsEnum)
                control = CreateCheckBox(value);
            else
                control = CreateRadioButton(value, first);

            control.Text = text;
            control.Tag = Tag;

            if ((control.Width + text.Length) > control.Width)
                control.Width += text.Length;

            return control;
        }

        private CheckBox CreateCheckBox(object value)
        {
            var checkBox = new CheckBox();
            if (selectedValues.Count > 0)
                checkBox.Checked = selectedValues.Contains(value);
            checkBox.CheckedChanged += OnCheckedChanged;
            return checkBox;
        }

        private RadioButton CreateRadioButton(object value, bool first)
        {
            var radioButton = new RadioButton();

            if (selectedValues.Count > 0)
                radioButton.Checked = selectedValues.Contains(value);
            else
            {
                radioButton.Checked = first;
                if (radioButton.Checked)
                    selectedValues.Add(value);
            }

            radioButton.CheckedChanged += OnCheckedChanged;
            return radioButton;
        }

        private void OnCheckedChanged(object? sender, EventArgs e)
        {
            if (sender == null || suppressCheckedChanged)
                return;

            if (sender is RadioButton radioButton)
                OnRadioButtonCheckedChanged(radioButton);
            else if (sender is CheckBox checkBox)
                OnCheckBoxCheckedChanged(checkBox);
        }

        private void OnRadioButtonCheckedChanged(RadioButton radionButton)
        {
            var value = radionButton.Tag;

            if (value == null)
                return;

            if (radionButton.Checked)
            {
                selectedValues.Clear();
                selectedValues.Add(value);
            }
            else
            { 
                selectedValues.Remove(value);
            }

            OnSelectedValuesChanged();
        }

        private void OnCheckBoxCheckedChanged(CheckBox checkBox)
        {
            var value = checkBox.Tag;

            if (value == null)
                return;

            if (checkBox.Checked)
            {
                selectedValues.Add(value);
            }
            else
            {
                selectedValues.Remove(value);
            }

            OnSelectedValuesChanged();
        }

        private string GetEnumText(object value)
        {
            var text = string.Empty;
            var str = value.ToString();

            if (str == null)
                return text;

            text = EnumTextResourceProvider?.GetString(str, string.Empty) ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(text))
                return text;

            var memberInfo = value.GetType().GetMember(str).FirstOrDefault();

            if (memberInfo == null)
                return text;

            var attribute = memberInfo.GetCustomAttribute<ResourceStringAttribute>(false);

            if (attribute != null)
                return attribute.ResourceValue;

            return Enum.GetName(value.GetType(), value) ?? string.Empty;
        }
    }
}
