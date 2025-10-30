using System.Xml;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents abstract <see cref="IXmlSerializer"/>.
    /// </summary>
    public abstract class BaseXmlSerializer : IXmlSerializer
    {
        /// <summary>
        /// Deserialize XML of specified <see cref="XmlDocument"/> to <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of deserialized instance.</typeparam>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance or default.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="document"/>.</exception>
        public T? Deserialize<T>(XmlDocument document)
        {
            var obj = Deserialize(document);
            return obj is T result ? result : default;
        }

        /// <summary>
        /// Deserialize XML to <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of deserialized instance.</typeparam>
        /// <param name="xml">The XML markup.</param>
        /// <returns>A <typeparamref name="T"/> instance or default.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="xml"/>.</exception>
        public T? Deserialize<T>(string xml)
        {
            var obj = Deserialize(xml);
            return obj is T result ? result : default;
        }

        /// <summary>
        /// Deserialize XML to object instance.
        /// </summary>
        /// <param name="xml">The XML markup.</param>
        /// <returns>A deserialized object or <c>null</c>.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="xml"/>.</exception>
        public object? Deserialize(string xml)
        {
            try
            {
                var document = new XmlDocument();
                document.LoadXml(xml);
                return Deserialize(document);
            }
            catch (Exception exception)
            {
                if (exception is XmlDeserializationException)
                    throw;
                else
                    throw new XmlDeserializationException(xml, "Error during deserialization of XML markup.", exception);
            }
        }

        /// <summary>
        /// Deserialize XML of specified <see cref="XmlDocument"/> to object instance.
        /// </summary>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <returns>A deserialized object or <c>null</c>.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="document"/>.</exception>
        public abstract object? Deserialize(XmlDocument document);

        /// <summary>
        /// Serialize object instance to XML markup.
        /// </summary>
        /// <param name="instance">The object instance to serialize.</param>
        /// <returns>A XML markup.</returns>
        /// <exception cref="XmlSerializationException">If exception occurs when serializing <paramref name="instance"/>.</exception>
        public abstract string Serialize(object instance);
    }
}
