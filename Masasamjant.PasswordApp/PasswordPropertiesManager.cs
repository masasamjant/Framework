using Masasamjant.PasswordApp.Properties;
using Masasamjant.Security.Passwords;

namespace Masasamjant.PasswordApp
{
    internal static class PasswordPropertiesManager
    {
        public static PasswordGeneratorProperties GetProperties()
        {
            var settings = Settings.Default;
            var complexity = GetPasswordComplexity(PasswordGeneratorProperties.DefaultComplexity, settings);
            var properties = new PasswordGeneratorProperties(settings.MinLength, settings.MaxLength, complexity);

            if (settings.SpecialCharacterCount > 0)
                properties.ChangeSpecialCharacterCount(settings.SpecialCharacterCount);

            return properties;
        }

        public static void SaveProperties(IPasswordProperties properties)
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

        private static PasswordComplexity GetPasswordComplexity(PasswordComplexity initial, Settings settings)
        {
            var complexity = initial;

            if (!settings.UseLowerCaseLetters)
                complexity = RemoveFlag(complexity, PasswordComplexity.LowerCaseLetters);

            if (!settings.UseUpperCaseLetters)
                complexity = RemoveFlag(complexity, PasswordComplexity.UpperCaseLetters);

            if (!settings.UseNumbers)
                complexity = RemoveFlag(complexity, PasswordComplexity.Numbers);

            if (!settings.UseSpecials)
                complexity = RemoveFlag(complexity, PasswordComplexity.Specials);

            return complexity;
        }

        private static PasswordComplexity RemoveFlag(PasswordComplexity current, PasswordComplexity flag)
        {
            return current & ~flag;
        }
    }
}
