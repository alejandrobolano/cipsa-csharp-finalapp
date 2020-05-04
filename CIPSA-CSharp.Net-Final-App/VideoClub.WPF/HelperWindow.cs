using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace VideoClub.WPF
{
    public class HelperWindow
    {
        public static void ClearFields(StackPanel panel)
        {
            foreach (var control in panel.Children)
            {
                switch (control)
                {
                    case TextBox box:
                        box.Text = string.Empty;
                        break;
                    case ComboBox box:
                        box.SelectedItem = string.Empty;
                        break;
                    case NumericUpDown numeric:
                        numeric.Value = new double();
                        break;
                    case StackPanel panelChild:
                        ClearFields(panelChild);
                        break;
                }
            }
        }
    }
}
