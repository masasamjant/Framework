namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Defines environment where password is used.
    /// </summary>
    public enum PasswordEnvironment : int
    {
        /// <summary>
        /// Development environment. 
        /// Should be used for local development environments.
        /// </summary>
        Development = 0,

        /// <summary>
        /// Testing environment. 
        /// Should be used for staging and pre-production environments and 
        /// should be different from development environment.
        /// </summary>
        Testing = 1,

        /// <summary>
        /// Production environment.
        /// Should be used for production environments where password security is critical.
        /// </summary>
        Production = 2
    }
}
