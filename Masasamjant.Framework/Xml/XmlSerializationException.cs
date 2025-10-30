namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents exception thrown when error occurs while serializing object instance to XML.
    /// </summary>
    public class XmlSerializationException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="XmlSerializationException"/> class.
        /// </summary>
        /// <param name="instance">The object instance that was attempt to serialize.</param>
        /// <param name="message">The exception message.</param>
        public XmlSerializationException(object instance, string message)
            : this(instance, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="XmlSerializationException"/> class.
        /// </summary>
        /// <param name="instance">The object instance that was attempt to serialize.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public XmlSerializationException(object instance, string message, Exception? innerException)
            : base(message, innerException)
        {
            Instance = instance;
        }

        /// <summary>
        /// Gets the object instance that was attempt to serialize.
        /// </summary>
        public object Instance { get; }
    }
}
