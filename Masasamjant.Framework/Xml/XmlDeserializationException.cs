using System.Xml;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents exception thrown when error occurs while deserializing XML document or markup. 
    /// </summary>
    public class XmlDeserializationException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="XmlDeserializationException"/> class.
        /// </summary>
        /// <param name="xml">The XML markup attempt to deserialize.</param>
        /// <param name="message">The exception message.</param>
        public XmlDeserializationException(string xml, string message)
            : this(xml, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="XmlDeserializationException"/> class.
        /// </summary>
        /// <param name="xml">The XML markup attempt to deserialize.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public XmlDeserializationException(string xml, string message, Exception? innerException)
            : base(message, innerException)
        {
            Xml = xml;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="XmlDeserializationException"/> class.
        /// </summary>
        /// <param name="document">The XML document attempt to deserialize.</param>
        /// <param name="message">The exception message.</param>
        public XmlDeserializationException(XmlDocument document, string message)
            : this(document, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="XmlDeserializationException"/> class.
        /// </summary>
        /// <param name="document">The XML document attempt to deserialize.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public XmlDeserializationException(XmlDocument document, string message, Exception? innerException)
            : base(message, innerException)
        {
            Document = document;
        }

        /// <summary>
        /// Gets the XML markup attempt to deserialize.
        /// </summary>
        public string? Xml { get; }

        /// <summary>
        /// Gets the XML document attempt to deserialize.
        /// </summary>
        public XmlDocument? Document { get; }
    }
}
