using System.Windows;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Extensions;

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
            //var movie = new MovieDto
            //{
            //    BuyYear = 2008,
            //    Duration = new TimeSpan(2, 23, 10),
            //    NumberDisc = 2,
            //    Price = 20,
            //    ProductionYear = 2000,
            //    State = StateProductEnum.Available,
            //    Title = "Casa de papel"
            //};
            var movieService = new MovieService();
            //movieService.Add(movie);

            //var client1 = new ClientDto
            //{
            //    Accreditation = "Y52723472",
            //    Address = "Hospitalet de llobregat",
            //    Email = "alfa@alfa.com",
            //    Name = "Alejandro",
            //    LastName = "Bolaño",
            //    PhoneContact = "34666090909",
            //    SubscriptionDate = DateTime.Today
            //};

            var clientService = new ClientService();
            //clientService.Add(client1);

            //var client = clientService.Get("CLI-IC8JLZ");
            //var product = movieService.Get("Casa negra");
            //var startRental = DateTime.Today;
            //var quantity = 2;
            //var quantityTimeSpan = new TimeSpan(quantity,0,0,0);
            //var finishRental = startRental.Add(quantityTimeSpan);
            //var rental = new RentalDto
            //{
            //    ClientAccreditation = client.Accreditation,
            //    ClientId = client.Id,
            //    ProductTitle = product.Title,
            //    ProductId = product.Id,
            //    FinishRental = finishRental,
            //    StartRental = startRental
            //};

            //var rentalService = new RentalService();
            //rentalService.StartRentalProduct(rental, StateProductEnum.NonAvailable);
            //var foo1 = rentalService.GetRentalsByClient("CLI-IC8JLZ");
            //var foo2 = rentalService.GetRentalsByProduct("CLI-IC8JLZ");
            //var foo3 = rentalService.GetRentalsByProduct("Casa de papel");

            //var videoGame = new VideoGameDto()
            //{
            //    NumberDisc = 1,
            //    Price = 2,
            //    Title = "Beta",
            //    Platform = GamePlatformEnum.Wii
            //};

            //var metroWindow = (Application.Current.MainWindow as MetroWindow);
            //metroWindow.ShowMessageAsync("Actualizacion", $"Nuevo nombre: ...");
        }

        private void ButtonMovieWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var movieWindows = new MovieWindow();
            movieWindows.Show();
            //var dialog = new MovieDialog();
            //dialog.ShowModalDialogExternally();
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

        private void ButtonRentalsWindows_OnClick(object sender, RoutedEventArgs e)
        {
            var rentalsWindow = new RentalsWindow();
            rentalsWindow.Show();
        }
    }
}
