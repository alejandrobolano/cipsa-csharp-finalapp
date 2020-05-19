using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
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
        private decimal _earningsRentals;
        private decimal _totalByCostRentals;
        private decimal _earningsVideoGames;
        private decimal _totalByCostVideoGames;
        private decimal _earningsMovies;
        private decimal _totalByCostMovies;

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
            LoadingProgress.Visibility = Visibility.Visible;
            await ProcessOfRentalsInBackground();
            await ProcessOfVideoGamesInBackground();
            await ProcessOfMoviesInBackground();
            LoadingProgress.Visibility = Visibility.Hidden;
        }

        #region Rentals

          private async Task ProcessOfRentalsInBackground()
        {
            var rentals = await ProcessOfRentalsOfReportTask();
            ShowRentalsData(rentals, _totalByCostRentals, _earningsRentals);
        }

        private async Task<List<RentalDto>> ProcessOfRentalsOfReportTask()
        {
            decimal eTemp = 0, tTemp = 0;
            var rentals = await Task.Run(() => GetRentalsOfReports(out eTemp, out tTemp));
            _earningsRentals = eTemp;
            _totalByCostRentals = tTemp;

            return rentals;
        }

        private List<RentalDto> GetRentalsOfReports(out decimal earnings, out decimal totalByCost)
        {
            return _rentalService.GetRentals(out earnings, out totalByCost);
        }

        private void ShowRentalsData(IReadOnlyCollection<RentalDto> rentals, decimal totalByCost, decimal earnings)
        {
            TotalRentalsTextBlock.Text = rentals.Count.ToString();
            WithoutDiscountRentalsTextBlock.Text =
                Convert.ToString(decimal.Round(totalByCost, 2), CultureInfo.InvariantCulture);
            DiscountRentalsTextBlock.Text = Convert.ToString(decimal.Round(earnings, 2), CultureInfo.InvariantCulture);
            PercentEarnRentalsTextBlock.Text =
                Convert.ToString(PercentEarnsByClients(totalByCost, earnings), CultureInfo.InvariantCulture) + "%";
        }

        #endregion
        
        #region VideoGames

        private async Task ProcessOfVideoGamesInBackground()
        {
            var rentals = await ProcessOfVideoGamesOfReportTask();
            ShowVideoGamesData(rentals, _totalByCostVideoGames, _earningsVideoGames);
        }


        private async Task<List<RentalDto>> ProcessOfVideoGamesOfReportTask()
        {
            decimal eTemp = 0, tTemp = 0;
            var rentals = await Task.Run(() => GetVideoGamesOfReports(out eTemp, out tTemp));
            _earningsVideoGames = eTemp;
            _totalByCostVideoGames = tTemp;

            return rentals;
        }

        private List<RentalDto> GetVideoGamesOfReports(out decimal earnings, out decimal totalByCost)
        {
            return _rentalService.GetRentalsOfProduct(CommonHelper.VideoGame, out earnings,
                out totalByCost);
        }

        private void ShowVideoGamesData(List<RentalDto> videoGames, decimal totalByCostVideoGame, decimal earningsVideoGame)
        {
            TotalVideoGamesTextBlock.Text = videoGames.Count.ToString();
            WithoutDiscountVideoGamesTextBlock.Text =
                Convert.ToString(decimal.Round(totalByCostVideoGame, 2), CultureInfo.InvariantCulture);
            DiscountVideoGamesTextBlock.Text =
                Convert.ToString(decimal.Round(earningsVideoGame, 2), CultureInfo.InvariantCulture);
            PercentEarnVideoGamesTextBlock.Text =
                Convert.ToString(PercentEarnsByClients(totalByCostVideoGame, earningsVideoGame), CultureInfo.InvariantCulture) +
                "%";
        }

        #endregion
        
        #region Movies

        private async Task ProcessOfMoviesInBackground()
        {
            var rentals = await ProcessOfMoviesOfReportTask();
            ShowMoviesData(rentals, _totalByCostMovies, _earningsMovies);
        }
        private async Task<List<RentalDto>> ProcessOfMoviesOfReportTask()
        {
            decimal eTemp = 0, tTemp = 0;
            var rentals = await Task.Run(() => GetMoviesOfReport(out eTemp, out tTemp));
            _earningsMovies = eTemp;
            _totalByCostMovies = tTemp;

            return rentals;
        }
        
        private List<RentalDto> GetMoviesOfReport(out decimal earnings, out decimal totalByCost)
        {
            return _rentalService.GetRentalsOfProduct(CommonHelper.VideoGame, out earnings,
                out totalByCost);
        }
        private void ShowMoviesData(List<RentalDto> movies, decimal totalByCostMovie, decimal earningsMovie)
        {
            TotalMoviesTextBlock.Text = movies.Count.ToString();
            WithoutDiscountMoviesTextBlock.Text =
                Convert.ToString(decimal.Round(totalByCostMovie, 2), CultureInfo.InvariantCulture);
            DiscountMoviesTextBlock.Text = Convert.ToString(decimal.Round(earningsMovie, 2), CultureInfo.InvariantCulture);
            PercentEarnMoviesTextBlock.Text =
                Convert.ToString(PercentEarnsByClients(totalByCostMovie, earningsMovie), CultureInfo.InvariantCulture) + "%";
        }

        #endregion

        private decimal PercentEarnsByClients(decimal total, decimal earnings)
        {
            return decimal.Round(100 * earnings / total, 2);
        }
    }
}
