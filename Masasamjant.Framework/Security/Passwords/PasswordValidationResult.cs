namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents the result of password validation.
    /// </summary>
    public sealed class PasswordValidationResult
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PasswordValidationResult"/> class.
        /// </summary>
        /// <param name="reason">The invalidity reason.</param>
        /// <param name="errorMessage">The error message for the invalid password.</param>
        internal PasswordValidationResult(PasswordInvalidityReason reason, string? errorMessage)
        {
            Reason = reason;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the reason for password invalidity.
        /// </summary>
        public PasswordInvalidityReason Reason { get; }

        /// <summary>
        /// Gets if password was valid.
        /// </summary>
        public bool IsValid
        {
            get { return Reason == PasswordInvalidityReason.None; }
        }

        /// <summary>
        /// Gets the error message for invalid password.
        /// </summary>
        public string? ErrorMessage { get; }
    }
}
