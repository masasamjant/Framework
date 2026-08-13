using Masasamjant.FormsDemoApp.Controls;

namespace Masasamjant.FormsDemoApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnMainFormLoad(object sender, EventArgs e)
        {
            SetDemoControl(new FileTreeDemo());
        }

        private void SetDemoControl(UserControl control)
        {
            if (panelDemoControl.Controls.Count > 0)
            {
                foreach (UserControl currentControl in panelDemoControl.Controls)
                    currentControl.Dispose();

                panelDemoControl.Controls.Clear();
            }

            panelDemoControl.Controls.Add(control);
        }
    }
}
