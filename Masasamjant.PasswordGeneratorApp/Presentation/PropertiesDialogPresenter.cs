using Masasamjant.Security.Passwords;
using Masasamjant.Windows.Presentation;
using System.Text;

namespace Masasamjant.PasswordGeneratorApp.Presentation
{
    public class PropertiesDialogPresenter : DialogViewPresenter<IPropertiesDialogView>
    {
        private PasswordGeneratorProperties properties;

        public PropertiesDialogPresenter(IPropertiesDialogView view) 
            : base(view)
        {
            View.UseLowerCaseLettersChanged.Executed += OnUseLowerCaseLettersChangedExecuted;
            View.UseUpperCaseLettersChanged.Executed += OnUseUpperCaseLettersChangedExecuted;
            View.UseNumbersChanged.Executed += OnUseNumbersChangedExecuted;
            View.UseSpecialCharactersChanged.Executed += OnUseSpecialCharactersChangedExecuted;
            View.MaxLengthChanged.Executed += OnMaxLengthChangedExecuted;
            View.MinLengthChanged.Executed += OnMinLengthChangedExecuted;
            View.SpecialCharacterCountChanged.Executed += OnSpecialCharacterCountChangedExecuted;
            View.SavePressed.Executed += OnSavePressedExecuted;
            View.CancelPressed.Executed += OnCancelPressedExecuted;
            properties = new PasswordGeneratorProperties();
        }

        public void OnUseLowerCaseLettersChanged(EventArgs e)
        {
            CheckDisposed();

            if (IsAnyComplexityUsed())
                ChangePasswordCompelixy(PasswordComplexity.LowerCaseLetters, View.UseLowerCaseLetters);
        }

        public void OnUseUpperCaseLettersChanged(EventArgs e)
        {
            CheckDisposed();

            if (IsAnyComplexityUsed())
                ChangePasswordCompelixy(PasswordComplexity.UpperCaseLetters, View.UseUpperCaseLetters);
        }

        public void OnUseNumbersChanged(EventArgs e)
        {
            CheckDisposed();

            if (IsAnyComplexityUsed())
                ChangePasswordCompelixy(PasswordComplexity.Numbers, View.UseNumbers);
        }

        public void OnUseSpecialCharactersChanged(EventArgs e)
        {
            CheckDisposed();
            
            if (IsAnyComplexityUsed())
            {
                ChangePasswordCompelixy(PasswordComplexity.Specials, View.UseSpecialCharacters);
                View.IsSpecialCharacterCountVisible = View.UseSpecialCharacters;
            }
        }

        public void OnMaxLengthChanged(EventArgs e)
        {
            CheckDisposed();

            if (View.MaxLength < View.MinLength)
            {
                View.ShowMessage(PresentationMessage.Error("Maximum length cannot be less than minimum length.", "Maximum length."));
                View.IsSaveButtonEnabled = false;
            }
            else
            {
                View.IsSaveButtonEnabled = true;
                properties.ChangeMaxLength(View.MaxLength);
            }
        }

        public void OnMinLengthChanged(EventArgs e)
        {
            CheckDisposed();

            if (View.MinLength > View.MaxLength)
            {
                View.ShowMessage(PresentationMessage.Error("Minimum length cannot be greater than maximum length.", "Minimum length."));
                View.IsSaveButtonEnabled = false;
            }
            else
            { 
                View.IsSaveButtonEnabled = true;
                properties.ChangeMinLength(View.MinLength);
            }
        }

        public void OnSpecialCharacterCountChanged(EventArgs e)
        {
            CheckDisposed();
            var value = View.SpecialCharacterCount;
            properties.ChangeSpecialCharacterCount(value > 0 ? value : null);
        }

        public void OnSavePressed(EventArgs e)
        {
            CheckDisposed();
            PasswordGeneratorPropertiesManager.SaveProperties(properties);
            View.SetDialogResult(DialogResult.OK);
            View.Close();
        }

        public void OnCancelPressed(EventArgs e)
        {
            CheckDisposed();
            View.SetDialogResult(DialogResult.Cancel);
            View.Close();
        }

        public override void OnViewLoading(EventArgs args)
        {
            base.OnViewLoading(args);

            try
            {
                View.DisableCommands();
                properties = PasswordGeneratorPropertiesManager.GetProperties();
                View.UseLowerCaseLetters = properties.Complexity.HasFlag(PasswordComplexity.LowerCaseLetters);
                View.UseUpperCaseLetters = properties.Complexity.HasFlag(PasswordComplexity.UpperCaseLetters);
                View.UseNumbers = properties.Complexity.HasFlag(PasswordComplexity.Numbers);
                View.UseSpecialCharacters = properties.Complexity.HasFlag(PasswordComplexity.Specials);
                View.MinLength = properties.MinLength;
                View.MaxLength = properties.MaxLength;
                View.IsSpecialCharacterCountVisible = View.UseSpecialCharacters;
                View.SpecialCharacterCount = properties.SpecialCharacterCount.GetValueOrDefault(0);
                View.CharactersText = BuildCharactersText();
            }
            finally
            {
                View.EnableCommands();
            }
        }

        private string BuildCharactersText()
        {
            var builder = new StringBuilder();

            static void AppendLine(StringBuilder sb)
            {
                if (sb.Length > 0)
                    sb.Append(Environment.NewLine);
            }

            if (View.UseLowerCaseLetters)
                builder.Append(string.Concat(PasswordCharacters.LowerCaseLetters));

            if (View.UseUpperCaseLetters)
            {
                AppendLine(builder);
                builder.Append(string.Concat(PasswordCharacters.UpperCaseLetters));
            }

            if (View.UseNumbers)
            {
                AppendLine(builder);
                builder.Append(string.Concat(PasswordCharacters.Numbers));
            }

            if (View.UseSpecialCharacters)
            {
                AppendLine(builder);
                builder.Append(string.Concat(PasswordCharacters.Specials));
            }

            return builder.ToString();
        }

        private void ChangePasswordCompelixy(PasswordComplexity flag, bool set)
        {
            var complexity = properties.Complexity;

            if (set)
                complexity |= flag;
            else
                complexity &= ~flag;

            properties.ChangeComplexity(complexity);
            View.CharactersText = BuildCharactersText();
        }

        private bool IsAnyComplexityUsed()
        {
            bool[] values = [View.UseLowerCaseLetters, View.UseLowerCaseLetters, View.UseNumbers, View.UseSpecialCharacters];

            if (values.Any(b => b))
            {
                View.IsSaveButtonEnabled = true;
                return true;
            }
            else
            {
                View.ShowMessage(PresentationMessage.Error("At least one character set must be selected.", "Password complexity."));
                View.IsSaveButtonEnabled = false;
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                base.Dispose(disposing);
                View.UseLowerCaseLettersChanged.Executed -= OnUseLowerCaseLettersChangedExecuted;
                View.UseUpperCaseLettersChanged.Executed -= OnUseUpperCaseLettersChangedExecuted;
                View.UseNumbersChanged.Executed -= OnUseNumbersChangedExecuted;
                View.UseSpecialCharactersChanged.Executed -= OnUseSpecialCharactersChangedExecuted;
                View.MaxLengthChanged.Executed -= OnMaxLengthChangedExecuted;
                View.MinLengthChanged.Executed -= OnMinLengthChangedExecuted;
                View.SpecialCharacterCountChanged.Executed -= OnSpecialCharacterCountChangedExecuted;
                View.SavePressed.Executed -= OnSavePressedExecuted;
                View.CancelPressed.Executed -= OnCancelPressedExecuted;
            }
        }

        private void OnUseLowerCaseLettersChangedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnUseLowerCaseLettersChanged(e.Original);
        }

        private void OnCancelPressedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnCancelPressed(e.Original);
        }

        private void OnSavePressedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnSavePressed(e.Original);
        }

        private void OnSpecialCharacterCountChangedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnSpecialCharacterCountChanged(e.Original);
        }

        private void OnMinLengthChangedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnMinLengthChanged(e.Original);
        }

        private void OnMaxLengthChangedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnMaxLengthChanged(e.Original);
        }

        private void OnUseSpecialCharactersChangedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnUseSpecialCharactersChanged(e.Original);
        }

        private void OnUseNumbersChangedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnUseNumbersChanged(e.Original);
        }

        private void OnUseUpperCaseLettersChangedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnUseUpperCaseLettersChanged(e.Original);
        }
    }
}
