using System.ComponentModel;

namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Represents a combo box control that is bound to an enumeration type.
    /// </summary>
    public class EnumComboBox : ComboBox
    {
        private Type? enumType;

        /// <summary>
        /// Initializes new instance of the <see cref="EnumComboBox"/> class.
        /// </summary>
        public EnumComboBox()
            : base()
        {
            base.ValueMember = "Value";
            base.DisplayMember = "Text";
        }

        /// <summary>
        /// Gets or sets whether or not selection change events should be avoided.
        /// If set to <c>true</c>, then selection change events do not occur.
        /// If set to <c>false</c>, default, then selection change events occur.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AvoidSelectionChanges { get; set; } = false;

        /// <summary>
        /// Gets the value member.
        /// </summary>
        /// <exception cref="NotSupportedException">If attempt to set value.</exception>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string ValueMember
        {
            get { return base.ValueMember; }
            set { throw new NotSupportedException("Setting the ValueMember is not supported."); }
        }

        /// <summary>
        /// Gets the text member.
        /// </summary>
        /// <exception cref="NotSupportedException">If attempt to set value.</exception>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string DisplayMember
        {
            get { return base.DisplayMember; }
            set { throw new NotSupportedException("Setting the DisplayMember is not supported."); }
        }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new EnumComboBoxItemList? DataSource
        {
            get { return base.DataSource as EnumComboBoxItemList; }
            set { base.DataSource = value; }
        }

        /// <summary>
        /// Gets items list.
        /// </summary>
        public new EnumComboBoxItemList Items
        {
            get
            {
                if (DataSource == null)
                    DataSource = new EnumComboBoxItemList();

                return DataSource;
            }
        }

        /// <summary>
        /// Gets selected item.
        /// </summary>
        public new EnumComboBoxItem? SelectedItem
        {
            get
            {
                if (SelectedIndex >= 0 && SelectedIndex <= Items.Count - 1)
                    return Items[SelectedIndex];

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the target enumeration type.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type? EnumType
        {
            get { return enumType; }
            set
            {
                if (value == null)
                {
                    DataSource = null;
                    enumType = null;
                }
                else
                {
                    if (!value.IsEnum)
                        throw new ArgumentException("Type must be an enumeration type.", nameof(EnumType));

                    enumType = value;
                    DataSource = EnumComboBoxItemList.Create(value);
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets selected value as <typeparamref name="TEnum"/> enumeration.
        /// </summary>
        /// <typeparam name="TEnum">The expected enumeration type.</typeparam>
        /// <returns>The selected value as <typeparamref name="TEnum"/> if it matches the expected type; otherwise, <c>null</c>.</returns>
        public TEnum? GetSelectedValue<TEnum>() where TEnum : struct, Enum 
        {
            var item = SelectedItem;

            if (item == null)
                return null;

            if (item.EnumType.Equals(typeof(TEnum)))
                return (TEnum)item.Value;

            return null;
        }

        /// <summary>
        /// Sets selected value as <typeparamref name="TEnum"/> enumeration.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <param name="value">The value to set as selected.</param>
        public void SetSelectedValue<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            for (int index = 0; index < Items.Count; index++)
            {
                if (Equals(Items[index].Value, value))
                { 
                    SelectedIndex = index;
                    break;
                }
            }
        }

        protected override void OnSelectedItemChanged(EventArgs e)
        {
            if (AvoidSelectionChanges)
                return;

            base.OnSelectedItemChanged(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (AvoidSelectionChanges)
                return;

            base.OnSelectedIndexChanged(e);
        }

        protected override void OnSelectedValueChanged(EventArgs e)
        {
            if (AvoidSelectionChanges)
                return;

            base.OnSelectedValueChanged(e);
        }
    }
}
