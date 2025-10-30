namespace Masasamjant.Xml
{
    /// <summary>
    /// Defines what XML serializator is created by <see cref="XmlSerializerFactory"/>.
    /// </summary>
    public enum XmlSerialization : int
    {
        /// <summary>
        /// Create <see cref="XmlDataSerializer"/>.
        /// </summary>
        Xml = 0,

        /// <summary>
        /// Create <see cref="XmlDataContractSerializer"/>.
        /// </summary>
        Contract = 1
    }
}
