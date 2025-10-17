namespace Masasamjant.IO
{
    /// <summary>
    /// Represents MIME type with associated file extension. 
    /// A MIME type is a two-part identifier that tells an application the format and nature of a file.
    /// </summary>
    public sealed class MimeType : IEquatable<MimeType>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="MimeType"/> class.
        /// </summary>
        /// <param name="value">The MIME type value.</param>
        /// <param name="fileExtension">The file extension associated with MIME type.</param>
        /// <param name="description">The human readable description.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="value"/> or <paramref name="fileExtension"/> is empty or only whitespace.</exception>
        public MimeType(string value, string fileExtension, string description)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "The MIME type value is empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentNullException(nameof(fileExtension), "The file extension is empty or only whitespace.");

            Value = value.ToLowerInvariant();
            Description = description;
            FileExtension = fileExtension.EnsureStartsWith('.').ToLowerInvariant();
        }

        /// <summary>
        /// Gets the MIME type value like 'text/plain'.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the file extension associated with MIME type like '.txt'.
        /// </summary>
        public string FileExtension { get; }

        /// <summary>
        /// Check if other <see cref="MimeType"/> is equal with this i.e. has same value and file extension.
        /// </summary>
        /// <param name="other">The other MIME type.</param>
        /// <returns><c>true</c> if <paramref name="other"/> has same value and file extension with this; <c>false</c> otherwise.</returns>
        public bool Equals(MimeType? other)
        {
            return other != null &&
                string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(FileExtension, other.FileExtension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Check if object instance is <see cref="MimeType"/> and equal with this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="MimeType"/> and equal with this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as MimeType);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, FileExtension);
        }

        /// <summary>
        /// Gets string presentation. Either only <see cref="Value"/> or <see cref="Description"/> and <see cref="Value"/>.
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Description))
                return Value;

            return $"{Description} ({Value})";
        }
    }
}
