namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents <see cref="IXmlSerializerFactory"/> that creates <see cref="XmlDataSerializer"/> or <see cref="XmlDataContractSerializer"/> instances.
    /// </summary>
    public sealed class XmlSerializerFactory : IXmlSerializerFactory
    {
        private readonly XmlSerialization serialization;

        /// <summary>
        /// Initializes new instance of the <see cref="XmlSerializerFactory"/> class.
        /// </summary>
        /// <param name="serialization">The <see cref="XmlSerialization"/>.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="serialization"/> is not defined.</exception>
        public XmlSerializerFactory(XmlSerialization serialization)
        {
            if (!Enum.IsDefined(serialization))
                throw new ArgumentException("The value is not defined.", nameof(serialization));

            this.serialization = serialization;
        }

        /// <summary>
        /// Creates instance of <see cref="IXmlSerializer"/> interface to serialize or deserialize 
        /// object of specified type.
        /// </summary>
        /// <param name="type">The type to serialize or deserialize.</param>
        /// <returns>A <see cref="IXmlSerializer"/>.</returns>
        public IXmlSerializer CreateSerializer(Type type)
        {
            if (serialization == XmlSerialization.Contract)
                return new XmlDataContractSerializer(type);
            else
                return new XmlDataSerializer(type);
        }
    }
}
