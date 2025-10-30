namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents factory to create instances of <see cref="IXmlSerializer"/> interface.
    /// </summary>
    public interface IXmlSerializerFactory
    {
        /// <summary>
        /// Creates instance of <see cref="IXmlSerializer"/> interface to serialize or deserialize 
        /// object of specified type.
        /// </summary>
        /// <param name="type">The type to serialize or deserialize.</param>
        /// <returns>A <see cref="IXmlSerializer"/>.</returns>
        IXmlSerializer CreateSerializer(Type type);
    }
}
