using System.Runtime.Serialization;

namespace Masasamjant.Xml
{
    [DataContract]
    public class XmlData
    {
        [DataMember]
        public string Key { get; set; } = string.Empty;

        [DataMember]
        public string Value { get; set; } = string.Empty;
    }
}
