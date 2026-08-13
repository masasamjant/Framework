namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Provides helper methods for <see cref="TextBox"/> control.
    /// </summary>
    public static class TextBoxHelper
    {
        private static char currentPasswordCharacter = DefaultPasswordCharacter;
        private static readonly Lock currentPasswordCharacterLock = new Lock();

        /// <summary>
        /// Character set when no password character is displayed in <see cref="TextBox"/>.
        /// </summary>
        public const char NoPasswordCharacter = char.MinValue;

        /// <summary>
        /// Default password character.
        /// </summary>
        public const char DefaultPasswordCharacter = '*';

        /// <summary>
        /// Gets the current password character.
        /// </summary>
        public static char CurrentPasswordCharacter
        {
            get
            {
                return GetCurrentPasswordCharacter();
            }
        }

        /// <summary>
        /// Sets the password character for the specified <see cref="TextBox"/> control.
        /// </summary>
        /// <param name="textBox">The text box to set password character.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="textBox"/> is <c>null</c>.</exception>
        public static void SetPasswordCharacter(this TextBox textBox)
        {
            ArgumentNullException.ThrowIfNull(textBox);
            textBox.PasswordChar = CurrentPasswordCharacter;
        }

        /// <summary>
        /// Sets the password character for the specified <see cref="TextBox"/> control to <see cref="NoPasswordCharacter"/>.
        /// </summary>
        /// <param name="textBox">The text box to remove password character.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="textBox"/> is <c>null</c>.</exception>
        public static void RemovePasswordCharacter(this TextBox textBox)
        {
            ArgumentNullException.ThrowIfNull(textBox);
            textBox.PasswordChar = NoPasswordCharacter;
        }

        /// <summary>
        /// Specifies the character to use as password character. By default it is <see cref="DefaultPasswordCharacter"/>.
        /// </summary>
        /// <param name="c">The character to use as password character.</param>
        /// <exception cref="ArgumentException">If <paramref name="c"/> is <see cref="NoPasswordCharacter"/>.</exception>
        public static void UsePasswordCharacter(char c)
        {
            if (NoPasswordCharacter.Equals(c))
                throw new ArgumentException($"The password character cannot be {c}.", nameof(c));
            
            SetCurrentPasswordCharacter(c);
        }

        /// <summary>
        /// Specifies to use <see cref="DefaultPasswordCharacter"/> as password character.
        /// </summary>
        public static void UseDefaultPasswordCharacter()
        {
            SetCurrentPasswordCharacter(DefaultPasswordCharacter);
        }

        private static char GetCurrentPasswordCharacter()
        {
            lock (currentPasswordCharacterLock)
            {
                return currentPasswordCharacter;
            }
        }

        private static void SetCurrentPasswordCharacter(char c)
        {
            lock (currentPasswordCharacterLock)
            {
                currentPasswordCharacter = c;
            }
        }
    }
}
