using System;
using System.Windows;
using MahApps.Metro.Controls;
using VideoClub.WPF.Utils;

namespace VideoClub.WPF.Views.Settings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        private readonly string _vipClientProcess = HelperWindow.VipUserProcessAutomatic;
        private readonly string _blockedClientProcess = HelperWindow.BlockedUserProcessAutomatic;
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void VipCheckBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TryValueOfProcess(sender, _vipClientProcess);
        }

        private void BlockedCheckBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TryValueOfProcess(sender, _blockedClientProcess);
        }

        private static void TryValueOfProcess(object sender, string keySetting)
        {
            bool.TryParse(HelperWindow.ReadSetting(keySetting), out var isAutomaticProcess);
            ((ToggleSwitch) sender).IsChecked = isAutomaticProcess;
        }

        private void VipCheckBox_OnChecked(object sender, EventArgs eventArgs)
        {
            var isSelected = ((ToggleSwitch) sender).IsChecked;
            HelperWindow.AddUpdateAppSettings(_vipClientProcess, isSelected.ToString());
        }

        private void BlockedCheckBox_OnChecked(object sender, EventArgs eventArgs)
        {
            var isSelected = ((ToggleSwitch)sender).IsChecked;
            HelperWindow.AddUpdateAppSettings(_blockedClientProcess, isSelected.ToString());
        }

        private void BlockedCheckBox_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isSelected = ((ToggleSwitch)sender).IsChecked;
            HelperWindow.AddUpdateAppSettings(_blockedClientProcess, isSelected.ToString());
        }
    }
}
