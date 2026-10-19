using Masasamjant.ComponentModel;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents a lock for an XML document transaction, allowing only one transaction to hold the lock for a specific XML document at a time.
    /// </summary>
    internal sealed class XmlDocumentTransactionLock : ObjectInstanceLock<XmlDocumentTransaction>
    {

    }
}
