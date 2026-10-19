using System.Runtime.CompilerServices;
using System.Xml;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Represents manager of <see cref="XmlDocumentTransactionLock"/> instances.
    /// </summary>
    internal static class XmlDocumentTransactionManager
    {
        private static readonly ConditionalWeakTable<XmlDocument, XmlDocumentTransactionLock> documentLocks
            = new ConditionalWeakTable<XmlDocument, XmlDocumentTransactionLock>();
        
        /// <summary>
        /// Gets or creates a lock for the specified XML document.
        /// </summary>
        /// <param name="document">The XML document for which to get or create the lock.</param>
        /// <returns>The <see cref="XmlDocumentTransactionLock"/> associated with the specified document.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="document"/> is null.</exception>
        public static XmlDocumentTransactionLock GetOrCreateLock(XmlDocument document)
        {
            ArgumentNullException.ThrowIfNull(document);
            return documentLocks.GetOrCreateValue(document);
        }
    }
}
