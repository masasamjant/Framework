namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents password validator.
    /// </summary>
    public sealed class PasswordValidator
    {
        private readonly IPasswordProperties properties;
        private readonly IPasswordErrorMessageProvider errorMessageProvider;

        /// <summary>
        /// Initializes new instance of the <see cref="PasswordValidator"/> class with default message provider.
        /// </summary>
        /// <param name="properties">The password properties.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is <c>null</c>.</exception>
        public PasswordValidator(IPasswordProperties properties)
            : this(properties, new DefaultPasswordErrorMessageProvider())
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="PasswordValidator"/> class.
        /// </summary>
        /// <param name="properties">The password properties.</param>
        /// <param name="errorMessageProvider">The error message provider.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="properties"/> or <paramref name="errorMessageProvider"/> is <c>null</c>.</exception>
        public PasswordValidator(IPasswordProperties properties, IPasswordErrorMessageProvider errorMessageProvider)
        {
            this.properties = properties ?? throw new ArgumentNullException(nameof(properties));
            this.errorMessageProvider = errorMessageProvider ?? throw new ArgumentNullException(nameof(errorMessageProvider));
        }

        /// <summary>
        /// Validates specified password.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>A password validation result.</returns>
        public PasswordValidationResult ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return CreateInvalidResult(PasswordInvalidityReason.EmptyOrWhitespaceOnly);

            char first = password[0];
            char last = password[password.Length - 1];

            if (char.IsWhiteSpace(first))
                return CreateInvalidResult(PasswordInvalidityReason.StartWithWhitespace);

            if (char.IsWhiteSpace(last))
                return CreateInvalidResult(PasswordInvalidityReason.EndWithWhitespace);

            if (password.All(c => c == first))
                return CreateInvalidResult(PasswordInvalidityReason.AllCharactersAreSame);

            if (password.Length < properties.MinLength)
                return CreateInvalidResult(PasswordInvalidityReason.TooShort);

            if (password.Length > properties.MaxLength)
                return CreateInvalidResult(PasswordInvalidityReason.TooLong);

            bool includeSpecials = properties.Complexity.HasFlag(PasswordComplexity.Specials);
            bool includeNumbers = properties.Complexity.HasFlag(PasswordComplexity.Numbers);
            bool includeLowerCaseLetters = properties.Complexity.HasFlag(PasswordComplexity.LowerCaseLetters);
            bool includeUpperCaseLetters = properties.Complexity.HasFlag(PasswordComplexity.UpperCaseLetters);
            int len = password.Length;
            int requiredSpecialCount = includeSpecials ? properties.GetSpecialCharacterCount(len) : 0;
            int requiredNumberCount = includeNumbers ? Math.Max(requiredSpecialCount, 1) + 1 : 0;

            if (includeSpecials && !HasRequiredSpecialCharacters(password, requiredSpecialCount))
                return CreateInvalidResult(PasswordInvalidityReason.NotEnoughSpecialCharacters);

            if (includeNumbers && !HasRequiredNumberCharacters(password, requiredNumberCount))
                return CreateInvalidResult(PasswordInvalidityReason.NotEnoughNumbers);

            if (includeLowerCaseLetters && !password.Any(PasswordCharacters.LowerCaseLetters.Contains))
                return CreateInvalidResult(PasswordInvalidityReason.NoLowerCaseLetters);

            if (includeUpperCaseLetters && !password.Any(PasswordCharacters.UpperCaseLetters.Contains))
                return CreateInvalidResult(PasswordInvalidityReason.NoUpperCaseLetters);

            return new PasswordValidationResult(PasswordInvalidityReason.None, null);
        }

        private static bool HasRequiredSpecialCharacters(string password, int requiredSpecialCount)
            => HasExpectedCountOfCharacters(password, requiredSpecialCount, PasswordCharacters.Specials);

        private static bool HasRequiredNumberCharacters(string password, int requiredNumberCount)
            => HasExpectedCountOfCharacters(password, requiredNumberCount, PasswordCharacters.Numbers);

        private static bool HasExpectedCountOfCharacters(string password, int requiredCount, IList<char> characters)
        {
            int count = 0;

            foreach (char c in password)
            {
                if (characters.Contains(c))
                {
                    count++;
                    if (count == requiredCount)
                        return true;
                }
            }
            return false;
        }

        private PasswordValidationResult CreateInvalidResult(PasswordInvalidityReason reason)
        {
            var message = errorMessageProvider.GetErrorMessage(reason);
            return new PasswordValidationResult(reason, message);
        }
    }
}
