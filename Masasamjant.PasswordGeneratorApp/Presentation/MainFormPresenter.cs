using Masasamjant.Security.Passwords;
using Masasamjant.Windows.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.PasswordGeneratorApp.Presentation
{
    public class MainFormPresenter : FormViewPresenter<IMainFormView>
    {
        private PasswordGeneratorProperties properties;
        private PasswordGenerator generator;

        public MainFormPresenter(IMainFormView view)
            : base(view)
        {
            properties = new PasswordGeneratorProperties();
            generator = new PasswordGenerator(properties);
            View.NextPressed.Executed += OnNextPressedExecuted;
            View.PropertiesPressed.Executed += OnPropertiesPressedExecuted;
            View.EditPressed.Executed += OnEditPressedExecuted;
        }

        public void OnEditPressed(EventArgs e)
        {
            CheckDisposed();

            if (View.IsPasswordEditable)
            {
                View.IsPasswordEditable = false;
                View.IsEditButtonEnabled = false;
                View.IsNextButtonEnabled = true;
                View.EditButtonText = "Edit";
            }
            else
            {
                View.IsPasswordEditable = true;
                View.IsNextButtonEnabled = false;
                View.EditButtonText = "Done";
            }
        }

        public void OnNextPressed(EventArgs e)
        {
            CheckDisposed();
            GeneratePassword();
        }

        public void OnPropertiesPressed(EventArgs e)
        {
            CheckDisposed();
            var dialogResult = View.ShowProperties();
            if (dialogResult == DialogResult.OK) 
            {
                properties = PasswordGeneratorPropertiesManager.GetProperties();
                generator = new PasswordGenerator(properties);
                View.PasswordText = string.Empty;
            }
        }

        public override void OnViewLoading(EventArgs args)
        {
            base.OnViewLoading(args);
            properties = PasswordGeneratorPropertiesManager.GetProperties();
            generator = new PasswordGenerator(properties);
            GeneratePassword();
        }

        private void GeneratePassword()
        {
            View.PasswordText = generator.Generate();
            View.IsEditButtonEnabled = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                base.Dispose(disposing);
                View.NextPressed.Executed -= OnNextPressedExecuted;
                View.PropertiesPressed.Executed -= OnPropertiesPressedExecuted;
                View.EditPressed.Executed -= OnEditPressedExecuted;
            }
        }

        private void OnEditPressedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnEditPressed(e.Original);
        }

        private void OnPropertiesPressedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnPropertiesPressed(e.Original);
        }

        private void OnNextPressedExecuted(object? sender, PresentationCommandEventArgs e)
        {
            if (IsEnabledCommand(e))
                OnNextPressed(e.Original);
        }
    }
}
