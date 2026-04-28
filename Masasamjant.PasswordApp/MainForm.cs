using Masasamjant.Security.Passwords;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;
using System.Text;

namespace Masasamjant.PasswordApp
{
    public partial class MainForm : Form
    {
        private PasswordGeneratorProperties properties = new PasswordGeneratorProperties();
        private bool suspendPropertyTabEvents = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnMainFormLoad(object sender, EventArgs e)
        {
            properties = PasswordPropertiesManager.GetProperties();
        }

        private void OnMainFormTabsSelected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage != null && e.TabPage.Name == nameof(propertiesTab))
            {
                RefreshPropertiesTab();
            }
        }

        private void ShowErrorMessageBox(string text, string title)
            => MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        #region Properties Tab

        private void OnButtonSavePropertiesClick(object sender, EventArgs e)
        {
            PasswordPropertiesManager.SaveProperties(properties);
            RefreshPropertiesTab();
        }

        private void OnButtonCancelPropertiesClick(object sender, EventArgs e)
        {
            properties = PasswordPropertiesManager.GetProperties();
            RefreshPropertiesTab();
        }

        private void OnNumericMinimumLengthValueChanged(object sender, EventArgs e)
        {
            if (suspendPropertyTabEvents)
                return;

            GetLengths(out var minLength, out var maxLength);

            if (minLength > maxLength)
            {
                DisablePropertySaveButton();
                ShowErrorMessageBox("Minimum length cannot be greater than maximum length.", "Invalid Minimum Length");
            }
            else
            {
                EnabledPropertySaveButton();
                properties.ChangeMinLength(minLength);
            }
        }

        private void OnNumericMaximumLengthValueChanged(object sender, EventArgs e)
        {
            if (suspendPropertyTabEvents)
                return;

            GetLengths(out var minLength, out var maxLength);

            if (maxLength < minLength)
            {
                DisablePropertySaveButton();
                ShowErrorMessageBox("Maximum lenght cannot be less than minimum length.", "Invalid Maximum Length");
            }
            else
            {
                EnabledPropertySaveButton();
                properties.ChangeMaxLength(maxLength);
            }
        }

        private void OnCheckLowerCaseLettersCheckedChanged(object sender, EventArgs e)
        {
            if (suspendPropertyTabEvents || !IsAnyComplexityChecked())
                return;
            ChangePasswordComplexity(PasswordComplexity.LowerCaseLetters, checkLowerCaseLetters.Checked);
        }

        private void OnCheckNumbersCheckedChanged(object sender, EventArgs e)
        {
            if (suspendPropertyTabEvents || !IsAnyComplexityChecked())
                return;
            ChangePasswordComplexity(PasswordComplexity.Numbers, checkNumbers.Checked);
        }

        private void OnCheckUpperCaseLettersCheckedChanged(object sender, EventArgs e)
        {
            if (suspendPropertyTabEvents || !IsAnyComplexityChecked())
                return;
            ChangePasswordComplexity(PasswordComplexity.UpperCaseLetters, checkUpperCaseLetters.Checked);
        }

        private void OnCheckSpecialsCheckedChanged(object sender, EventArgs e)
        {
            if (suspendPropertyTabEvents || !IsAnyComplexityChecked())
                return;
            ChangePasswordComplexity(PasswordComplexity.Specials, checkSpecials.Checked);
        }

        private void OnNumericSpecialCountValueChanged(object sender, EventArgs e)
        {
            if (suspendPropertyTabEvents || !checkSpecials.Checked)
                return;
            int value = Convert.ToInt32(numericSpecialCount.Value);
            properties.ChangeSpecialCharacterCount(value > 0 ? value : null);
        }

        private void RefreshPropertiesTab()
        {
            try
            {
                suspendPropertyTabEvents = true;
                checkLowerCaseLetters.Checked = properties.Complexity.HasFlag(PasswordComplexity.LowerCaseLetters);
                checkUpperCaseLetters.Checked = properties.Complexity.HasFlag(PasswordComplexity.UpperCaseLetters);
                checkNumbers.Checked = properties.Complexity.HasFlag(PasswordComplexity.Numbers);
                checkSpecials.Checked = properties.Complexity.HasFlag(PasswordComplexity.Specials);
                numericMinimumLength.Value = properties.MinLength;
                numericMaximumLength.Value = properties.MaxLength;
                numericSpecialCount.Value = properties.SpecialCharacterCount.GetValueOrDefault(0);
                BuildCharactersText();
            }
            finally
            {
                suspendPropertyTabEvents = false;
            }
        }

        private void BuildCharactersText()
        {
            var builder = new StringBuilder();

            if (checkLowerCaseLetters.Checked)
                builder.Append(string.Concat(PasswordCharacters.LowerCaseLetters));

            if (checkUpperCaseLetters.Checked)
            {
                AppendNewLine(builder);
                builder.Append(string.Concat(PasswordCharacters.UpperCaseLetters));
            }

            if (checkNumbers.Checked)
            {
                AppendNewLine(builder);
                builder.Append(string.Concat(PasswordCharacters.Numbers));
            }

            if (checkSpecials.Checked)
            {
                AppendNewLine(builder);
                builder.Append(string.Concat(PasswordCharacters.Specials));
            }

            textBoxChacacters.Text = builder.ToString();
        }

        private static void AppendNewLine(StringBuilder builder)
        {
            if (builder.Length > 0)
                builder.Append(Environment.NewLine);
        }

        private void GetLengths(out int minLength, out int maxLength)
        {
            minLength = Convert.ToInt32(numericMinimumLength.Value);
            maxLength = Convert.ToInt32(numericMaximumLength.Value);
        }

        private void EnabledPropertySaveButton()
            => buttonSaveProperties.Enabled = true;

        private void DisablePropertySaveButton()
            => buttonSaveProperties.Enabled = false;

        private bool IsAnyComplexityChecked()
        {
            bool[] states = [checkLowerCaseLetters.Checked, checkUpperCaseLetters.Checked, checkNumbers.Checked, checkSpecials.Checked];

            if (states.Any(s => s))
            {
                return true;
            }
            else
            {
                ShowErrorMessageBox("At least one character set must be selected.", "No Characters");
                return false;
            }
        }

        private void ChangePasswordComplexity(PasswordComplexity flag, bool set)
        {
            var complexity = properties.Complexity;

            if (set)
                complexity |= flag;
            else
                complexity &= ~flag;

            properties.ChangeComplexity(complexity);

            BuildCharactersText();
        }

        #endregion
    }
}
