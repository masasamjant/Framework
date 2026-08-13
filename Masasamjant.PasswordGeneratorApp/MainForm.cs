using Masasamjant.PasswordGeneratorApp.Presentation;
using Masasamjant.Windows.Presentation;
using System.Collections;
using System.ComponentModel;

namespace Masasamjant.PasswordGeneratorApp
{
    public partial class MainForm : Form, IMainFormView, IFormView, IView, IPresentationCommands
    {
        private readonly PresentationCommandCollection commands;
        private readonly MainFormPresenter presenter;

        public MainForm()
        {
            InitializeComponent();
            commands = new PresentationCommandCollection();
            ViewLoadingCommand = commands.CreateCommand(nameof(ViewLoadingCommand));
            FormClosingCommand = commands.CreateCommand<FormClosingEventArgs>(nameof(FormClosingCommand));
            FormClosedCommand = commands.CreateCommand<FormClosedEventArgs>(nameof(FormClosedCommand));
            PropertiesPressed = commands.CreateCommand(nameof(PropertiesPressed));
            EditPressed = commands.CreateCommand(nameof(EditPressed));
            NextPressed = commands.CreateCommand(nameof(NextPressed));
            presenter = new MainFormPresenter(this);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PasswordText
        {
            get => textPassword.Text;
            set => textPassword.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPasswordEditable
        {
            get { return textPassword.ReadOnly == false; }
            set
            {
                if (value)
                {
                    textPassword.ReadOnly = false;
                    textPassword.Enabled = true;
                }
                else
                {
                    textPassword.ReadOnly = true;
                    textPassword.Enabled = false;
                }
            }
        }

        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        public bool IsEditButtonEnabled
        {
            get => buttonEdit.Enabled;
            set => buttonEdit.Enabled = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditButtonText
        {
            get => buttonEdit.Text;
            set => buttonEdit.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNextButtonEnabled
        {
            get => buttonNext.Enabled;
            set => buttonNext.Enabled = value;
        }

        public IPresentationCommand ViewLoadingCommand { get; }

        public IPresentationCommand<FormClosingEventArgs> FormClosingCommand { get; }

        public IPresentationCommand<FormClosedEventArgs> FormClosedCommand { get; }

        public IPresentationCommand PropertiesPressed { get; }

        public IPresentationCommand EditPressed { get; }

        public IPresentationCommand NextPressed { get; }

        public IEnumerator<IPresentationCommand> GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        public DialogResult ShowMessage(PresentationMessage message)
        {
            return message.ShowMessageBox(this);
        }

        public DialogResult ShowProperties()
        {
            using (var propertiesDialog = new PropertiesDialog())
            {
                return propertiesDialog.ShowDialog(this);
            }
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

        private void OnNextClick(object sender, EventArgs e)
        {
            NextPressed.Execute(e);
        }

        private void OnEditLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditPressed.Execute(e);
        }

        private void OnPropertiesLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PropertiesPressed.Execute(e);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
