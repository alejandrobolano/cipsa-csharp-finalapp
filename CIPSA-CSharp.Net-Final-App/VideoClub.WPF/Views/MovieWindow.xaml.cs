using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;
using VideoClub.WPF.Utils;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow : MetroWindow
    {
        private DateTime _todayDateTime;
        private MovieService _movieService;
        private IList<MovieDto> _movies;
        private MovieDto _movieSelected;
        private readonly ResourceManager _resourceManager = Properties.Resources.ResourceManager;
        private IDictionary<StateProductEnum, string> _itemsStateProduct;
        private Dictionary<StateProductEnum, string> _changeItemsStateProduct;
        private StateProductEnum _stateProduct;
        public MovieWindow(StateProductEnum stateProduct)
        {
            InitializeComponent();
            _stateProduct = stateProduct;
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            _todayDateTime = DateTime.Today;
            _movieService = MovieService.Instance;

            _itemsStateProduct = new Dictionary<StateProductEnum, string>
            {
                {StateProductEnum.All, _resourceManager.GetResourceValue("ALL_MOVIES")}
            };
            _itemsStateProduct.Append(GetAllStateProductEnum());
            _changeItemsStateProduct = GetAllStateProductEnum();
        }

        private Dictionary<StateProductEnum, string> GetAllStateProductEnum()
        {
            return new Dictionary<StateProductEnum, string>
            {
                {StateProductEnum.Available, StateProductEnum.Available.GetDescription()},
                {StateProductEnum.BadState, StateProductEnum.BadState.GetDescription()},
                {StateProductEnum.Lost, StateProductEnum.Lost.GetDescription()},
                {StateProductEnum.NonAvailable, StateProductEnum.NonAvailable.GetDescription()}
            };
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
            MainPanel.IsEnabled = true;
        }

        private void UpdateButtonDataGrid_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateData(sender);
        }

        private async Task LoadDataGrid()
        {
            LoadingPanel.Visibility = Visibility.Visible;
            StateProductPanel.Visibility = Visibility.Collapsed;
            await LoadingDataTask();
            MovieDataGrid.ItemsSource = _movies;
            LoadingPanel.Visibility = Visibility.Hidden;
            StateProductPanel.Visibility = Visibility.Visible;
        }

        private void LoadingData()
        {
            _movies = _stateProduct.Equals(StateProductEnum.All)
                ? _movieService.All()
                : _movieService.GetMoviesByState(_stateProduct);
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
            ChangeProductStateComboBox.SelectedItem = _changeItemsStateProduct[_movieSelected.State];
        }

        private bool AddMovie()
        {
            var movie = new MovieDto();
            FillDataFromFields(movie);
            return _movieService.Add(movie, out _);
        }

        private void FillDataFromFields(MovieDto movie)
        {
            movie.Title = TitleText.Text.RemoveMultipleSpace().ToUpperAllFirstLetter();
            movie.Price = Convert.ToDecimal(PriceNumeric?.Value);
            movie.QuantityDisc = Convert.ToInt32(QuantityNumeric?.Value);
            int.TryParse(ProductionYearText?.Text, out var productionYear);
            movie.ProductionYear = productionYear;
            int.TryParse(BuyYearText?.Text, out var buyYear);
            movie.BuyYear = buyYear;
            var durationTimeSpan = new TimeSpan(
                Convert.ToInt32(HourDurationNumeric?.Value),
                Convert.ToInt32(MinuteDurationNumeric?.Value),
                Convert.ToInt32(SecondDurationNumeric?.Value)
                );
            movie.Duration = durationTimeSpan;
            var state = _changeItemsStateProduct.FirstOrDefault(valuePair => valuePair.Value.Equals(ChangeProductStateComboBox.SelectedValue)).Key;
            movie.State = state;
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
            WindowHelper.ClearFields(MainPanel);
            MainPanel.IsEnabled = true;
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckFields()) return;
            if (AddMovie())
            {
                await LoadDataGrid();
            }
            else
            {
                this.ShowGenericErrorMessage(_resourceManager);
                WindowHelper.HandleLogError(string.Empty);
            }
        }

        private async void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckFields()) return;

            if (UpdateMovie())
            {
                await LoadDataGrid();
            }
            else
            {
                this.ShowGenericErrorMessage(_resourceManager);
                WindowHelper.HandleLogError(string.Empty);
            }
        }

        private bool CheckFields()
        {
            if (WindowHelper.HasAnyEmptyFields(MainPanel))
            {
                this.ShowGenericErrorDataMessage(_resourceManager);
                WindowHelper.HandleLogError(string.Empty);
                return true;
            }

            return false;
        }

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            MovieSelected(sender);
            if (RemoveMovie(_movieSelected))
            {
                await LoadDataGrid();
            }
            WindowHelper.ClearFields(MainPanel);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var value = ((TextBox)sender).Text;
            e.Handled = !int.TryParse(value + e.Text, out _);
        }

        private void MovieStateComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            var combo = (ComboBox)sender;
            combo.ItemsSource = _itemsStateProduct.Values;
            combo.SelectedItem = _itemsStateProduct[_stateProduct];
        }

        private async void MovieStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var stateEnumSelected = _itemsStateProduct.FirstOrDefault(x => x.Value.Equals(((ComboBox)sender).SelectedValue)).Key;
            if (stateEnumSelected.Equals(_stateProduct)) return;
            _stateProduct = stateEnumSelected;
            await LoadDataGrid();
        }

        private void ChangeMovieStateComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            var combo = (ComboBox)sender;
            combo.ItemsSource = _changeItemsStateProduct.Values;
        }
    }
}
