using Masasamjant.Resources;
using System.Reflection;

namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Represents a item of combo box that display enumeration.
    /// </summary>
    public sealed class EnumComboBoxItem
    {
        private readonly object value;

        /// <summary>
        /// Initializes new instance of the <see cref="EnumComboBoxItem"/> class.
        /// </summary>
        /// <param name="value">The enumeration value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="value"/> is not a valid enumeration value.</exception>
        public EnumComboBoxItem(object value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (!value.GetType().IsEnum)
                throw new ArgumentException("The value must be enumeration value.", nameof(value));

            if (!Enum.IsDefined(value.GetType(), value))
                throw new ArgumentException("The value is undefined.", nameof(value));

            this.value = value;
        }

        /// <summary>
        /// Gets the display text.
        /// </summary>
        public string Text
        {
            get
            {
                var text = GetResourceText();
                
                if (text != null)
                    return text;

                return Enum.GetName(EnumType, Value) ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets the value of enumeration.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets the enumeration type.
        /// </summary>
        public Type EnumType
        {
            get { return Value.GetType(); }
        }

        /// <summary>
        /// Gets the string presentation.
        /// </summary>
        /// <returns>A <see cref="Text"/>.</returns>
        public override string ToString()
        {
            return Text;
        }

        private string? GetResourceText()
        {
            var str = Value.ToString();
            
            if (str == null)
                return null;
            
            var memberInfo = EnumType.GetMember(str).FirstOrDefault();
            
            if (memberInfo == null)
                return null;
            
            var attribute = memberInfo.GetCustomAttribute<ResourceStringAttribute>(false);
            
            if (attribute == null)
                return null;

            return attribute.ResourceValue;
        }
    }
}
