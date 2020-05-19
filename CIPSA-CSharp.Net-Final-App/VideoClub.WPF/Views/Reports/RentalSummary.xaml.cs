using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Utils;
using VideoClub.WPF.Utils;

namespace VideoClub.WPF.Views.Settings
{
    /// <summary>
    /// Interaction logic for RentalSummary.xaml
    /// </summary>
    public partial class RentalSummary : MetroWindow
    {
        private RentalService _rentalService;
        public RentalSummary()
        {
            InitializeComponent();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            _rentalService = RentalService.Instance;
        }

        private async void RentalSummary_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProcessReportRentals();
            ProcessReportVideoGames();
            ProcessReportMovies();
        }

       

        private void ProcessReportRentals()
        {
            var rentals = _rentalService.GetRentals(out var earnings, out var totalByCost);
            TotalRentalsTextBlock.Text = rentals.Count.ToString();
            WithoutDiscountRentalsTextBlock.Text = Convert.ToString(decimal.Round(totalByCost, 2), CultureInfo.InvariantCulture);
            DiscountRentalsTextBlock.Text = Convert.ToString(decimal.Round(earnings, 2), CultureInfo.InvariantCulture);
            PercentEarnMoviesTextBlock.Text =
                Convert.ToString(PercentEarnsByClients(totalByCost, earnings), CultureInfo.InvariantCulture) + "%";
        }

        private void ProcessReportVideoGames()
        {
            var videoGames = _rentalService.GetRentalsOfProduct(CommonHelper.VideoGame, out var earningsVideoGame,
                out var totalByCostVideoGame);
            TotalVideoGamesTextBlock.Text = videoGames.Count.ToString();
            WithoutDiscountVideoGamesTextBlock.Text = Convert.ToString(decimal.Round(totalByCostVideoGame, 2), CultureInfo.InvariantCulture);
            DiscountVideoGamesTextBlock.Text = Convert.ToString(decimal.Round(earningsVideoGame, 2), CultureInfo.InvariantCulture);
            PercentEarnMoviesTextBlock.Text =
                Convert.ToString(PercentEarnsByClients(totalByCostVideoGame, earningsVideoGame), CultureInfo.InvariantCulture) + "%";
        }

        private void ProcessReportMovies()
        {
            var movies =
                _rentalService.GetRentalsOfProduct(CommonHelper.Movie, out var earningsMovie, out var totalByCostMovie);
            TotalMoviesTextBlock.Text = movies.Count.ToString();
            WithoutDiscountMoviesTextBlock.Text = Convert.ToString(decimal.Round(totalByCostMovie, 2), CultureInfo.InvariantCulture);
            DiscountMoviesTextBlock.Text = Convert.ToString(decimal.Round(earningsMovie, 2), CultureInfo.InvariantCulture);
            PercentEarnMoviesTextBlock.Text =
                Convert.ToString(PercentEarnsByClients(totalByCostMovie, earningsMovie), CultureInfo.InvariantCulture) + "%";
        }

        private decimal PercentEarnsByClients(decimal total, decimal earnings)
        {
            return decimal.Round(100 * earnings / total, 2);
        }
    }
}
