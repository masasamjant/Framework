namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents identity of the user.
    /// </summary>
    public interface IUserIdentity
    {
        /// <summary>
        /// Gets the user identity as string. This can be user name, identifier, email address etc. 
        /// The value should be something that uniquely identifies user. 
        /// If returns empty string, then user is considered to be anonymous.
        /// </summary>
        string Identity { get; }
    }
}
