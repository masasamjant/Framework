using Masasamjant.Serialization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Describes sorting by specified property name.
    /// </summary>
    public sealed class SortDescriptor : IEquatable<SortDescriptor>, IJsonSerializable
    {
        /// <summary>
        /// Initializes new instance of the <see cref="SortDescriptor"/> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="sortOrder"/> is not defined.</exception>
        public SortDescriptor(string propertyName, SortOrder sortOrder)
        {
            if (!Enum.IsDefined(sortOrder))
                throw new ArgumentException("The value is not defined.", nameof(sortOrder));

            PropertyName = propertyName;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Initializes new default instance of the <see cref="SortDescriptor"/> class.
        /// </summary>
        public SortDescriptor()
        { }

        /// <summary>
        /// Gets the property name to sort by. Default is empty string.
        /// </summary>
        [JsonInclude]
        public string PropertyName { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the sort order. Default is <see cref="SortOrder.None"/>.
        /// </summary>
        [JsonInclude]
        public SortOrder SortOrder { get; internal set; } = SortOrder.None;

        /// <summary>
        /// Check if other <see cref="SortDescriptor"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The other <see cref="SortDescriptor"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this instance; <c>false</c> otherwise.</returns>
        public bool Equals(SortDescriptor? other)
        {
            return other != null &&
                PropertyName.Equals(other.PropertyName, StringComparison.Ordinal) &&
                SortOrder == other.SortOrder;
        }

        /// <summary>
        /// Check if object instance is <see cref="SortDescriptor"/> and equal to this instance.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="SortDescriptor"/> and equal to this instance; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as SortDescriptor);
        }

        /// <summary>
        /// Gets hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(PropertyName, SortOrder);
        }

        /// <summary>
        /// Gets the string presentation.
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            if (PropertyName.Length == 0)
                return string.Empty;

            var orderBy = SortOrder.ToSql();

            if (orderBy.Length > 0)
                return PropertyName + " " + orderBy;

            return PropertyName;
        }

        /// <summary>
        /// Creates <see cref="SortDescriptor"/> for the specified property of specified object instance 
        /// ensuring that object has public instance get property with specified name.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>A <see cref="SortDescriptor"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If type of <paramref name="obj"/> does not have public instance property specified by <paramref name="propertyName"/>.
        /// -or-
        /// If value of <paramref name="sortOrder"/> is not defined.
        /// </exception>
        public static SortDescriptor Create(object obj, string propertyName, SortOrder sortOrder)
        {
            return Create(obj.GetType(), propertyName, sortOrder);
        }

        /// <summary>
        /// Creates <see cref="SortDescriptor"/> for the specified property of specified type ensuring 
        /// that type has public instance get property with specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>A <see cref="SortDescriptor"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If <paramref name="type"/> does not have public instance property specified by <paramref name="propertyName"/>.
        /// -or-
        /// If value of <paramref name="sortOrder"/> is not defined.
        /// </exception>
        public static SortDescriptor Create(Type type, string propertyName, SortOrder sortOrder)
        {
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            if (property == null)
                throw new ArgumentException($"The type '{type}' does not have public instance get property of '{propertyName}'.", nameof(propertyName));
            return new SortDescriptor(propertyName, sortOrder);
        }
    }
}
