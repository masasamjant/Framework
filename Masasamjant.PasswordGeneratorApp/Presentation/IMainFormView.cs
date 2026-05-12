using Masasamjant.Windows.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.PasswordGeneratorApp.Presentation
{
    public interface IMainFormView : IFormView
    {
        string PasswordText { get; set; }

        bool IsPasswordEditable { get; set; }

        bool IsEditButtonEnabled { get; set; }

        bool IsNextButtonEnabled { get; set; }

        string EditButtonText { get; set; }

        IPresentationCommand PropertiesPressed { get; }

        IPresentationCommand EditPressed { get; }

        IPresentationCommand NextPressed { get; }

        DialogResult ShowProperties();
    }
}
