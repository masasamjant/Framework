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

            // Get encrypt string properties.
            var encryptStringProperties = new List<PropertyInfo>();

            GetEncryptStringProperties(type, encryptStringProperties);

            // No any encrypt string properties.
            if (encryptStringProperties.Count == 0)
                return;

            // Get setter methods.
            var setMethods = GetSetMethods(type, encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            // Check each property and if value is string and has setter, then encrypt and set cipher text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
            {
                var value = encryptStringGetProperty.GetValue(instance, null);

                if (value is string clearText && setMethods.TryGetValue(encryptStringGetProperty.Name, out var setMethod))
                {
                    var cipherText = await Cryptography.EncryptStringAsync(clearText, password, salt, Encoding, cancellationToken);
                    setMethod.Invoke(instance, [cipherText]);
                }
            }
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

            // Get encrypt string properties.
            var encryptStringProperties = new List<PropertyInfo>();

            GetEncryptStringProperties(type, encryptStringProperties);

            // No any encrypt string properties.
            if (encryptStringProperties.Count == 0)
                return;

            // Get setter methods.
            var setMethods = GetSetMethods(type, encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            // Check each property and if value is string and has setter, then decrypt and set clear text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
            {
                var value = encryptStringGetProperty.GetValue(instance, null);

                if (value is string cipherText && setMethods.TryGetValue(encryptStringGetProperty.Name, out var setMethod))
                {
                    var clearText = await Cryptography.DecryptStringAsync(cipherText, password, salt, Encoding, cancellationToken);
                    setMethod.Invoke(instance, [clearText]);
                }
            }
        }

        protected static Dictionary<string, MethodInfo> GetSetMethods(Type type, IEnumerable<PropertyInfo> properties, bool allowNonPublicSetter)
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

        protected static void GetEncryptStringProperties(Type type, List<PropertyInfo> encryptStringProperties)
        {
            // Get public string properties.
            var properties = type.GetProperties(GetPropertyBinding())
                .Where(x => x.PropertyType.Equals(typeof(string)))
                .ToArray();

            if (properties.Length == 0)
                return;

            // Get encrypt string properties.
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<EncryptedStringPropertyAttribute>(true);
                if (attribute != null)
                    encryptStringProperties.Add(property);
            }
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

            // Get encrypt string properties.
            var encryptStringProperties = new List<PropertyInfo>();

            GetEncryptStringProperties(type, encryptStringProperties);

            // No any encrypt string properties.
            if (encryptStringProperties.Count == 0)
                return;

            // Get setter methods.
            var setMethods = GetSetMethods(type, encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            // Check each property and if value is string and has setter, then encrypt and set cipher text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
            {
                var value = encryptStringGetProperty.GetValue(instance, null);

                if (value is string clearText && setMethods.TryGetValue(encryptStringGetProperty.Name, out var setMethod))
                {
                    var cipherText = await Cryptography.EncryptStringAsync(clearText, key, Encoding, cancellationToken);
                    setMethod.Invoke(instance, [cipherText]);
                }
            }
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

            // Get encrypt string properties.
            var encryptStringProperties = new List<PropertyInfo>();

            GetEncryptStringProperties(type, encryptStringProperties);

            // No any encrypt string properties.
            if (encryptStringProperties.Count == 0)
                return;

            // Get setter methods.
            var setMethods = GetSetMethods(type, encryptStringProperties, AllowNonPublicSetter);

            if (setMethods.Count == 0)
                return;

            // Check each property and if value is string and has setter, then decrypt and set clear text as value.
            foreach (var encryptStringGetProperty in encryptStringProperties)
            {
                var value = encryptStringGetProperty.GetValue(instance, null);

                if (value is string cipherText && setMethods.TryGetValue(encryptStringGetProperty.Name, out var setMethod))
                {
                    var clearText = await Cryptography.DecryptStringAsync(cipherText, key, Encoding, cancellationToken);
                    setMethod.Invoke(instance, [clearText]);
                }
            }
        }

        private new IDataCryptography<TCryptoKey> Cryptography
        {
            get { return (IDataCryptography<TCryptoKey>)base.Cryptography; }
        }
    }
}
