using Masasamjant.Collections;
using System.Collections;
using System.Reflection;

namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Represents list of <see cref="EnumComboBoxItem"/>s.
    /// </summary>
    public sealed class EnumComboBoxItemList : Collection<EnumComboBoxItem>, IList<EnumComboBoxItem>, IList
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="EnumComboBoxItemList"/> class.
        /// </summary>
        public EnumComboBoxItemList()
            : base(new List<EnumComboBoxItem>())
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="EnumComboBoxItemList"/> class with specified items.
        /// </summary>
        /// <param name="items">The initial items.</param>
        /// <exception cref="ArgumentException">If not all items in <paramref name="items"/> have the same enumeration type.</exception>
        public EnumComboBoxItemList(IEnumerable<EnumComboBoxItem> items)
            : base(new List<EnumComboBoxItem>(items))
        {
            if (Count > 1)
            {
                Type enumType = List[0].EnumType;

                if (!Items.All(x => x.EnumType.Equals(enumType)))
                    throw new ArgumentException("All items must have same enumeration type.", nameof(items));
            }
        }

        /// <summary>
        /// Gets or sets the item at specified index.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The item at the specified index.</returns>
        public EnumComboBoxItem this[int index]
        {
            get { return List[index]; }
            set
            {
                ValidataEnumType(value, nameof(value));
                List[index] = value;
            }
        }

        /// <summary>
        /// Add item to the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="ArgumentException">If the item's enumeration type does not match the existing items.</exception>
        public override void Add(EnumComboBoxItem item)
        {
            ValidataEnumType(item, nameof(item));
            base.Add(item);
        }

        /// <summary>
        /// Add range of items to the list.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentException">If not all items in <paramref name="items"/> have the same enumeration type.</exception>
        public override void AddRange(IEnumerable<EnumComboBoxItem> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            if (!items.Any())
                return;

            Type enumType = Count > 0 ? List[0].EnumType : items.First().EnumType;

            if (!items.All(x => x.EnumType.Equals(enumType)))
                throw new ArgumentException("All items must have same enumeration type.", nameof(items));

            base.AddRange(items);
        }

        /// <summary>
        /// Gets the index of the specified item in the list.
        /// </summary>
        /// <param name="item">The item to locate in the list.</param>
        /// <returns>The index of the item if found; otherwise, -1.</returns>
        public int IndexOf(EnumComboBoxItem item)
        {
            ArgumentNullException.ThrowIfNull(item);
            return List.IndexOf(item);
        }

        /// <summary>
        /// Insert item at specified index in the list.
        /// </summary>
        /// <param name="index">The index at which to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/> is null.</exception>
        /// <exception cref="ArgumentException">If the item's enumeration type does not match the existing items.</exception>
        public void Insert(int index, EnumComboBoxItem item)
        {
            CheckReadOnly();
            ArgumentNullException.ThrowIfNull(item);
            ValidataEnumType(item, nameof(item));
            List.Insert(index, item);
        }

        /// <summary>
        /// Remove item at specified index from the list.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            CheckReadOnly();
            List.RemoveAt(index);
        }

        /// <summary>
        /// Create <see cref="EnumComboBoxItemList"/> for the specified enumeration type.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <returns>A new instance of <see cref="EnumComboBoxItemList"/> containing items for the specified enumeration type.</returns>
        public static EnumComboBoxItemList Create<TEnum>() where TEnum : struct, Enum
            => Create(typeof(TEnum));

        /// <summary>
        /// Create <see cref="EnumComboBoxItemList"/> for the specified enumeration type.
        /// </summary>
        /// <param name="enumType">The enumeration type.</param>
        /// <returns>A new instance of <see cref="EnumComboBoxItemList"/> containing items for the specified enumeration type.</returns>
        public static EnumComboBoxItemList Create(Type enumType)
        {
            var list = new EnumComboBoxItemList();
            var values = Enum.GetValues(enumType);

            foreach (var value in values)
            {
                var memberInfo = value.GetType().GetMember(value.ToString() ?? string.Empty).FirstOrDefault();

                if (memberInfo != null)
                {
                    var attribute = memberInfo.GetCustomAttribute<EnumVisibilityAttribute>();

                    if (attribute != null && !attribute.IsVisible)
                        continue;
                }

                list.Add(new EnumComboBoxItem(value));
            }

            return list;
        }

        private void ValidataEnumType(EnumComboBoxItem item, string paramName)
        {
            if (Count > 0)
            {
                Type enumType = List[0].EnumType;

                if (!item.EnumType.Equals(enumType))
                    throw new ArgumentException("All items must have same enumeration type.", paramName);
            }
        }

        private List<EnumComboBoxItem> List
        {
            get { return (List<EnumComboBoxItem>)Items; }
        }

        object? IList.this[int index]
        {
            get { return this[index]; }
            set
            {
                var item = value as EnumComboBoxItem;

                if (item == null)
                    throw new ArgumentNullException(nameof(value));

                ValidataEnumType(item, nameof(value));

                this[index] = item;
            }
        }

        bool IList.IsFixedSize => false;

        bool IList.IsReadOnly => base.IsReadOnly;

        int IList.Add(object? value)
        {
            var item = value as EnumComboBoxItem;

            if (item == null)
                return -1;

            Add(item);
            return IndexOf(item);
        }

        bool IList.Contains(object? value)
        {
            var item = value as EnumComboBoxItem;

            if (item != null)
                return Contains(item);

            return false;
        }

        int IList.IndexOf(object? value)
        {
            var item = value as EnumComboBoxItem;

            if (item != null)
                return IndexOf(item);

            return -1;
        }

        void IList.Insert(int index, object? value)
        {
            var item = value as EnumComboBoxItem;

            if (item != null)
                Insert(index, item);
        }

        void IList.Remove(object? value)
        {
            var item = value as EnumComboBoxItem;

            if (item != null)
                Remove(item);
        }
    }
}
