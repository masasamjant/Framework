using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents XML serializer that use <see cref="XmlSerializer"/> for serialization.
    /// </summary>
    public sealed class XmlDataSerializer : BaseXmlSerializer, IXmlSerializer
    {
        private readonly XmlSerializer serializer;

        /// <summary>
        /// Initializes new instance of the <see cref="XmlDataContractSerializer"/> class with specified type.
        /// </summary>
        /// <param name="type">The type to serialize or deserialize.</param>
        public XmlDataSerializer(Type type)
            : this(new XmlSerializer(type))
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="XmlDataContractSerializer"/> class with specified <see cref="DataContractSerializer"/>.
        /// </summary>
        /// <param name="serializer">The <see cref="DataContractSerializer"/>.</param>
        public XmlDataSerializer(XmlSerializer serializer)
        {
            this.serializer = serializer;
        }

        /// <summary>
        /// Deserialize XML of specified <see cref="XmlDocument"/> to object instance.
        /// </summary>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <returns>A deserialized object or <c>null</c>.</returns>
        public override object? Deserialize(XmlDocument document)
        {
            try
            {
                object? obj;

                using (var reader = new XmlNodeReader(document))
                    obj = serializer.Deserialize(reader);

                return obj;
            }
            catch (Exception exception)
            {
                throw new XmlDeserializationException(document, "Error during deserialization of XML document.", exception);
            }
        }

        /// <summary>
        /// Serialize object instance to XML markup.
        /// </summary>
        /// <param name="instance">The object instance to serialize.</param>
        /// <returns>A XML markup.</returns>
        /// <exception cref="XmlSerializationException">If exception occurs when serializing <paramref name="instance"/>.</exception>
        public override string Serialize(object instance)
        {
            try
            {
                var builder = new StringBuilder();
                using (var writer = new StringWriter(builder))
                using (var xml = XmlWriter.Create(writer))
                {
                    serializer.Serialize(xml, instance);
                    writer.Flush();
                }
                return builder.ToString();
            }
            catch (Exception exception)
            {
                throw new XmlSerializationException(instance, "Error during serialization of object instance.", exception);
            }
        }
    }
}
