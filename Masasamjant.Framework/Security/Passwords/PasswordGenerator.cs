using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents password generator.
    /// </summary>
    public sealed class PasswordGenerator
    {
        private readonly PasswordGeneratorProperties properties;
        private readonly Random random;

        /// <summary>
        /// Initializes new instance of the <see cref="PasswordGenerator"/> class.
        /// </summary>
        /// <param name="properties">The <see cref="PasswordGeneratorProperties"/>.</param>
        public PasswordGenerator(PasswordGeneratorProperties properties)
        {
            ArgumentNullException.ThrowIfNull(properties);
            this.random = RandomHelper.CreateRandom();
            this.properties= properties;
        }

        /// <summary>
        /// Generates random password.
        /// </summary>
        /// <returns>A password value.</returns>
        public string Generate()
        {
            var currentProperties = properties.Clone();
            var requirements = GetCharacterRequirements(currentProperties.Complexity);
            int length = GetPasswordLength(currentProperties);
            int specialCount = GetSpecialCharacterCount(requirements, currentProperties, length);
            int numberCount = GetNumberCharacterCount(requirements, specialCount);
            var characters = new List<char>(length);
            FillWithSpecials(requirements, characters, length, specialCount);
            FillWithNumbers(requirements, characters, numberCount, length, specialCount);
            FillWithLetters(requirements, characters, length);
            return BuildFinalPassword(characters, length);
        }

        private static CharacterRequirements GetCharacterRequirements(PasswordComplexity complexity)
        {
            bool hasSpecials = complexity.HasFlag(PasswordComplexity.Specials);
            bool hasNumbers = complexity.HasFlag(PasswordComplexity.Numbers);
            bool hasLowerCaseLetters = complexity.HasFlag(PasswordComplexity.LowerCaseLetters);
            bool hasUpperCaseLetters = complexity.HasFlag(PasswordComplexity.UpperCaseLetters);
            return new CharacterRequirements(hasNumbers, hasLowerCaseLetters, hasUpperCaseLetters, hasSpecials);
        }

        private int GetPasswordLength(IPasswordProperties properties)
        {
            return properties.MinLength == properties.MaxLength
                ? properties.MinLength : random.Next(properties.MinLength, properties.MaxLength + 1);
        }

        private static int GetSpecialCharacterCount(CharacterRequirements requirements, IPasswordProperties properties, int length)
            => requirements.HasSpecials ? properties.GetSpecialCharacterCount(length) : 0;

        private static int GetNumberCharacterCount(CharacterRequirements requirements, int specialCount)
            => requirements.HasNumbers ? Math.Max(specialCount, 1) + 1 : 0;

        private void FillWithSpecials(CharacterRequirements requirements, List<char> characters, int length, int specialCount)
        {
            if (requirements.HasSpecials)
            {
                // If should contain only specials, then fill only with them.
                if (requirements.HasNumbers == false && requirements.HasLowerCaseLetters == false && requirements.HasUpperCaseLetters == false)
                    specialCount = length;

                while (characters.Count < specialCount)
                    characters.Add(GetRandomCharacter(PasswordCharacters.Specials, random));
            }
        }

        private void FillWithNumbers(CharacterRequirements requirements, List<char> characters, int numberCount, int length, int specialCount)
        {
            if (requirements.HasNumbers)
            {
                // If should contain only numbers, then fill only with them.
                // If should contain only specials and numbers, then remain after specials if filled with numbers.
                if (requirements.HasSpecials == false && requirements.HasLowerCaseLetters == false && requirements.HasUpperCaseLetters == false)
                    numberCount = length;
                else if (requirements.HasLowerCaseLetters == false && requirements.HasUpperCaseLetters == false)
                    numberCount = length - specialCount;

                int n = 0;
                while (n < numberCount)
                {
                    characters.Add(GetRandomCharacter(PasswordCharacters.Numbers, random));
                    n++;
                }
            }
        }

        private void FillWithLetters(CharacterRequirements requirements, List<char> characters, int length)
        {
            if (requirements.HasLowerCaseLetters && requirements.HasUpperCaseLetters)
            {
                FillWithLowerAndUpperCaseLetters(characters, length);
            }
            else if (requirements.HasLowerCaseLetters)
            {
                FillWithLowerCaseLetters(characters, length);
            }
            else if (requirements.HasUpperCaseLetters)
            {
                FillWithUpperCaseLetters(characters, length);
            }
        }

        private void FillWithLowerAndUpperCaseLetters(List<char> characters, int length)
        {
            int remain = length - characters.Count;
            int c = remain / 2;
            int n = 0;

            // Fill half of the remain with lower case letters.
            while (n < c)
            {
                characters.Add(GetRandomCharacter(PasswordCharacters.LowerCaseLetters, random));
                n++;
            }

            // Fill what is left with upper case letters.
            while (characters.Count < length)
                characters.Add(GetRandomCharacter(PasswordCharacters.UpperCaseLetters, random));
        }

        private void FillWithLowerCaseLetters(List<char> characters, int length)
        {
            while (characters.Count < length)
                characters.Add(GetRandomCharacter(PasswordCharacters.LowerCaseLetters, random));
        }

        private void FillWithUpperCaseLetters(List<char> characters, int length)
        {
            while (characters.Count < length)
                characters.Add(GetRandomCharacter(PasswordCharacters.UpperCaseLetters, random));
        }

        private string BuildFinalPassword(List<char> characters, int length)
        {
            var builder = new StringBuilder(length);
            while (builder.Length < length)
            {
                int index = random.Next(0, characters.Count);
                builder.Append(characters[index]);
                characters.RemoveAt(index);
            }
            return builder.ToString();
        }

        private static char GetRandomCharacter(IList<char> list, Random random) => list[random.Next(0, list.Count)];

        private record CharacterRequirements(bool HasNumbers, bool HasLowerCaseLetters, bool HasUpperCaseLetters, bool HasSpecials);
    }
}
