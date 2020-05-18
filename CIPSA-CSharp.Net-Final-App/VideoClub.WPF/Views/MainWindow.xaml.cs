using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;
using VideoClub.WPF.Utils;
using VideoClub.WPF.Views.DialogsView;
using VideoClub.WPF.Views.Settings;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonMovieWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var movieWindows = new MovieWindow();
            movieWindows.Show();
        }

        private void ButtonClientsWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var clientsWindows = new ClientWindow();
            clientsWindows.Show();
        }

        private void ButtonVideoGamesWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var videoGameWindows = new VideoGameWindow();
            videoGameWindows.Show();
        }

        private void StartRentalsWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var rentalsWindow = new RentalsWindow(StateRentalEnum.All);
            rentalsWindow.Show();
        }

        private void FinishRentalsWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var rentalsWindow = new RentalsWindow(StateRentalEnum.Activated);
            rentalsWindow.Show();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await ProcessBlockedClientsInBackground(false);
            await ProcessVipClientsInBackground(false);
            await ProcessDiscountClientsInBackground(false);
        }

        #region Process of blocked

        #endregion
        private async Task ProcessBlockedClientsInBackground(bool isClicked)
        {
            bool.TryParse(HelperWindow.ReadSetting(HelperWindow.BlockedClientProcessAutomatic), out var isAutomaticProcess);
            if (isAutomaticProcess || isClicked)
            {
                HideTile(BlockedTile, BlockedUserProgress);
                await ProcessOfChangeStateToBlockedTask();
                VisibleTile(BlockedTile, BlockedUserProgress);
            }
            else
            {
                VisibleTile(BlockedTile, BlockedUserProgress);
            }
        }
        private async Task ProcessOfChangeStateToBlockedTask()
        {
            await Task.Run(ProcessOfChangeStateToBlocked);
        }
        private void ProcessOfChangeStateToBlocked()
        {
            ClientService.Instance.ProcessOfChangeStateToBlocked();
        }

        #region Process of vip

        private async Task ProcessVipClientsInBackground(bool isClicked)
        {
            bool.TryParse(HelperWindow.ReadSetting(HelperWindow.VipClientProcessAutomatic), out var isAutomaticProcess);
            if (isAutomaticProcess || isClicked)
            {
                HideTile(VipTile, VipUserProgress);
                await ProcessOfVipClientsTask();
                VisibleTile(VipTile, VipUserProgress);
            }
            else
            {
                VisibleTile(VipTile, VipUserProgress);
            }
        }
        private void ProcessOfVipClients()
        {
            ClientService.Instance.UpdateClientsForVip();
        }
        private async Task ProcessOfVipClientsTask()
        {
            await Task.Run(ProcessOfVipClients);
        }

        #endregion

        #region Process of discount

        private async Task ProcessDiscountClientsInBackground(bool isClicked)
        {
            bool.TryParse(HelperWindow.ReadSetting(HelperWindow.DiscountClientProcessAutomatic), out var isAutomaticProcess);
            var today = DateTime.Today;
            var lastDayMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

            if (isAutomaticProcess && lastDayMonth.Equals(today) || isClicked)
            {
                HideTile(DiscountTile, DiscountUserProgress);
                await ProcessOfDiscountClientsTask();
                VisibleTile(DiscountTile, DiscountUserProgress);
            }
            else
            {
                VisibleTile(DiscountTile, DiscountUserProgress);
            }
        }


        private void ProcessOfDiscountClients()
        {
            ClientService.Instance.UpdateDiscountForVip();
        }
        private async Task ProcessOfDiscountClientsTask()
        {
            await Task.Run(ProcessOfDiscountClients);
        }

        #endregion
        private void VisibleTile(UIElement tile, UIElement progress)
        {
            tile.Visibility = Visibility.Visible;
            progress.Visibility = Visibility.Hidden;
        }
        private void HideTile(UIElement tile, UIElement progress)
        {
            tile.Visibility = Visibility.Hidden;
            progress.Visibility = Visibility.Visible;
        }


        private async void BlockedTile_OnClick(object sender, RoutedEventArgs e)
        {
            await ProcessBlockedClientsInBackground(true);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void DiscountTile_OnClick(object sender, RoutedEventArgs e)
        {
            await ProcessDiscountClientsInBackground(true);
        }

        private async void VipTile_OnClick(object sender, RoutedEventArgs e)
        {
            await ProcessVipClientsInBackground(true);
        }
    }
}
