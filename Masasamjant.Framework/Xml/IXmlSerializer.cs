using System.Xml;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents XML serializer.
    /// </summary>
    public interface IXmlSerializer
    {
        /// <summary>
        /// Deserialize XML to object instance.
        /// </summary>
        /// <param name="xml">The XML markup.</param>
        /// <returns>A deserialized object or <c>null</c>.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="xml"/>.</exception>
        object? Deserialize(string xml);

        /// <summary>
        /// Deserialize XML of specified <see cref="XmlDocument"/> to object instance.
        /// </summary>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <returns>A deserialized object or <c>null</c>.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="document"/>.</exception>
        object? Deserialize(XmlDocument document);

        /// <summary>
        /// Deserialize XML to <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of deserialized instance.</typeparam>
        /// <param name="xml">The XML markup.</param>
        /// <returns>A <typeparamref name="T"/> instance or default.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="xml"/>.</exception>
        T? Deserialize<T>(string xml);

        /// <summary>
        /// Deserialize XML of specified <see cref="XmlDocument"/> to <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of deserialized instance.</typeparam>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance or default.</returns>
        /// <exception cref="XmlDeserializationException">If exception occurs when deserializing <paramref name="document"/>.</exception>
        T? Deserialize<T>(XmlDocument document);

        /// <summary>
        /// Serialize object instance to XML markup.
        /// </summary>
        /// <param name="instance">The object instance to serialize.</param>
        /// <returns>A XML markup.</returns>
        /// <exception cref="XmlSerializationException">If exception occurs when serializing <paramref name="instance"/>.</exception>
        string Serialize(object instance);
    }
}
