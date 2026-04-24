namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents a provider of error messages for invalid password because of specified reason.
    /// </summary>
    public interface IPasswordErrorMessageProvider
    {
        /// <summary>
        /// Gets the error message for invalidity reason specified by <see cref="PasswordInvalidityReason"/>.
        /// </summary>
        /// <param name="reason">The reason for the password invalidity.</param>
        /// <returns>A error message corresponding to the specified reason, or null if no message is available.</returns>
        string? GetErrorMessage(PasswordInvalidityReason reason);
    }
}
