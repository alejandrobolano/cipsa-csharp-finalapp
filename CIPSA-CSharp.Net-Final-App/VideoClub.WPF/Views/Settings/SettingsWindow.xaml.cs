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
        private readonly string _vipClientProcess = HelperWindow.VipClientProcessAutomatic;
        private readonly string _blockedClientProcess = HelperWindow.BlockedClientProcessAutomatic;
        private readonly string _discountClientProcess = HelperWindow.DiscountClientProcessAutomatic;
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
            AddUpdateSettings(sender, _vipClientProcess);
        }

        private void BlockedCheckBox_OnChecked(object sender, EventArgs eventArgs)
        {
            AddUpdateSettings(sender, _blockedClientProcess);
        }

        
        private void DiscountCheckBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TryValueOfProcess(sender, _discountClientProcess);
        }

        private void DiscountCheckBox_OnChecked(object sender, EventArgs e)
        {
            AddUpdateSettings(sender, _discountClientProcess);
        }

        private void AddUpdateSettings(object sender, string key)
        {
            var isSelected = ((ToggleSwitch) sender).IsChecked;
            HelperWindow.AddUpdateAppSettings(key, isSelected.ToString());
        }
    }
}
