using Masasamjant.PasswordGeneratorApp.Properties;
using Masasamjant.Security.Passwords;

namespace Masasamjant.PasswordGeneratorApp
{
    internal static class PasswordGeneratorPropertiesManager
    {
        public static PasswordGeneratorProperties GetProperties()
        {
            var complexity = PasswordGeneratorProperties.DefaultComplexity;
            var settings = Settings.Default;

            if (!settings.UseLowerCaseLetters)
                complexity &= ~PasswordComplexity.LowerCaseLetters;

            if (!settings.UseUpperCaseLetters)
                complexity &= PasswordComplexity.UpperCaseLetters;

            if (!settings.UseNumbers)
                complexity &= ~PasswordComplexity.Numbers;

            if (!settings.UseSpecials)
                complexity &= ~PasswordComplexity.Specials;

            var properties = new PasswordGeneratorProperties(settings.MinLength, settings.MaxLength, complexity);

            if (settings.SpecialCharacterCount > 0)
                properties.ChangeSpecialCharacterCount(settings.SpecialCharacterCount);

            return properties;
        }

        public static void SaveProperties(PasswordGeneratorProperties properties)
        {
            var settings = Settings.Default;
            settings.MinLength = properties.MinLength;
            settings.MaxLength = properties.MaxLength;
            settings.UseLowerCaseLetters = properties.Complexity.HasFlag(PasswordComplexity.LowerCaseLetters);
            settings.UseUpperCaseLetters = properties.Complexity.HasFlag(PasswordComplexity.UpperCaseLetters);
            settings.UseNumbers = properties.Complexity.HasFlag(PasswordComplexity.Numbers);
            settings.UseSpecials = properties.Complexity.HasFlag(PasswordComplexity.Specials);
            settings.SpecialCharacterCount = properties.SpecialCharacterCount.GetValueOrDefault(0);
            settings.Save();
        }
    }
}
