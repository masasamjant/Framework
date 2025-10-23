namespace Masasamjant.Security
{
    /// <summary>
    /// Represents attribute applied to string property to indicate that it should be used 
    /// in encryption by <see cref="EncryptedStringPropertyManager"/> or <see cref="EncryptedStringPropertyManager{TCryptoKey}"/> class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class EncryptedStringPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EncryptedStringPropertyAttribute"/> class.
        /// </summary>
        public EncryptedStringPropertyAttribute()
        { }
    }
}
