using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;
using VideoClub.WPF.Utils;
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
            DataHelper.LoadData();
        }

    
        private void ButtonMovieWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var movieWindows = new MovieWindow(StateProductEnum.All);
            movieWindows.Show();
        }

        private void ButtonClientsWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var clientsWindows = new ClientWindow(StateClientEnum.All);
            clientsWindows.Show();
        }

        private void ButtonVideoGamesWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var videoGameWindows = new VideoGameWindow(StateProductEnum.All);
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
            bool.TryParse(WindowHelper.ReadSetting(WindowHelper.BlockedClientProcessAutomatic), out var isBlockedAutomaticProcess);
            if (isBlockedAutomaticProcess)
            {
                await ProcessBlockedClientsInBackground();
            }
            else
            {
                VisibleTile(BlockedTile, BlockedUserProgress);
            }

            bool.TryParse(WindowHelper.ReadSetting(WindowHelper.VipClientProcessAutomatic), out var isVipAutomaticProcess);
            if (isVipAutomaticProcess)
            {
                await ProcessVipClientsInBackground();
            }
            else
            {
                VisibleTile(VipTile, VipUserProgress);
            }

            bool.TryParse(WindowHelper.ReadSetting(WindowHelper.DiscountClientProcessAutomatic), out var isAutomaticProcess);
            var today = DateTime.Today;
            var lastDayMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

            if (isAutomaticProcess && lastDayMonth.Equals(today))
            {
                await ProcessDiscountClientsInBackground();
            }
            else
            {
                VisibleTile(DiscountTile, DiscountUserProgress);
            }
        }

        #region Process of blocked


        private async Task ProcessBlockedClientsInBackground()
        {
            HideTile(BlockedTile, BlockedUserProgress);
            await ProcessOfChangeStateToBlockedTask();
            VisibleTile(BlockedTile, BlockedUserProgress);

        }
        private async Task ProcessOfChangeStateToBlockedTask()
        {
            await Task.Run(ProcessOfChangeStateToBlocked);
        }
        private void ProcessOfChangeStateToBlocked()
        {
            ClientService.Instance.ProcessOfChangeStateToBlocked();
        }

        #endregion

        #region Process of vip

        private async Task ProcessVipClientsInBackground()
        {
            HideTile(VipTile, VipUserProgress);
            await ProcessOfVipClientsTask();
            VisibleTile(VipTile, VipUserProgress);
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

        private async Task ProcessDiscountClientsInBackground()
        {
            HideTile(DiscountTile, DiscountUserProgress);
            await ProcessOfDiscountClientsTask();
            VisibleTile(DiscountTile, DiscountUserProgress);
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
            await ProcessBlockedClientsInBackground();
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
            await ProcessDiscountClientsInBackground();
        }

        private async void VipTile_OnClick(object sender, RoutedEventArgs e)
        {
            await ProcessVipClientsInBackground();
        }

        private void ReportsTile_OnClick(object sender, RoutedEventArgs e)
        {
            var reportsWindow = new RentalSummary();
            reportsWindow.Show();
        }
    }
}
