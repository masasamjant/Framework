namespace Masasamjant.Web.Routing
{
    /// <summary>
    /// Represents named route value.
    /// </summary>
    public class RouteValue : IRouteValue, IEquatable<RouteValue>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="RouteValue"/> class.
        /// </summary>
        /// <param name="name">The route value name.</param>
        /// <param name="value">The route value.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="name"/> is empty or only whitespace.</exception>
        public RouteValue(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "The name cannot be empty or only whitespace.");

            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets route value name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets route value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Gets string presentation.
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            return $"{Name}={Value}";
        }

        /// <summary>
        /// Check if other <see cref="RouteValue"/> is equal to this. 
        /// Route values are equal if both <see cref="Name"/> and <see cref="Value"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="RouteValue"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> has same name and value with this route value; <c>false</c> otherwise.</returns>
        public bool Equals(RouteValue? other) 
        {
            if (other != null)
            {
                return Name == other.Name && Value.Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Check if object instance is <see cref="RouteValue"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="RouteValue"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as RouteValue);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Value);
        }
    }
}
