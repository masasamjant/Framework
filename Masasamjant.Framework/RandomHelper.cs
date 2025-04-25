using System.Security.Cryptography;

namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to work with random values.
    /// </summary>
    public static class RandomHelper
    {
        private static readonly char[] defaultCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        /// <summary>
        /// Gets default characters used in generating random string, if no other characters is specified.
        /// </summary>
        public static char[] DefaultCharacters
        {
            get { return (char[])defaultCharacters.Clone(); }
        }

        /// <summary>
        /// Creates new <see cref="Random"/> instance.
        /// </summary>
        /// <returns>A <see cref="Random"/>.</returns>
        public static Random CreateRandom()
        {
            var buffer = RandomNumberGenerator.GetBytes(3);
            int seed = 129 * buffer[0] * buffer[1] * buffer[2];
            return new Random(seed);
        }

        /// <summary>
        /// Generates random string.
        /// </summary>
        /// <param name="random">The <see cref="Random"/>.</param>
        /// <param name="length">The length of generated string.</param>
        /// <param name="characters">The array of characters to use. If <c>null</c> or empty, then uses <see cref="DefaultCharacters"/>.</param>
        /// <returns>A random string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="length"/> is less than 0.</exception>
        public static string GetString(this Random random, int length, char[]? characters = null)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "The value must be greater than or equal to 0.");

            if (length == 0)
                return string.Empty;

            if (characters == null || characters.Length == 0)
                characters = DefaultCharacters;

            int min = 0;
            int max = characters.Length;
            var array = new char[length];

            for (int count = 0; count < length; count++)
            {
                int index = random.Next(min, max);
                array[count] = characters[index];
            }

            return new string(array);
        }

        /// <summary>
        /// Generates array of random bytes.
        /// </summary>
        /// <param name="random">The <see cref="Random"/>.</param>
        /// <param name="length">The count how many bytes to create.</param>
        /// <returns>A array of random bytes.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="length"/> is less than 0.</exception>
        public static byte[] GetBytes(this Random random, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "The value must be greater than or equal to 0.");

            if (length == 0)
                return [];

            var buffer = new byte[length];
            random.NextBytes(buffer);
            return buffer;
        }

        /// <summary>
        /// Gets random value from the specified collection of values.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="random">The <see cref="Random"/>.</param>
        /// <param name="values">The values.</param>
        /// <returns>A random value from <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="values"/> is empty.</exception>
        public static T GetValue<T>(this Random random, IEnumerable<T> values)
        {
            var array = values.ToArray();

            if (array.Length == 0)
                throw new ArgumentException("At least one value must be set.", nameof(values));

            int index = random.Next(0, array.Length);
            return array[index];
        }

        /// <summary>
        /// Gets random <see cref="DateTime"/> value from the specified collection of values.
        /// </summary>
        /// <param name="random">The <see cref="Random"/>.</param>
        /// <param name="values">The values.</param>
        /// <returns>A random <see cref="DateTime"/> from <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="values"/> is empty.</exception>
        public static DateTime GetDateTime(this Random random, IEnumerable<DateTime> values)
            => GetValue(random, values);
    }
}
