namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Provides helper methods to <see cref="IPasswordProperties"/> interface.
    /// </summary>
    public static class PasswordPropertiesHelper
    {
        /// <summary>
        /// Gets the required number of special characters based on the password properties or password length.
        /// </summary>
        /// <param name="properties">The password properties.</param>
        /// <param name="length">The password length.</param>
        /// <returns>A required number of special characters.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="length"/> is less than 1.</exception>
        public static int GetSpecialCharacterCount(this IPasswordProperties properties, int length)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException(nameof(length), length, "The length must be greater than 0.");

            if (properties.SpecialCharacterCount.HasValue)
                return properties.SpecialCharacterCount.Value;
            else
            {
                if (length <= 8)
                    return 1;
                else if (length <= 16)
                    return 2;
                else if (length <= 32)
                    return 4;
                else if (length <= 64)
                    return 6;
                else if (length <= 128)
                    return 8;
                return 10;
            }
        }
    }
}
