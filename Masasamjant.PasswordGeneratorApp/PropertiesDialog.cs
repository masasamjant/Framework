using Masasamjant.PasswordGeneratorApp.Presentation;
using Masasamjant.Windows.Presentation;
using System.Collections;
using System.ComponentModel;

namespace Masasamjant.PasswordGeneratorApp
{
    public partial class PropertiesDialog : Form, IPropertiesDialogView, IFormView, IView, IPresentationCommands
    {
        private readonly PresentationCommandCollection commands;
        private readonly PropertiesDialogPresenter presenter;

        public PropertiesDialog()
        {
            InitializeComponent();
            commands = new PresentationCommandCollection();
            MinLengthChanged = commands.CreateCommand(nameof(MinLengthChanged));
            MaxLengthChanged = commands.CreateCommand(nameof(MaxLengthChanged));
            UseLowerCaseLettersChanged = commands.CreateCommand(nameof(UseLowerCaseLettersChanged));
            UseUpperCaseLettersChanged = commands.CreateCommand(nameof(UseUpperCaseLettersChanged));
            UseNumbersChanged = commands.CreateCommand(nameof(UseNumbersChanged));
            UseSpecialCharactersChanged = commands.CreateCommand(nameof(UseSpecialCharactersChanged));
            SpecialCharacterCountChanged = commands.CreateCommand(nameof(SpecialCharacterCountChanged));
            SavePressed = commands.CreateCommand(nameof(SavePressed));
            CancelPressed = commands.CreateCommand(nameof(CancelPressed));
            ViewLoadingCommand = commands.CreateCommand(nameof(ViewLoadingCommand));
            FormClosingCommand = commands.CreateCommand<FormClosingEventArgs>(nameof(FormClosingCommand));
            FormClosedCommand = commands.CreateCommand<FormClosedEventArgs>(nameof(FormClosedCommand));
            presenter = new PropertiesDialogPresenter(this);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinLength
        {
            get => Convert.ToInt32(numMinLength.Value);
            set => numMinLength.Value = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxLength
        {
            get => Convert.ToInt32(numMaxLength.Value);
            set => numMaxLength.Value = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseLowerCaseLetters
        {
            get => checkLowerCaseLetters.Checked;
            set => checkLowerCaseLetters.Checked = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseUpperCaseLetters
        {
            get => checkUpperCaseLetters.Checked;
            set => checkUpperCaseLetters.Checked = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseNumbers
        {
            get => checkNumbers.Checked;
            set => checkNumbers.Checked = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseSpecialCharacters
        {
            get => checkSpecials.Checked;
            set => checkSpecials.Checked = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SpecialCharacterCount
        {
            get => Convert.ToInt32(numSpecialCount.Value);
            set => numSpecialCount.Value = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CharactersText
        {
            get { return textCharacters.Text; }
            set { textCharacters.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSpecialCharacterCountVisible
        {
            get => panelSpecialCount.Visible;
            set => panelSpecialCount.Visible = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSaveButtonEnabled
        {
            get => buttonSave.Enabled;
            set => buttonSave.Enabled = value;
        }

        public IPresentationCommand MinLengthChanged { get; }

        public IPresentationCommand MaxLengthChanged { get; }

        public IPresentationCommand UseLowerCaseLettersChanged { get; }
        public IPresentationCommand UseUpperCaseLettersChanged { get; }

        public IPresentationCommand UseNumbersChanged { get; }

        public IPresentationCommand UseSpecialCharactersChanged { get; }
        public IPresentationCommand SpecialCharacterCountChanged { get; }

        public IPresentationCommand SavePressed { get; }

        public IPresentationCommand CancelPressed { get; }

        public IPresentationCommand ViewLoadingCommand { get; }

        public IPresentationCommand<FormClosingEventArgs> FormClosingCommand { get; }

        public IPresentationCommand<FormClosedEventArgs> FormClosedCommand { get; }

        public IEnumerator<IPresentationCommand> GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        public void SetDialogResult(DialogResult result)
        {
            DialogResult = result;
        }

        public DialogResult ShowMessage(PresentationMessage message)
        {
            return message.ShowMessageBox(this);
        }

        private void OnMinLengthValueChanged(object sender, EventArgs e)
        {
            MinLengthChanged.Execute(e);
        }

        private void OnMaxLengthValueChanged(object sender, EventArgs e)
        {
            MaxLengthChanged.Execute(e);
        }

        private void OnLowerCaseLettersCheckedChanged(object sender, EventArgs e)
        {
            UseLowerCaseLettersChanged.Execute(e);
        }

        private void OnUpperCaseLettersCheckedChanged(object sender, EventArgs e)
        {
            UseUpperCaseLettersChanged.Execute(e);
        }

        private void OnNumbersCheckedChanged(object sender, EventArgs e)
        {
            UseNumbersChanged.Execute(e);
        }

        private void OnSpecialsCheckedChanged(object sender, EventArgs e)
        {
            UseSpecialCharactersChanged.Execute(e);
        }

        private void OnSpecialCountValueChanged(object sender, EventArgs e)
        {
            SpecialCharacterCountChanged.Execute(e);
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            SavePressed.Execute(e);
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            CancelPressed.Execute(e);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ViewLoadingCommand.Execute(e);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            FormClosingCommand.Execute(e);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            FormClosedCommand.Execute(e);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
