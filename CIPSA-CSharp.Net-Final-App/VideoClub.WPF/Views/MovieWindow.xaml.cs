using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Extensions;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow : MetroWindow
    {
        private readonly DateTime _todayDateTime;
        private MovieService _movieService;
        private IList<MovieDto> _movies;
        private MovieDto _movieSelected;
        public MovieWindow()
        {
            InitializeComponent();
            _todayDateTime = DateTime.Today;
        }

        private void DateNowTextBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            DateNowTextBlock.Text = _todayDateTime.ToShortDateString();
        }

        private async void MovieDataGrid_OnLoadedDataGrid(object sender, RoutedEventArgs e)
        {
            await LoadDataGrid();
        }

        private void MovieDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateData(sender);
        }

        private void UpdateData(object sender)
        {
            ChangeEnabledToButtons(true);
            FillFields(sender);
        }

        private void UpdateButtonDataGrid_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateData(sender);
        }

        private async Task LoadDataGrid()
        {
            LoadingPanel.Visibility = Visibility.Visible;
            await LoadingDataTask();
            MovieDataGrid.ItemsSource = _movies;
            LoadingPanel.Visibility = Visibility.Hidden;
        }

        private void LoadingData()
        {
            _movieService = new MovieService();
            _movies = _movieService.All();
        }
        private async Task LoadingDataTask()
        {
            await Task.Run(LoadingData);
        }

        private void ChangeEnabledToButtons(bool isEnabled)
        {
            UpdateButton.IsEnabled = isEnabled;
            DeleteButton.IsEnabled = isEnabled;
            AddButton.IsEnabled = !isEnabled;
            TitleText.IsEnabled = !isEnabled;
        }

        private void FillFields(object sender)
        {
            MovieSelected(sender);
            FillDataFromDataGrid();
            MainPanel.IsEnabled = true;
        }

        private void MovieSelected(object sender)
        {
            switch (sender)
            {
                case Button rowButton:
                    _movieSelected = (MovieDto)rowButton?.DataContext;
                    break;
                case DataGrid rowDataGrid:
                    _movieSelected = (MovieDto)rowDataGrid?.SelectedValue;
                    break;
            }
        }

        private void FillDataFromDataGrid()
        {
            if (_movieSelected == null) return;
            TitleText.Text = _movieSelected.Title;
            PriceNumeric.Value = Convert.ToDouble(_movieSelected.Price, CultureInfo.CurrentCulture);
            QuantityNumeric.Value = _movieSelected.QuantityDisc;
            ProductionYearText.Text = _movieSelected.ProductionYear.ToString();
            BuyYearText.Text = _movieSelected.BuyYear.ToString();
            HourDurationNumeric.Value = _movieSelected.Duration.Hours;
            MinuteDurationNumeric.Value = _movieSelected.Duration.Minutes;
            SecondDurationNumeric.Value = _movieSelected.Duration.Seconds;
        }

        private bool AddMovie()
        {
            var movie = new MovieDto();
            FillDataFromFields(movie);
            return _movieService.Add(movie);
        }

        private void FillDataFromFields(MovieDto movie)
        {
            movie.Title = TitleText.Text.RemoveMultipleSpace().ToUpperAllFirstLetter();
            movie.Price = Convert.ToDecimal(PriceNumeric.Value);
            movie.QuantityDisc = Convert.ToInt32(QuantityNumeric.Value);
            movie.ProductionYear = Convert.ToInt32(ProductionYearText.Text);
            movie.BuyYear = Convert.ToInt32(BuyYearText.Text);
            var durationTimeSpan = new TimeSpan(
                Convert.ToInt32(HourDurationNumeric.Value),
                Convert.ToInt32(MinuteDurationNumeric.Value),
                Convert.ToInt32(SecondDurationNumeric.Value)
                );
            movie.Duration = durationTimeSpan;
        }

        private bool RemoveMovie(MovieDto movie)
        {
            return _movieService.Remove(movie.Id);
        }
        private bool UpdateMovie()
        {
            FillDataFromFields(_movieSelected);
            return _movieService.Update(_movieSelected);
        }
        private void NewButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeEnabledToButtons(false);
            HelperWindow.ClearFields(MainPanel);
            MainPanel.IsEnabled = true;
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AddMovie())
            {
                await LoadDataGrid();
            }
        }

        private async void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (UpdateMovie())
            {
                await LoadDataGrid();
            }
        }

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            MovieSelected(sender);
            if (RemoveMovie(_movieSelected))
            {
                await LoadDataGrid();
            }
            HelperWindow.ClearFields(MainPanel);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var value = ((TextBox)sender).Text;
            e.Handled = !int.TryParse(value + e.Text, out _);
        }
    }
}
