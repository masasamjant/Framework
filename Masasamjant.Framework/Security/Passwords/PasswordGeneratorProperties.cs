namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents properties of generated password.
    /// </summary>
    public sealed class PasswordGeneratorProperties : IPasswordProperties, ICloneable
    {
        /// <summary>
        /// Default minimum length of 8, that is also lowest value of <see cref="MinLength"/>. 
        /// </summary>
        public const int DefaultMinLength = 8;

        /// <summary>
        /// Default maximum length of 200, that is also highest value of <see cref="MaxLength"/>.
        /// </summary>
        public const int DefaultMaxLength = 200;

        /// <summary>
        /// Default password complexity requirements.
        /// </summary>
        public const PasswordComplexity DefaultComplexity = PasswordComplexity.LowerCaseLetters | PasswordComplexity.UpperCaseLetters | PasswordComplexity.Numbers | PasswordComplexity.Specials;

        /// <summary>
        /// Initializes new instance of the <see cref="PasswordGeneratorProperties"/> class with default values.
        /// </summary>
        public PasswordGeneratorProperties()
            : this(DefaultMinLength, DefaultMaxLength, DefaultComplexity)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="PasswordGeneratorProperties"/> class.
        /// </summary>
        /// <param name="length">The password length.</param>
        /// <param name="complexity">The password complexity requirements.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="length"/> is less than <see cref="DefaultMinLength"/>.
        /// -of-
        /// If value of <paramref name="length"/> is greater than <see cref="DefaultMaxLength"/>.
        /// </exception>
        /// <exception cref="ArgumentException">If value of <paramref name="complexity"/> equals <see cref="PasswordComplexity.None"/>.</exception>
        public PasswordGeneratorProperties(int length, PasswordComplexity complexity)
            : this(length, length, complexity)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="PasswordGeneratorProperties"/> class 
        /// where password length is random between <paramref name="minLength"/> and <paramref name="maxLength"/>. 
        /// If <paramref name="minLength"/> and <paramref name="maxLength"/> are equal, then password lenght is static.
        /// </summary>
        /// <param name="minLength">The minimum password length.</param>
        /// <param name="maxLength">The maximum password length.</param>
        /// <param name="complexity">The password complexity requirements.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="minLength"/> is less than <see cref="DefaultMinLength"/>.
        /// -or-
        /// If value of <see cref="maxLength"/> is greater than <see cref="DefaultMaxLength"/>.
        /// -or-
        /// If value of <paramref name="minLength"/> is greater than value of <paramref name="maxLength"/>.
        /// </exception>
        /// <exception cref="ArgumentException">If value of <paramref name="complexity"/> equals <see cref="PasswordComplexity.None"/>.</exception>
        public PasswordGeneratorProperties(int minLength, int maxLength, PasswordComplexity complexity)
        {
            if (minLength < DefaultMinLength)
                throw new ArgumentOutOfRangeException(nameof(minLength), minLength, $"Min length must be greater than or equal to {DefaultMinLength}.");

            if (maxLength > DefaultMaxLength)
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, $"Max length must be less than or equal to {DefaultMaxLength}.");

            if (minLength > maxLength)
                throw new ArgumentOutOfRangeException(nameof(minLength), minLength, "Min length must be less than or equal to max length.");

            if (complexity.Equals(PasswordComplexity.None))
                throw new ArgumentException("At least one complexity requirement must be set.", nameof(complexity));

            MinLength = minLength;
            MaxLength = maxLength;
            Complexity = complexity;
        }

        /// <summary>
        /// Gets the password minimum length.
        /// </summary>
        public int MinLength { get; private set; }

        /// <summary>
        /// Gets the password maximum length.
        /// </summary>
        public int MaxLength { get; private set; }

        /// <summary>
        /// Gets the password complexity requirements.
        /// </summary>
        public PasswordComplexity Complexity { get; private set; }

        /// <summary>
        /// Gets how many special characters are included in password.
        /// Used only if <see cref="Complexity"/> has <see cref="PasswordComplexity.Specials"/> flag.
        /// </summary>
        /// <remarks>If password consist only from special characters, then whole password if filled with special characters.</remarks>
        public int? SpecialCharacterCount { get; private set; }

        /// <summary>
        /// Change password complexity requirements.
        /// </summary>
        /// <param name="complexity">The new password complexity requirements.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="complexity"/> equals <see cref="PasswordComplexity.None"/>.</exception>
        public void ChangeComplexity(PasswordComplexity complexity)
        {
            if (Complexity == complexity) return;

            if (complexity.Equals(PasswordComplexity.None))
                throw new ArgumentException("At least one complexity requirement must be set.", nameof(complexity));

            Complexity = complexity;
        }

        /// <summary>
        /// Change password minimum length.
        /// </summary>
        /// <param name="minLength">The new minimum password length.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="minLength"/> is less than <see cref="DefaultMinLength"/>.
        /// -or-
        /// If value of <paramref name="minLength"/> is greater than <see cref="DefaultMaxLength"/>.
        /// -or-
        /// If value of <paramref name="minLength"/> is greater than value of <see cref="MaxLength"/>.
        /// </exception>
        public void ChangeMinLength(int minLength)
        {
            if (MinLength == minLength) return;

            if (minLength < DefaultMinLength)
                throw new ArgumentOutOfRangeException(nameof(minLength), minLength, $"Min length must be greater than or equal to {DefaultMinLength}.");

            if (minLength > DefaultMaxLength)
                throw new ArgumentOutOfRangeException(nameof(minLength), minLength, $"Min length must be less than or equal to {DefaultMaxLength}.");

            if (minLength > MaxLength)
                throw new ArgumentOutOfRangeException(nameof(minLength), minLength, "Min length must be less than or equal to current max length.");

            MinLength = minLength;
        }

        /// <summary>
        /// Change password maximum length.
        /// </summary>
        /// <param name="maxLength">The new maximum password length.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="maxLength"/> is less than <see cref="DefaultMinLength"/>.
        /// -or-
        /// If value of <paramref name="maxLength"/> is greater than <see cref="DefaultMaxLength"/>.
        /// -or-
        /// If value of <paramref name="maxLength"/> is less than value of <see cref="MinLength"/>.
        /// </exception>
        public void ChangeMaxLength(int maxLength)
        {
            if (MaxLength == maxLength) return;

            if (maxLength < DefaultMinLength)
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, $"Max length must be greater than or equal to {DefaultMinLength}.");

            if (maxLength > DefaultMaxLength)
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, $"Max length must be less than or equal to {DefaultMaxLength}.");

            if (maxLength < MinLength)
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, "Max length must be greater than or equal to current min length.");

            MaxLength = maxLength;
        }

        /// <summary>
        /// Change <see cref="SpecialCharacterCount"/>.
        /// </summary>
        /// <param name="count">The new special character count.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> has value and value is less than 1.</exception>
        public void ChangeSpecialCharacterCount(int? count)
        {
            if (SpecialCharacterCount == count) return;

            if (count.HasValue && count.Value < 1)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Special character count must be greater than or equal to 1.");

            SpecialCharacterCount = count;
        }

        /// <summary>
        /// Creates copy from current instance.
        /// </summary>
        /// <returns>A copy from current instance.</returns>
        public PasswordGeneratorProperties Clone() => new(MinLength, MaxLength, Complexity)
        {
            SpecialCharacterCount = SpecialCharacterCount
        };

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
