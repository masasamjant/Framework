using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Masasamjant.Xml
{
    /// <summary>
    /// Provides helper methods to work with XML data.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Check if specified <see cref="XmlNode"/> is an element of specified name.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/>.</param>
        /// <param name="name">The element name.</param>
        /// <returns><c>true</c> if <paramref name="node"/> represents XML element with specified name; <c>false</c> otherwise.</returns>
        public static bool IsElementOf(this XmlNode node, string name) => node.NodeType == XmlNodeType.Element && node.Name == name;

        /// <summary>
        /// Check if specified <see cref="XmlNode"/> is an end element of specified name.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/>.</param>
        /// <param name="name">The element name.</param>
        /// <returns><c>true</c> if <paramref name="node"/> represents end of XML element with specified name; <c>false</c> otherwise.</returns>
        public static bool IsEndElementOf(this XmlNode node, string name) => node.NodeType == XmlNodeType.EndElement && node.Name == name;

        /// <summary>
        /// Check if specified <see cref="XmlNode"/> is an attribute of specified name.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/>.</param>
        /// <param name="name">The attribute name.</param>
        /// <returns><c>true</c> if <paramref name="node"/> represents XML attribute with specified name; <c>false</c> otherwise.</returns>
        public static bool IsAttributeOf(this XmlNode node, string name) => node.NodeType == XmlNodeType.Attribute && node.Name == name;

        /// <summary>
        /// Gets XML attribute from specified <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/>.</param>
        /// <param name="attributeName">The attribute name.</param>
        /// <param name="mandatory"><c>true</c> if attribute is mandatory and should exist; <c>false</c> otherwise.</param>
        /// <returns>A found <see cref="XmlAttribute"/> or <c>null</c>, if <paramref name="mandatory"/> is <c>false</c>.</returns>
        /// <exception cref="XmlException">If <paramref name="mandatory"/> is <c>true</c> and attribute does not exist.</exception>
        public static XmlAttribute? GetAttribute(this XmlNode node, string attributeName, bool mandatory)
        {
            ArgumentNullException.ThrowIfNull(node);
            ArgumentNullException.ThrowIfNull(attributeName);

            if (IsAttributeOf(node, attributeName))
                return (XmlAttribute)node;

            bool exists = TryGetAttribute(node, attributeName, out XmlAttribute? attribute);

            if (exists)
                return attribute;

            if (mandatory)
                throw new XmlException($"The attribute '{attributeName}' is mandatory.");

            return null;
        }

        /// <summary>
        /// Try get XML attribute from specified <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/>.</param>
        /// <param name="attributeName">The attribute name.</param>
        /// <param name="attribute">The <see cref="XmlAttribute"/> if returns <c>true</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if <paramref name="node"/> has attribute with specified name; <c>false</c> otherwise.</returns>
        public static bool TryGetAttribute(this XmlNode node, string attributeName, [MaybeNullWhen(false)] out XmlAttribute attribute)
        {
            ArgumentNullException.ThrowIfNull(node);
            ArgumentNullException.ThrowIfNull(attributeName);

            attribute = null;

            if (IsAttributeOf(node, attributeName))
                attribute = (XmlAttribute)node;
            else
            {
                if (node.Attributes == null || node.Attributes.Count == 0)
                    return false;
                attribute = node.Attributes[attributeName];
            }

            return attribute != null;
        }

        /// <summary>
        /// Write XML element using specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/>.</param>
        /// <param name="elementName">The element name.</param>
        /// <param name="elementValue">The element value.</param>
        /// <param name="prefix">The namespace prefix or <c>null</c>.</param>
        /// <param name="ns">The namespace or <c>null</c>.</param>
        public static void WriteElement(this XmlWriter writer, string elementName, string? elementValue, string? prefix = null, string? ns = null)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(elementName);

            WriteXmlElement(writer, elementName, elementValue, prefix, ns);
        }

        /// <summary>
        /// Write XML element using specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/>.</param>
        /// <param name="elementName">The element name.</param>
        /// <param name="elementValue">The element value.</param>
        /// <param name="prefix">The namespace prefix or <c>null</c>.</param>
        /// <param name="ns">The namespace or <c>null</c>.</param>
        public static async Task WriteElementAsync(this XmlWriter writer, string elementName, string? elementValue, string? prefix = null, string? ns = null)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(elementName);

            await WriteXmlElementAsync(writer, elementName, elementValue, prefix, ns);
        }

        /// <summary>
        /// Write XML elements using specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/>.</param>
        /// <param name="elementName">The element name.</param>
        /// <param name="elementValues">The element values.</param>
        /// <param name="prefix">The namespace prefix or <c>null</c>.</param>
        /// <param name="ns">The namespace or <c>null</c>.</param>
        public static void WriteElements(this XmlWriter writer, string elementName, IEnumerable<string?> elementValues, string? prefix = null, string? ns = null)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(elementName);
            ArgumentNullException.ThrowIfNull(elementValues);

            foreach (var elementValue in elementValues)
                WriteXmlElement(writer, elementName, elementValue, prefix, ns);
        }

        /// <summary>
        /// Write XML elements using specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/>.</param>
        /// <param name="elementName">The element name.</param>
        /// <param name="elementValues">The element values.</param>
        /// <param name="prefix">The namespace prefix or <c>null</c>.</param>
        /// <param name="ns">The namespace or <c>null</c>.</param>
        public static async Task WriteElementsAsync(this XmlWriter writer, string elementName, IEnumerable<string?> elementValues, string? prefix = null, string? ns = null)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(elementName);
            ArgumentNullException.ThrowIfNull(elementValues);

            foreach (var elementValue in elementValues)
                await WriteXmlElementAsync(writer, elementName, elementValue, prefix, ns);
        }

        /// <summary>
        /// Write XML elements using specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/>.</param>
        /// <param name="elements">The element names and values.</param>
        /// <param name="prefix">The namespace prefix or <c>null</c>.</param>
        /// <param name="ns">The namespace or <c>null</c>.</param>
        public static void WriteElements(this XmlWriter writer, IDictionary<string, string?> elements, string? prefix = null, string? ns = null)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(elements);

            foreach (var keyValue in elements)
                WriteXmlElement(writer, keyValue.Key, keyValue.Value, prefix, ns);
        }

        /// <summary>
        /// Write XML elements using specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/>.</param>
        /// <param name="elements">The element names and values.</param>
        /// <param name="prefix">The namespace prefix or <c>null</c>.</param>
        /// <param name="ns">The namespace or <c>null</c>.</param>
        public static async Task WriteElementsAsync(this XmlWriter writer, IDictionary<string, string?> elements, string? prefix = null, string? ns = null)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(elements);

            foreach (var keyValue in elements)
                await WriteXmlElementAsync(writer, keyValue.Key, keyValue.Value, prefix, ns);
        }

        /// <summary>
        /// Append checksum attribute to specified <see cref="XmlDocument"/>. Checksum is computed from content of the document before 
        /// appending the attribute to the root element.
        /// </summary>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <param name="rootElementName">The root element name.</param>
        /// <param name="checksumAttributeName">The checksum attribute name.</param>
        /// <exception cref="ArgumentException">If <paramref name="document"/> already has attribute with name of <paramref name="checksumAttributeName"/>.</exception>
        public static void AppendChecksumAttribute(XmlDocument document, string rootElementName, string checksumAttributeName)
        {
            ArgumentNullException.ThrowIfNull(document);
            var rootElement = GetRootElementWithAttributes(document, rootElementName);
            var attribute = GetAttribute(rootElement, checksumAttributeName, false);

            if (attribute != null)
                throw new ArgumentException($"The checksum attribute name '{checksumAttributeName}' is not unique. Element already has attribute with same name.", nameof(checksumAttributeName));

            var checksum = CalculateXmlChecksum(document);
            attribute = document.CreateAttribute(checksumAttributeName);
            attribute.Value = checksum;
            rootElement.Attributes?.Append(attribute);
        }

        /// <summary>
        /// Verify checksum attribute appended to <see cref="XmlDocument"/> using <see cref="AppendChecksumAttribute(XmlDocument, string, string)"/> method. 
        /// Removes the attribute from document before computing the checksum from content and then verifies against the attribute value. If checksum match, then
        /// checksum attribute is removed from document.
        /// </summary>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <param name="rootElementName">The root element name.</param>
        /// <param name="checksumAttributeName">The checksum attribute name.</param>
        /// <returns><c>true</c> if checksum matches; <c>false</c> otherwise.</returns>
        /// <exception cref="XmlException">
        /// If document has not root element with name specified by <paramref name="rootElementName"/>.
        /// -or-
        /// If root element is not XML element.
        /// -or-
        /// If root element has no attribute with name specified by <paramref name="checksumAttributeName"/>.
        /// </exception>
        public static bool VerifyChecksumAttribute(XmlDocument document, string rootElementName, string checksumAttributeName)
        {
            ArgumentNullException.ThrowIfNull(document);
            var rootElement = GetRootElementWithAttributes(document, rootElementName);
            var attribute = GetAttribute(rootElement, checksumAttributeName, false);
            if (attribute == null)
                throw new XmlException($"The element '{rootElementName}' has not '{checksumAttributeName}' attribute.");
            var currentChecksum = attribute?.Value ?? string.Empty;
            rootElement.Attributes?.Remove(attribute);
            var checksum = CalculateXmlChecksum(document);
            bool result = string.Equals(currentChecksum, checksum, StringComparison.Ordinal);
            if (!result)
                rootElement.Attributes?.Append(attribute!);
            return result;
        }

        /// <summary>
        /// Gets content of <see cref="XmlDocument"/> as XML string.
        /// </summary>
        /// <param name="document">The <see cref="XmlDocument"/>.</param>
        /// <returns>A XML string.</returns>
        public static string ToXml(this XmlDocument document, XmlWriterSettings? settings = null)
        {
            ArgumentNullException.ThrowIfNull(document);
            var encoding = settings?.Encoding ?? document.GetEncoding();
            var builder = new StringBuilder();
            var writer = new XmlHelperStringWriter(builder, encoding);
            var xml = XmlWriter.Create(writer, settings);
            document.Save(xml);
            xml.Flush();
            return builder.ToString();
        }

        /// <summary>
        /// Creates an asynchronous XML writer that writes XML data to the specified text output using the provided settings.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to which the XML content will be written. Cannot be null.</param>
        /// <param name="settings">The optional <see cref="XmlWriterSettings"/> that configure the behavior of the XML writer. If null, default settings are used.</param>
        /// <returns>An <see cref="XmlWriter"/> instance configured for asynchronous operations that writes to the specified <see cref="TextWriter"/>.</returns>
        /// <remarks>The Async property of the provided or default XmlWriterSettings is set to <see
        /// langword="true"/>, enabling asynchronous writing. Ensure that the <see cref="TextWriter"/> supports asynchronous
        /// operations for optimal performance.</remarks>
        public static XmlWriter CreateAsyncWriter(TextWriter writer, XmlWriterSettings? settings = null)
        {
            ArgumentNullException.ThrowIfNull(writer);

            if (settings == null)
                settings = new XmlWriterSettings();

            settings.Async = true;
            return XmlWriter.Create(writer, settings);
        }

        /// <summary>
        /// Gets the encoding used in XML document or if not specified, then UTF8 encoding.
        /// </summary>
        /// <param name="document">The XML document.</param>
        /// <returns>A encoding found from XML document or UTF8 encoding.</returns>
        public static Encoding GetEncoding(this XmlDocument document)
        {
            if (document.FirstChild is XmlDeclaration declaration && !string.IsNullOrWhiteSpace(declaration.Encoding))
            {
                try
                {
                    return Encoding.GetEncoding(declaration.Encoding);
                }
                catch (Exception)
                {
                    return Encoding.UTF8;
                }
            }

            return Encoding.UTF8;
        }

        private static string CalculateXmlChecksum(XmlDocument document)
        {
            var xml = document.ToXml();
            var encoding = document.GetEncoding();
            var buffer = encoding.GetBytes(xml);
            buffer = SHA256.HashData(buffer);
            return Convert.ToBase64String(buffer);
        }

        private static XmlNode GetRootElementWithAttributes(XmlDocument document, string rootElementName)
        {
            XmlNode? rootElement = document.SelectSingleNode($"/{rootElementName}");

            if (rootElement == null)
                throw new XmlException($"The document has not '{rootElementName}' root element.");

            if (rootElement.Attributes == null)
                throw new XmlException($"The element '{rootElementName}' is not XML element.");

            return rootElement;
        }

        private static void WriteXmlElement(XmlWriter writer, string elementName, string? elementValue, string? prefix = null, string? ns = null)
        {
            writer.WriteStartElement(prefix, elementName, ns);
            writer.WriteValue(elementValue ?? string.Empty);
            writer.WriteEndElement();
        }

        private static async Task WriteXmlElementAsync(XmlWriter writer, string elementName, string? elementValue, string? prefix = null, string? ns = null)
        {
            await writer.WriteStartElementAsync(prefix, elementName, ns);
            writer.WriteValue(elementValue ?? string.Empty);
            await writer.WriteEndElementAsync();
        }

        private class XmlHelperStringWriter : StringWriter
        {
            private readonly Encoding encoding;

            public XmlHelperStringWriter(StringBuilder sb, Encoding encoding)
                : base(sb)
            {
                this.encoding = encoding;
            }

            public override Encoding Encoding => encoding;
        }
    }
}
