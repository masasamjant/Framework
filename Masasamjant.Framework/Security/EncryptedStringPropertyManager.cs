using Masasamjant.Security.Abstractions;
using System.Reflection;
using System.Text;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents manager that encrypts or decrypts properties of object decorated with <see cref="EncryptedStringPropertyAttribute"/>.
    /// </summary>
    public class EncryptedStringPropertyManager
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="EncryptedStringPropertyManager"/> class that use unicode encoding 
        /// and does not use non-public property setters.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography"/>.</param>
        public EncryptedStringPropertyManager(IDataCryptography cryptography)
            : this(cryptography, null, false)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="EncryptedStringPropertyManager"/> class does not use non-public property setters.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography"/>.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        public EncryptedStringPropertyManager(IDataCryptography cryptography, Encoding? encoding)
            : this(cryptography, encoding, false)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="EncryptedStringPropertyManager"/> class that use unicode encoding.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography"/>.</param>
        /// <param name="allowNonPublicSetter"><c>true</c> if allowed to use non-public setters; <c>false</c> to ignore properties with non-public setters.</param>
        public EncryptedStringPropertyManager(IDataCryptography cryptography, bool allowNonPublicSetter)
            : this(cryptography, null, allowNonPublicSetter)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="EncryptedStringPropertyManager"/> class.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography"/>.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        /// <param name="allowNonPublicSetter"><c>true</c> if allowed to use non-public setters; <c>false</c> to ignore properties with non-public setters.</param>
        public EncryptedStringPropertyManager(IDataCryptography cryptography, Encoding? encoding, bool allowNonPublicSetter)
        {
            Cryptography = cryptography;
            Encoding = encoding;
            AllowNonPublicSetter = allowNonPublicSetter;
        }

        /// <summary>
        /// Gets the <see cref="IDataCryptography"/>.
        /// </summary>
        protected IDataCryptography Cryptography { get; }

        /// <summary>
        /// Gets the encoding. If <c>null</c>, then use <see cref="Encoding.Unicode"/>.
        /// </summary>
        protected Encoding? Encoding { get; }

        /// <summary>
        /// Gets if or not it is allowed to use non-public property setter. If <c>true</c>, then can use non-public setter. 
        /// If <c>false</c>, then will use property only if it has public setter.
        /// </summary>
        protected bool AllowNonPublicSetter { get; }

        /// <summary>
        /// Encrypt properties of specified object instance that are decorated with <see cref="EncryptedStringPropertyAttribute"/>.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption.</returns>
        /// <remarks>Properties must be public and have type of string.</remarks>
        public async Task EncryptPropertiesAsync(object instance, string password, Salt salt, CancellationToken cancellationToken = default)
        {
            var type = instance.GetType();
            var encryptStringProperties = GetEncryptStringProperties(type);

            if (encryptStringProperties.Count == 0)
                return;

            var setMethods = GetSetMethods(encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            bool encryption = true;

            // Check each property and if value is string and has setter, then encrypt and set cipher text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
                await EncryptOrDecryptStringPropertyAsync(encryptStringGetProperty, encryption, setMethods, instance, password, salt, cancellationToken);
        }

        /// <summary>
        /// Decrypt properties of specified object instance that are decorated with <see cref="EncryptedStringPropertyAttribute"/>.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing decryption.</returns>
        /// <remarks>Properties must be public and have type of string.</remarks>
        public async Task DecryptPropertiesAsync(object instance, string password, Salt salt, CancellationToken cancellationToken = default)
        {
            var type = instance.GetType();
            var encryptStringProperties = GetEncryptStringProperties(type);

            if (encryptStringProperties.Count == 0)
                return;

            var setMethods = GetSetMethods(encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            bool encryption = false;

            // Check each property and if value is string and has setter, then decrypt and set clear text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
                await EncryptOrDecryptStringPropertyAsync(encryptStringGetProperty, encryption, setMethods, instance, password, salt, cancellationToken);
        }

        private async Task EncryptOrDecryptStringPropertyAsync(PropertyInfo encryptStringGetProperty, bool encryption, Dictionary<string, MethodInfo> setMethods, object instance, string password, Salt salt, CancellationToken cancellationToken)
        {
            var propertyValue = encryptStringGetProperty.GetValue(instance, null);

            if (propertyValue is string propertyValueText && setMethods.TryGetValue(encryptStringGetProperty.Name, out var setMethod))
            {
                var propertyValueTextResult = encryption
                    ? await Cryptography.EncryptStringAsync(propertyValueText, password, salt, Encoding, cancellationToken)
                    : await Cryptography.DecryptStringAsync(propertyValueText, password, salt, Encoding, cancellationToken);

                setMethod.Invoke(instance, [propertyValueTextResult]);
            }
        }

        /// <summary>
        /// Gets setter methods of the specified properties.
        /// </summary>
        /// <param name="properties">The properties to get setter methods.</param>
        /// <param name="allowNonPublicSetter"><c>true</c> to allow using non-public setter; <c>false</c> otherwise.</param>
        /// <returns>A dictionary where key is property name and value is setter method.</returns>
        protected static Dictionary<string, MethodInfo> GetSetMethods(IEnumerable<PropertyInfo> properties, bool allowNonPublicSetter)
        {
            var dictionary = new Dictionary<string, MethodInfo>();

            foreach (var property in properties)
            {
                var setMethod = property.GetSetMethod(allowNonPublicSetter);
                if (setMethod != null)
                    dictionary[property.Name] = setMethod;
            }

            return dictionary;
        }

        /// <summary>
        /// Gets properties decorated with <see cref="EncryptedStringPropertyAttribute"/> attribute.
        /// </summary>
        /// <param name="type">The type to get properties.</param>
        /// <returns>A collection of properties that was decorated with <see cref="EncryptedStringPropertyAttribute"/>.</returns>
        protected static IReadOnlyCollection<PropertyInfo> GetEncryptStringProperties(Type type)
        {
            var encryptStringProperties = new List<PropertyInfo>();

            // Get public string properties.
            var properties = type.GetProperties(GetPropertyBinding())
                .Where(x => x.PropertyType.Equals(typeof(string)))
                .ToArray();

            if (properties.Length == 0)
                return encryptStringProperties;

            // Get encrypt string properties.
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<EncryptedStringPropertyAttribute>(true);
                if (attribute != null)
                    encryptStringProperties.Add(property);
            }

            return encryptStringProperties.AsReadOnly();
        }

        protected static BindingFlags GetPropertyBinding()
            => BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance;
    }

    /// <summary>
    /// Represents manager that encrypts or decrypts properties of object decorated 
    /// with <see cref="EncryptedStringPropertyAttribute"/> using specified <typeparamref name="TCryptoKey"/>.
    /// </summary>
    /// <typeparam name="TCryptoKey">The type of the cryptography key.</typeparam>
    public sealed class EncryptedStringPropertyManager<TCryptoKey> : EncryptedStringPropertyManager where TCryptoKey : CryptoKey
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="EncryptedStringPropertyManager"/> class that use unicode encoding 
        /// and does not use non-public property setters.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography{TCryptoKey}"/>.</param>
        public EncryptedStringPropertyManager(IDataCryptography<TCryptoKey> cryptography)
            : this(cryptography, null, false)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="EncryptedStringPropertyManager"/> class does not use non-public property setters.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography{TCryptoKey}"/>.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        public EncryptedStringPropertyManager(IDataCryptography<TCryptoKey> cryptography, Encoding? encoding)
            : this(cryptography, encoding, false)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="EncryptedStringPropertyManager"/> class that use unicode encoding.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography{TCryptoKey}"/>.</param>
        /// <param name="allowNonPublicSetter"><c>true</c> if allowed to use non-public setters; <c>false</c> to ignore properties with non-public setters.</param>
        public EncryptedStringPropertyManager(IDataCryptography<TCryptoKey> cryptography, bool allowNonPublicSetter)
            : this(cryptography, null, allowNonPublicSetter)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="EncryptedStringPropertyManager"/> class.
        /// </summary>
        /// <param name="cryptography">The <see cref="IDataCryptography{TCryptoKey}"/>.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        /// <param name="allowNonPublicSetter"><c>true</c> if allowed to use non-public setters; <c>false</c> to ignore properties with non-public setters.</param>
        public EncryptedStringPropertyManager(IDataCryptography<TCryptoKey> cryptography, Encoding? encoding, bool allowNonPublicSetter)
            : base(cryptography, encoding, allowNonPublicSetter)
        { }

        /// <summary>
        /// Encrypt properties of specified object instance that are decorated with <see cref="EncryptedStringPropertyAttribute"/>.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="key">The cryptography key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption.</returns>
        /// <remarks>Properties must be public and have type of string.</remarks>
        public async Task EncryptPropertiesAsync(object instance, TCryptoKey key, CancellationToken cancellationToken = default)
        {
            var type = instance.GetType();
            var encryptStringProperties = GetEncryptStringProperties(type);

            if (encryptStringProperties.Count == 0)
                return;

            var setMethods = GetSetMethods(encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            bool encryption = true;

            // Check each property and if value is string and has setter, then encrypt and set cipher text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
                await EncryptOrDecryptStringPropertyAsync(encryptStringGetProperty, encryption, setMethods, instance, key, cancellationToken);
        }

        /// <summary>
        /// Decrypt properties of specified object instance that are decorated with <see cref="EncryptedStringPropertyAttribute"/>.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="key">The cryptography key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing decryption.</returns>
        /// <remarks>Properties must be public and have type of string.</remarks>
        public async Task DecryptPropertiesAsync(object instance, TCryptoKey key, CancellationToken cancellationToken = default)
        {
            var type = instance.GetType();
            var encryptStringProperties = GetEncryptStringProperties(type);

            if (encryptStringProperties.Count == 0)
                return;

            var setMethods = GetSetMethods(encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            bool encryption = false;

            // Check each property and if value is string and has setter, then decrypt and set clear text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
                await EncryptOrDecryptStringPropertyAsync(encryptStringGetProperty, encryption, setMethods, instance, key, cancellationToken);
        }

        private async Task EncryptOrDecryptStringPropertyAsync(PropertyInfo encryptStringGetProperty, bool encryption, Dictionary<string, MethodInfo> setMethods, object instance, TCryptoKey key, CancellationToken cancellationToken)
        {
            var propertyValue = encryptStringGetProperty.GetValue(instance, null);

            if (propertyValue is string propertyValueText && setMethods.TryGetValue(encryptStringGetProperty.Name, out var setMethod))
            {
                var propertyValueTextResult = encryption
                    ? await Cryptography.EncryptStringAsync(propertyValueText, key, Encoding, cancellationToken)
                    : await Cryptography.DecryptStringAsync(propertyValueText, key, Encoding, cancellationToken);

                setMethod.Invoke(instance, [propertyValueTextResult]);
            }
        }

        private new IDataCryptography<TCryptoKey> Cryptography
        {
            get { return (IDataCryptography<TCryptoKey>)base.Cryptography; }
        }
    }
}
