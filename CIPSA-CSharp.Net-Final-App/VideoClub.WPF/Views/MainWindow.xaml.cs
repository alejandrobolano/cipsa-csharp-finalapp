using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;

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
            await ProcessBlockedClientsInBackground();
            await ProcessVipClientsInBackground();
        }

        private async Task ProcessBlockedClientsInBackground()
        {
            //BlockedTile.Visibility = Visibility.Hidden;
            await ProcessOfChangeStateToBlockedTask();
            BlockedUserProgress.Visibility = Visibility.Hidden;
            BlockedTile.Visibility = Visibility.Visible;
        }
        private async Task ProcessVipClientsInBackground()
        {
            //VipTile.Visibility = Visibility.Hidden;
            await ProcessOfChangeStateToBlockedTask();
            VipUserProgress.Visibility = Visibility.Hidden;
            VipTile.Visibility = Visibility.Visible;
        }

        private void ProcessOfChangeStateToBlocked()
        {
            ClientService.Instance.ProcessOfChangeStateToBlocked();
        }
        private void ProcessOfVipClients()
        {
            ClientService.Instance.UpdateClientsForVip();
        }

        private async Task ProcessOfVipClientsTask()
        {
            await Task.Run(ProcessOfVipClients);
        }
        private async Task ProcessOfChangeStateToBlockedTask()
        {
            await Task.Run(ProcessOfChangeStateToBlocked);
        }

        private async void BlockedTile_OnClick(object sender, RoutedEventArgs e)
        {
            await ProcessBlockedClientsInBackground();
        }
    }
}
