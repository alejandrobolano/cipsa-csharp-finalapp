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
    }
}
