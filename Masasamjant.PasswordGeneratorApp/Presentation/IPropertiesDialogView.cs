using Masasamjant.Windows.Presentation;

namespace Masasamjant.PasswordGeneratorApp.Presentation
{
    public interface IPropertiesDialogView : IDialogView
    {
        int MinLength { get; set; }

        int MaxLength { get; set; }

        bool UseLowerCaseLetters { get; set; }

        bool UseUpperCaseLetters { get; set; }

        bool UseNumbers { get; set; }

        bool UseSpecialCharacters { get; set; }

        int SpecialCharacterCount { get; set; }

        string CharactersText { get; set; }

        bool IsSpecialCharacterCountVisible { get; set; }

        bool IsSaveButtonEnabled { get; set; }

        IPresentationCommand MinLengthChanged { get; }

        IPresentationCommand MaxLengthChanged { get; }

        IPresentationCommand UseLowerCaseLettersChanged { get; }

        IPresentationCommand UseUpperCaseLettersChanged { get; }

        IPresentationCommand UseNumbersChanged { get; } 

        IPresentationCommand UseSpecialCharactersChanged { get; }

        IPresentationCommand SpecialCharacterCountChanged { get; }

        IPresentationCommand SavePressed { get; }

        IPresentationCommand CancelPressed { get; }
    }
}
