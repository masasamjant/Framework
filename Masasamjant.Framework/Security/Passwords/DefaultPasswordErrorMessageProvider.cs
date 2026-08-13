using Masasamjant.Security.Passwords.Resources;

namespace Masasamjant.Security.Passwords
{
    internal sealed class DefaultPasswordErrorMessageProvider : IPasswordErrorMessageProvider
    {
        public string? GetErrorMessage(PasswordInvalidityReason reason)
        {
            switch (reason)
            {
                case PasswordInvalidityReason.StartWithWhitespace:
                    return PasswordInvalidityReasonResource.StartWithWhitespace;
                case PasswordInvalidityReason.EndWithWhitespace:
                    return PasswordInvalidityReasonResource.EndWithWhitespace;
                case PasswordInvalidityReason.TooShort:
                    return PasswordInvalidityReasonResource.TooShort;
                case PasswordInvalidityReason.TooLong:
                    return PasswordInvalidityReasonResource.TooLong;
                case PasswordInvalidityReason.NotEnoughSpecialCharacters:
                    return PasswordInvalidityReasonResource.NotEnoughSpecialCharacters;
                case PasswordInvalidityReason.NotEnoughNumbers:
                    return PasswordInvalidityReasonResource.NotEnoughNumbers;
                case PasswordInvalidityReason.NoLowerCaseLetters:
                    return PasswordInvalidityReasonResource.NoLowerCaseLetters;
                case PasswordInvalidityReason.NoUpperCaseLetters:
                    return PasswordInvalidityReasonResource.NoUpperCaseLetters;
                case PasswordInvalidityReason.AllCharactersAreSame:
                    return PasswordInvalidityReasonResource.AllCharactersAreSame;
                case PasswordInvalidityReason.EmptyOrWhitespaceOnly:
                    return PasswordInvalidityReasonResource.EmptyOrWhitespaceOnly;
                default:
                    return null;
            }
        }
    }
}
