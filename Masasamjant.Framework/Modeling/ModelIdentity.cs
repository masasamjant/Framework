using Masasamjant.Modeling.Abstractions;
using System.Security.Cryptography;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Represents identity of specified <see cref="IModel"/>.
    /// </summary>
    public sealed class ModelIdentity : IEquatable<ModelIdentity>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ModelIdentity"/> class.
        /// </summary>
        /// <param name="model">The <see cref="IModel"/>.</param>
        /// <param name="keys">The values of key properties.</param>
        /// <exception cref="ArgumentException">If <paramref name="keys"/> is empty.</exception>
        public ModelIdentity(IModel model, object[] keys)
        {
            if (keys.Length == 0)
                throw new ArgumentException("The model must have at least one key property value.", nameof(keys));

            var values = new List<string>(keys.Length + 1)
            {
                TypeHelper.GetTypeName(model.GetType())
            };
            
            foreach (var key in keys)
                values.Add(key.ToString() ?? string.Empty);

            var bytes = string.Concat(values).GetByteArray();
            Value = Convert.ToBase64String(SHA1.HashData(bytes));
        }

        /// <summary>
        /// Gets the SHA1 to identify model.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Check if other <see cref="ModelIdentity"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="ModelIdentity"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(ModelIdentity? other)
        {
            return other != null && string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Check if object instance is <see cref="ModelIdentity"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="ModelIdentity"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as ModelIdentity);
        }

        /// <summary>
        /// Gets hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
