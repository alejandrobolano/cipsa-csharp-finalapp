using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for RentalsWindow.xaml
    /// </summary>
    public partial class RentalsWindow : MetroWindow
    {
        private readonly DateTime _todayDateTime;
        private RentalService _rentalService;
        private IList<RentalDto> _rentals;
        private RentalDto _rentalSelected;
        public RentalsWindow()
        {
            InitializeComponent();
            _todayDateTime = DateTime.Today;
        }
        
        private void DateNowTextBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            DateNowTextBlock.Text = _todayDateTime.ToShortDateString();
        }

        private async void RentalsDataGrid_OnLoadedDataGrid(object sender, RoutedEventArgs e)
        {
            await LoadDataGrid();
        }
        

        private async Task LoadDataGrid()
        {
            LoadingPanel.Visibility = Visibility.Visible;
            await LoadingDataTask();
            RentalsDataGrid.ItemsSource = _rentals;
            LoadingPanel.Visibility = Visibility.Hidden;
        }

        private void LoadingData()
        {
            _rentalService = new RentalService();
            _rentals = _rentalService.All();
        }
        private async Task LoadingDataTask()
        {
            await Task.Run(LoadingData);
        }


        private void RentalSelected(object sender)
        {
            switch (sender)
            {
                case Button rowButton:
                    _rentalSelected = (RentalDto)rowButton?.DataContext;
                    break;
                case DataGrid rowDataGrid:
                    _rentalSelected = (RentalDto)rowDataGrid?.SelectedValue;
                    break;
            }
        }
        private bool RemoveRental(RentalDto rental)
        {
            return _rentalService.Remove(rental.Id);
        }
        

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            RentalSelected(sender);
            if (RemoveRental(_rentalSelected))
            {
                await LoadDataGrid();
            }
        }

    }
}
